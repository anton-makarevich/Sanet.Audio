using NAudio.Wave;

namespace Sanet.Audio.NAudio;

public class AudioPlayerNAudio: IAudioPlayer
{
    public async Task Play(byte[] audioData, AudioFormats format)
    {
        if (format != AudioFormats.Mp3)
        {
            throw new NotSupportedException();
        }
        using var memoryStream = new MemoryStream(audioData);
        await using var waveReader = new Mp3FileReader(memoryStream);
        using var outputDevice = new WaveOutEvent();
        outputDevice.Init(waveReader);
        outputDevice.Play();
        while (outputDevice.PlaybackState == PlaybackState.Playing)
        {
            await Task.Delay(100);
        }
    }
}