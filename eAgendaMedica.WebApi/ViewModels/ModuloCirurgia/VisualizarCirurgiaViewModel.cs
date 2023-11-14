namespace eAgendaMedica.WebApi.ViewModels.ModuloCirurgia
{
    public class VisualizarCirurgiaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public List<string> Medicos { get; set; }
    }
}
