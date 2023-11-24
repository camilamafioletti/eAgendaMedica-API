using eAgendaMedica.Dominio.Compartilhado;

namespace eAgendaMedica.Dominio.ModuloCirurgia
{
    public interface IRepositorioCirurgia : IRepositorioBase<Cirurgia>
    {
        public Task<List<Cirurgia>> SelecionarCirurgiasMedico(Guid id);
    }
}
