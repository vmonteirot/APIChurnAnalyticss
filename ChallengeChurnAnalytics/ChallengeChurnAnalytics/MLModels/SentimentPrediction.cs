using Microsoft.ML.Data;

namespace ChallengeChurnAnalytics.MLModels
{
    /// <summary>
    /// classe que representa o resultado da previsão de sentimento gerada pelo modelo
    /// </summary>
    public class SentimentPrediction
    {
        // indica a previsão de sentimento (positivo ou negativo) com base no modelo treinado
        // atributo que mapeia a saída do modelo para "PredictedLabel"
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        // armazena a probabilidade da previsão de sentimento (entre 0 e 1)
        public float Probability { get; set; }

        // armazena a pontuação bruta calculada pelo modelo para a previsão
        public float Score { get; set; }
    }
}
