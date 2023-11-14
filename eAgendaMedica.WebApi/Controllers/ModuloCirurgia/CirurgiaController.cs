using AutoMapper;
using eAgendaMedica.Aplicacao.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.WebApi.ViewModels.ModuloCirurgia;
using Microsoft.AspNetCore.Mvc;

namespace eAgendaMedica.WebApi.Controllers.ModuloCirurgia
{
    [Route("api/[controller]")]
    [ApiController]
    public class CirurgiaController : ControllerBase
    {
        private ServicoCirurgia servicoCirurgia;
        private IMapper mapeador;

        public CirurgiaController(ServicoCirurgia servicoCirurgia, IMapper mapeador)
        {
            this.servicoCirurgia = servicoCirurgia;
            this.mapeador = mapeador;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarCirurgiaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodos()
        {
            var cirurgiaResult = await servicoCirurgia.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarCirurgiaViewModel>>(cirurgiaResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarCirurgiaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var cirurgiaResult = await servicoCirurgia.SelecionarPorIdAsync(id);

            if (cirurgiaResult.IsFailed)
                return NotFound(cirurgiaResult.Errors);

            var viewModel = mapeador.Map<VisualizarCirurgiaViewModel>(cirurgiaResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormsCirurgiaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(FormsCirurgiaViewModel viewModel)
        {
            var cirurgia = mapeador.Map<Cirurgia>(viewModel);

            var cirurgiaResult = await servicoCirurgia.InserirAsync(cirurgia);

            if (cirurgiaResult.IsFailed)
                return BadRequest(cirurgiaResult.Errors);

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormsCirurgiaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, FormsCirurgiaViewModel viewModel)
        {
            var selecaoCirurgiaResult = await servicoCirurgia.SelecionarPorIdAsync(id);

            if (selecaoCirurgiaResult.IsFailed)
                return NotFound(selecaoCirurgiaResult.Errors);

            var cirurgia = mapeador.Map(viewModel, selecaoCirurgiaResult.Value);

            var consultaResult = await servicoCirurgia.EditarAsync(cirurgia);

            if (consultaResult.IsFailed)
                return BadRequest(consultaResult.Errors);

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var cirurgiaResult = await servicoCirurgia.ExcluirAsync(id);

            if (cirurgiaResult.IsFailed)
                return NotFound(cirurgiaResult.Errors);

            return Ok();
        }
    }
}