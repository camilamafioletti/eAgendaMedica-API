using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloConsulta;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.Compartilhado;
using eAgendaMedica.Infra.Orm.ModuloCirurgia;
using eAgendaMedica.Infra.Orm.ModuloConsulta;
using eAgendaMedica.Infra.Orm.ModuloMedico;
using FizzWare.NBuilder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace eAgendaMedica.TestesIntegracao.Compartilhado
{
    public class TestesIntegracaoBase
    {
        protected IRepositorioMedico repositorioMedico;
        protected IRepositorioConsulta repositorioConsulta;
        protected IRepositorioCirurgia repositorioCirurgia;

        protected IContextoPersistencia contextoPersistencia;

        public TestesIntegracaoBase()
        {

            string connectionString = ObterConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<eAgendaMedicaDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            var dbContext = new eAgendaMedicaDbContext(optionsBuilder.Options);
            contextoPersistencia = dbContext;

            repositorioMedico = new RepositorioMedicoOrm(dbContext);
            repositorioConsulta = new RepositorioConsultaOrm(dbContext);
            repositorioCirurgia = new RepositorioCirurgiaOrm(dbContext);


            BuilderSetup.SetCreatePersistenceMethod<Medico>((Medico) =>
            {
                Task.Run(async () =>
                {
                    await repositorioMedico.InserirAsync(Medico);
                    await contextoPersistencia.GravarAsync();
                }).GetAwaiter().GetResult();
            });

            BuilderSetup.SetCreatePersistenceMethod<Consulta>((Consulta) =>
            {
                Task.Run(async () =>
                {
                    await repositorioConsulta.InserirAsync(Consulta);
                    await contextoPersistencia.GravarAsync();
                }).GetAwaiter().GetResult();
            });

            BuilderSetup.SetCreatePersistenceMethod<Cirurgia>((Cirurgia) =>
            {
                Task.Run(async () =>
                {
                    await repositorioCirurgia.InserirAsync(Cirurgia);
                    await contextoPersistencia.GravarAsync();
                }).GetAwaiter().GetResult();
            });
        }

        protected static string ObterConnectionString()
        {
            var configuracao = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");
            return connectionString;
        }
    }
}