using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloConsulta;
using FluentResults;

namespace eAgendaMedica.Aplicacao.ModuloConsulta
{
    public class ServicoConsulta
    {
        private IRepositorioConsulta repositorioConsulta;
        private IRepositorioCirurgia repositorioCirurgia;
        private IContextoPersistencia contextoPersistencia;

        public ServicoConsulta(
                    IRepositorioConsulta repositorioConsulta,
                    IRepositorioCirurgia repositorioCirurgia,
                    IContextoPersistencia contexto)
        {
            this.repositorioConsulta = repositorioConsulta;
            this.repositorioCirurgia = repositorioCirurgia;
            this.contextoPersistencia = contexto;
        }

        public async Task<Result<Consulta>> InserirAsync(Consulta consulta)
        {
            TimeSpan horarioLimite = new TimeSpan(23, 30, 0);

            if (consulta.HoraTermino > horarioLimite)
                return Result.Fail("O horário Término limite é 23:30");

            TimeSpan periodoDescanso = TimeSpan.FromMinutes(20);

            consulta.HoraTermino += periodoDescanso;

            var JaExisteConsulta = await repositorioConsulta.ExisteConsultaNesseHorarioPorMedicoId(consulta.MedicoId, consulta.HoraInicio, consulta.HoraTermino, consulta.Data);

            var JaExisteCirurgia = await repositorioCirurgia.ExisteCirurgiasNesseHorarioPorMedicoId(consulta.MedicoId, consulta.HoraInicio, consulta.HoraTermino, consulta.Data);

            if (JaExisteConsulta || JaExisteCirurgia)
                return Result.Fail("Horário indísponivel");

            var resultadoValidacao = ValidarConsulta(consulta);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            await repositorioConsulta.InserirAsync(consulta);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(consulta);
        }

        public async Task<Result<Consulta>> EditarAsync(Consulta consulta)
        {
            var resultadoValidacao = ValidarConsulta(consulta);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            repositorioConsulta.Editar(consulta);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(consulta);
        }

        public async Task<Result> ExcluirAsync(Guid id)
        {
            var consulta = await repositorioConsulta.SelecionarPorIdAsync(id);

            if (consulta == null)
                return Result.Fail($"Consulta {id} não encontrada");

            repositorioConsulta.Excluir(consulta);

            await contextoPersistencia.GravarAsync();

            return Result.Ok();
        }

        public async Task<Result<List<Consulta>>> SelecionarTodosAsync()
        {
            var consulta = await repositorioConsulta.SelecionarTodosAsync();

            return Result.Ok(consulta);
        }

        public async Task<Result<Consulta>> SelecionarPorIdAsync(Guid id)
        {
            var consulta = await repositorioConsulta.SelecionarPorIdAsync(id);

            return Result.Ok(consulta);
        }

        private Result ValidarConsulta(Consulta consulta)
        {
            ValidadorConsulta validador = new ValidadorConsulta();

            var resultadoValidacao = validador.Validate(consulta);

            List<Error> erros = new List<Error>();

            foreach (var erro in resultadoValidacao.Errors)
                erros.Add(new Error(erro.ErrorMessage));

            if (erros.Any())
                return Result.Fail(erros.ToArray());

            return Result.Ok();
        }
    }
}