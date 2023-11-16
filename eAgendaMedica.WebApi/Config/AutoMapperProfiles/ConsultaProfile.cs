using AutoMapper;
using eAgendaMedica.Dominio.ModuloConsulta;
using eAgendaMedica.WebApi.ViewModels.ModuloConsulta;

namespace eAgendaMedica.WebApi.Config.AutoMapperProfiles
{
    public class ConsultaProfile : Profile
    {
        public ConsultaProfile()
        {
            CreateMap<Consulta, ListarConsultaViewModel>()
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")));

            CreateMap<Consulta, VisualizarConsultaViewModel>()
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")));

            CreateMap<FormsConsultaViewModel, Consulta>()
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")));
        }
    }
}