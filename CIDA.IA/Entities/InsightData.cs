using Microsoft.ML.Data;

namespace CIDA.IA.Entities
{
    public class InsightData
    {
        [LoadColumn(0)]
        public float Variacao;

        [LoadColumn(1)]
        public string Estacao;

        [LoadColumn(2)]
        public string Regiao;
    }
}
