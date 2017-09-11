// Copyright 2017 Dávid Kaya. All rights reserved.
// Use of this source code is governed by the MIT license,
// as found in the LICENSE file.

using System;

namespace Dependify.Attributes {
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class RegisterFactory : DependifyAttribute {
        public Type ReturnType { get; }

        protected RegisterFactory(Type returnType) {
            ReturnType = returnType;
        }
    }
}