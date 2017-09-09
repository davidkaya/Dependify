# Dependify

[![Build Status](https://travis-ci.org/davidkaya/Dependify.svg?branch=master)](https://travis-ci.org/davidkaya/Dependify)

Version: _1.0.0-beta_

Library that allows the developer to register his services by adding attributes to class or factory methods. Library works only with Microsoft's `Microsoft.Extensions.DependencyInjection` package.

Why the name? Because I wanted to contribute to the following [list](http://www.thenameinspector.com/wp-content/uploads/ify-names-chart-20141.pdf).

# Installation

`dotnet add package Dependify --version 1.0.0-beta` or search using NuGet Package Manager.

# Requirements

* `.netstandard 2.0`
* `Microsoft.Extensions.DependencyInjection`

# Usage

Let's have following interface and class
```c#
public interface ICar {

}

public class Audi : ICar {

}
```

## Class Attributes

### Single interface 

If you want to add `Audi` as an implementation of `ICar` into `IServiceCollection` you just need to add `RegisterTransient`, `RegisterScoped` or `RegisterSingleton` attribute like

```c#
[RegisterTransient]
public class Audi : ICar {

}
```

and call 
```c#
public void ConfigureServices(IServiceCollection services) {
    services.AutoRegister(); 
}
```

### Multiple interfaces

If your class implements multiple interfaces, you can specify which one should be registered

```c#
[RegisterTransient(typeof(IFuelConsumer)]
public class Audi : ICar, IFuelConsumer {

}
```

## Factory method attributes

If you want to add `Audi` as an implementation of `ICar` using factory method, you can do following
```c#
[RegisterTransientFactory(typeof(ICar))]
public Audi CreateAudi(IServiceProvider provider) {
    return new Audi();
}
```
