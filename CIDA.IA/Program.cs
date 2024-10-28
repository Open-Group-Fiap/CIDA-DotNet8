
using CIDA.IA.Entities;
using Microsoft.ML;
using System.Net;

namespace CIDA.IA;
public class Program
{
    public static void Main(string[] args)
    {
        // Download CSV file
        string url = "https://cidastore.blob.core.windows.net/data-csharp/variacao_precos.csv";
        string dataPath = "variacao_precos.csv";

        using (WebClient client = new WebClient())
        {
            client.DownloadFile(url, dataPath);
        }

        Console.WriteLine("Arquivo CSV baixado com sucesso.");

        // Create context
        var mlContext = new MLContext();

        IDataView dataView = mlContext.Data.LoadFromTextFile<InsightData>(dataPath, hasHeader: true, separatorChar: ',');

        // Config the train
        var pipeline = mlContext.Transforms.Categorical.OneHotEncoding("Estacao")
            .Append(mlContext.Transforms.Categorical.OneHotEncoding("Regiao"))
            .Append(mlContext.Transforms.Concatenate("Features", "Estacao", "Regiao"))
            .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: "Variacao", maximumNumberOfIterations: 100));

        // train
        var model = pipeline.Fit(dataView);

        // avaliate the model
        var predictions = model.Transform(dataView);
        var metrics = mlContext.Regression.Evaluate(predictions, labelColumnName: "Variacao");

        Console.WriteLine($"R^2: {metrics.RSquared}");
        Console.WriteLine($"RMSE: {metrics.RootMeanSquaredError}");

        // make the prediction
        var predictionEngine = mlContext.Model.CreatePredictionEngine<InsightData, InsightPrediction>(model);

        var sampleData = new InsightData { Estacao = "Verão", Regiao = "Sul" };
        var prediction = predictionEngine.Predict(sampleData);

        Console.WriteLine($"Previsão de variação para Verão na região Sul: {prediction.Variacao}");
    }
}