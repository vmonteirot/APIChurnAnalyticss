using Microsoft.AspNetCore.Mvc; // importa o namespace para o controlador da API
using ChallengeChurnAnalytics.MLModels; // importa o namespace para os modelos de aprendizado de máquina

namespace ChallengeChurnAnalytics.Controllers // define o namespace do controlador
{
    [ApiController] // indica que esta classe é um controlador de API
    [Route("api/[controller]")] // define a rota base para as ações deste controlador
    public class SentimentController : ControllerBase // classe que herda de ControllerBase
    {
        private readonly SentimentModelTrainer _trainer; // variável para armazenar a instância do modelo de treinamento de sentimento

        public SentimentController() // construtor do controlador
        {
            _trainer = new SentimentModelTrainer(); // inicializa o modelo de treinamento de sentimento
        }

        [HttpPost("analyze")] // define uma ação HTTP POST na rota "analyze"
        public ActionResult<SentimentPrediction> AnalyzeSentiment([FromBody] string text) // método para analisar o sentimento do texto
        {
            var prediction = _trainer.Predict(text); // chama o método de previsão passando o texto
            return Ok(prediction); // retorna o resultado da previsão com status 200 (OK)
        }
    }
}
