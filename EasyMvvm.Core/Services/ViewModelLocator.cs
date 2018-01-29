using System;
using System.Collections.Generic;
using Autofac;

namespace EasyMvvm.Core.Services
{
    public static class ViewModelLocator
    {
        static IContainer container;

        public static void Init(IList<Module> modules)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NavigationService>().As<INavigationService>();

            foreach (var mod in modules)
                builder.RegisterModule(mod);

             container = builder.Build();
        }

        public static T Resolve<T>() => container.Resolve<T>();
        public static object Resolve(Type type) => container.Resolve(type);
    }
}
