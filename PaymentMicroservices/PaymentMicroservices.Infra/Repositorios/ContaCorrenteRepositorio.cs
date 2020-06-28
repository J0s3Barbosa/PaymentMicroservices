using Dapper;
using PaymentMicroservices.Application.Repositorios.Interfaces;
using PaymentMicroservices.Domain.Modelo;
using System.Data;
using System.Linq;
using System.Text;


namespace PaymentMicroservices.Infra.Repositorios
{
    public class ContaCorrenteRepositorio : Repositorio, IContaCorrenteRepositorio
    {
        public ContaCorrenteRepositorio(IDbConnection connection) : base(connection)
        {
        }

       
        public CommandDefinition ConstrutorComandoSelecao()
        {
            var commandText = new StringBuilder();

            commandText.AppendLine("SELECT ");
            commandText.AppendLine("CC.ID, ");
            commandText.AppendLine("CC.Agencia,");
            commandText.AppendLine("CC.Numero,");
            commandText.AppendLine("CC.Digito,");
            commandText.AppendLine("CC.Saldo");
            commandText.AppendLine("FROM ContaCorrente CC");
            commandText.AppendLine("WHERE 1=1 ");

            return new CommandDefinition(commandText.ToString());
        }

        public CommandDefinition ConstrutorComandoAtualizacaoSaldo(ContaCorrente entidade)
        {
            var commandText = new StringBuilder();
            commandText.AppendLine(@"UPDATE ContaCorrente");
            commandText.AppendLine(@"SET");
            commandText.AppendLine(@"Saldo = @saldo");
            commandText.AppendLine(@"Where Id = @id");

            return new CommandDefinition(commandText.ToString(), new
            {
                id = entidade.Id,
                saldo = entidade.Saldo
            });
        }


        public void AtualizarSaldo(ContaCorrente entidade)
        {
            var command = ConstrutorComandoAtualizacaoSaldo(entidade);
            this.Connection.Execute(command);
        }

        public ContaCorrente Obter(ContaCorrente entidade)
        {
            var comando = ConstrutorComandoSelecao();
            var sql = new StringBuilder(comando.CommandText);

            sql.AppendLine("AND CC.Agencia = @agencia");
            sql.AppendLine("AND CC.Numero = @numero");
            sql.AppendLine("AND (@digito is null OR CC.Digito = @digito)");

            var result = this.Connection.Query<ContaCorrente>
                (sql.ToString(), new { agencia = entidade.Agencia, numero = entidade.Numero, digito = entidade.Digito }).FirstOrDefault();

            return result;
        }
    }
}
