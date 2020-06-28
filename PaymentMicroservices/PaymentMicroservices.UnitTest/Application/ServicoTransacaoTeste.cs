using Moq;
using PaymentMicroservices.Application.Repositorios.Interfaces;
using PaymentMicroservices.Application.Transacao;
using PaymentMicroservices.Application.Transacao.Requisicao;
using PaymentMicroservices.Domain.Modelo;
using System;
using Xunit;

namespace PaymentMicroservices.UnitTest.Application
{
    public class ServicoTransacaoTeste
    {
        private readonly ServicoTransacao _servicoTransacao;
        private readonly Mock<IContaCorrenteRepositorio> _contaCorrenteRepositorio;
        private readonly ServicoMovimentacao _movimentacao;

        public ServicoTransacaoTeste()
        {
            _contaCorrenteRepositorio = new Mock<IContaCorrenteRepositorio>();
            _movimentacao = new ServicoMovimentacao(_contaCorrenteRepositorio.Object);
            _servicoTransacao = new ServicoTransacao(_contaCorrenteRepositorio.Object, _movimentacao);

        }

        [Fact(DisplayName = "Cenario - Valida Conta Corrente Parametro Valor For Zero")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_Valor_For_Zero()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao();
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }


        [Fact(DisplayName = "Cenario - Valida Conta Corrente Origem E Destino Forem Nulas")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Origem_E_Destino_Forem_Nulas()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M
            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact(DisplayName = "Cenario - Valida Conta Corrente Origem For Nula")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Origem_For_Nula()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },

            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact(DisplayName = "Cenario - Valida Conta Corrente Destino For Nula")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Destino_For_Nula()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },
            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact(DisplayName = "Cenario - Valida Conta Corrente Origem E Destino Forem Vazios")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Origem_E_Destino_Forem_Vazios()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente(),
                Destino = new RequisicaoDeContaCorrente()
            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact(DisplayName = "Cenario - Valida Conta Corrente Origem E Destino Nao Possuirem Numeros Validos")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Origem_E_Destino_Nao_Possuirem_Numero_Valido()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "A123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456Z", Digito = "7" }
            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact(DisplayName = "Cenario - Valida Conta Corrente Origem Nao Possuir Numero Valido")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Origem_Nao_Possuir_Numero_Valido()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "A123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" }
            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact(DisplayName = "Cenario - Valida Conta Corrente Destino Nao Possuir Numero Valido")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Parametro_ContaCorrente_Destino_Nao_Possuir_Numero_Valido()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456Z", Digito = "7" }
            };
            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact(DisplayName = "Cenario - Valida Nao Encontrar Conta Corrente Origem")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Nao_Encontar_ContaCorrente_Origem()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "654321", Digito = "0" }
            };


            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "123456"))).Returns((ContaCorrente)null);
            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "654321"))).Returns(new ContaCorrente("0001", "654321", "0") { Id = Guid.NewGuid().ToString(), Saldo = 200M });

            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }


        [Fact(DisplayName = "Cenario - Valida Nao Encontrar Conta Corrente Destino")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Nao_Encontar_ContaCorrente_Destino()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "654321", Digito = "0" }
            };


            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "123456"))).Returns(new ContaCorrente("0001", "123456", "7") { Id = Guid.NewGuid().ToString(), Saldo = 500M });
            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "654321"))).Returns((ContaCorrente)null);

            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact(DisplayName = "Cenario - Valida Exececao Ao Persistir Saldo")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_Sucesso_Como_Falso_Quando_Der_Alguma_Exececao_AO_Persistir_Saldo()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "654321", Digito = "0" }
            };


            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "123456"))).Returns(new ContaCorrente("0001", "123456", "7") { Id = Guid.NewGuid().ToString(), Saldo = 500M });
            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "654321"))).Returns(new ContaCorrente("0001", "654321", "0") { Id = Guid.NewGuid().ToString(), Saldo = 200M });

            _contaCorrenteRepositorio.Setup(x => x.AtualizarSaldo(It.IsAny<ContaCorrente>())).Throws(new Exception());


            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.False(response.Sucesso);
        }

        [Fact(DisplayName = "Cenario - Valida Transação Valida")]
        [Trait("Category", "Success")]
        public void Deve_Retornar_Sucesso_Como_Verdadeiro_Efetuar_Transacao()
        {
            var requisicaoTransacao = new RequisicaoDeTransacao()
            {
                Valor = 100M,
                Origem = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "123456", Digito = "7" },
                Destino = new RequisicaoDeContaCorrente() { Agencia = "0001", Numero = "654321", Digito = "0" }
            };


            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "123456"))).Returns(new ContaCorrente("0001", "123456", "7") { Id = Guid.NewGuid().ToString(), Saldo = 500M });
            _contaCorrenteRepositorio.Setup(x => x.Obter(It.Is<ContaCorrente>(x => x.Numero == "654321"))).Returns(new ContaCorrente("0001", "654321", "0") { Id = Guid.NewGuid().ToString(), Saldo = 200M });

            _contaCorrenteRepositorio.Setup(x => x.AtualizarSaldo(It.Is<ContaCorrente>(x => x.Numero == "123456")));
            _contaCorrenteRepositorio.Setup(x => x.AtualizarSaldo(It.Is<ContaCorrente>(x => x.Numero == "654321")));

            var response = _servicoTransacao.Efetuar(requisicaoTransacao);
            Assert.True(response.Sucesso);
        }
    }

}
