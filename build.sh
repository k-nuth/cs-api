#!/usr/bin/env bash

#Run tests
cd bitprim.tests
dotnet build /property:Platform=x64 -c Release -f netcoreapp2.0 -v normal
dotnet test /property:Platform=x64 -c Release -f netcoreapp2.0 -v normal
