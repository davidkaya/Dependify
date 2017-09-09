using System;

namespace Dependify.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterSingleton : Register {
        public RegisterSingleton() { }

        public RegisterSingleton(params Type[] interfaceTypes) : base(interfaceTypes) { }
    }
}