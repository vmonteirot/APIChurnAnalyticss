using Moq;
using Xunit;
using Microsoft.Extensions.Configuration;
using ChallengeChurnAnalytics.Services;

public class PaymentServiceTests
{
    private readonly PaymentService _paymentService;
    private readonly Mock<IConfiguration> _configurationMock;

    public PaymentServiceTests()
    {
        _configurationMock = new Mock<IConfiguration>();

        // configurar o mock do IConfiguration para fornecer uma chave do Stripe de teste
        _configurationMock.Setup(c => c["Stripe:SecretKey"]).Returns("sk_test_fake_key");

        _paymentService = new PaymentService(_configurationMock.Object);
    }

    [Fact]
    public async Task CreatePaymentIntent_ShouldReturnPaymentIntent_WhenAmountIsValid()
    {
        // Arrange
        decimal amount = 100; // valor de teste

        // Act
        var paymentIntent = await _paymentService.CreatePaymentIntent(amount);

        // Assert
        Assert.NotNull(paymentIntent);
        Assert.Equal("usd", paymentIntent.Currency);
        Assert.Equal((long)(amount * 100), paymentIntent.Amount);
    }
}
