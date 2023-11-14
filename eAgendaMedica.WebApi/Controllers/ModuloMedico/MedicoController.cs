using AutoMapper;
using eAgendaMedica.Aplicacao.ModuloMedico;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebApi.ViewModels.ModuloMedico;
using Microsoft.AspNetCore.Mvc;

namespace eAgendaMedica.WebApi.Controllers.ModuloMedico
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private ServicoMedico servicoMedico;
        private IMapper mapeador;

        public MedicoController(ServicoMedico servicoMedico, IMapper mapeador)
        {
            this.servicoMedico = servicoMedico;
            this.mapeador = mapeador;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodos()
        {
            var medicoResult = await servicoMedico.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarMedicoViewModel>>(medicoResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var medicoResult = await servicoMedico.SelecionarPorIdAsync(id);

            if (medicoResult.IsFailed)
                return NotFound(medicoResult.Errors);

            var viewModel = mapeador.Map<VisualizarMedicoViewModel>(medicoResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormsMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(FormsMedicoViewModel viewModel)
        {
            var medico = mapeador.Map<Medico>(viewModel);

            var categoriaResult = await servicoMedico.InserirAsync(medico);

            if (categoriaResult.IsFailed)
                return BadRequest(categoriaResult.Errors);

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormsMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, FormsMedicoViewModel viewModel)
        {
            var selecaoCategoriaResult = await servicoMedico.SelecionarPorIdAsync(id);

            if (selecaoCategoriaResult.IsFailed)
                return NotFound(selecaoCategoriaResult.Errors);

            var categoria = mapeador.Map(viewModel, selecaoCategoriaResult.Value);

            var categoriaResult = await servicoMedico.EditarAsync(categoria);

            if (categoriaResult.IsFailed)
                return BadRequest(categoriaResult.Errors);

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var categoriaResult = await servicoMedico.ExcluirAsync(id);

            if (categoriaResult.IsFailed)
                return NotFound(categoriaResult.Errors);

            return Ok();
        }
    }
}
