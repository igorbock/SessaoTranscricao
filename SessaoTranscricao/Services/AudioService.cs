namespace SessaoTranscricao.Services;

public class AudioService : IAudioService
{
    public AudioService()
    {
        GlobalFFOptions.Configure(new FFOptions { BinaryFolder = @"C:\Users\igorb\Downloads\ffmpeg-n5.1-latest-win64-gpl-5.1\ffmpeg-n5.1-latest-win64-gpl-5.1\bin", TemporaryFilesFolder = Path.GetTempPath() });
    }

    public async Task<Stream> ExtractAudioToStream(string videoUrl)
    {
        var youtube = new YoutubeClient();
        var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
        var teste = streamManifest.GetVideoStreams();
        var streamInfo = teste.GetWithHighestVideoQuality();

        // Baixe o vídeo temporariamente
        var tempVideoPath = Path.GetTempFileName();
        await youtube.Videos.Streams.DownloadAsync(streamInfo, tempVideoPath);

        // Crie um stream em memória para o áudio
        var audioStream = new MemoryStream();

        // Use FFmpeg para extrair o áudio e enviá-lo para o stream em memória
        await FFMpegArguments.FromFileInput(tempVideoPath)
            .OutputToPipe(new StreamPipeSink(audioStream), options => options
                .WithAudioCodec("pcm_s16le") // Codec de áudio PCM (compatível com Vosk)
                .WithCustomArgument("-ac 1") // Converter para mono (necessário para Vosk)
                .WithCustomArgument("-ar 16000") // Taxa de amostragem de 16kHz (recomendado para Vosk)
            )
            .ProcessAsynchronously();

        // Delete o arquivo de vídeo temporário
        File.Delete(tempVideoPath);

        // Retorne o stream de áudio
        audioStream.Position = 0; // Reinicie a posição do stream para o início
        return audioStream;
    }

    public async Task<string> TranscribeAudio(Stream audioStream)
    {
        // Caminho para o modelo de linguagem do Vosk
        var modelPath = "C:\\Users\\igorb\\Downloads\\vosk-model-small-pt-0.3\\vosk-model-small-pt-0.3"; // Substitua pelo caminho do seu modelo

        // Carregue o modelo do Vosk
        Vosk.Vosk.SetLogLevel(0); // Desative logs desnecessários
        var model = new Model(modelPath);

        // Crie um reconhecedor de fala
        var recognizer = new VoskRecognizer(model, 16000.0f);

        // Buffer para ler o stream de áudio
        var buffer = new byte[4096];
        int bytesRead;

        // Processe o stream de áudio
        while ((bytesRead = await audioStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            if (recognizer.AcceptWaveform(buffer, bytesRead))
            {
                // Resultado parcial da transcrição
                var result = recognizer.Result();
                Console.WriteLine(result);
            }
            else
            {
                // Resultado parcial alternativo
                var partialResult = recognizer.PartialResult();
                Console.WriteLine(partialResult);
            }
        }

        // Obtenha o resultado final da transcrição
        var finalResult = recognizer.FinalResult();
        Console.WriteLine(finalResult);

        return finalResult;
    }
}
