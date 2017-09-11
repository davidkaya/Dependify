// Copyright 2017 Dávid Kaya. All rights reserved.
// Use of this source code is governed by the MIT license,
// as found in the LICENSE file.

using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ShouldRegisterOneScoped;
using ShouldRegisterOneSingleton;
using ShouldRegisterOneTransient;
using ShouldRegisterScoped;
using ShouldRegisterScopedWithoutInterface;
using ShouldRegisterSingleton;
using ShouldRegisterSingletonWithoutInterface;
using ShouldRegisterTransient;
using ShouldRegisterTransientWithoutInterface;

namespace Dependify.Test {
    [TestFixture]
    public class RegisterAttributeTests {
        [TestCase(nameof(ShouldRegisterTransient), typeof(ImplementationTransient), typeof(IInterface), ServiceLifetime.Transient)]
        [TestCase(nameof(ShouldRegisterSingleton), typeof(ImplementationSingleton), typeof(IInterface), ServiceLifetime.Singleton)]
        [TestCase(nameof(ShouldRegisterScoped), typeof(ImplementationScoped), typeof(IInterface), ServiceLifetime.Scoped)]
        [TestCase(nameof(ShouldRegisterTransientWithoutInterface), typeof(ImplementationTransientWithoutInterface), typeof(ImplementationTransientWithoutInterface), ServiceLifetime.Transient)]
        [TestCase(nameof(ShouldRegisterSingletonWithoutInterface), typeof(ImplementationSingletonWithoutInterface), typeof(ImplementationSingletonWithoutInterface), ServiceLifetime.Singleton)]
        [TestCase(nameof(ShouldRegisterScopedWithoutInterface), typeof(ImplementationScopedWithoutInterface), typeof(ImplementationScopedWithoutInterface), ServiceLifetime.Scoped)]
        [TestCase(nameof(ShouldRegisterOneTransient), typeof(ImplementationTransientOneInterface), typeof(IInterface2), ServiceLifetime.Transient)]
        [TestCase(nameof(ShouldRegisterOneSingleton), typeof(ImplementationSingletonOneInterface), typeof(IInterface2), ServiceLifetime.Singleton)]
        [TestCase(nameof(ShouldRegisterOneScoped), typeof(ImplementationScopedOneInterface), typeof(IInterface2), ServiceLifetime.Scoped)]
        public void RegisterAttribute_RegistersClass_WhenDefined(string @namespace, Type classType, Type interfaceType, ServiceLifetime serviceLifetime) {
            IServiceCollection services = new ServiceCollection();
            services.AutoRegister(@namespace);
            var service = services.First();
            Assert.AreEqual(1, services.Count);
            Assert.AreEqual(interfaceType, service.ServiceType);
            Assert.AreEqual(classType, service.ImplementationType);
            Assert.AreEqual(serviceLifetime, service.Lifetime);
        }

        [TestCase(nameof(ShouldRegisterFactoryTransient), typeof(ImplementationTransient), typeof(IInterface), ServiceLifetime.Transient)]
        [TestCase(nameof(ShouldRegisterFactorySingleton), typeof(ImplementationSingleton), typeof(IInterface), ServiceLifetime.Singleton)]
        [TestCase(nameof(ShouldRegisterFactoryScoped), typeof(ImplementationScoped), typeof(IInterface), ServiceLifetime.Scoped)]
        public void RegisterFactoryAttribute_RegistersFactoryForInterface_WhenDefined(string @namespace, Type classType, Type interfaceType, ServiceLifetime serviceLifetime) {
            IServiceCollection services = new ServiceCollection();
            services.AutoRegister(@namespace);
            var service = services.First();
            Assert.AreEqual(1, services.Count);
            Assert.AreEqual(interfaceType, service.ServiceType);
            Assert.NotNull(service.ImplementationFactory);
            Assert.AreEqual(serviceLifetime, service.Lifetime);
        }
    }
}