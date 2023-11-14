using FluentValidation;

namespace eAgendaMedica.Dominio.ModuloConsulta
{
    public class ValidadorConsulta : AbstractValidator<Consulta>
    {
        public ValidadorConsulta()
        {
            RuleFor(x => x.Titulo)
            .NotNull().NotEmpty();

            RuleFor(x => x.HoraInicio)
            .NotNull().NotEmpty();

            RuleFor(x => x.HoraTermino)
            .NotNull().NotEmpty();

            //RuleFor(x => x.Medico)
            //.NotNull().NotEmpty();
        }
    }
}
