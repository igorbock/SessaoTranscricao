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

---

Esse foi o bloco de código sugerido pelo DeepSeek para comunicação com os modelos de IA da Hugging Face.
Segue o link da página com mais modelos, além do GPT2:
https://huggingface.co/models

```cs
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private static readonly string apiUrl = "https://api-inference.huggingface.co/models/gpt2"; // Endpoint do modelo GPT-2
    private static readonly string apiKey = "seu-token-aqui"; // Substitua pelo seu token do Hugging Face

    static async Task Main(string[] args)
    {
        using (HttpClient client = new HttpClient())
        {
            // Adiciona o token de autenticação no cabeçalho
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            // Define o texto de entrada
            var requestBody = new
            {
                inputs = "Explique o que é inteligência artificial em uma frase."
            };

            // Converte o corpo da requisição para JSON
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Faz a chamada POST para a API
            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            // Verifica se a requisição foi bem-sucedida
            if (response.IsSuccessStatusCode)
            {
                string responseJson = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Resposta do Hugging Face:");
                Console.WriteLine(responseJson);
            }
            else
            {
                Console.WriteLine("Erro na requisição:");
                Console.WriteLine($"Status Code: {response.StatusCode}");
                string errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(errorResponse);
            }
        }
    }
}
```
