using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ChallengeChurnAnalytics.Controllers;
using ChallengeChurnAnalytics.Services;
using ChallengeChurnAnalytics.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

public class PaymentsControllerTests
{
    private readonly PaymentsController _controller;
    private readonly Mock<PaymentService> _paymentServiceMock;

    public PaymentsControllerTests()
    {
        // Mock do PaymentService
        _paymentServiceMock = new Mock<PaymentService>(null);
        _controller = new PaymentsController(_paymentServiceMock.Object);
    }

    [Fact]
    public async Task CreatePaymentIntent_ShouldReturnOkResult_WithClientSecret()
    {
        // Arrange
        decimal amount = 100;
        _paymentServiceMock
            .Setup(p => p.CreatePaymentIntent(It.IsAny<decimal>()))
            .ReturnsAsync(new Stripe.PaymentIntent { ClientSecret = "test_client_secret" });

        // Act
        var result = await _controller.CreatePaymentIntent(amount) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var data = result.Value as dynamic;
        Assert.Equal("test_client_secret", data.clientSecret);
    }
}
