using System.Windows;
namespace Pomodorino;
public partial class SettingsWindow : Window
{
    public TimerSettings Settings { get; private set; }
    public SettingsWindow(TimerSettings current)
    {
        InitializeComponent(); Settings = current;
        PhasesBox.Text = current.Phases.ToString(); WorkBox.Text = current.WorkMinutes.ToString();
        ShortBox.Text = current.ShortBreakMinutes.ToString(); LongBox.Text = current.LongBreakMinutes.ToString();
    }
    private void Save_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(PhasesBox.Text, out var phases) || phases < 1 ||
            !int.TryParse(WorkBox.Text, out var work) || work < 1 ||
            !int.TryParse(ShortBox.Text, out var shortBreak) || shortBreak < 0 ||
            !int.TryParse(LongBox.Text, out var longBreak) || longBreak < 0)
        { MessageBox.Show("Inserisci numeri validi.", "Impostazioni"); return; }
        Settings = new TimerSettings(phases, work, shortBreak, longBreak); DialogResult = true;
    }
}
