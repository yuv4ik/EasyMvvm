using NUnit.Framework;
using EasyMvvm.UnitTests.Views;
using EasyMvvm.Core.Services;
using EasyMvvm.UnitTests.ViewModels;
using System;

namespace EasyMvvm.UnitTests
{
    [TestFixture()]
    public class TypeMapperServiceTests
    {
        [Test()]
        public void MapViewModelToView()
        {
            // Arrange
            var vm = new TestViewModel();
            var typeMapperService = new TypeMapperService();

            // Act
            var viewType = typeMapperService.MapViewModelToView(vm.GetType());

            // Assert
            Assert.AreEqual(typeof(TestView), viewType);
        }

        [Test()]
        public void MapViewModelToMissingView()
        {
            // Arrange
            var vm = new TestViewModelWithoutV();
            var typeMapperService = new TypeMapperService();

            // Act
            var v = typeMapperService.MapViewModelToView(vm.GetType());

            // Assert
            Assert.IsNull(v);
        }

        [Test()]
        [ExpectedException(typeof(NullReferenceException))]
        public void MapNullViewModelToView()
        {
            // Arrange
            var typeMapperService = new TypeMapperService();

            // Act
            typeMapperService.MapViewModelToView(null);

            // Assert
        }

        [Test()]
        public void MapViewToViewModel()
        {
            // Arrange
            var v = new TestView();
            var typeMapperService = new TypeMapperService();

            // Act
            var viewModelType = typeMapperService.MapViewToViewModel(v.GetType());

            // Assert
            Assert.AreEqual(typeof(TestViewModel), viewModelType);
        }

        [Test()]
        public void MapViewToMissingViewModel()
        {
            // Arrange
            var v = new TestViewWithoutVM();
            var typeMapperService = new TypeMapperService();

            // Act
            var vm = typeMapperService.MapViewToViewModel(v.GetType());

            // Assert
            Assert.IsNull(vm);
        }

        [Test()]
        [ExpectedException(typeof(NullReferenceException))]
        public void MapNullViewToViewModel()
        {
            // Arrange
            var typeMapperService = new TypeMapperService();

            // Act
            typeMapperService.MapViewToViewModel(null);

            // Assert
        }
    }
}

namespace EasyMvvm.UnitTests.Views
{
    class TestView { }
    class TestViewWithoutVM { }
}

namespace EasyMvvm.UnitTests.ViewModels
{
    class TestViewModel { }
    class TestViewModelWithoutV { }
}