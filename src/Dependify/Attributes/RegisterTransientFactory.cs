using System;

namespace Dependify.Attributes {
    [AttributeUsage(AttributeTargets.Method)]
    public class RegisterTransientFactory : RegisterFactory {
        public RegisterTransientFactory(Type returnType) : base(returnType) { }
    }
}