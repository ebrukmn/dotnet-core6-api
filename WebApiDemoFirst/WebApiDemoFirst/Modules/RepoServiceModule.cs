using System.Reflection;
using Autofac;
using NLayerCaching;
using NLayerCore.Repositories;
using NLayerCore.Services;
using NLayerCore.UnitOfWorks;
using NLayerRepository;
using NLayerRepository.Repositories;
using NLayerRepository.UnitOfWorks;
using NLayerService.Mapping;
using NLayerService.Services;
using Module = Autofac.Module;

namespace WebApiDemoFirst.Modules;

public class RepoServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
        builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>));
        builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork));

        var apiAssembly = Assembly.GetExecutingAssembly();
        var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
        var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

        //Sonu Repository ve Service ile biten injectionları AutoFac ile otomatik register ettik..
        builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

        builder.RegisterType(typeof(ProductServiceWithCaching)).As(typeof(IProductService));
    }
}