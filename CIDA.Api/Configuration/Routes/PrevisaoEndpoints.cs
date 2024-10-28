using CIDA.Api.Models.Requests;
using CIDA.IA.Service;

namespace CIDA.Api.Configuration.Routes;

public static class PrevisaoEndpoints
{
    public static void MapPrevisaoEndpoints(this WebApplication app)
    {
        var previsaoGroup = app.MapGroup("/previsao");

        previsaoGroup.MapPost("/", async (PrevisaoRequest model) =>
        {

        });

    }
}

