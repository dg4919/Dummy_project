using Autofac;
using MyProject.Context;

namespace MyProject.Api.Modules
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(dbContext)).As(typeof(dbContext)).InstancePerLifetimeScope();
        }
    }
}