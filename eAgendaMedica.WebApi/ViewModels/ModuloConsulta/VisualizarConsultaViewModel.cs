namespace eAgendaMedica.WebApi.ViewModels.ModuloConsulta
{
    public class VisualizarConsultaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public Guid MedicoId { get; set; }
    }
}
