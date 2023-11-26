using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Infra.Orm.ModuloCirurgia
{
    public class RepositorioCirurgiaOrm : RepositorioBase<Cirurgia>, IRepositorioCirurgia
    {
        public RepositorioCirurgiaOrm(IContextoPersistencia ctx) : base(ctx)
        {
        }

        public override Cirurgia SelecionarPorId(Guid id)
        {
            return registros.Include(x => x.Medicos).SingleOrDefault(x => x.Id == id);
        }

        public override async Task<Cirurgia> SelecionarPorIdAsync(Guid id)
        {
            return registros.Include(x => x.Medicos).SingleOrDefault(x => x.Id == id);
        }

        public override async Task<List<Cirurgia>> SelecionarTodosAsync()
        {
            return await registros.Include(x => x.Medicos).ToListAsync();
        }

        public async Task<List<Cirurgia>> SelecionarCirurgiasMedico(Guid id)
        {
            return await registros.Where(cirurgia => cirurgia.Medicos.Any(medico => medico.Id == id)).ToListAsync();
        }

        public async Task<bool> ExisteCirurgiasNesseHorarioPorMedicoId(Guid medicoId, TimeSpan horaInicio, TimeSpan horaTermino, DateTime data)
        {
            TimeSpan periodoDescanso = TimeSpan.FromHours(4);

            return await registros.Where(cirurgia => cirurgia.Medicos.Any(medico => medico.Id == medicoId))
                .AnyAsync(x => ((horaInicio >= x.HoraInicio && horaInicio <= x.HoraTermino && data.Date == x.Data.Date) ||
                (horaTermino >= x.HoraInicio && horaTermino <= x.HoraTermino && data.Date == x.Data.Date)) ||
                (x.HoraInicio >= horaInicio && x.HoraTermino <= horaTermino && data.Date == x.Data.Date));
        }
    }
}
