using System;
using System.IO;
using NAudio.Wave;

namespace DigimonWorld.Frontend.WPF.Services;

public static class SoundService
{
    public static void PlaySfx(string fileName, float volume = 1.0f)
    {
        try
        {
            string sfxPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SFX", fileName);

            if (!File.Exists(sfxPath))
            {
                return;
            }

            AudioFileReader audioFile = new(sfxPath);
            WaveOutEvent soundOut = new();
            soundOut.Init(audioFile);
            soundOut.Volume = volume;
            soundOut.PlaybackStopped += (_, _) =>
            {
                soundOut.Dispose();
                audioFile.Dispose();
            };
            soundOut.Play();
        } catch
        {
            // Sound playback is non-critical
        }
    }
}
