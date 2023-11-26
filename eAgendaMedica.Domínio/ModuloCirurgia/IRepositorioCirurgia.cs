using eAgendaMedica.Dominio.Compartilhado;

namespace eAgendaMedica.Dominio.ModuloCirurgia
{
    public interface IRepositorioCirurgia : IRepositorioBase<Cirurgia>
    {
        public Task<List<Cirurgia>> SelecionarCirurgiasMedico(Guid id);

        public Task<bool> ExisteCirurgiasNesseHorarioPorMedicoId(Guid medicoId, TimeSpan horaInicio, TimeSpan horaTermino, DateTime data);
    }
}
