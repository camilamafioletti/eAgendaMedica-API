using eAgendaMedica.WebApi.ViewModels.ModuloMedico;

namespace eAgendaMedica.WebApi.ViewModels.ModuloCirurgia
{
    public class FormsCirurgiaViewModel
    {
        public string Titulo { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public List<SelecaoMedicoViewModel> MedicosSelecionados { get; set; }

        public FormsCirurgiaViewModel()
        {
            MedicosSelecionados = new List<SelecaoMedicoViewModel>();
        }
    }
}