from kthbuild import get_version_from_branch_name, get_version_from_git_describe, is_development_branch


def get_version():
    version = get_version_from_branch_name()

    # print("version 3: %s" % (version,))

    if version is None:
        version = get_version_from_git_describe(None, is_development_branch())

    # print("version 4: %s" % (version,))
    # print('------------------------------------------------------')

    return version


print(get_version())