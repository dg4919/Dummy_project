using Autofac;
using System.Linq;
using System.Reflection;

namespace MyProject.Api.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var d = builder.RegisterAssemblyTypes(Assembly.Load("MyProject.Data"));

            builder.RegisterAssemblyTypes(Assembly.Load("MyProject.Data"))
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();
        }
    }
}