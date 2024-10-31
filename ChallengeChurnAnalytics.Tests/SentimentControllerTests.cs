using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ChallengeChurnAnalytics.Controllers;
using ChallengeChurnAnalytics.MLModels;

public class SentimentControllerTests
{
    private readonly SentimentController _controller;
    private readonly Mock<SentimentModelTrainer> _trainerMock;

    public SentimentControllerTests()
    {
        // cria um mock para SentimentModelTrainer
        _trainerMock = new Mock<SentimentModelTrainer>();

        // injeta o mock no controlador
        _controller = new SentimentController();
    }

    [Fact]
    public void AnalyzeSentiment_ShouldReturnPredictionResult()
    {
        // arrange: define um texto de teste e a previsão esperada
        string testText = "This product is great!";
        var expectedPrediction = new SentimentPrediction
        {
            Prediction = true,
            Probability = 0.9f,
            Score = 0.8f
        };

        // configura o mock para retornar o resultado esperado
        _trainerMock.Setup(trainer => trainer.Predict(testText)).Returns(expectedPrediction);

        // act: chama o método AnalyzeSentiment no controlador
        var result = _controller.AnalyzeSentiment(testText) as OkObjectResult;

        // assert: verifica se o resultado é o esperado
        Assert.NotNull(result);
        Assert.IsType<SentimentPrediction>(result.Value);

        var actualPrediction = result.Value as SentimentPrediction;
        Assert.Equal(expectedPrediction.Prediction, actualPrediction.Prediction);
        Assert.Equal(expectedPrediction.Probability, actualPrediction.Probability);
        Assert.Equal(expectedPrediction.Score, actualPrediction.Score);
    }
}
