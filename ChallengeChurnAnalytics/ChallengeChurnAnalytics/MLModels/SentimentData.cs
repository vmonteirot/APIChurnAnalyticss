using Microsoft.ML.Data;

namespace ChallengeChurnAnalytics.MLModels
{
    /// <summary>
    /// classe que representa os dados de entrada para análise de sentimento
    /// </summary>
    public class SentimentData
    {
        // define a coluna de texto de entrada para o modelo de ML
        // O índice 0 indica a posição da coluna no arquivo de dados
        [LoadColumn(0)]
        public string Text { get; set; }
    }
}
