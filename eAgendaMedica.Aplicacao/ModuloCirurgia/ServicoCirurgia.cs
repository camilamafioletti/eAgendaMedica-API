using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloConsulta;
using FluentResults;

namespace eAgendaMedica.Aplicacao.ModuloCirurgia
{
    public class ServicoCirurgia
    {
        private IRepositorioCirurgia repositorioCirurgia;
        private IRepositorioConsulta repositorioConsulta;
        private IContextoPersistencia contextoPersistencia;

        public ServicoCirurgia(
            IRepositorioCirurgia repositorioCirurgia,
            IRepositorioConsulta repositorioConsulta,
            IContextoPersistencia contexto)
        {
            this.repositorioCirurgia = repositorioCirurgia;
            this.repositorioConsulta = repositorioConsulta;
            this.contextoPersistencia = contexto;
        }

        public async Task<Result<Cirurgia>> InserirAsync(Cirurgia cirurgia)
        {
            TimeSpan horarioLimite = new TimeSpan(19, 59, 0);

            if (cirurgia.HoraTermino > horarioLimite)
                return Result.Fail("O horário Término limite é 19:59");

            TimeSpan periodoDescanso = TimeSpan.FromHours(4);

            cirurgia.HoraTermino += periodoDescanso;

            var medicosIds = cirurgia.Medicos.Select(m => m.Id).ToList();

            foreach (var medicoId in medicosIds)
            {
                var JaExisteConsulta = await repositorioConsulta.ExisteConsultaNesseHorarioPorMedicoId(medicoId, cirurgia.HoraInicio, cirurgia.HoraTermino, cirurgia.Data);

                var JaExisteCirurgia = await repositorioCirurgia.ExisteCirurgiasNesseHorarioPorMedicoId(medicoId, cirurgia.HoraInicio, cirurgia.HoraTermino, cirurgia.Data);

                if (JaExisteConsulta || JaExisteCirurgia)
                    return Result.Fail("Horário indísponivel");
            }

            var resultadoValidacao = ValidarCirurgia(cirurgia);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            await repositorioCirurgia.InserirAsync(cirurgia);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(cirurgia);
        }

        public async Task<Result<Cirurgia>> EditarAsync(Cirurgia cirurgia)
        {
            var resultadoValidacao = ValidarCirurgia(cirurgia);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            repositorioCirurgia.Editar(cirurgia);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(cirurgia);
        }

        public async Task<Result> ExcluirAsync(Guid id)
        {
            var cirurgia = await repositorioCirurgia.SelecionarPorIdAsync(id);

            if (cirurgia == null)
                return Result.Fail($"Cirurgia {id} não encontrada");

            repositorioCirurgia.Excluir(cirurgia);

            await contextoPersistencia.GravarAsync();

            return Result.Ok();
        }

        public async Task<Result<List<Cirurgia>>> SelecionarTodosAsync()
        {
            var cirurgia = await repositorioCirurgia.SelecionarTodosAsync();

            return Result.Ok(cirurgia);
        }

        public async Task<Result<Cirurgia>> SelecionarPorIdAsync(Guid id)
        {
            var cirurgia = await repositorioCirurgia.SelecionarPorIdAsync(id);

            return Result.Ok(cirurgia);
        }

        private Result ValidarCirurgia(Cirurgia cirurgia)
        {
            ValidadorCirurgia validador = new ValidadorCirurgia();

            var resultadoValidacao = validador.Validate(cirurgia);

            List<Error> erros = new List<Error>();

            foreach (var erro in resultadoValidacao.Errors)
                erros.Add(new Error(erro.ErrorMessage));

            if (erros.Any())
                return Result.Fail(erros.ToArray());

            return Result.Ok();
        }
    }
}
