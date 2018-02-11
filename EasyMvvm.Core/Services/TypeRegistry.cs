using System;
using System.Collections.Generic;
using Autofac;

namespace EasyMvvm.Core.Services
{
    public static class TypeRegistry
    {
        static IContainer container;

        public static void Init(IList<Module> modules)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<ModalNavigationService>().As<IModalNavigationService>().SingleInstance();
            builder.RegisterType<PopUpsService>().As<IPopUpsService>().SingleInstance();
            builder.RegisterType<TypeMapperService>().As<ITypeMapperService>().SingleInstance();

            foreach (var mod in modules)
                builder.RegisterModule(mod);

            container = builder.Build();
        }

        public static T Resolve<T>() => container.Resolve<T>();
        public static object Resolve(Type type) => container.Resolve(type);
    }
}
