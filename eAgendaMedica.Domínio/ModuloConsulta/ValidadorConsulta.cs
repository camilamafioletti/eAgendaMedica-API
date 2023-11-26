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

            RuleFor(x => x.Titulo)
                .MinimumLength(3)
                .WithMessage("O título da consulta deve ter no mínimo 3 caracteres.");

        }
    }
}
