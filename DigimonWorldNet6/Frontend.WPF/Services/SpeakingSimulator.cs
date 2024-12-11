using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigimonWorld.Frontend.WPF.Services;

public static class SpeakingSimulator
{
    private static CancellationTokenSource _typingCancellationTokenSource = new();
    private static readonly char[] Separator = [' '];

    public static async Task WriteAsSpeech(string fullText, Action<string> updateTextAction)
    {
        await _typingCancellationTokenSource.CancelAsync();
        
        _typingCancellationTokenSource.Dispose();
        _typingCancellationTokenSource = new CancellationTokenSource();
        
        CancellationToken cancellationToken = _typingCancellationTokenSource.Token;

        string currentText = string.Empty;

        try
        {
            string[] words = fullText.Split(Separator, StringSplitOptions.None);

            foreach (string word in words)
            {
                cancellationToken.ThrowIfCancellationRequested();

                currentText += word + " ";

                updateTextAction(currentText);

                string lastFiveChars = currentText.Length >= 6
                    ? currentText[^6..]
                    : currentText;

                if (lastFiveChars.Contains(". . ."))
                {
                    await Task.Delay(800, cancellationToken);
                }
                else
                {
                    await Task.Delay(150, cancellationToken);
                }
            }
        }
        catch (OperationCanceledException)
        {
            // Graceful cancellation
        }
    }
}