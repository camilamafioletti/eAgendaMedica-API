using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloConsulta;
using eAgendaMedica.Dominio.ModuloMedico;
using FluentResults;
using Serilog;

namespace eAgendaMedica.Aplicacao.ModuloMedico
{
    public class ServicoMedico
    {
        private IRepositorioMedico repositorioMedico;
        private IRepositorioConsulta repositorioConsulta;
        private IRepositorioCirurgia repositorioCirurgia;
        private IContextoPersistencia contextoPersistencia;

        public ServicoMedico(
            IRepositorioMedico repositorioMedico,
            IRepositorioConsulta repositorioConsulta,
            IRepositorioCirurgia repositorioCirurgia,
            IContextoPersistencia contexto)
        {
            this.repositorioMedico = repositorioMedico;
            this.contextoPersistencia = contexto;
            this.repositorioConsulta = repositorioConsulta;
            this.repositorioCirurgia = repositorioCirurgia;
        }

        public async Task<Result<Medico>> InserirAsync(Medico medico)
        {
            var resultadoValidacao = ValidarMedico(medico);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            await repositorioMedico.InserirAsync(medico);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(medico);
        }

        public async Task<Result<Medico>> EditarAsync(Medico medico)
        {
            var resultadoValidacao = ValidarMedico(medico);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            repositorioMedico.Editar(medico);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(medico);
        }

        public async Task<Result> ExcluirAsync(Guid id)
        {
            var medico = await repositorioMedico.SelecionarPorIdAsync(id);

            if (medico == null)
                return Result.Fail($"Medico {id} não encontrado");

            repositorioMedico.Excluir(medico);

            await contextoPersistencia.GravarAsync();

            return Result.Ok();
        }

        public async Task<Result<List<Medico>>> SelecionarTodosAsync()
        {
            var medico = await repositorioMedico.SelecionarTodosAsync();

            return Result.Ok(medico);
        }

        public async Task<Result<Medico>> SelecionarPorIdAsync(Guid id)
        {
            var medico = await repositorioMedico.SelecionarPorIdAsync(id);

            return Result.Ok(medico);
        }

        private Result ValidarMedico(Medico medico)
        {
            ValidadorMedico validador = new ValidadorMedico();

            var resultadoValidacao = validador.Validate(medico);

            List<Error> erros = new List<Error>();

            foreach (var erro in resultadoValidacao.Errors)
                erros.Add(new Error(erro.ErrorMessage));

            if (erros.Any())
                return Result.Fail(erros.ToArray());

            return Result.Ok();
        }

        public async Task<Result<List<Consulta>>> SelecionarConsultasMedicoAsync(Guid id)
        {
            var consultas = await repositorioConsulta.SelecionarConsultasMedico(id);

            if (consultas == null)
            {
                Log.Logger.Warning($"Nenhuma Consulta encontrada");

                return Result.Fail($"Nenhuma Consulta encontrada");
            }

            return Result.Ok(consultas);
        }

        public async Task<Result<List<Cirurgia>>> SelecionarCirurgiasMedicoAsync(Guid id)
        {
            var cirurgias = await repositorioCirurgia.SelecionarCirurgiasMedico(id);

            if (cirurgias == null)
            {
                Log.Logger.Warning($"Nenhuma Cirurgia encontrada");

                return Result.Fail($"Nenhuma Cirurgia encontrada");
            }

            return Result.Ok(cirurgias);
        }
    }
}