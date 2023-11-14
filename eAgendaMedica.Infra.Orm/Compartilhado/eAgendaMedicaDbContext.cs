using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Infra.Orm.ModuloCirurgia;
using eAgendaMedica.Infra.Orm.ModuloConsulta;
using eAgendaMedica.Infra.Orm.ModuloMedico;
using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Infra.Orm.Compartilhado
{
    public class eAgendaMedicaDbContext : DbContext, IContextoPersistencia
    {
        public eAgendaMedicaDbContext(DbContextOptions options) : base(options)
        {
        }

        public async Task<bool> GravarAsync()
        {
            await SaveChangesAsync();
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapeadorCirurgiaOrm());

            modelBuilder.ApplyConfiguration(new MapeadorConsultaOrm());

            modelBuilder.ApplyConfiguration(new MapeadorMedicoOrm());

            base.OnModelCreating(modelBuilder);
        }
    }
}
