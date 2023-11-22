using eAgendaMedica.Domínio.ModuloCirurgia;

namespace eAgendaMedica.WebApi.ViewModels.ModuloMedico
{
    public class SelecaoMedicoViewModel
    {
        public Guid Id { get; set; }
        public StatusMedicoCirurgia Status { get; set; }
    }
}
