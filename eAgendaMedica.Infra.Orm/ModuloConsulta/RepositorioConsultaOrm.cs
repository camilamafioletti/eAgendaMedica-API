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

        public async Task<bool> ExisteConsultaNesseHorarioPorMedicoId(Consulta consulta)
        {
            return await registros.AnyAsync(x => x.MedicoId == consulta.MedicoId && ((consulta.HoraInicio >= x.HoraInicio && consulta.HoraInicio <= x.HoraTermino) ||
            (consulta.HoraTermino >= x.HoraInicio && consulta.HoraTermino <= x.HoraTermino)) && consulta.Data.Date == x.Data.Date);
        }
    }
}
