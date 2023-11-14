using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloCirurgia;
using FluentResults;

namespace eAgendaMedica.Aplicacao.ModuloCirurgia
{
    public class ServicoCirurgia
    {
        private readonly IRepositorioCirurgia repositorioCirurgia;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoCirurgia(IRepositorioCirurgia repositorioCirurgia, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioCirurgia = repositorioCirurgia;
            this.contextoPersistencia = contextoPersistencia;
        }

        public async Task<Result<Cirurgia>> InserirAsync(Cirurgia cirurgia)
        {
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
