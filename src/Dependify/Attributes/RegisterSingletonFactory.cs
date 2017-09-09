using System;

namespace Dependify.Attributes {
    [AttributeUsage(AttributeTargets.Method)]
    public class RegisterSingletonFactory : RegisterFactory {
        public RegisterSingletonFactory(Type returnType) : base(returnType) { }
    }
}