﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Autofac;
using Xamarin.Forms;

namespace EasyMvvm.Core.Services
{
    public static class ViewModelLocator
    {

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable) =>
            (bool)bindable.GetValue(AutoWireViewModelProperty);

        public static void SetAutoWireViewModel(BindableObject bindable, bool value) =>
            bindable.SetValue(AutoWireViewModelProperty, value);

        static IContainer container;

        public static void Init(IList<Autofac.Module> modules)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NavigationService>().As<INavigationService>();

            foreach (var mod in modules)
                builder.RegisterModule(mod);

             container = builder.Build();
        }

        public static T Resolve<T>() => container.Resolve<T>();
        public static object Resolve(Type type) => container.Resolve(type);

        static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
