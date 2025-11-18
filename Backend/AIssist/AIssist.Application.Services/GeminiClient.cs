using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace AIssist.Application.Services
{
    public class GeminiClient
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiOptions _options;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public GeminiClient(HttpClient httpClient, IOptions<GeminiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<string> GenerateText(string prompt, int maxTokens = 50)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                throw new ArgumentException("Prompt não pode ser vazio.", nameof(prompt));

            var request = new GeminiRequest
            {
                Prompt = new PromptRequest { Text = prompt },
                MaxOutputTokens = maxTokens
            };

            using var response = await _httpClient.PostAsJsonAsync(_options.Url, request, _jsonOptions);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<GeminiResponse>(_jsonOptions);
            return result?.Candidates?.FirstOrDefault()?.Content ?? "";
        }
    }

    public class GeminiOptions
    {
        public string ApiKey { get; set; } = "";
        public string Url { get; set; } = "";
    }

    public class GeminiDelegatingHandler : DelegatingHandler
    {
        private readonly string _apiKey;

        public GeminiDelegatingHandler(IOptions<GeminiOptions> options)
        {
            _apiKey = options.Value.ApiKey;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            request.Headers.Remove("x-goog-api-key");
            request.Headers.Add("x-goog-api-key", _apiKey);

            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return base.SendAsync(request, cancellationToken);
        }
    }

    // DTOs
    public class GeminiRequest
    {
        [JsonPropertyName("prompt")]
        public PromptRequest Prompt { get; set; } = new();

        [JsonPropertyName("maxOutputTokens")]
        public int MaxOutputTokens { get; set; } = 50;
    }

    public class PromptRequest
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = "";
    }

    public class GeminiResponse
    {
        [JsonPropertyName("candidates")]
        public GeminiCandidate[] Candidates { get; set; } = Array.Empty<GeminiCandidate>();
    }

    public class GeminiCandidate
    {
        [JsonPropertyName("content")]
        public string Content { get; set; } = "";
    }
}
