using FluentValidation;

namespace eAgendaMedica.Dominio.ModuloCirurgia
{
    public class ValidadorCirurgia : AbstractValidator<Cirurgia>
    {
        public ValidadorCirurgia()
        {
            RuleFor(x => x.Titulo)
            .NotNull().NotEmpty();

            RuleFor(x => x.HoraInicio)
            .NotNull().NotEmpty();

            RuleFor(x => x.HoraTermino)
            .NotNull()
            .NotEmpty();

            RuleFor(x => x.Medicos)
            .NotNull().NotEmpty().WithMessage("A cirurgia deve ter pelo menos um médico.")
            .Must(medicos => medicos != null && medicos.Count > 0).WithMessage("A cirurgia deve ter pelo menos um médico.");
        }
    }
}
