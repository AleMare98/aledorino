# Logica del timer

La logica è in `MainWindow.xaml.cs` e usa un `DispatcherTimer` che esegue `Tick()` ogni secondo.

## Stato memorizzato

- `secondsLeft`: secondi rimanenti.
- `phase`: numero della fase di lavoro corrente.
- `state`: tipo di timer corrente: `Work`, `ShortBreak` oppure `LongBreak`.
- `running`: indica se il conto alla rovescia è attivo.

## Cosa accade ogni secondo

`Tick()` diminuisce `secondsLeft` di uno e chiama `UpdateView()`, che aggiorna fase, stato e testo `MM:SS` nell'interfaccia.

Quando il tempo raggiunge zero, `Tick()`:

1. riproduce il suono di sistema;
2. invia una notifica Windows;
3. chiama `Advance()` per preparare lo stato successivo;
4. ferma il timer e imposta `running` a `false`.

Quindi nessuna fase parte automaticamente: per avviare il timer appena preparato si deve premere `Avvia`.

## Transizioni

```text
Lavoro fase N -> Pausa breve       (se N non è l'ultima fase)
Lavoro ultima fase -> Pausa lunga
Pausa breve -> Lavoro fase N + 1
Pausa lunga -> Lavoro fase 1
```

`Advance()` calcola infine la nuova durata in secondi usando le impostazioni dell'utente.

## Pulsanti

- `Avvia`: avvia il `DispatcherTimer` della fase già pronta.
- `Pausa/Riprendi`: ferma o riattiva il conto alla rovescia senza cambiare fase.
- `Reset`: ferma tutto e torna a fase 1 con la durata di lavoro completa.
