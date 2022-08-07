using PoupaDev.API.Enums;

namespace PoupaDev.Api.Models
{
    public class OperacaoInputModel
    {
        public decimal Valor { get; set; }
        public TipoOperacao TipoOperacao { get; set; }
    }
}