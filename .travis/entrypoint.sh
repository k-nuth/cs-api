#!/bin/bash

# ------------------------------------------------------------
# Prerequisites

# sudo apt-get install -y curl
# sudo apt-get install -y wget


# ------------------------------------------------------------
# Conan Installation
pip install --upgrade pip --user > /dev/null
pip install conan_package_tools --upgrade --user > /dev/null
pip install conan --upgrade --user > /dev/null
conan user


# ------------------------------------------------------------
# Install .NET Core 2.x

sudo apt-get update -y
sudo apt-get install -y apt-transport-https
wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update -y
sudo apt-get install -y dotnet-sdk-2.1.202


# curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
# sudo mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg
# sudo sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-trusty-prod trusty main" > /etc/apt/sources.list.d/dotnetdev.list'
# sudo apt-get update -y
# sudo apt-get install dotnet-sdk-2.0.2 -y


# ------------------------------------------------------------
gcc --version
g++ --version
sudo ldconfig

# ------------------------------------------------------------
# Install Mono, only to run cake
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
# sudo apt install apt-transport-https
echo "deb https://download.mono-project.com/repo/ubuntu stable-trusty main" | sudo tee /etc/apt/sources.list.d/mono-official-stable.list
sudo apt install -y mono-devel


# ------------------------------------------------------------
# Build steps

conan remote add bitprim_temp https://api.bintray.com/conan/bitprim/bitprim

cd /home/bitprim/bitprim-cs
chmod +x build.sh
./build.sh


