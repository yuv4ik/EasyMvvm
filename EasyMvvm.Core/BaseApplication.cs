using System.Collections.Generic;
using Autofac;
using EasyMvvm.Core.Services;
using Xamarin.Forms;

namespace EasyMvvm.Core
{
    public abstract class BaseApplication : Application
    {
        protected abstract IList<Module> IocModules { get; }

        protected override void OnStart()
        {
            base.OnStart();
            TypeRegistry.Init(IocModules);
        }
    }
}
