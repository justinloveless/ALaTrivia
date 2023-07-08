using ALaTrivia.Models;
using ALaTrivia.Models.Mongo;
using ALaTrivia.Models.OpenAi;
using ALaTrivia.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenAI.Interfaces;
using OpenAiModels=OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

namespace ALaTrivia.Controllers;

[ApiController]
[Route("[controller]")]
public class OpenAiController : ControllerBase
{
    private readonly ILogger<OpenAiController> _logger;
    private readonly IOpenAIService _openAiService;
    private readonly ITriviaQuestionRepository _triviaQuestionRepository;

    // API Key: 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

    public OpenAiController(ILogger<OpenAiController> logger, IOpenAIService openAiService, 
        ITriviaQuestionRepository triviaQuestionRepository)
    {
        _logger = logger;
        _openAiService = openAiService;
        _triviaQuestionRepository = triviaQuestionRepository;
        _openAiService.SetDefaultModelId(OpenAiModels.Models.ChatGpt3_5Turbo);
    }

    [HttpGet]
    public async Task<IEnumerable<TriviaQuestion>> GetQuestionsByTag([FromQuery] string tag)
    {
        return await _triviaQuestionRepository.GetQuestionsByTag(tag);
    }

    [HttpPost]
    public async Task<OpenAiTriviaQuestionContainer?> GenerateTriviaQuestions([FromBody] string userTopic, [FromQuery] int numOfQuestions)
    {
        // generate new questions from OpenAI
        var completionResult = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem($@"
You will generate batches of {numOfQuestions} question and answer pairs for a given topic. 
The answers should only be one or two words, and should only be nouns.
Here's an example for the topic ""Benedict Cumberbatch"":
{{""questions"": [{{
""question"": ""What was Benedict Cumberbatch's role in the movie The Imitation Game?"", 
""options"": [""Alan Turing"", ""Joan Clarke"",""Peter Hilton"", ""Hugh Alexander""],
""answer"": ""Alan Turing""}}
],
""tags"":[""Benedict Cumberbatch"", ""Actors"", ""Hollywood""]
}}"),
                ChatMessage.FromUser(userTopic)
            },
            Model = OpenAiModels.Models.ChatGpt3_5Turbo,
            Temperature = (float?)1.0,
            FrequencyPenalty = (float?)0.82,
            PresencePenalty = (float?)1.41,
        });
        if (!completionResult.Successful) return null;
        Console.WriteLine(completionResult.Choices.First().Message.Content);
        var result = completionResult.Choices.First().Message.Content;
        var questionContainer = JsonConvert.DeserializeObject<OpenAiTriviaQuestionContainer>(result);
        // save questions to mongo
        if (questionContainer != null) await SaveQuestions(questionContainer);
        return questionContainer;
    }

    private async Task SaveQuestions(OpenAiTriviaQuestionContainer container)
    {
        var questions = new List<TriviaQuestion>();
        if (container.Questions != null) 
            questions.AddRange(
            container.Questions.Select(openAiTriviaQuestion => 
                new TriviaQuestion()
                {
                    Question = openAiTriviaQuestion.Question, 
                    Answer = openAiTriviaQuestion.Answer, 
                    Tags = container.Tags, 
                    Options = openAiTriviaQuestion.Options,
                    HasBeenReviewed = openAiTriviaQuestion.HasBeenReviewed
                }));

        await _triviaQuestionRepository.BatchCreateTriviaQuestions(questions);

    }


    
     
    
}