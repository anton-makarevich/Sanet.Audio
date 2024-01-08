using AVFoundation;
using Sanet.Audio.Core;

namespace Sanet.Audio.iOS;

public class AudioPlayerIos:IAudioPlayer
{
    public async Task Play(byte[] audioData, AudioFormats format)
    {
        AVAudioPlayer? audioPlayer = null;
        var player = audioPlayer;
        EventHandler<AVStatusEventArgs>? finishedHandler = null;
        finishedHandler = (_, _) =>
        {
            if (player == null) return;
            player.FinishedPlaying -= finishedHandler;
            player.Dispose();
        };

        using var data = NSData.FromArray(audioData);
        audioPlayer = AVAudioPlayer.FromData(data, out var error);

        if (error != null)
        {
            // Handle error
            return;
        }

        await Task.Run(() =>
        {
            if (audioPlayer == null) return;
            audioPlayer.FinishedPlaying += finishedHandler;
            audioPlayer.Play();
        });
    }
}