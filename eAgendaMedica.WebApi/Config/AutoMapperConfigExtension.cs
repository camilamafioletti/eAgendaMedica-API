using E_AgendaMedicaApi.Config.AutomapperConfig;
using eAgendaMedica.WebApi.Config.AutoMapperProfiles;

namespace eAgenda.WebApi.Config.AutomapperConfig
{
    public static class AutoMapperConfigExtension
    {
        public static void ConfigurarAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<CirurgiaProfile>();
                opt.AddProfile<ConsultaProfile>();
                opt.AddProfile<MedicoProfile>();
            });
        }
    }
}
