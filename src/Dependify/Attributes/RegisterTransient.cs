// Copyright 2017 Dávid Kaya. All rights reserved.
// Use of this source code is governed by the MIT license,
// as found in the LICENSE file.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Dependify.Attributes {
    /// <summary>
    /// Classes with this attribute will be registered with <see cref="ServiceLifetime.Transient"/> lifetime.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterTransient : Register {
        public RegisterTransient() { }

        public RegisterTransient(params Type[] interfaceTypes) : base(interfaceTypes) { }
    }
}