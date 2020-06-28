using FluentValidation;
using PaymentMicroservices.Application.Transacao.Requisicao;

namespace PaymentMicroservices.Application.Transacao.Validacao
{
    public class ValidadorContaCorrente : AbstractValidator<RequisicaoDeContaCorrente>
    {
        public ValidadorContaCorrente(string tipo) 
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x).NotNull().WithMessage("Nenhum parâmetro informado");
            RuleFor(x => x.Agencia).NotEmpty().WithMessage($"Informar o número da agência de {tipo}")
                                   .MaximumLength(5).WithMessage($"Agência de {tipo} deve conter no máximo 5 caractéres");
            RuleFor(x => x.Numero).NotEmpty().WithMessage($"Informar o número da conta de {tipo} ")
                                  .Must(Numerico).WithMessage($"Numero de conta de {tipo} deve ser apenas números");
                                  
        }

        private bool Numerico(string numero)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(numero??string.Empty, "^[0-9]*$");
        }
    }
}
