using eAgendaMedica.Dominio.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgendaMedica.Infra.Orm.ModuloMedico
{
    public class MapeadorMedicoOrm : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("TBMedico");

            builder.Property(x => x.Id)
               .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired();

            builder.Property(x => x.Crm)
                .IsRequired();

            builder.Property(x => x.Telefone)
                .IsRequired();

            builder.Property(x => x.Disponibilidade)
                .HasDefaultValue(true).IsRequired();


            builder.HasMany(x => x.Cirurgias)
                .WithMany(x => x.Medicos)
                .UsingEntity(x => x.ToTable("TBMedico_TBCirurgia"));
        }
    }
}