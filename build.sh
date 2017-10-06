#!/usr/bin/env bash

#Exit if any command fails
set -e

#Build bitprim-cs class library
cd bitprim-cs
dotnet restore
dotnet build bitprim-cs.csproj
#TODO: Publish library to nuget.org

#Build webtest project
cd ..
cd webtest
dotnet restore
dotnet build webtest.csproj

#TODO Run tests