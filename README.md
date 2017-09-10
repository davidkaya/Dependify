# Dependify

[![Build Status](https://travis-ci.org/davidkaya/Dependify.svg?branch=master)](https://travis-ci.org/davidkaya/Dependify) [![Version](http://img.shields.io/nuget/vpre/Dependify.svg)](http://nuget.org/packages/Dependify)

Library that allows the developer to register services by adding attributes to class or factory methods. Library works only with Microsoft's `Microsoft.Extensions.DependencyInjection` package.

Why the name? Because I wanted to contribute to the following [list](http://www.thenameinspector.com/wp-content/uploads/ify-names-chart-20141.pdf).

# Installation

`dotnet add package Dependify --version 1.0.0-*` or search using NuGet Package Manager.

# Requirements

* `.netstandard 2.0`
* Currently works only with `IServiceCollection` from `Microsoft.Extensions.DependencyInjection`

# Usage

See [Wiki](https://github.com/davidkaya/Dependify/wiki/Usage) for usage instructions.
