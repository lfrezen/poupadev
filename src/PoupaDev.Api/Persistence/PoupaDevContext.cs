using PoupaDev.API.Entities;

namespace PoupaDev.Api.Persistence
{
    public class PoupaDevContext
    {
        public List<ObjetivoFinanceiro> Objetivos { get; set; }

        public PoupaDevContext()
        {
            Objetivos = new List<ObjetivoFinanceiro>();
        }
    }
}