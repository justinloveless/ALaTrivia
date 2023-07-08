using System.Text.Json.Serialization;

namespace ALaTrivia.Models.OpenAi;

public class OpenAiTriviaQuestion
{
    public int? Id { get; set; }
    [JsonPropertyName("question")]
    public string? Question { get; set; }
    [JsonPropertyName("options")]
    public IEnumerable<string>? Options { get; set; }
    [JsonPropertyName("answer")]
    public string? Answer { get; set; }

    public bool HasBeenReviewed { get; set; } = false;
}