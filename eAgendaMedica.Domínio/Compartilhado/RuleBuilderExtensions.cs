using FluentValidation;

namespace eAgendaMedica.Domínio.Compartilhado
{
    public static class RuleBuilderExtentions
    {
        public static IRuleBuilder<T, string> Telefone<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .Matches(@"^((\d{2})?\s?)?(\d{4,5}-\d{4})$");

            return options;
        }
    }
}