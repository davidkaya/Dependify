using System;
using Dependify;
using Dependify.Attributes;
using ShouldRegisterScoped;
using ShouldRegisterSingleton;
using ShouldRegisterTransient;

public interface IInterface { }
    
public interface IInterface2 { }  

namespace ShouldRegisterTransient {
    [RegisterTransient]
    public class ImplementationTransient : IInterface { }
}

namespace ShouldRegisterScoped {
    [RegisterScoped]
    public class ImplementationScoped : IInterface { }
}

namespace ShouldRegisterSingleton {
    [RegisterSingleton]
    public class ImplementationSingleton : IInterface { }
}

namespace ShouldRegisterOneTransient {
    [RegisterTransient(typeof(IInterface2))]
    public class ImplementationTransientOneInterface : IInterface, IInterface2 { }
}

namespace ShouldRegisterOneScoped {
    [RegisterScoped(typeof(IInterface2))]
    public class ImplementationScopedOneInterface : IInterface, IInterface2 { }
}

namespace ShouldRegisterOneSingleton {
    [RegisterSingleton(typeof(IInterface2))]
    public class ImplementationSingletonOneInterface : IInterface, IInterface2 { }
}

namespace ShouldRegisterFactoryTransient {
    public class TransientFactory {
        [RegisterTransientFactory(typeof(IInterface))]
        public static ImplementationTransient CreateImplementationTransient(IServiceProvider provider) {
            return new ImplementationTransient();
        }
    }
}

namespace ShouldRegisterFactoryScoped {
    public class ScopedFactory {
        [RegisterScopedFactory(typeof(IInterface))]
        public static ImplementationScoped CreateImplementationTransient(IServiceProvider provider) {
            return new ImplementationScoped();
        }
    }
}

namespace ShouldRegisterFactorySingleton {
    public class SingletonFactory {
        [RegisterSingletonFactory(typeof(IInterface))]
        public static ImplementationSingleton CreateImplementationTransient(IServiceProvider provider) {
            return new ImplementationSingleton();
        }
    }
}