# Pomodorino — stato del progetto

## Obiettivo

Applicazione desktop Windows in C# / WPF per un timer Pomodoro a fasi, con interfaccia in italiano e tema pastello/anime originale.

## Funzionalità realizzate

- Ciclo configurabile: per impostazione predefinita 4 fasi di lavoro da 25 minuti, pause brevi da 5 minuti e pausa lunga da 30 minuti.
- Indicazione costante della fase (es. `Fase 2/4`), dello stato (`Lavoro`, `Pausa breve`, `Pausa lunga`) e del tempo nel formato `MM:SS`.
- Pulsanti `Avvia`, `Pausa/Riprendi` e `Reset`.
- Finestra Impostazioni per modificare numero di fasi e durate; i valori sono salvati in `settings.json` accanto all'eseguibile.
- Suono di sistema alla fine di ogni timer.
- Notifica toast Windows alla fine di ogni timer, tramite `Microsoft.Toolkit.Uwp.Notifications`.
- Alla scadenza di qualsiasi timer l'app passa alla fase successiva ma il nuovo timer rimane fermo: l'utente deve premere `Avvia` per iniziarlo.
- Dopo l'ultima fase di lavoro viene predisposta la pausa lunga; dopo la pausa lunga torna alla fase 1, sempre in attesa di `Avvia`.

## Struttura utile

- `MainWindow.xaml`: interfaccia principale.
- `MainWindow.xaml.cs`: stato del timer, transizioni, pulsanti e invio delle notifiche.
- `SettingsWindow.xaml` e `SettingsWindow.xaml.cs`: modifica e validazione delle impostazioni.
- `Pomodorino.csproj`: progetto .NET 8 WPF e dipendenza per le notifiche.

## Compilazione e pubblicazione

Prerequisito: .NET SDK 8 o successivo su Windows.

```powershell
dotnet build
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
```

L'eseguibile pubblicato è in:

```text
bin\Release\net8.0-windows10.0.17763.0\win-x64\publish\Pomodorino.exe
```

L'ultima compilazione e pubblicazione sono riuscite senza errori né avvisi.

## Possibili evoluzioni

- Aggiungere una prova rapida delle scadenze nelle impostazioni per testare timer di pochi secondi.
- Rendere attivabili/disattivabili suono e notifiche.
- Migliorare ulteriormente la grafica del tema con immagini originali e accessibili.
- Aggiungere una cronologia dei cicli completati.
