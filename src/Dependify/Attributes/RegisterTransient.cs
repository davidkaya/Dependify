using System;

namespace Dependify.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterTransient : Register {
        public RegisterTransient() { }

        public RegisterTransient(params Type[] interfaceTypes) : base(interfaceTypes) { }
    }
}