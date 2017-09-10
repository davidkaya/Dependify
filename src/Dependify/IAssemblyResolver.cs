using System.Collections.Generic;
using System.Reflection;
using Dependify.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Dependify {
    /// <summary>
    /// Specifies a resolver that knows how to locate assemblies that will be scanned for registration in <see cref="IServiceCollection"/>.
    /// </summary>
    public interface IAssemblyResolver {
        /// <summary>
        /// Returns assemblies that will be scanned for <see cref="Register"/> and <see cref="RegisterFactory"/> attributes.
        /// </summary>
        /// <returns>Collection of assemblies.</returns>
        IEnumerable<Assembly> Resolve();
    }
}