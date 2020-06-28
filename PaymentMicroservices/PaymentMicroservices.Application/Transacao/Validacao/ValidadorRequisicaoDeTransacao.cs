using FluentValidation;
using PaymentMicroservices.Application.Transacao.Requisicao;

namespace PaymentMicroservices.Application.Transacao.Validacao
{
    public class ValidadorRequisicaoDeTransacao : AbstractValidator<RequisicaoDeTransacao>
    {
        public ValidadorRequisicaoDeTransacao()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x).NotNull().WithMessage("Nenhum parâmetro informado");
            RuleFor(x => x.Valor).GreaterThan(0).WithMessage(x => $"Por favor informar o valor da transação.");
            RuleFor(x => x.Origem).NotNull().WithMessage(x => $"Conta Corrente de origem deve ser informada.");
            When(x => x.Origem != null, () => { RuleFor(x => x.Origem).SetValidator(new ValidadorContaCorrente("origem")); });
            RuleFor(x => x.Destino).NotNull().WithMessage(x => $"Conta Corrente de destino deve ser informada.");
            When(x => x.Destino != null, () => { RuleFor(x => x.Destino).SetValidator(new ValidadorContaCorrente("destino")); });
                                  ;

        }
    }
}
