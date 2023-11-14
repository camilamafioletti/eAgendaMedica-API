using eAgendaMedica.Domínio.Compartilhado;
using FluentValidation;

namespace eAgendaMedica.Dominio.ModuloMedico
{
    public class ValidadorMedico : AbstractValidator<Medico>
    {
        public ValidadorMedico()
        {
            RuleFor(x => x.Nome)
               .NotNull().NotEmpty();

            RuleFor(x => x.Crm)
               .NotNull().NotEmpty();

            RuleFor(x => x.Telefone)
               .Telefone();
        }
    }
}
