using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.Compartilhado;

namespace eAgendaMedica.Infra.Orm.ModuloMedico
{
    public class RepositorioMedicoOrm : RepositorioBase<Medico>, IRepositorioMedico
    {
        public RepositorioMedicoOrm(IContextoPersistencia ctx) : base(ctx)
        {
        }

        public List<Medico> SelecionarMuitos(List<Guid> idsMedicosSelecionados)
        {
            return registros.Where(medico => idsMedicosSelecionados.Contains(medico.Id)).ToList();
        }

        public List<Guid> SelecionarMuitos(List<Medico> medicos)
        {
            return medicos.Select(medico => medico.Id).ToList();
        }
    }
}
