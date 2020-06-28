using PaymentMicroservices.Domain.Modelo;
using System;
using Xunit;

namespace PaymentMicroservices.UnitTest.Domain
{
    public class ContaCorrenteTeste
    {

        [Fact(DisplayName = "Cenario - valida credito em conta")]
        [Trait("Category", "Success")]
        public void Deve_Retornar_Sucesso_Ao_Creditar_Valor()
        {
            var contaCorrente = new ContaCorrente("0001", "123456", "7")
            {
                Saldo = 100
            };

            contaCorrente.Creditar(50);

            Assert.Equal(150, contaCorrente.Saldo);

        }

        [Fact(DisplayName = "Cenario - valida debito")]
        [Trait("Category", "Success")]
        public void Deve_Retornar_Sucesso_Ao_Debitar_Valor()
        {
            var contaCorrente = new ContaCorrente("0001", "123456", "7")
            {
                Saldo = 100
            };

            contaCorrente.Debitar(63.28M);

            Assert.Equal(36.72M, contaCorrente.Saldo);

        }

        [Fact(DisplayName = "Cenario - valida Conta é Valida")]
        [Trait("Category", "Success")]
        public void Deve_Retornar_Sucesso_Ao_Obter_Id()
        {
            var contaCorrente = new ContaCorrente("0001", "123456", "7")
            {
                Id = Guid.NewGuid().ToString(),
                Saldo = 100
            };

            Assert.True(contaCorrente.ContaValida());
        }

        [Fact(DisplayName = "Cenario - valida Conta inValida")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_Sucesso_Ao_Obter_Id_Vazio()
        {
            var contaCorrente = new ContaCorrente("0001", "123456", "7")
            {
                Saldo = 100
            };

            Assert.False(contaCorrente.ContaValida());
        }
    }

}
