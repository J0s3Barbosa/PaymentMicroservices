using Autofac.Builder;
using Autofac.Core;
using System.Reflection;

namespace PaymentMicroservices.Api.DI
{
    public class InitializeOptions
    {
        public static InitializeOptions Default { get; } = new InitializeOptions(new[] { typeof(InitializeOptions).GetTypeInfo().Assembly }, ContainerBuildOptions.None);
        public Assembly[] AssembliesToScan { get; private set; }
        public IModule[] Modules { get; private set; }
        public ContainerBuildOptions BuildOptions { get; private set; }

        public InitializeOptions(Assembly[] collection, ContainerBuildOptions buildOptions) : this(collection, new IModule[0], buildOptions)
        {
            this.AssembliesToScan = collection;
            this.BuildOptions = buildOptions;
        }

        public InitializeOptions(Assembly[] collection, IModule[] modules, ContainerBuildOptions buildOptions)
        {
            this.AssembliesToScan = collection;
            this.Modules = modules;
            this.BuildOptions = buildOptions;
        }
    }
}
