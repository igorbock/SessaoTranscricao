namespace SessaoTranscricao.Interfaces;

public interface IAudioService
{
    Task<Stream> ExtractAudioToStream(string videoUrl);
    Task<string> TranscribeAudio(Stream audioStream);
}
