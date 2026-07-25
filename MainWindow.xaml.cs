using System.Text.Json;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Toolkit.Uwp.Notifications;

namespace Pomodorino;

public record TimerSettings(int Phases = 4, int WorkMinutes = 25, int ShortBreakMinutes = 5, int LongBreakMinutes = 30);

public partial class MainWindow : Window
{
    private readonly DispatcherTimer timer = new() { Interval = TimeSpan.FromSeconds(1) };
    private TimerSettings settings = new();
    private int phase = 1, secondsLeft; private TimerState state = TimerState.Work; private bool running;
    private readonly string settingsFile = System.IO.Path.Combine(AppContext.BaseDirectory, "settings.json");
    private enum TimerState { Work, ShortBreak, LongBreak }
    public MainWindow()
    {
        InitializeComponent(); timer.Tick += (_, _) => Tick(); LoadSettings(); ResetTimer();
    }
    private void LoadSettings() { try { if (System.IO.File.Exists(settingsFile)) settings = JsonSerializer.Deserialize<TimerSettings>(System.IO.File.ReadAllText(settingsFile)) ?? new(); } catch { settings = new(); } }
    private void SaveSettings() => System.IO.File.WriteAllText(settingsFile, JsonSerializer.Serialize(settings));
    private void ResetTimer() { timer.Stop(); running = false; phase = 1; state = TimerState.Work; secondsLeft = settings.WorkMinutes * 60; PauseButton.Content = "Pausa"; StartButton.Content = "Avvia"; UpdateView(); }
    private void Tick()
    {
        if (--secondsLeft > 0) { UpdateView(); return; }
        System.Media.SystemSounds.Asterisk.Play();
        NotifyExpiry();
        Advance();
        // Ogni nuova fase attende una scelta esplicita dell'utente.
        timer.Stop();
        running = false;
        StartButton.Content = "Avvia";
        PauseButton.Content = "Pausa";
    }
    private void NotifyExpiry()
    {
        var title = state == TimerState.Work ? "Fase completata" : state == TimerState.ShortBreak ? "Pausa terminata" : "Pausa lunga terminata";
        var text = state == TimerState.Work ? "È il momento di fare una pausa." : "È il momento di riprendere il lavoro.";
        try
        {
            new ToastContentBuilder().AddText("Pomodorino — " + title).AddText(text).Show(_ => { });
        }
        catch { /* Se le notifiche sono disabilitate, il suono resta disponibile. */ }
    }
    private void Advance()
    {
        if (state == TimerState.Work) { if (phase < settings.Phases) { state = TimerState.ShortBreak; } else { state = TimerState.LongBreak; MessageText.Text = "Ciclo completato — pausa lunga"; } }
        else if (state == TimerState.ShortBreak) { phase++; state = TimerState.Work; }
        else { phase = 1; state = TimerState.Work; }
        secondsLeft = state == TimerState.Work ? settings.WorkMinutes * 60 : state == TimerState.ShortBreak ? settings.ShortBreakMinutes * 60 : settings.LongBreakMinutes * 60; UpdateView();
    }
    private void UpdateView() { PhaseText.Text = $"Fase {phase}/{settings.Phases}"; StateText.Text = state switch { TimerState.Work => "Lavoro", TimerState.ShortBreak => "Pausa breve", _ => "Pausa lunga" }; TimeText.Text = $"{secondsLeft / 60:00}:{secondsLeft % 60:00}"; if (state != TimerState.LongBreak) MessageText.Text = state == TimerState.Work ? "Concentrati e fai del tuo meglio ✨" : "Respira e ricaricati 🌸"; }
    private void Start_Click(object sender, RoutedEventArgs e) { running = true; timer.Start(); StartButton.Content = "In corso"; }
    private void Pause_Click(object sender, RoutedEventArgs e) { if (!running) { running = true; timer.Start(); PauseButton.Content = "Pausa"; } else { running = false; timer.Stop(); PauseButton.Content = "Riprendi"; } }
    private void Reset_Click(object sender, RoutedEventArgs e) => ResetTimer();
    private void Settings_Click(object sender, RoutedEventArgs e) { var dialog = new SettingsWindow(settings) { Owner = this }; if (dialog.ShowDialog() == true) { settings = dialog.Settings; SaveSettings(); ResetTimer(); } }
}
