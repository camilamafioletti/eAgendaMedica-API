using eAgendaMedica.Dominio.Compartilhado;

namespace eAgendaMedica.Dominio.ModuloMedico
{
    public interface IRepositorioMedico : IRepositorioBase<Medico>
    {
        public List<Medico> SelecionarMuitos(List<Guid> medicos);
        public List<Guid> SelecionarMuitos(List<Medico> medicos);


    }
}
