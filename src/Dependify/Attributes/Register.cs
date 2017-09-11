// Copyright 2017 Dávid Kaya. All rights reserved.
// Use of this source code is governed by the MIT license,
// as found in the LICENSE file.

using System;
using System.Collections.Generic;

namespace Dependify.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class Register : DependifyAttribute {
        public IEnumerable<Type> InterfaceTypes { get; }

        protected Register() { }

        protected Register(params Type[] interfaceTypes) {
            InterfaceTypes = interfaceTypes;
        }
    }
}