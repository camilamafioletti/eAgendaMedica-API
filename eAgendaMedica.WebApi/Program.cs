using eAgendaMedica.Aplicacao.ModuloCirurgia;
using eAgendaMedica.Aplicacao.ModuloConsulta;
using eAgendaMedica.Aplicacao.ModuloMedico;
using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloConsulta;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.Compartilhado;
using eAgendaMedica.Infra.Orm.ModuloCirurgia;
using eAgendaMedica.Infra.Orm.ModuloConsulta;
using eAgendaMedica.Infra.Orm.ModuloMedico;
using eAgendaMedica.WebApi.Config;
using eAgendaMedica.WebApi.Config.AutoMapperProfiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NoteKeeper.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;
            });

            var connectionString = builder.Configuration.GetConnectionString("SqlServer");

            builder.Services.AddDbContext<IContextoPersistencia, eAgendaMedicaDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString);
            });

            builder.Services.AddTransient<IRepositorioMedico, RepositorioMedicoOrm>();
            builder.Services.AddTransient<ServicoMedico>();

            builder.Services.AddTransient<IRepositorioConsulta, RepositorioConsultaOrm>();
            builder.Services.AddTransient<ServicoConsulta>();

            builder.Services.AddTransient<IRepositorioCirurgia, RepositorioCirurgiaOrm>();
            builder.Services.AddTransient<ServicoCirurgia>();

           //builder.Services.AddTransient<ConfigurarCategoriaMappingAction>();

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile<MedicoProfile>();
                config.AddProfile<ConsultaProfile>();
                config.AddProfile<CirurgiaProfile>();
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ManipuladorExcecoes>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}