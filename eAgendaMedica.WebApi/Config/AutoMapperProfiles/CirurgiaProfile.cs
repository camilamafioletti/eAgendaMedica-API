using AutoMapper;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Domínio.ModuloCirurgia;
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
                .ForMember(destino => destino.MedicosSelecionados, opt => opt.MapFrom(origem => origem.Medicos));
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
            foreach (var medicoVM in viewModel.MedicosSelecionados)
            {
                if (medicoVM.Status == StatusMedicoCirurgia.Adicionado)
                {
                    var medico = repositorioMedico.SelecionarPorId(medicoVM.Id);
                    cirurgia.AdicionarMedico(medico);
                }
                else if (medicoVM.Status == StatusMedicoCirurgia.Removido)
                {
                    cirurgia.RemoverMedico(medicoVM.Id);
                }
            }
        }
    }
}