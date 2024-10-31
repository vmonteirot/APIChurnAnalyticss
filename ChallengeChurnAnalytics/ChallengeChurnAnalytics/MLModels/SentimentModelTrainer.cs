using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;

namespace ChallengeChurnAnalytics.MLModels
{
    public class SentimentModelTrainer
    {
        private readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "MLModels", "sentiment_model.zip");
        private readonly MLContext _mlContext;

        public SentimentModelTrainer()
        {
            _mlContext = new MLContext();
        }

        // Método para treinar o modelo de análise de sentimento
        public void TrainModel()
        {
            // Define o caminho para o arquivo de dados de treino (sentiment_data.csv na pasta Data)
            var dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "sentiment_data.csv");

            // Carrega os dados de treino do CSV e define o pipeline de transformação e treinamento
            var data = _mlContext.Data.LoadFromTextFile<SentimentData>(dataPath, hasHeader: false);
            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));

            // Treina o modelo
            var model = pipeline.Fit(data);

            // Salva o modelo treinado no caminho especificado
            _mlContext.Model.Save(model, data.Schema, _modelPath);
        }

        // Método para fazer previsões de sentimento
        public SentimentPrediction Predict(string text)
        {
            // Carrega o modelo treinado
            var model = _mlContext.Model.Load(_modelPath, out _);

            // Cria um motor de predição com o modelo carregado
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);

            // Faz a previsão com base no texto fornecido
            var prediction = predictionEngine.Predict(new SentimentData { Text = text });
            return prediction;
        }
    }
}
