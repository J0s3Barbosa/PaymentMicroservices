namespace PaymentMicroservices.Application.Transacao.Requisicao
{
    public class RequisicaoDeTransacao
    {
        public RequisicaoDeContaCorrente Origem { get; set; }
        public RequisicaoDeContaCorrente Destino { get; set; }
        public decimal Valor { get; set; }
    }
}
