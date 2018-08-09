#!/bin/bash

chmod +x travis_install_common.sh
./travis_install_common.sh

chmod +x travis_install_linux.sh
./travis_install_linux.sh

# ----------------------------

conan remote add bitprim_temp https://api.bintray.com/conan/bitprim/bitprim
cd /home/conan/project
chmod +x build.sh
./build.sh
