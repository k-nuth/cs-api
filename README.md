# Bitprim C# binding

C# binding for the Bitprim API.
Includes blockchain query interface. 

NuGet Bitcoin Cash:
[![NuGet](https://img.shields.io/nuget/v/bitprim-bch.svg)](https://www.nuget.org/packages/bitprim-bch)

NuGet Bitcoin:
[![NuGet](https://img.shields.io/nuget/v/bitprim-btc.svg)](https://www.nuget.org/packages/bitprim-btc)

Travis: [![Build Status](https://travis-ci.org/bitprim/bitprim-cs.svg?branch=dev)](https://travis-ci.org/bitprim/bitprim-cs)

Appveyor: [![Appveyor Status](https://ci.appveyor.com/api/projects/status/github/bitprim/bitprim-cs?branch=dev&svg=true)](https://ci.appveyor.com/project/bitprim/bitprim-cs?branch=dev)

[comment]: # (TODO Implement a test coverage badge)


## Documentation

To view the full documentation, reference and tutorials please go to [https://bitprim.github.io/docfx/content/developer_guide/dotnet/dotnet-Interface.html](https://bitprim.github.io/docfx/content/developer_guide/dotnet/dotnet-Interface.html)

## Installation

You can install the Bitprim C# binding via Nuget.

There are two packages available:

For Bitcoin Cash (BCH) you need to use  https://www.nuget.org/packages/bitprim-bch/ 
and for Bitcoin Legacy (BTC) you need to use https://www.nuget.org/packages/bitprim-btc/

If you use Visual Studio , right click en your proyect, Manage Nuget Packages.

Or

use the package manager.

```
Install-Package bitprim-bch -Version 0.4.2
Install-Package bitprim-btc -Version 0.4.2
```

If you are using dotnet cli:

```
dotnet add package bitprim-bch --version 0.4.2
dotnet add package bitprim-btc --version 0.4.2
```

##Building from source

If you want to build from source, you need the following prerequisites:

* .Net Framework 4.6.1
* .Net Core 2.0
* Powershell

Run the following commands:

```
git clone https://github.com/bitprim/bitprim-cs.git
cd bitprim-cs
powershell ./build.ps1
```

if you have problems running build.ps1 please check this link 
[https://cakebuild.net/docs/tutorials/powershell-security](https://cakebuild.net/docs/tutorials/powershell-security)



##Examples

For a logging integration example see: [Example](https://github.com/bitprim/bitprim-cs/tree/dev/bitprim.console)

