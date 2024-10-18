using Azure.Storage.Blobs;
using CIDA.Api.Configuration.HealthChecks;
using CIDA.Api.Configuration.Routes;
using Cida.Data;
using CIDA.Domain.Entities;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace CIDA.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Database

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<CidaDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));
            var serviceProvider = builder.Services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CidaDbContext>();

                context.Autenticacoes.Add(new Autenticacao
                    { IdAutenticacao = 1, Email = "example2@example.com", HashSenha = "123456" });
                context.Autenticacoes.Add(new Autenticacao
                    { IdAutenticacao = 2, Email = "example@example.com", HashSenha = "123456" });

                context.Usuarios.AddRange(
                    new Usuario
                    {
                        IdUsuario = 1, Nome = "João Silva", Status = Status.ativo, TipoDocumento = TipoDocumento.CPF,
                        NumDocumento = "000.000.000-00", Telefone = "(21) 88888-9999", IdAutenticacao = 1,
                        DataCriacao = DateTime.Now
                    },
                    new Usuario
                    {
                        IdUsuario = 2, Nome = "Maria Santos", Status = Status.inativo,
                        TipoDocumento = TipoDocumento.CPF, NumDocumento = "111.111.111-11",
                        Telefone = "(12) 99999-9999", IdAutenticacao = 2, DataCriacao = DateTime.Now
                    }
                );
                context.SaveChanges();
            }
        }
        else
        {
            builder.Services.AddDbContext<CidaDbContext>(options =>
            {
                options.UseOracle(builder.Configuration.GetConnectionString("FiapOracleConnection"));
            });
        }

        #endregion

        #region Azure Blob Storage

// add singleton azure blob service
        builder.Services.AddSingleton(x =>
        {
            var connectionString = builder.Configuration.GetConnectionString("AzureStorage");
            return new BlobServiceClient(connectionString);
        });

        #endregion

        // add HttpClient
        builder.Services.AddHttpClient();

        // HealthChecks
        builder.Services.ConfigureHealthChecks(builder.Configuration);

        #region Swagger Configuration

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Cida API",
                Description = "Uma API para gerenciamento do banco de dados da CIDA"
            });
            options.ExampleFilters();
        });
        builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

        #endregion

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.MapHealthChecks("/api/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        app.UseHealthChecksUI(options => { options.UIPath = "/healthcheck-ui"; });

        app.MapUsuarioEndpoints();
        app.MapResumoEndpoints();
        app.MapInsightEndpoints();
        app.MapLoginEndpoints();
        app.MapArquivoEndpoints();
        app.Run();
    }
}