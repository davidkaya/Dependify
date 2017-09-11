// Copyright 2017 Dávid Kaya. All rights reserved.
// Use of this source code is governed by the MIT license,
// as found in the LICENSE file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dependify.Attributes;
using Dependify.Extensions;
using Dependify.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Dependify {
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions {
        /// <summary>
        /// Adds all classes and factory methods with <see cref="RegisterScoped"/>,
        /// <see cref="RegisterSingleton"/> or <see cref="RegisterTransient"/> attribute for classes
        /// and <see cref="RegisterScopedFactory"/>, <see cref="RegisterSingletonFactory"/> or <see cref="RegisterTransientFactory"/> for
        /// factory methods to <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AutoRegister(this IServiceCollection services) {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return services.AutoRegister(assemblies);
        }

        /// <summary>
        /// Adds all classes and factory methods from specified <paramref name="assemblies"/> with <see cref="RegisterScoped"/>,
        /// <see cref="RegisterSingleton"/> or <see cref="RegisterTransient"/> attribute for classes
        /// and <see cref="RegisterScopedFactory"/>, <see cref="RegisterSingletonFactory"/> or <see cref="RegisterTransientFactory"/> for
        /// factory methods to <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="assemblies">Assemblies to scan</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AutoRegister(this IServiceCollection services, params Assembly[] assemblies) {
            services.AddFactories(DependifyUtils.GetFactoryMethods(assemblies));
            services.AddClasses(DependifyUtils.GetClassTypes(assemblies));
            return services;
        }

        /// <summary>
        /// Adds all classes and factory methods, from namespaces that start with <paramref name="namespace"/>, with <see cref="RegisterScoped"/>,
        /// <see cref="RegisterSingleton"/> or <see cref="RegisterTransient"/> attribute for classes
        /// and <see cref="RegisterScopedFactory"/>, <see cref="RegisterSingletonFactory"/> or <see cref="RegisterTransientFactory"/> for
        /// factory methods to <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="namespace">Scanned namespace.</param>
        /// <returns></returns>
        public static IServiceCollection AutoRegister(this IServiceCollection services, string @namespace) {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddFactories(DependifyUtils.GetFactoryMethodsFromNamespace(assemblies, @namespace));
            services.AddClasses(DependifyUtils.GetClassTypesFromNamespace(assemblies, @namespace));
            return services;
        }

        /// <summary>
        /// Adds all classes and factory methods resolved from <paramref name="assemblyResolver"/> with <see cref="RegisterScoped"/>,
        /// <see cref="RegisterSingleton"/> or <see cref="RegisterTransient"/> attribute for classes
        /// and <see cref="RegisterScopedFactory"/>, <see cref="RegisterSingletonFactory"/> or <see cref="RegisterTransientFactory"/> for
        /// factory methods to <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="assemblyResolver">Assembly resolver.</param>
        /// <returns></returns>
        public static IServiceCollection AutoRegister(this IServiceCollection services, IAssemblyResolver assemblyResolver) {
            return services.AutoRegister(assemblyResolver.Resolve().ToArray());
        }

        private static IServiceCollection AddFactories(this IServiceCollection services, IEnumerable<MethodInfo> methodInfos) {
            foreach (var methodInfo in methodInfos) {
                var factoryAttribute = methodInfo.GetCustomAttributes<RegisterFactory>(true).First();
                var factoryReturnType = factoryAttribute.ReturnType;
                var factoryLifetime = factoryAttribute.MapToLifetime();
                services.AddFactoryMethods(factoryLifetime, factoryReturnType, methodInfo);
            }
            return services;
        }

        private static IServiceCollection AddFactoryMethods(this IServiceCollection services, ServiceLifetime factoryLifetime, Type factoryReturnType, MethodInfo methodInfo) {
            switch (factoryLifetime) {
                case ServiceLifetime.Singleton :
                    return services.AddSingleton(factoryReturnType, DependifyUtils.GetFactoryMethod(methodInfo));
                case ServiceLifetime.Scoped :
                    return services.AddScoped(factoryReturnType, DependifyUtils.GetFactoryMethod(methodInfo));
                case ServiceLifetime.Transient :
                    return services.AddTransient(factoryReturnType, DependifyUtils.GetFactoryMethod(methodInfo));
                default :
                    throw new ArgumentOutOfRangeException($"{factoryLifetime} is not supported.");
            }
        }

        private static IServiceCollection AddClasses(this IServiceCollection services, IEnumerable<Type> classTypes) {
            foreach (var classType in classTypes) {
                var classAttributes = classType.GetCustomAttributes<Register>(true);
                foreach (var classAttribute in classAttributes) {
                    var classLifetime = classAttribute.MapToLifetime();
                    if (classAttribute.InterfaceTypes?.Any() ?? false)
                        services.AddInterfaceImplementations(classLifetime, classAttribute.InterfaceTypes, classType);
                    else if (classType.GetInterfaces().Any())
                        services.AddInterfaceImplementations(classLifetime, classType.GetInterfaces(), classType);
                    else
                        services.AddClassImplementation(classLifetime, classType);
                }
            }
            return services;
        }

        private static IServiceCollection AddInterfaceImplementations(this IServiceCollection services, ServiceLifetime serviceLifetime, IEnumerable<Type> interfaceTypes, Type classType) {
            switch (serviceLifetime) {
                case ServiceLifetime.Singleton :
                    interfaceTypes.ForEach(interfaceType => services.AddSingleton(interfaceType, classType));
                    return services;
                case ServiceLifetime.Scoped :
                    interfaceTypes.ForEach(interfaceType => services.AddScoped(interfaceType, classType));
                    return services;
                case ServiceLifetime.Transient :
                    interfaceTypes.ForEach(interfaceType => services.AddTransient(interfaceType, classType));
                    return services;
                default :
                    throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null);
            }
        }

        private static IServiceCollection AddClassImplementation(this IServiceCollection services, ServiceLifetime serviceLifetime, Type classType) {
            switch (serviceLifetime) {
                case ServiceLifetime.Singleton :
                    return services.AddSingleton(classType);
                case ServiceLifetime.Scoped :
                    return services.AddScoped(classType);
                case ServiceLifetime.Transient :
                    return services.AddTransient(classType);
                default :
                    throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null);
            }
        }
    }
}