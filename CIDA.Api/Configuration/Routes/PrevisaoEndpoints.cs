using CIDA.Api.Models;
using CIDA.Api.Models.Metadatas;
using CIDA.Api.Models.Requests;
using CIDA.Api.Models.Responses;
using CIDA.IA.Entities;
using CIDA.IA.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace CIDA.Api.Configuration.Routes;

public static class PrevisaoEndpoints
{
    public static void MapPrevisaoEndpoints(this WebApplication app)
    {
        var previsaoGroup = app.MapGroup("/previsao");

        previsaoGroup.MapPost("/", async (PrevisaoRequest model) =>
        {

            var possiblesEstacao = new List<string> (["Verão", "Inverno", "Primavera", "Outono"]);
            var possiblesRegioes = new List<string>(["Sul", "Sudeste", "Centro-Oeste", "Norte", "Nordeste"]);


            if (!possiblesEstacao.Contains(model.Estacao)) return Results.BadRequest("Estação inválida. Use: Verão, Inverno, Primavera ou Outono");
            if(!possiblesRegioes.Contains(model.Regiao)) return Results.BadRequest("Região inválida. Use: Sul, Sudeste, Centro-Oeste, Norte ou Nordeste");

            var data = new InsightData
            {
                Estacao = model.Estacao,
                Regiao = model.Regiao
            };

            var prediction = MLModelService.PredictPrice(data);


            var reponse = new PrevisaoModel(prediction.Variacao);

                return Results.Ok(reponse);
           
        })
            .Accepts<PrevisaoRequest>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("Previsão")
            .WithTags("IA")
            .WithDescription("Gere a variação de valor do insight dependendo da região e estação do ano")
            .WithMetadata(new SwaggerRequestExampleAttribute(typeof(PrevisaoRequestMetadata),
                typeof(PrevisaoRequestMetadata)))
            .WithOpenApi();

        previsaoGroup.MapGet("/metrics", () =>
        {
            var (RSquared, RootMeanSquaredError) = MLModelService.EvaluateModel();

            return new MetricsModel(RSquared, RootMeanSquaredError);
        })
            .Produces<MetricsModel>()
            .WithName("Metrics")
            .WithTags("IA")
            .WithDescription("Retorna as métricas de avaliação do modelo de IA (R2 e RMSE)")
            .WithOpenApi();

    }
}

