using CIDA.Api.Models.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CIDA.Api.Models.Metadatas
{
    public class PrevisaoRequestMetadata : IExamplesProvider<PrevisaoRequest>
    {
        public PrevisaoRequest GetExamples()
        {
            return new PrevisaoRequest
            (
                "Verão",
                "Sul"
            );
        }
    }
}