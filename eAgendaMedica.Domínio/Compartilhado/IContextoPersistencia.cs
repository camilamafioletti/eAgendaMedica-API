namespace eAgendaMedica.Dominio.Compartilhado
{
    public interface IContextoPersistencia
    {
        Task<bool> GravarAsync();
    }
}
