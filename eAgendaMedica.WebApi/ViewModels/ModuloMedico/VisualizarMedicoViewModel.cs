namespace eAgendaMedica.WebApi.ViewModels.ModuloMedico
{
    public class VisualizarMedicoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Disponibilidade { get; set; }
        public string Crm { get; private set; }
    }
}
