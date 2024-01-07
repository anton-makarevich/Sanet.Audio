using Android.Media;
using Sanet.Audio.Core;

namespace Sanet.Audio.Android;

public class AudioPlayerAndroid:IAudioPlayer
{
    public async Task Play(byte[] audioData, AudioFormats format)
    {
        if (format != AudioFormats.Mp3)
        {
            throw new NotSupportedException();
        }
        var mediaPlayer = new MediaPlayer();

        EventHandler? completionHandler = null;
        completionHandler = (_, _) =>
        {
            mediaPlayer.Completion -= completionHandler;
            mediaPlayer.Reset();
            mediaPlayer.Release();
        };
        
        await Task.Run(() =>
        {
            var base64EncodedString = Convert.ToBase64String(audioData);
            var url = "data:audio/mp3;base64," + base64EncodedString;
            mediaPlayer.SetDataSource(url);
            mediaPlayer.Prepare();
            mediaPlayer.Completion += completionHandler;
            mediaPlayer.Start();
        });
    }
}