using System.Text.Json.Serialization;

namespace ALaTrivia.Models.OpenAi;

public class OpenAiTriviaQuestionContainer
{
    [JsonPropertyName("questions")]
    public IEnumerable<OpenAiTriviaQuestion>? Questions { get; set; }
    [JsonPropertyName("tags")] public IEnumerable<string> Tags { get; set; }
}