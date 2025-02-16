Utilizar os componentes e pacotes Nuget gratuitos:

- [https://www.nuget.org/packages/FFmpeg.AutoGen] FFmpeg.AutoGen - Para cortar o vídeo em pequenas partes
- [https://www.nuget.org/packages/FFMpegCore] FFmpegCore - Transforma os vídeos em áudios.
- [https://www.nuget.org/packages/Vosk] Vosk - Transcrever os áudios - 100% Offline
- [https://www.nuget.org/packages/Whisper.net] Whisper - Transcrever os áudios - Maior precisão

API
- [https://platform.openai.com/docs/guides/speech-to-text] OpenAI Speech to Text - Transcrever os áudios


Utilizar o MAUI para um aplicativo Windows e Mac, no Desktop.
Criar uma API para o backend do sistema.

---

Pedi ajuda da DeepSeek para auxiliar no projeto e sugerir ferramentas gratuitas:
- Extrair Áudio: FFmpeg.
- Transcrição: Whisper (Open Source) ou Google Speech-to-Text (60 minutos grátis/mês).
- Diarização: PyAnnote (Open Source) ou identificação manual por padrões.
- Sumarização: Hugging Face Transformers (Open Source) ou ChatGPT (limite gratuito).
- Publicação: WordPress ou site estático.

Planejo publicar as falas de cada sessão através do GitHubPages utilizando o Blazor para essa tarefa.

---

Outro ótimo plano é utilizar o Docker para separar o serviço do Whisper (transcrição) e a API em C#. Como Whisper é uma lib em Python posso criar uma tratativa para expor como uma API suas funcionalidades.
