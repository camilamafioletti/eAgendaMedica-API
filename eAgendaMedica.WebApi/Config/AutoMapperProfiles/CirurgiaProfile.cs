using AutoMapper;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebApi.ViewModels.ModuloCirurgia;

namespace E_AgendaMedicaApi.Config.AutomapperConfig
{
    public class CirurgiaProfile : Profile
    {
        public CirurgiaProfile()
        {
            CreateMap<Cirurgia, ListarCirurgiaViewModel>()
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")));

            CreateMap<Cirurgia, VisualizarCirurgiaViewModel>()
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")));

            CreateMap<FormsCirurgiaViewModel, Cirurgia>()
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")))
                .ForMember(destino => destino.Medicos, opt => opt.Ignore())
                .AfterMap<FormsCirurgiaMappingAction>();

            CreateMap<Cirurgia, FormsCirurgiaViewModel>()
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")))
                .ForMember(destino => destino.MedicosSelecionados, opt => opt.Ignore())
                .AfterMap<FormsCirurgiaMappingActionInverso>();
        }
    }

    public class FormsCirurgiaMappingAction : IMappingAction<FormsCirurgiaViewModel, Cirurgia>
    {
        private readonly IRepositorioMedico repositorioMedico;

        public FormsCirurgiaMappingAction(IRepositorioMedico repositorioMedico)
        {
            this.repositorioMedico = repositorioMedico;
        }

        public void Process(FormsCirurgiaViewModel viewModel, Cirurgia cirurgia, ResolutionContext context)
        {
            cirurgia.Medicos = repositorioMedico.SelecionarMuitos(viewModel.MedicosSelecionados);
        }
    }

    public class FormsCirurgiaMappingActionInverso : IMappingAction<Cirurgia, FormsCirurgiaViewModel>
    {
        private readonly IRepositorioMedico repositorioMedico;

        public FormsCirurgiaMappingActionInverso(IRepositorioMedico repositorioMedico)
        {
            this.repositorioMedico = repositorioMedico;
        }

        public void Process(Cirurgia destination, FormsCirurgiaViewModel source, ResolutionContext context)
        {
            source.MedicosSelecionados = repositorioMedico.SelecionarMuitos(destination.Medicos);
        }
    }
}