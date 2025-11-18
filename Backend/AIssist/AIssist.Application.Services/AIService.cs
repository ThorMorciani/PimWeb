using Google.GenAI;
using Google.GenAI.Types;
using AIssist.Application.Services.Interfaces;

namespace AIssist.Application.Services
{
    public class AIService : IAService
    {
        private readonly Client _client;

        public AIService(Client client)
        {
            _client = client;
        }

        public async Task<string> GetSuggestion(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return "Descreva o problema para receber uma sugestão.";

            try
            {
                var response = await _client.Models.GenerateContentAsync(
                    model: "gemini-2.0-flash",
                    contents: description
                );

                return response.Candidates.FirstOrDefault()?.Content.Parts.FirstOrDefault()?.Text
                       ?? "Não foi possível gerar a sugestão.";
            }
            catch
            {
                return "Erro ao se comunicar com a IA.";
            }
        }

        public async Task<bool> AnalyzePriority(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return false;

            var prompt = $"Classifique a prioridade do seguinte ticket como 'Alta' ou 'Normal': {description}";
            try
            {
                var response = await _client.Models.GenerateContentAsync(
                    model: "gemini-2.0-flash",
                    contents: prompt,
                    config: new GenerateContentConfig { MaxOutputTokens = 10 }
                );

                var text = response.Candidates.FirstOrDefault()?.Content.Parts.FirstOrDefault()?.Text ?? "";
                return text.Contains("Alta", StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}
