using AutoMapper;
using eAgendaMedica.Aplicacao.ModuloConsulta;
using eAgendaMedica.Dominio.ModuloConsulta;
using eAgendaMedica.WebApi.ViewModels.ModuloConsulta;
using Microsoft.AspNetCore.Mvc;

namespace eAgendaMedica.WebApi.Controllers.ModuloConsulta
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ApiControllerBase
    {
        private ServicoConsulta servicoConsulta;
        private IMapper mapeador;

        public ConsultaController(ServicoConsulta servicoConsulta, IMapper mapeador)
        {
            this.servicoConsulta = servicoConsulta;
            this.mapeador = mapeador;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodos()
        {
            var consultaResult = await servicoConsulta.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarConsultaViewModel>>(consultaResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarCompletoPorId(Guid id)
        {
            var consultaResult = await servicoConsulta.SelecionarPorIdAsync(id);

            if (consultaResult.IsFailed)
                return NotFound(consultaResult.Errors);

            var viewModel = mapeador.Map<VisualizarConsultaViewModel>(consultaResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormsConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var consultaResult = await servicoConsulta.SelecionarPorIdAsync(id);

            if (consultaResult.IsFailed)
                return NotFound(consultaResult.Errors);

            var viewModel = mapeador.Map<FormsConsultaViewModel>(consultaResult.Value);

            return Ok(viewModel);
        }


        [HttpPost]
        [ProducesResponseType(typeof(FormsConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(FormsConsultaViewModel viewModel)
        {
            var consulta = mapeador.Map<Consulta>(viewModel);

            var consultaResult = await servicoConsulta.InserirAsync(consulta);

            if (consultaResult.IsFailed)
                return BadRequest(consultaResult.Errors);

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormsConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, FormsConsultaViewModel viewModel)
        {
            var selecaoConsultaResult = await servicoConsulta.SelecionarPorIdAsync(id);

            if (selecaoConsultaResult.IsFailed)
                return NotFound(selecaoConsultaResult.Errors);

            var consulta = mapeador.Map(viewModel, selecaoConsultaResult.Value);

            var consultaResult = await servicoConsulta.EditarAsync(consulta);

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
            var consultaResult = await servicoConsulta.ExcluirAsync(id);

            if (consultaResult.IsFailed)
                return NotFound(consultaResult.Errors);

            return Ok();
        }
    }
}
