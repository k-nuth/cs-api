#!/usr/bin/env bash

#exit if any command fails
set -e

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then  
  rm -R $artifactsFolder
fi

cd api

dotnet restore

# Ideally we would use the 'dotnet test' command to test netcoreapp and net451 so restrict for now 
# but this currently doesn't work due to https://github.com/dotnet/cli/issues/3073 so restrict to netcoreapp

dotnet test api.csproj -c Release -f netcoreapp1.0

# Instead, run directly with mono for the full .net version 
dotnet build api.csproj -c Release -f net451

# TODO Run tests
#mono \  
#./test/TEST_PROJECT_NAME/bin/Release/net451/*/dotnet-test-xunit.exe \
#./test/TEST_PROJECT_NAME/bin/Release/net451/*/TEST_PROJECT_NAME.dll

#TODO Publish to public NuGet repo
#revision=${TRAVIS_JOB_ID:=1}  
#revision=$(printf "%04d" $revision) 
#dotnet pack ./src/PROJECT_NAME -c Release -o ./artifacts --version-suffix=$revision 