namespace eAgendaMedica.WebApi.ViewModels.ModuloCirurgia
{
    public class ListarCirurgiaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }

    }
}
