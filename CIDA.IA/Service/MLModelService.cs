
using CIDA.IA.Entities;
using Microsoft.ML;
using System.Net;

namespace CIDA.IA.Service;

public static class MLModelService
{
    private static readonly string ModelPath = "model.zip";
    private static readonly MLContext MlContext = new MLContext(seed: 1);
    private static ITransformer _model;
    private static PredictionEngine<InsightData, InsightPrediction> _predictionEngine;

    static MLModelService()
    {
        LoadOrTrainModel();
        _predictionEngine = MlContext.Model.CreatePredictionEngine<InsightData, InsightPrediction>(_model);
    }

    private static void LoadOrTrainModel()
    {
        if (File.Exists(ModelPath))
        {
            _model = MlContext.Model.Load(ModelPath, out var _);
            return;
        }

        TrainModel();
    }

    private static void TrainModel()
    {
        string url = "https://cidastore.blob.core.windows.net/data-csharp/variacao_precos.csv";
        string dataPath = "variacao_precos.csv";
        using (WebClient client = new WebClient())
        {
            client.DownloadFile(url, dataPath);
        }

        IDataView dataView = MlContext.Data.LoadFromTextFile<InsightData>(
            dataPath, hasHeader: true, separatorChar: ',');

        var pipeline = MlContext.Transforms.Categorical.OneHotEncoding("Estacao")
            .Append(MlContext.Transforms.Categorical.OneHotEncoding("Regiao"))
            .Append(MlContext.Transforms.Concatenate("Features", "Estacao", "Regiao"))
            .Append(MlContext.Regression.Trainers.Sdca(
                labelColumnName: "Variacao",
                maximumNumberOfIterations: 100));

        _model = pipeline.Fit(dataView);
        MlContext.Model.Save(_model, dataView.Schema, ModelPath);
    }

    public static InsightPrediction PredictPrice(InsightData data)
    {
        return _predictionEngine.Predict(data);
    }

    public static (double RSquared, double RootMeanSquaredError) EvaluateModel()
    {
        var dataPath = "variacao_precos.csv";
        var dataView = MlContext.Data.LoadFromTextFile<InsightData>(
            dataPath, hasHeader: true, separatorChar: ',');
        var predictions = _model.Transform(dataView);
        var metrics = MlContext.Regression.Evaluate(
            predictions, labelColumnName: "Variacao");

        return (metrics.RSquared, metrics.RootMeanSquaredError);
    }
}