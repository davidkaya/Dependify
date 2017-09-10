// Copyright 2017 Dávid Kaya. All rights reserved.
// Use of this source code is governed by the MIT license,
// as found in the LICENSE file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dependify.Attributes;

namespace Dependify.Utilities {
    internal static class DependifyUtils {
        internal static IEnumerable<MethodInfo> GetFactoryMethods(IEnumerable<Assembly> assemblies) {
            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass)
                .SelectMany(MethodsWithAttribute);
        }

        internal static IEnumerable<MethodInfo> GetFactoryMethodsFromNamespace(IEnumerable<Assembly> assemblies, string @namespace) {
            return assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && (type.Namespace?.StartsWith(@namespace)).GetValueOrDefault())
                .SelectMany(MethodsWithAttribute);
        }

        private static IEnumerable<MethodInfo> MethodsWithAttribute(Type type) {
            return type.GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(methodInfo => methodInfo.GetCustomAttributes<RegisterFactory>(true).Any());
        }

        internal static Func<IServiceProvider, object> GetFactoryMethod(MethodInfo factoryMethodInfo) {
            if (!IsFactoryMethod(factoryMethodInfo))
                throw new ArgumentException($"{factoryMethodInfo.Name} is not a factory method. ");
            return (Func<IServiceProvider, object>)Delegate.CreateDelegate(typeof(Func<IServiceProvider, object>), factoryMethodInfo);

            bool IsFactoryMethod(MethodInfo methodInfo) {
                var methodParameters = methodInfo.GetParameters();
                return methodParameters.Length == 1 && methodParameters.First().ParameterType == typeof(IServiceProvider);
            }
        }

        internal static IEnumerable<Type> GetClassTypes(IEnumerable<Assembly> assemblies) {
            return assemblies.SelectMany(assembly => assembly.GetTypes()).Where(type => type.IsClass);
        }

        internal static IEnumerable<Type> GetClassTypesFromNamespace(IEnumerable<Assembly> assemblies, string @namespace) {
            return assemblies.SelectMany(assembly => assembly.GetTypes()).Where(type => type.IsClass && BelongsToNamespace(type.Namespace, @namespace));
        }

        private static bool BelongsToNamespace(string testedNamespace, string parentNamespace)
            => testedNamespace != null &&
                (testedNamespace == parentNamespace || testedNamespace.StartsWith(parentNamespace + "."));
    }
}