using System;

namespace Dependify.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterScoped : Register {
        public RegisterScoped() { }

        public RegisterScoped(params Type[] interfaceTypes) : base(interfaceTypes) { }
    }
}