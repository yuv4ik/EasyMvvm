# EasyMvvm

Small framework that simplifies MVVM on Xamarin.Forms (in development).

## Installation
TODO: Publish to NuGet

## Features

* IOC container ([Autofac](http://autofaccn.readthedocs.io/en/latest/index.html))
* ViewModel first navigation with optional parameters. Please note that currently only [Hierarchical Navigation](https://developer.xamarin.com/guides/xamarin-forms/application-fundamentals/navigation/hierarchical/) is supported.
* BaseViewModel with implemented INotifyPropertyChanged (just add [Fody](https://github.com/Fody/PropertyChanged))

### Prerequisites

By convention all the Views should be in View directory and file names should end with View: 
```
\Views\MainView.cs
```
 All the ViewModels should be in ViewModels directory, should extend EasyMvvm.Core.BaseViewModel, should end with ViewModel and should match the View name:
```
\ViewModels\MainViewModel.cs
```

This way EasyMvvm will be able to match the View by ViewModelName.
### Usage

Simply extend 'EasyMvvm.Core.BaseApplication':
```
public partial class App : EasyMvvm.Core.BaseApplication
{
    // IoC Modules are registered automatically OnStart()
    protected sealed override IList<Module> IocModules { get; }

    public App()
    {
        InitializeComponent();

        IocModules = new List<Module>
        {
            new ViewModelsModule()
        };
    }

    protected async override void OnStart()
    {
        base.OnStart();
        // Set the first page
        var navigation = ViewModelLocator.Resolve<INavigationService>();
        await navigation.PushAsync<FirstViewModel>();
    }
}
```
Example of an IOC Module:
```
public class ViewModelsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<FirstViewModel>().AsSelf();
    }
}
```
Example of a ViewModel:
```
public class FirstViewModel : EasyMvvm.Core.BaseViewModel
{
    public ICommand NavigateToMainPageCmd { get; }

    public FirstViewModel()
    {
        // INavigationService is available from the parent
        NavigateToMainPageCmd = new Command(async () => await NavigationService.PushAsync<MainViewModel>("Hello world!"));
    }
}
```

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
