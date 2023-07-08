namespace ALaTrivia.Models.Mongo;

public class TriviaDatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? QuestionCollection { get; set; }
    public string? AccountCollection { get; set; }
}