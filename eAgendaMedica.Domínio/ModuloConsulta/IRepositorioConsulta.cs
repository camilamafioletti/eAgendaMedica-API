using eAgendaMedica.Dominio.Compartilhado;

namespace eAgendaMedica.Dominio.ModuloConsulta
{
    public interface IRepositorioConsulta : IRepositorioBase<Consulta>
    {
        public Task<List<Consulta>> SelecionarConsultasMedico(Guid id);
    }
}
