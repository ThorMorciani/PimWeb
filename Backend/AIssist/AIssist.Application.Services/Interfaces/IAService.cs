// IAService.cs
using System.Threading.Tasks;

namespace AIssist.Application.Services.Interfaces
{
    public interface IAService
    {
        Task<string> GetSuggestion(string description);
        Task<bool> AnalyzePriority(string description);
    }
}
