namespace CIDA.Api.Models.Responses;

public record MetricsModel (
    double RSquared,
    double RootMeanSquaredError
);
