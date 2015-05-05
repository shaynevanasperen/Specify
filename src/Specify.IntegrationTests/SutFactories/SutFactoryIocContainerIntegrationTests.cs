﻿using Specify.Autofac;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.SutFactories
{
    public class SutFactoryIocContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var container = new AutofacContainer();
            container.Register<IDependency1, Dependency1>();
            container.Register<IDependency2, Dependency2>();
            container.Register<ConcreteObjectWithMultipleConstructors>();
            container.Register<ConcreteObjectWithNoConstructor>();
            return new SutFactory<T>(container);
        }
    }
}