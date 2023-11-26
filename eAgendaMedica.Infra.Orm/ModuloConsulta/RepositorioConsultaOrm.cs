using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloConsulta;
using eAgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Infra.Orm.ModuloConsulta
{
    public class RepositorioConsultaOrm : RepositorioBase<Consulta>, IRepositorioConsulta
    {
        public RepositorioConsultaOrm(IContextoPersistencia ctx) : base(ctx)
        {
        }

        public override async Task<Consulta> SelecionarPorIdAsync(Guid id)
        {
            return await registros.Include(x => x.Medico)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<List<Consulta>> SelecionarTodosAsync()
        {
            return await registros.Include(x => x.Medico).ToListAsync();
        }

        public async Task<List<Consulta>> SelecionarConsultasMedico(Guid id)
        {
            return await registros.Where(x => x.Medico.Id == id).ToListAsync();
        }

        public async Task<bool> ExisteConsultaNesseHorarioPorMedicoId(Guid medicoId, TimeSpan horaInicio, TimeSpan horaTermino, DateTime data)
        {
            return await registros.AnyAsync(x => x.MedicoId == medicoId &&
            (((horaInicio >= x.HoraInicio && horaInicio <= x.HoraTermino && data.Date == x.Data.Date) ||
                (horaTermino >= x.HoraInicio && horaTermino <= x.HoraTermino && data.Date == x.Data.Date)) ||
                (x.HoraInicio >= horaInicio && x.HoraTermino <= horaTermino && data.Date == x.Data.Date)));
        }
    }
}
