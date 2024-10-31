using Stripe;

public class PaymentService
{
    public PaymentService(IConfiguration configuration)
    {
        // Configura a chave secreta do Stripe
        StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
    }

    public async Task<PaymentIntent> CreatePaymentIntent(decimal amount, string currency = "usd")
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(amount * 100), // Stripe trabalha com valores em centavos
            Currency = currency,
            PaymentMethodTypes = new List<string> { "card" }
        };

        var service = new PaymentIntentService();

        try
        {
            // Cria o PaymentIntent e retorna a resposta
            return await service.CreateAsync(options);
        }
        catch (StripeException ex)
        {
            // Log e tratamento adicional podem ser adicionados aqui
            throw new Exception("Erro ao criar a intenção de pagamento", ex);
        }
    }
}
