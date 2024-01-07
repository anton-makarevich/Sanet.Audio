namespace Sanet.Audio.Core;

public interface IAudioPlayer
{
    Task Play(byte[] audioData, AudioFormats format);
}