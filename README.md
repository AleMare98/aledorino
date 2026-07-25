# Pomodorino

Timer Pomodoro a fasi per Windows, sviluppato in C# con WPF e .NET 8.

## Funzionalità

- Fasi di lavoro e pause configurabili.
- Arresto automatico alla fine di ogni timer: la fase successiva parte solo premendo **Avvia**.
- Suono e notifica Windows a ogni scadenza.
- Impostazioni salvate localmente.
- Interfaccia italiana con tema pastello/anime originale.

## Avvio e pubblicazione

```powershell
dotnet build
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
```

L'eseguibile viene creato in `bin/Release/net8.0-windows10.0.17763.0/win-x64/publish/Pomodorino.exe`.

Per una descrizione completa dello stato del progetto, consulta [WORK_HANDOFF.md](WORK_HANDOFF.md). Per la logica del timer, consulta [TIMER_LOGIC.md](TIMER_LOGIC.md).
