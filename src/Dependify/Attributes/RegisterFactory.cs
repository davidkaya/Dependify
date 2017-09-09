using System;

namespace Dependify.Attributes {
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class RegisterFactory : Attribute {
        public Type ReturnType { get; }

        protected RegisterFactory(Type returnType) {
            ReturnType = returnType;
        }
    }
}