using System;

namespace Dependify.Attributes {
    [AttributeUsage(AttributeTargets.Method)]
    public class RegisterScopedFactory : RegisterFactory {
        public RegisterScopedFactory(Type returnType) : base(returnType) { }
    }
}