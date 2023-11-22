using AutoMapper;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebApi.ViewModels.ModuloMedico;

namespace eAgendaMedica.WebApi.Config.AutoMapperProfiles
{
    public class MedicoProfile : Profile
    {
        public MedicoProfile()
        {
            CreateMap<Medico, ListarMedicoViewModel>();
            CreateMap<Medico, VisualizarMedicoViewModel>();
            CreateMap<FormsMedicoViewModel, Medico>();
            CreateMap<ListarMedicoViewModel, Medico>();
            CreateMap<Medico, ListarMedicoViewModel> ();
        }
    }
}