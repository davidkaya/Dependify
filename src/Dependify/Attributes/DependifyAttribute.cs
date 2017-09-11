// Copyright 2017 Dávid Kaya. All rights reserved.
// Use of this source code is governed by the MIT license,
// as found in the LICENSE file.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Dependify.Attributes {
    public abstract class DependifyAttribute : Attribute {
        public ServiceLifetime MapToLifetime() {
            switch (this) {
                case RegisterTransient _ :
                case RegisterTransientFactory _ :
                    return ServiceLifetime.Transient;
                case RegisterScoped _ :
                case RegisterScopedFactory _ :
                    return ServiceLifetime.Scoped;
                case RegisterSingleton _ :
                case RegisterSingletonFactory _ :
                    return ServiceLifetime.Singleton;
                default :
                    throw new ArgumentOutOfRangeException($"{GetType()} is not supported");
            }
        }
    }
}