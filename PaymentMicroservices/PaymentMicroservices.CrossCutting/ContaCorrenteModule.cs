using Autofac;
using MySql.Data.MySqlClient;
using PaymentMicroservices.Application.Repositorios.Interfaces;
using PaymentMicroservices.Application.Transacao;
using PaymentMicroservices.Infra.Modulo;
using PaymentMicroservices.Infra.Repositorios;

namespace DBserver.CrossCutting
{
    public class ContaCorrenteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterInfrastructure(builder);
            RegisterServices(builder);
            RegisterRepositories(builder);
            base.Load(builder);
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {

            builder.Register(ctx => new ContaCorrenteRepositorio(ctx.Resolve<MySqlConnection>()))
               .As<IContaCorrenteRepositorio>()
               .InstancePerLifetimeScope()
               .AsSelf();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ServicoTransacao>().As<IServicoTransacao>()
             .InstancePerLifetimeScope()
             .AutoActivate();
            builder.RegisterType<ServicoMovimentacao>().As<IServicoMovimentacao>()
            .InstancePerLifetimeScope()
            .AutoActivate();         

        }

        private static void RegisterInfrastructure(ContainerBuilder builder)
        {
            builder.RegisterModule<ModuloConexaoMySQL>();

            builder.Register(ctx => new Repositorio(ctx.Resolve<MySqlConnection>()))
                .As<Repositorio>()
                .AsSelf();
        }


    }
}
