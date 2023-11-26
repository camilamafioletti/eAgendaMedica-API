using eAgendaMedica.Dominio.ModuloCirurgia;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using eAgendaMedica.Dominio.ModuloConsulta;

namespace eAgendaMedica.Infra.Orm.ModuloConsulta
{
    public class MapeadorConsultaOrm : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("TBConsulta");

            builder.Property(x => x.Id)
               .ValueGeneratedNever();

            builder.Property(x => x.Titulo)
                .IsRequired();

            builder.Property(x => x.Data)
                .IsRequired();

            builder.Property(x => x.HoraInicio)
                .IsRequired();

            builder.Property(x => x.HoraTermino)
                .IsRequired();

            builder.HasOne(x => x.Medico)
                .WithMany(x => x.Consultas)
                .HasForeignKey(x => x.MedicoId)
                .HasConstraintName("FK_TBMedico_TBConsulta")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
