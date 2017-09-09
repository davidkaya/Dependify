using System;
using System.Collections.Generic;

namespace Dependify.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class Register : Attribute {
        public IEnumerable<Type> InterfaceTypes { get; }

        protected Register() { }

        protected Register(params Type[] interfaceTypes) {
            InterfaceTypes = interfaceTypes;
        }
    }
}