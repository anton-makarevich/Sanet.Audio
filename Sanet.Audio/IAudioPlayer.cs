namespace Sanet.Audio;

public interface IAudioPlayer
{
    Task Play(byte[] audioData, AudioFormats format);
}