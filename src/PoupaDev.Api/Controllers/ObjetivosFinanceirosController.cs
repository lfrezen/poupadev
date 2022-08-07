using Microsoft.AspNetCore.Mvc;
using PoupaDev.Api.Models;
using PoupaDev.Api.Persistence;
using PoupaDev.API.Entities;

namespace PoupaDev.Api.Controllers
{
    [ApiController]
    [Route("api/objetivos-financeiros")]
    public class ObjetivosFinanceirosController : ControllerBase
    {
        private readonly PoupaDevContext _context;

        public ObjetivosFinanceirosController(PoupaDevContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var objetivos = _context.Objetivos;

            return Ok(objetivos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var objetivo = _context.Objetivos
                .SingleOrDefault(o => o.Id == id);

            if (objetivo == null) return NotFound();

            return Ok(objetivo);
        }

        [HttpPost]
        public IActionResult Post(ObjetivoFinanceiroInputModel model)
        {
            var objetivo = new ObjetivoFinanceiro(
                model.Titulo,
                model.Descricao,
                model.ValorObjetivo);

            _context.Objetivos.Add(objetivo);

            var id = objetivo.Id;

            return CreatedAtAction(nameof(GetById), new { id }, model);
        }

        [HttpPost("{id}/operacoes")]
        public IActionResult PostOperacao(int id, OperacaoInputModel model)
        {
            var operacao = new Operacao(
                model.Valor,
                model.TipoOperacao);

            var objetivo = _context.Objetivos
                .SingleOrDefault(o => o.Id == id);

            if (objetivo == null) return NotFound();

            objetivo.RealizarOperacao(operacao);

            return NoContent();
        }
    }
}