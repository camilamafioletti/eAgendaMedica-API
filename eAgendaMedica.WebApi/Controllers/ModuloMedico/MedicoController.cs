using AutoMapper;
using eAgendaMedica.Aplicacao.ModuloMedico;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebApi.ViewModels.ModuloCirurgia;
using eAgendaMedica.WebApi.ViewModels.ModuloConsulta;
using eAgendaMedica.WebApi.ViewModels.ModuloMedico;
using Microsoft.AspNetCore.Mvc;

namespace eAgendaMedica.WebApi.Controllers.ModuloMedico
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ApiControllerBase
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

        [HttpGet("visualizar-medico-consultas/{id}")]
        [ProducesResponseType(typeof(List<ListarConsultaViewModel>), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarConsultasMedico(Guid id)
        {
            var consultasResult = await servicoMedico.SelecionarConsultasMedicoAsync(id);

            if (consultasResult.IsFailed)
                return NotFound(consultasResult.Errors);

            var viewModel = mapeador.Map<List<ListarConsultaViewModel>>(consultasResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualizar-medico-cirurgias/{id}")]
        [ProducesResponseType(typeof(List<ListarCirurgiaViewModel>), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarCirurgiasMedico(Guid id)
        {
            var cirurgiasResult = await servicoMedico.SelecionarCirurgiasMedicoAsync(id);

            if (cirurgiasResult.IsFailed)
                return NotFound(cirurgiasResult.Errors);

            var viewModel = mapeador.Map<List<ListarCirurgiaViewModel>>(cirurgiasResult.Value);

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

            var medicoResult = await servicoMedico.InserirAsync(medico);

            if (medicoResult.IsFailed)
                return BadRequest(medicoResult.Errors);

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormsMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, FormsMedicoViewModel viewModel)
        {
            var selecaoMedicoResult = await servicoMedico.SelecionarPorIdAsync(id);

            if (selecaoMedicoResult.IsFailed)
                return NotFound(selecaoMedicoResult.Errors);

            var medico = mapeador.Map(viewModel, selecaoMedicoResult.Value);

            var medicoResult = await servicoMedico.EditarAsync(medico);

            if (medicoResult.IsFailed)
                return BadRequest(medicoResult.Errors);

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var medicoResult = await servicoMedico.ExcluirAsync(id);

            if (medicoResult.IsFailed)
                return NotFound(medicoResult.Errors);

            return Ok();
        }
    }
}
