using System;
using EasyMvvm.Core.Services;
using NUnit.Framework;

namespace EasyMvvm.UnitTests.Services
{
    [TestFixture()]
    public class TypeRegistryTests
    {
        [Test()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ResolveWithoutCallingInit()
        {
            // Arrange

            // Act
            TypeRegistry.Resolve<INavigationService>();

            // Assert
        }
    }
}
