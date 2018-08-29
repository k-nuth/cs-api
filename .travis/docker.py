#!/usr/bin/python
import argparse
from os import getcwd, chmod
import os
from subprocess import Popen
import tempfile
import stat

class Docker(object):


    def __load_arguments(self):
        parser = argparse.ArgumentParser(description='Execute docker run in current directory')
        parser.add_argument('-i', '--image', type=str, help='docker image to be used', default='lasote/conangcc63')
        parser.add_argument('-s', '--shell', type=str, help='shell script to be executed', default='')
        # parser.add_argument('-u', '--upgrade', help='upgrade conan', action='store_true')
        # parser.add_argument('-t', '--test', help='Execute test package', action='store_true')
        # parser.add_argument('-p', '--privileged', help='Request privileged user when execute test_package', action='store_true')
        args = parser.parse_args()
        return args

    # def __entrypoint(self):
    #     temp = tempfile.NamedTemporaryFile(mode='wt', delete=False)
    #     temp.write("#!/bin/bash\n")
    #     temp.write("sudo pip install --upgrade conan_package_tools\n")
    #     temp.write("conan user\n")

    #     # temp.write("conan remote add bintray https://api.bintray.com/conan/uilianries/conan\n")
    #     temp.write("conan remote add bitprim_temp https://api.bintray.com/conan/bitprim/bitprim\n")
        
    #     temp.write("conan install .\n")
    #     temp.write("pip install  -e .\n")

    #     temp.write("/bin/bash\n")
    #     temp.flush()
    #     temp.close()
        
    #     st = os.stat(temp.name)
    #     # chmod(temp.name, st.st_mode | stat.S_IEXEC)
    #     chmod(temp.name, st.st_mode | stat.S_IXUSR | stat.S_IXGRP | stat.S_IXOTH)
    #     return temp.name

    # def __test_package(self, privileged):
    #     temp = tempfile.NamedTemporaryFile(delete=False)
    #     temp.write("#!/bin/bash\n")
    #     temp.write("sudo pip install --upgrade conan_package_tools\n")
    #     temp.write("conan user\n")
    #     temp.write("conan remote remove conan.io\n")
    #     temp.write("cd /home/conan/project\n")
    #     if privileged:
    #         temp.write("sudo -E conan test_package\n")
    #     else:
    #         temp.write("conan test_package\n")
    #     temp.flush()
    #     temp.close()
    #     st = os.stat(temp.name)
    #     chmod(temp.name, st.st_mode | stat.S_IEXEC)
    #     return temp.name


    def run(self):
        args = self.__load_arguments()

        try:
            os.remove("/tmp/entrypoint.py")
        except:
            pass

        command = "/usr/bin/docker run --rm -ti -v %s:/home/conan/project " % getcwd()

        # if args.upgrade:
        #     entrypoint = self.__entrypoint()
        #     command += "-v %s:/tmp/entrypoint.sh %s /bin/bash -c /tmp/entrypoint.sh " % (entrypoint, args.image)

        # entrypoint = self.__entrypoint()
        # command += "-v %s:/tmp/entrypoint.sh %s /bin/bash -c /tmp/entrypoint.sh " % (entrypoint, args.image)

        entrypoint = self.__entrypoint()
        command += "-v %s:/tmp/entrypoint.sh %s /bin/bash -c /tmp/entrypoint.sh " % (entrypoint, args.image)


        # if args.test:
        #     entrypoint = self.__test_package(args.privileged)
        #     command += "-v %s:/tmp/entrypoint.sh %s /bin/bash -c /tmp/entrypoint.sh " % (entrypoint, args.image)
        # else:
        #     command += args.image

        Popen(command.split()).wait()

if __name__ == "__main__":
    docker = Docker()
    docker.run()