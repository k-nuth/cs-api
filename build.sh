#!/usr/bin/env bash

#Exit if any command fails
set -e

#Build webtest project (this build bitprim-cs class library as a dependency)
cd webtest
dotnet restore webtest.csproj
dotnet build webtest.csproj -c Release

#Run tests
cd ..
#Copy native dependencies
cp bitprim/* ./bitprim-cs.tests/bin/Release/netcoreapp1.1 
cd bitprim-cs.tests
dotnet restore
dotnet test