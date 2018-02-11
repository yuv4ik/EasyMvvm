using System;

namespace EasyMvvm.Core.Services
{
    public interface ITypeMapperService
    {
        Type MapViewModelToView(Type viewModelType);
        Type MapViewToViewModel(Type vType);
    }
}