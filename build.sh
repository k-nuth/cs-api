#!/usr/bin/env bash

#Run tests
cd bitprim.tests
dotnet restore
dotnet test /property:Platform=x64 -f netcoreapp2.0
