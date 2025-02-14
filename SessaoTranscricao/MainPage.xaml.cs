namespace SessaoTranscricao;

public partial class MainPage : ContentPage
{
    private readonly IAudioService _audioService;

    public string VideoUrl { get; set; }

    public MainPage(IAudioService service)
    {
        InitializeComponent();

        _audioService = service;
    }

    private async void LoadVideo()
    {
        var youtube = new YoutubeClient();
        var videoUrl = "https://www.youtube.com/watch?v=JE0VEmV8zw4";

        var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
        var teste = streamManifest.GetVideoStreams();
        var streamInfo = teste.GetWithHighestVideoQuality();

        VideoUrl = streamInfo.Url;
        OnPropertyChanged(nameof(VideoUrl));

        // Extraia o áudio para um stream
        var audioStream = await _audioService.ExtractAudioToStream(videoUrl);

        // Transcreva o áudio
        var transcription = await _audioService.TranscribeAudio(audioStream);

        LabelPrincipal.Text = transcription;
    }

    private void CounterBtn_Clicked(object sender, EventArgs e) => LoadVideo();
}
