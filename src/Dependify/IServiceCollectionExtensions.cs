using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dependify.Attributes;
using Dependify.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Dependify {
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions {
        public static IServiceCollection AutoRegister(this IServiceCollection services) {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddFactories(DependifyUtils.GetFactoryMethods(assemblies));
            services.AddClasses(DependifyUtils.GetClassTypes(assemblies));
            return services;
        }

        public static IServiceCollection AutoRegister(this IServiceCollection services, params Assembly[] assemblies) {
            services.AddFactories(DependifyUtils.GetFactoryMethods(assemblies));
            services.AddClasses(DependifyUtils.GetClassTypes(assemblies));
            return services;
        }
        
        public static IServiceCollection AutoRegister(this IServiceCollection services, string @namespace) {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddFactories(DependifyUtils.GetFactoryMethodsFromNamespace(assemblies, @namespace));
            services.AddClasses(DependifyUtils.GetClassTypesFromNamespace(assemblies, @namespace));
            return services;
        }

        internal static IServiceCollection AddFactories(this IServiceCollection services, IEnumerable<MethodInfo> methodInfos) {
            foreach (var methodInfo in methodInfos) {
                var factoryAttribute = methodInfo.GetCustomAttributes<RegisterFactory>(true).First();
                var factoryAttributeType = factoryAttribute.GetType();
                var factoryReturnType = factoryAttribute.ReturnType;

                if (factoryAttributeType == typeof(RegisterTransientFactory))
                    services.AddTransient(factoryReturnType, DependifyUtils.GetFactoryMethod(methodInfo));
                if (factoryAttributeType == typeof(RegisterScopedFactory))
                    services.AddScoped(factoryReturnType, DependifyUtils.GetFactoryMethod(methodInfo));
                if (factoryAttributeType == typeof(RegisterSingletonFactory))
                    services.AddSingleton(factoryReturnType, DependifyUtils.GetFactoryMethod(methodInfo));
            }
            return services;
        }

        internal static void AddClasses(this IServiceCollection services, IEnumerable<Type> classTypes) {
            foreach (var classType in classTypes) {
                var classAttributes = classType.GetCustomAttributes<Register>(true);
                foreach (var classAttribute in classAttributes) {
                    var registrationType = classAttribute.GetType();
                    var interfaceTypes = classAttribute.InterfaceTypes == null || !classAttribute.InterfaceTypes.Any() ? classType.GetInterfaces() : classAttribute.InterfaceTypes;
                    
                    foreach (var interfaceType in interfaceTypes) {
                        if (registrationType == typeof(RegisterTransient))
                            services.AddTransient(interfaceType, classType);
                        if (registrationType == typeof(RegisterScoped))
                            services.AddScoped(interfaceType, classType);
                        if (registrationType == typeof(RegisterSingleton))
                            services.AddSingleton(interfaceType, classType);
                    }
                }
            }
        }
    }
}