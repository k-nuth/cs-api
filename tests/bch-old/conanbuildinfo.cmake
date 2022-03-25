include(CMakeParseArguments)

macro(conan_find_apple_frameworks FRAMEWORKS_FOUND FRAMEWORKS SUFFIX BUILD_TYPE)
    if(APPLE)
        if(CMAKE_BUILD_TYPE)
            set(_BTYPE ${CMAKE_BUILD_TYPE})
        elseif(NOT BUILD_TYPE STREQUAL "")
            set(_BTYPE ${BUILD_TYPE})
        endif()
        if(_BTYPE)
            if(${_BTYPE} MATCHES "Debug|_DEBUG")
                set(CONAN_FRAMEWORKS${SUFFIX} ${CONAN_FRAMEWORKS${SUFFIX}_DEBUG} ${CONAN_FRAMEWORKS${SUFFIX}})
                set(CONAN_FRAMEWORK_DIRS${SUFFIX} ${CONAN_FRAMEWORK_DIRS${SUFFIX}_DEBUG} ${CONAN_FRAMEWORK_DIRS${SUFFIX}})
            elseif(${_BTYPE} MATCHES "Release|_RELEASE")
                set(CONAN_FRAMEWORKS${SUFFIX} ${CONAN_FRAMEWORKS${SUFFIX}_RELEASE} ${CONAN_FRAMEWORKS${SUFFIX}})
                set(CONAN_FRAMEWORK_DIRS${SUFFIX} ${CONAN_FRAMEWORK_DIRS${SUFFIX}_RELEASE} ${CONAN_FRAMEWORK_DIRS${SUFFIX}})
            elseif(${_BTYPE} MATCHES "RelWithDebInfo|_RELWITHDEBINFO")
                set(CONAN_FRAMEWORKS${SUFFIX} ${CONAN_FRAMEWORKS${SUFFIX}_RELWITHDEBINFO} ${CONAN_FRAMEWORKS${SUFFIX}})
                set(CONAN_FRAMEWORK_DIRS${SUFFIX} ${CONAN_FRAMEWORK_DIRS${SUFFIX}_RELWITHDEBINFO} ${CONAN_FRAMEWORK_DIRS${SUFFIX}})
            elseif(${_BTYPE} MATCHES "MinSizeRel|_MINSIZEREL")
                set(CONAN_FRAMEWORKS${SUFFIX} ${CONAN_FRAMEWORKS${SUFFIX}_MINSIZEREL} ${CONAN_FRAMEWORKS${SUFFIX}})
                set(CONAN_FRAMEWORK_DIRS${SUFFIX} ${CONAN_FRAMEWORK_DIRS${SUFFIX}_MINSIZEREL} ${CONAN_FRAMEWORK_DIRS${SUFFIX}})
            endif()
        endif()
        foreach(_FRAMEWORK ${FRAMEWORKS})
            # https://cmake.org/pipermail/cmake-developers/2017-August/030199.html
            find_library(CONAN_FRAMEWORK_${_FRAMEWORK}_FOUND NAME ${_FRAMEWORK} PATHS ${CONAN_FRAMEWORK_DIRS${SUFFIX}} CMAKE_FIND_ROOT_PATH_BOTH)
            if(CONAN_FRAMEWORK_${_FRAMEWORK}_FOUND)
                list(APPEND ${FRAMEWORKS_FOUND} ${CONAN_FRAMEWORK_${_FRAMEWORK}_FOUND})
            else()
                message(FATAL_ERROR "Framework library ${_FRAMEWORK} not found in paths: ${CONAN_FRAMEWORK_DIRS${SUFFIX}}")
            endif()
        endforeach()
    endif()
endmacro()


#################
###  C-API
#################
set(CONAN_C-API_ROOT "/home/fernando/.conan/data/c-api/0.22.1/kth/staging/package/98069248353d6ac271695922fd1d0d12469c7e24")
set(CONAN_INCLUDE_DIRS_C-API "/home/fernando/.conan/data/c-api/0.22.1/kth/staging/package/98069248353d6ac271695922fd1d0d12469c7e24/include")
set(CONAN_LIB_DIRS_C-API "/home/fernando/.conan/data/c-api/0.22.1/kth/staging/package/98069248353d6ac271695922fd1d0d12469c7e24/lib")
set(CONAN_BIN_DIRS_C-API )
set(CONAN_RES_DIRS_C-API )
set(CONAN_SRC_DIRS_C-API )
set(CONAN_BUILD_DIRS_C-API "/home/fernando/.conan/data/c-api/0.22.1/kth/staging/package/98069248353d6ac271695922fd1d0d12469c7e24/")
set(CONAN_FRAMEWORK_DIRS_C-API )
set(CONAN_LIBS_C-API kth-c-api)
set(CONAN_PKG_LIBS_C-API kth-c-api)
set(CONAN_SYSTEM_LIBS_C-API )
set(CONAN_FRAMEWORKS_C-API )
set(CONAN_FRAMEWORKS_FOUND_C-API "")  # Will be filled later
set(CONAN_DEFINES_C-API )
set(CONAN_BUILD_MODULES_PATHS_C-API )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_C-API )

set(CONAN_C_FLAGS_C-API "")
set(CONAN_CXX_FLAGS_C-API "")
set(CONAN_SHARED_LINKER_FLAGS_C-API "")
set(CONAN_EXE_LINKER_FLAGS_C-API "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_C-API_LIST "")
set(CONAN_CXX_FLAGS_C-API_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_C-API_LIST "")
set(CONAN_EXE_LINKER_FLAGS_C-API_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_C-API "${CONAN_FRAMEWORKS_C-API}" "_C-API" "")
# Append to aggregated values variable
set(CONAN_LIBS_C-API ${CONAN_PKG_LIBS_C-API} ${CONAN_SYSTEM_LIBS_C-API} ${CONAN_FRAMEWORKS_FOUND_C-API})


#################
###  NODE
#################
set(CONAN_NODE_ROOT "/home/fernando/.conan/data/node/0.22.0/kth/staging/package/cd90a97627504e847813354ae7dfdcf26a1903ef")
set(CONAN_INCLUDE_DIRS_NODE "/home/fernando/.conan/data/node/0.22.0/kth/staging/package/cd90a97627504e847813354ae7dfdcf26a1903ef/include")
set(CONAN_LIB_DIRS_NODE "/home/fernando/.conan/data/node/0.22.0/kth/staging/package/cd90a97627504e847813354ae7dfdcf26a1903ef/lib")
set(CONAN_BIN_DIRS_NODE )
set(CONAN_RES_DIRS_NODE )
set(CONAN_SRC_DIRS_NODE )
set(CONAN_BUILD_DIRS_NODE "/home/fernando/.conan/data/node/0.22.0/kth/staging/package/cd90a97627504e847813354ae7dfdcf26a1903ef/")
set(CONAN_FRAMEWORK_DIRS_NODE )
set(CONAN_LIBS_NODE kth-node)
set(CONAN_PKG_LIBS_NODE kth-node)
set(CONAN_SYSTEM_LIBS_NODE )
set(CONAN_FRAMEWORKS_NODE )
set(CONAN_FRAMEWORKS_FOUND_NODE "")  # Will be filled later
set(CONAN_DEFINES_NODE )
set(CONAN_BUILD_MODULES_PATHS_NODE )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_NODE )

set(CONAN_C_FLAGS_NODE "")
set(CONAN_CXX_FLAGS_NODE "")
set(CONAN_SHARED_LINKER_FLAGS_NODE "")
set(CONAN_EXE_LINKER_FLAGS_NODE "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_NODE_LIST "")
set(CONAN_CXX_FLAGS_NODE_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_NODE_LIST "")
set(CONAN_EXE_LINKER_FLAGS_NODE_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_NODE "${CONAN_FRAMEWORKS_NODE}" "_NODE" "")
# Append to aggregated values variable
set(CONAN_LIBS_NODE ${CONAN_PKG_LIBS_NODE} ${CONAN_SYSTEM_LIBS_NODE} ${CONAN_FRAMEWORKS_FOUND_NODE})


#################
###  BLOCKCHAIN
#################
set(CONAN_BLOCKCHAIN_ROOT "/home/fernando/.conan/data/blockchain/0.17.0/kth/staging/package/0a0f078456c6b77fa2113533c5567c10753ecaf0")
set(CONAN_INCLUDE_DIRS_BLOCKCHAIN "/home/fernando/.conan/data/blockchain/0.17.0/kth/staging/package/0a0f078456c6b77fa2113533c5567c10753ecaf0/include")
set(CONAN_LIB_DIRS_BLOCKCHAIN "/home/fernando/.conan/data/blockchain/0.17.0/kth/staging/package/0a0f078456c6b77fa2113533c5567c10753ecaf0/lib")
set(CONAN_BIN_DIRS_BLOCKCHAIN )
set(CONAN_RES_DIRS_BLOCKCHAIN )
set(CONAN_SRC_DIRS_BLOCKCHAIN )
set(CONAN_BUILD_DIRS_BLOCKCHAIN "/home/fernando/.conan/data/blockchain/0.17.0/kth/staging/package/0a0f078456c6b77fa2113533c5567c10753ecaf0/")
set(CONAN_FRAMEWORK_DIRS_BLOCKCHAIN )
set(CONAN_LIBS_BLOCKCHAIN kth-blockchain)
set(CONAN_PKG_LIBS_BLOCKCHAIN kth-blockchain)
set(CONAN_SYSTEM_LIBS_BLOCKCHAIN )
set(CONAN_FRAMEWORKS_BLOCKCHAIN )
set(CONAN_FRAMEWORKS_FOUND_BLOCKCHAIN "")  # Will be filled later
set(CONAN_DEFINES_BLOCKCHAIN )
set(CONAN_BUILD_MODULES_PATHS_BLOCKCHAIN )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_BLOCKCHAIN )

set(CONAN_C_FLAGS_BLOCKCHAIN "")
set(CONAN_CXX_FLAGS_BLOCKCHAIN "")
set(CONAN_SHARED_LINKER_FLAGS_BLOCKCHAIN "")
set(CONAN_EXE_LINKER_FLAGS_BLOCKCHAIN "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_BLOCKCHAIN_LIST "")
set(CONAN_CXX_FLAGS_BLOCKCHAIN_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_BLOCKCHAIN_LIST "")
set(CONAN_EXE_LINKER_FLAGS_BLOCKCHAIN_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_BLOCKCHAIN "${CONAN_FRAMEWORKS_BLOCKCHAIN}" "_BLOCKCHAIN" "")
# Append to aggregated values variable
set(CONAN_LIBS_BLOCKCHAIN ${CONAN_PKG_LIBS_BLOCKCHAIN} ${CONAN_SYSTEM_LIBS_BLOCKCHAIN} ${CONAN_FRAMEWORKS_FOUND_BLOCKCHAIN})


#################
###  NETWORK
#################
set(CONAN_NETWORK_ROOT "/home/fernando/.conan/data/network/0.20.0/kth/staging/package/89babca437eca3f3abf7dd2ea71ee56d242e3dc3")
set(CONAN_INCLUDE_DIRS_NETWORK "/home/fernando/.conan/data/network/0.20.0/kth/staging/package/89babca437eca3f3abf7dd2ea71ee56d242e3dc3/include")
set(CONAN_LIB_DIRS_NETWORK "/home/fernando/.conan/data/network/0.20.0/kth/staging/package/89babca437eca3f3abf7dd2ea71ee56d242e3dc3/lib")
set(CONAN_BIN_DIRS_NETWORK )
set(CONAN_RES_DIRS_NETWORK )
set(CONAN_SRC_DIRS_NETWORK )
set(CONAN_BUILD_DIRS_NETWORK "/home/fernando/.conan/data/network/0.20.0/kth/staging/package/89babca437eca3f3abf7dd2ea71ee56d242e3dc3/")
set(CONAN_FRAMEWORK_DIRS_NETWORK )
set(CONAN_LIBS_NETWORK kth-network)
set(CONAN_PKG_LIBS_NETWORK kth-network)
set(CONAN_SYSTEM_LIBS_NETWORK )
set(CONAN_FRAMEWORKS_NETWORK )
set(CONAN_FRAMEWORKS_FOUND_NETWORK "")  # Will be filled later
set(CONAN_DEFINES_NETWORK )
set(CONAN_BUILD_MODULES_PATHS_NETWORK )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_NETWORK )

set(CONAN_C_FLAGS_NETWORK "")
set(CONAN_CXX_FLAGS_NETWORK "")
set(CONAN_SHARED_LINKER_FLAGS_NETWORK "")
set(CONAN_EXE_LINKER_FLAGS_NETWORK "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_NETWORK_LIST "")
set(CONAN_CXX_FLAGS_NETWORK_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_NETWORK_LIST "")
set(CONAN_EXE_LINKER_FLAGS_NETWORK_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_NETWORK "${CONAN_FRAMEWORKS_NETWORK}" "_NETWORK" "")
# Append to aggregated values variable
set(CONAN_LIBS_NETWORK ${CONAN_PKG_LIBS_NETWORK} ${CONAN_SYSTEM_LIBS_NETWORK} ${CONAN_FRAMEWORKS_FOUND_NETWORK})


#################
###  DATABASE
#################
set(CONAN_DATABASE_ROOT "/home/fernando/.conan/data/database/0.16.0/kth/staging/package/819cae77b0c1d51b96016fd74618ff5c35115fd0")
set(CONAN_INCLUDE_DIRS_DATABASE "/home/fernando/.conan/data/database/0.16.0/kth/staging/package/819cae77b0c1d51b96016fd74618ff5c35115fd0/include")
set(CONAN_LIB_DIRS_DATABASE "/home/fernando/.conan/data/database/0.16.0/kth/staging/package/819cae77b0c1d51b96016fd74618ff5c35115fd0/lib")
set(CONAN_BIN_DIRS_DATABASE )
set(CONAN_RES_DIRS_DATABASE )
set(CONAN_SRC_DIRS_DATABASE )
set(CONAN_BUILD_DIRS_DATABASE "/home/fernando/.conan/data/database/0.16.0/kth/staging/package/819cae77b0c1d51b96016fd74618ff5c35115fd0/")
set(CONAN_FRAMEWORK_DIRS_DATABASE )
set(CONAN_LIBS_DATABASE kth-database)
set(CONAN_PKG_LIBS_DATABASE kth-database)
set(CONAN_SYSTEM_LIBS_DATABASE )
set(CONAN_FRAMEWORKS_DATABASE )
set(CONAN_FRAMEWORKS_FOUND_DATABASE "")  # Will be filled later
set(CONAN_DEFINES_DATABASE )
set(CONAN_BUILD_MODULES_PATHS_DATABASE )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_DATABASE )

set(CONAN_C_FLAGS_DATABASE "")
set(CONAN_CXX_FLAGS_DATABASE "")
set(CONAN_SHARED_LINKER_FLAGS_DATABASE "")
set(CONAN_EXE_LINKER_FLAGS_DATABASE "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_DATABASE_LIST "")
set(CONAN_CXX_FLAGS_DATABASE_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_DATABASE_LIST "")
set(CONAN_EXE_LINKER_FLAGS_DATABASE_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_DATABASE "${CONAN_FRAMEWORKS_DATABASE}" "_DATABASE" "")
# Append to aggregated values variable
set(CONAN_LIBS_DATABASE ${CONAN_PKG_LIBS_DATABASE} ${CONAN_SYSTEM_LIBS_DATABASE} ${CONAN_FRAMEWORKS_FOUND_DATABASE})


#################
###  CONSENSUS
#################
set(CONAN_CONSENSUS_ROOT "/home/fernando/.conan/data/consensus/0.12.0/kth/staging/package/fac4b53985ca1cefd528bb5be8ab9f7bcec012ee")
set(CONAN_INCLUDE_DIRS_CONSENSUS "/home/fernando/.conan/data/consensus/0.12.0/kth/staging/package/fac4b53985ca1cefd528bb5be8ab9f7bcec012ee/include")
set(CONAN_LIB_DIRS_CONSENSUS "/home/fernando/.conan/data/consensus/0.12.0/kth/staging/package/fac4b53985ca1cefd528bb5be8ab9f7bcec012ee/lib")
set(CONAN_BIN_DIRS_CONSENSUS )
set(CONAN_RES_DIRS_CONSENSUS )
set(CONAN_SRC_DIRS_CONSENSUS )
set(CONAN_BUILD_DIRS_CONSENSUS "/home/fernando/.conan/data/consensus/0.12.0/kth/staging/package/fac4b53985ca1cefd528bb5be8ab9f7bcec012ee/")
set(CONAN_FRAMEWORK_DIRS_CONSENSUS )
set(CONAN_LIBS_CONSENSUS kth-consensus)
set(CONAN_PKG_LIBS_CONSENSUS kth-consensus)
set(CONAN_SYSTEM_LIBS_CONSENSUS )
set(CONAN_FRAMEWORKS_CONSENSUS )
set(CONAN_FRAMEWORKS_FOUND_CONSENSUS "")  # Will be filled later
set(CONAN_DEFINES_CONSENSUS )
set(CONAN_BUILD_MODULES_PATHS_CONSENSUS )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_CONSENSUS )

set(CONAN_C_FLAGS_CONSENSUS "")
set(CONAN_CXX_FLAGS_CONSENSUS "")
set(CONAN_SHARED_LINKER_FLAGS_CONSENSUS "")
set(CONAN_EXE_LINKER_FLAGS_CONSENSUS "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_CONSENSUS_LIST "")
set(CONAN_CXX_FLAGS_CONSENSUS_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_CONSENSUS_LIST "")
set(CONAN_EXE_LINKER_FLAGS_CONSENSUS_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_CONSENSUS "${CONAN_FRAMEWORKS_CONSENSUS}" "_CONSENSUS" "")
# Append to aggregated values variable
set(CONAN_LIBS_CONSENSUS ${CONAN_PKG_LIBS_CONSENSUS} ${CONAN_SYSTEM_LIBS_CONSENSUS} ${CONAN_FRAMEWORKS_FOUND_CONSENSUS})


#################
###  LMDB
#################
set(CONAN_LMDB_ROOT "/home/fernando/.conan/data/lmdb/0.9.24/kth/stable/package/39aaeb71f358f382684814bdb6f04e3fc669ee47")
set(CONAN_INCLUDE_DIRS_LMDB "/home/fernando/.conan/data/lmdb/0.9.24/kth/stable/package/39aaeb71f358f382684814bdb6f04e3fc669ee47/include")
set(CONAN_LIB_DIRS_LMDB "/home/fernando/.conan/data/lmdb/0.9.24/kth/stable/package/39aaeb71f358f382684814bdb6f04e3fc669ee47/lib")
set(CONAN_BIN_DIRS_LMDB )
set(CONAN_RES_DIRS_LMDB )
set(CONAN_SRC_DIRS_LMDB )
set(CONAN_BUILD_DIRS_LMDB "/home/fernando/.conan/data/lmdb/0.9.24/kth/stable/package/39aaeb71f358f382684814bdb6f04e3fc669ee47/")
set(CONAN_FRAMEWORK_DIRS_LMDB )
set(CONAN_LIBS_LMDB lmdb pthread)
set(CONAN_PKG_LIBS_LMDB lmdb pthread)
set(CONAN_SYSTEM_LIBS_LMDB )
set(CONAN_FRAMEWORKS_LMDB )
set(CONAN_FRAMEWORKS_FOUND_LMDB "")  # Will be filled later
set(CONAN_DEFINES_LMDB )
set(CONAN_BUILD_MODULES_PATHS_LMDB )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_LMDB )

set(CONAN_C_FLAGS_LMDB "")
set(CONAN_CXX_FLAGS_LMDB "")
set(CONAN_SHARED_LINKER_FLAGS_LMDB "")
set(CONAN_EXE_LINKER_FLAGS_LMDB "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_LMDB_LIST "")
set(CONAN_CXX_FLAGS_LMDB_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_LMDB_LIST "")
set(CONAN_EXE_LINKER_FLAGS_LMDB_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_LMDB "${CONAN_FRAMEWORKS_LMDB}" "_LMDB" "")
# Append to aggregated values variable
set(CONAN_LIBS_LMDB ${CONAN_PKG_LIBS_LMDB} ${CONAN_SYSTEM_LIBS_LMDB} ${CONAN_FRAMEWORKS_FOUND_LMDB})


#################
###  DOMAIN
#################
set(CONAN_DOMAIN_ROOT "/home/fernando/.conan/data/domain/0.17.0/kth/staging/package/6c0e803888d15a2b9405b081594a94706d76d588")
set(CONAN_INCLUDE_DIRS_DOMAIN "/home/fernando/.conan/data/domain/0.17.0/kth/staging/package/6c0e803888d15a2b9405b081594a94706d76d588/include")
set(CONAN_LIB_DIRS_DOMAIN "/home/fernando/.conan/data/domain/0.17.0/kth/staging/package/6c0e803888d15a2b9405b081594a94706d76d588/lib")
set(CONAN_BIN_DIRS_DOMAIN )
set(CONAN_RES_DIRS_DOMAIN )
set(CONAN_SRC_DIRS_DOMAIN )
set(CONAN_BUILD_DIRS_DOMAIN "/home/fernando/.conan/data/domain/0.17.0/kth/staging/package/6c0e803888d15a2b9405b081594a94706d76d588/")
set(CONAN_FRAMEWORK_DIRS_DOMAIN )
set(CONAN_LIBS_DOMAIN kth-domain pthread)
set(CONAN_PKG_LIBS_DOMAIN kth-domain pthread)
set(CONAN_SYSTEM_LIBS_DOMAIN )
set(CONAN_FRAMEWORKS_DOMAIN )
set(CONAN_FRAMEWORKS_FOUND_DOMAIN "")  # Will be filled later
set(CONAN_DEFINES_DOMAIN "-DKD_STATIC")
set(CONAN_BUILD_MODULES_PATHS_DOMAIN )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_DOMAIN "KD_STATIC")

set(CONAN_C_FLAGS_DOMAIN "")
set(CONAN_CXX_FLAGS_DOMAIN "")
set(CONAN_SHARED_LINKER_FLAGS_DOMAIN "")
set(CONAN_EXE_LINKER_FLAGS_DOMAIN "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_DOMAIN_LIST "")
set(CONAN_CXX_FLAGS_DOMAIN_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_DOMAIN_LIST "")
set(CONAN_EXE_LINKER_FLAGS_DOMAIN_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_DOMAIN "${CONAN_FRAMEWORKS_DOMAIN}" "_DOMAIN" "")
# Append to aggregated values variable
set(CONAN_LIBS_DOMAIN ${CONAN_PKG_LIBS_DOMAIN} ${CONAN_SYSTEM_LIBS_DOMAIN} ${CONAN_FRAMEWORKS_FOUND_DOMAIN})


#################
###  ALGORITHM
#################
set(CONAN_ALGORITHM_ROOT "/home/fernando/.conan/data/algorithm/0.1.239/tao/stable/package/4840973d2444cde9f04ae865e38e3aaa6dc0f094")
set(CONAN_INCLUDE_DIRS_ALGORITHM "/home/fernando/.conan/data/algorithm/0.1.239/tao/stable/package/4840973d2444cde9f04ae865e38e3aaa6dc0f094/include")
set(CONAN_LIB_DIRS_ALGORITHM )
set(CONAN_BIN_DIRS_ALGORITHM )
set(CONAN_RES_DIRS_ALGORITHM )
set(CONAN_SRC_DIRS_ALGORITHM )
set(CONAN_BUILD_DIRS_ALGORITHM "/home/fernando/.conan/data/algorithm/0.1.239/tao/stable/package/4840973d2444cde9f04ae865e38e3aaa6dc0f094/")
set(CONAN_FRAMEWORK_DIRS_ALGORITHM )
set(CONAN_LIBS_ALGORITHM )
set(CONAN_PKG_LIBS_ALGORITHM )
set(CONAN_SYSTEM_LIBS_ALGORITHM )
set(CONAN_FRAMEWORKS_ALGORITHM )
set(CONAN_FRAMEWORKS_FOUND_ALGORITHM "")  # Will be filled later
set(CONAN_DEFINES_ALGORITHM )
set(CONAN_BUILD_MODULES_PATHS_ALGORITHM )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_ALGORITHM )

set(CONAN_C_FLAGS_ALGORITHM "")
set(CONAN_CXX_FLAGS_ALGORITHM "")
set(CONAN_SHARED_LINKER_FLAGS_ALGORITHM "")
set(CONAN_EXE_LINKER_FLAGS_ALGORITHM "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_ALGORITHM_LIST "")
set(CONAN_CXX_FLAGS_ALGORITHM_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_ALGORITHM_LIST "")
set(CONAN_EXE_LINKER_FLAGS_ALGORITHM_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_ALGORITHM "${CONAN_FRAMEWORKS_ALGORITHM}" "_ALGORITHM" "")
# Append to aggregated values variable
set(CONAN_LIBS_ALGORITHM ${CONAN_PKG_LIBS_ALGORITHM} ${CONAN_SYSTEM_LIBS_ALGORITHM} ${CONAN_FRAMEWORKS_FOUND_ALGORITHM})


#################
###  INFRASTRUCTURE
#################
set(CONAN_INFRASTRUCTURE_ROOT "/home/fernando/.conan/data/infrastructure/0.14.0/kth/staging/package/a8347adac4c3fe9e912308eeca2e397bd9055b76")
set(CONAN_INCLUDE_DIRS_INFRASTRUCTURE "/home/fernando/.conan/data/infrastructure/0.14.0/kth/staging/package/a8347adac4c3fe9e912308eeca2e397bd9055b76/include")
set(CONAN_LIB_DIRS_INFRASTRUCTURE "/home/fernando/.conan/data/infrastructure/0.14.0/kth/staging/package/a8347adac4c3fe9e912308eeca2e397bd9055b76/lib")
set(CONAN_BIN_DIRS_INFRASTRUCTURE )
set(CONAN_RES_DIRS_INFRASTRUCTURE )
set(CONAN_SRC_DIRS_INFRASTRUCTURE )
set(CONAN_BUILD_DIRS_INFRASTRUCTURE "/home/fernando/.conan/data/infrastructure/0.14.0/kth/staging/package/a8347adac4c3fe9e912308eeca2e397bd9055b76/")
set(CONAN_FRAMEWORK_DIRS_INFRASTRUCTURE )
set(CONAN_LIBS_INFRASTRUCTURE kth-infrastructure pthread)
set(CONAN_PKG_LIBS_INFRASTRUCTURE kth-infrastructure pthread)
set(CONAN_SYSTEM_LIBS_INFRASTRUCTURE )
set(CONAN_FRAMEWORKS_INFRASTRUCTURE )
set(CONAN_FRAMEWORKS_FOUND_INFRASTRUCTURE "")  # Will be filled later
set(CONAN_DEFINES_INFRASTRUCTURE "-DKI_STATIC")
set(CONAN_BUILD_MODULES_PATHS_INFRASTRUCTURE )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_INFRASTRUCTURE "KI_STATIC")

set(CONAN_C_FLAGS_INFRASTRUCTURE "")
set(CONAN_CXX_FLAGS_INFRASTRUCTURE "")
set(CONAN_SHARED_LINKER_FLAGS_INFRASTRUCTURE "")
set(CONAN_EXE_LINKER_FLAGS_INFRASTRUCTURE "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_INFRASTRUCTURE_LIST "")
set(CONAN_CXX_FLAGS_INFRASTRUCTURE_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_INFRASTRUCTURE_LIST "")
set(CONAN_EXE_LINKER_FLAGS_INFRASTRUCTURE_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_INFRASTRUCTURE "${CONAN_FRAMEWORKS_INFRASTRUCTURE}" "_INFRASTRUCTURE" "")
# Append to aggregated values variable
set(CONAN_LIBS_INFRASTRUCTURE ${CONAN_PKG_LIBS_INFRASTRUCTURE} ${CONAN_SYSTEM_LIBS_INFRASTRUCTURE} ${CONAN_FRAMEWORKS_FOUND_INFRASTRUCTURE})


#################
###  SECP256K1
#################
set(CONAN_SECP256K1_ROOT "/home/fernando/.conan/data/secp256k1/0.8.0/kth/staging/package/94868bdef937e4065e64dbe74f919be7aaf0042a")
set(CONAN_INCLUDE_DIRS_SECP256K1 "/home/fernando/.conan/data/secp256k1/0.8.0/kth/staging/package/94868bdef937e4065e64dbe74f919be7aaf0042a/include")
set(CONAN_LIB_DIRS_SECP256K1 "/home/fernando/.conan/data/secp256k1/0.8.0/kth/staging/package/94868bdef937e4065e64dbe74f919be7aaf0042a/lib")
set(CONAN_BIN_DIRS_SECP256K1 )
set(CONAN_RES_DIRS_SECP256K1 )
set(CONAN_SRC_DIRS_SECP256K1 )
set(CONAN_BUILD_DIRS_SECP256K1 "/home/fernando/.conan/data/secp256k1/0.8.0/kth/staging/package/94868bdef937e4065e64dbe74f919be7aaf0042a/")
set(CONAN_FRAMEWORK_DIRS_SECP256K1 )
set(CONAN_LIBS_SECP256K1 secp256k1)
set(CONAN_PKG_LIBS_SECP256K1 secp256k1)
set(CONAN_SYSTEM_LIBS_SECP256K1 )
set(CONAN_FRAMEWORKS_SECP256K1 )
set(CONAN_FRAMEWORKS_FOUND_SECP256K1 "")  # Will be filled later
set(CONAN_DEFINES_SECP256K1 )
set(CONAN_BUILD_MODULES_PATHS_SECP256K1 )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_SECP256K1 )

set(CONAN_C_FLAGS_SECP256K1 "")
set(CONAN_CXX_FLAGS_SECP256K1 "")
set(CONAN_SHARED_LINKER_FLAGS_SECP256K1 "")
set(CONAN_EXE_LINKER_FLAGS_SECP256K1 "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_SECP256K1_LIST "")
set(CONAN_CXX_FLAGS_SECP256K1_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_SECP256K1_LIST "")
set(CONAN_EXE_LINKER_FLAGS_SECP256K1_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_SECP256K1 "${CONAN_FRAMEWORKS_SECP256K1}" "_SECP256K1" "")
# Append to aggregated values variable
set(CONAN_LIBS_SECP256K1 ${CONAN_PKG_LIBS_SECP256K1} ${CONAN_SYSTEM_LIBS_SECP256K1} ${CONAN_FRAMEWORKS_FOUND_SECP256K1})


#################
###  BOOST
#################
set(CONAN_BOOST_ROOT "/home/fernando/.conan/data/boost/1.76.0/_/_/package/dc8aedd23a0f0a773a5fcdcfe1ae3e89c4205978")
set(CONAN_INCLUDE_DIRS_BOOST "/home/fernando/.conan/data/boost/1.76.0/_/_/package/dc8aedd23a0f0a773a5fcdcfe1ae3e89c4205978/include")
set(CONAN_LIB_DIRS_BOOST "/home/fernando/.conan/data/boost/1.76.0/_/_/package/dc8aedd23a0f0a773a5fcdcfe1ae3e89c4205978/lib")
set(CONAN_BIN_DIRS_BOOST )
set(CONAN_RES_DIRS_BOOST )
set(CONAN_SRC_DIRS_BOOST )
set(CONAN_BUILD_DIRS_BOOST "/home/fernando/.conan/data/boost/1.76.0/_/_/package/dc8aedd23a0f0a773a5fcdcfe1ae3e89c4205978/")
set(CONAN_FRAMEWORK_DIRS_BOOST )
set(CONAN_LIBS_BOOST boost_contract boost_coroutine boost_fiber_numa boost_fiber boost_context boost_graph boost_iostreams boost_json boost_log_setup boost_log boost_locale boost_math_c99 boost_math_c99f boost_math_c99l boost_math_tr1 boost_math_tr1f boost_math_tr1l boost_nowide boost_program_options boost_random boost_regex boost_stacktrace_addr2line boost_stacktrace_backtrace boost_stacktrace_basic boost_stacktrace_noop boost_timer boost_type_erasure boost_thread boost_atomic boost_chrono boost_container boost_date_time boost_unit_test_framework boost_prg_exec_monitor boost_test_exec_monitor boost_exception boost_wave boost_filesystem boost_wserialization boost_serialization)
set(CONAN_PKG_LIBS_BOOST boost_contract boost_coroutine boost_fiber_numa boost_fiber boost_context boost_graph boost_iostreams boost_json boost_log_setup boost_log boost_locale boost_math_c99 boost_math_c99f boost_math_c99l boost_math_tr1 boost_math_tr1f boost_math_tr1l boost_nowide boost_program_options boost_random boost_regex boost_stacktrace_addr2line boost_stacktrace_backtrace boost_stacktrace_basic boost_stacktrace_noop boost_timer boost_type_erasure boost_thread boost_atomic boost_chrono boost_container boost_date_time boost_unit_test_framework boost_prg_exec_monitor boost_test_exec_monitor boost_exception boost_wave boost_filesystem boost_wserialization boost_serialization)
set(CONAN_SYSTEM_LIBS_BOOST dl rt pthread)
set(CONAN_FRAMEWORKS_BOOST )
set(CONAN_FRAMEWORKS_FOUND_BOOST "")  # Will be filled later
set(CONAN_DEFINES_BOOST "-DBOOST_STACKTRACE_ADDR2LINE_LOCATION=\"/usr/bin/addr2line\""
			"-DBOOST_STACKTRACE_USE_ADDR2LINE"
			"-DBOOST_STACKTRACE_USE_BACKTRACE"
			"-DBOOST_STACKTRACE_USE_NOOP")
set(CONAN_BUILD_MODULES_PATHS_BOOST )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_BOOST "BOOST_STACKTRACE_ADDR2LINE_LOCATION=\"/usr/bin/addr2line\""
			"BOOST_STACKTRACE_USE_ADDR2LINE"
			"BOOST_STACKTRACE_USE_BACKTRACE"
			"BOOST_STACKTRACE_USE_NOOP")

set(CONAN_C_FLAGS_BOOST "")
set(CONAN_CXX_FLAGS_BOOST "")
set(CONAN_SHARED_LINKER_FLAGS_BOOST "")
set(CONAN_EXE_LINKER_FLAGS_BOOST "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_BOOST_LIST "")
set(CONAN_CXX_FLAGS_BOOST_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_BOOST_LIST "")
set(CONAN_EXE_LINKER_FLAGS_BOOST_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_BOOST "${CONAN_FRAMEWORKS_BOOST}" "_BOOST" "")
# Append to aggregated values variable
set(CONAN_LIBS_BOOST ${CONAN_PKG_LIBS_BOOST} ${CONAN_SYSTEM_LIBS_BOOST} ${CONAN_FRAMEWORKS_FOUND_BOOST})


#################
###  SPDLOG
#################
set(CONAN_SPDLOG_ROOT "/home/fernando/.conan/data/spdlog/1.9.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9")
set(CONAN_INCLUDE_DIRS_SPDLOG "/home/fernando/.conan/data/spdlog/1.9.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include")
set(CONAN_LIB_DIRS_SPDLOG )
set(CONAN_BIN_DIRS_SPDLOG )
set(CONAN_RES_DIRS_SPDLOG )
set(CONAN_SRC_DIRS_SPDLOG )
set(CONAN_BUILD_DIRS_SPDLOG "/home/fernando/.conan/data/spdlog/1.9.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/")
set(CONAN_FRAMEWORK_DIRS_SPDLOG )
set(CONAN_LIBS_SPDLOG )
set(CONAN_PKG_LIBS_SPDLOG )
set(CONAN_SYSTEM_LIBS_SPDLOG pthread)
set(CONAN_FRAMEWORKS_SPDLOG )
set(CONAN_FRAMEWORKS_FOUND_SPDLOG "")  # Will be filled later
set(CONAN_DEFINES_SPDLOG "-DSPDLOG_FMT_EXTERNAL")
set(CONAN_BUILD_MODULES_PATHS_SPDLOG )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_SPDLOG "SPDLOG_FMT_EXTERNAL")

set(CONAN_C_FLAGS_SPDLOG "")
set(CONAN_CXX_FLAGS_SPDLOG "")
set(CONAN_SHARED_LINKER_FLAGS_SPDLOG "")
set(CONAN_EXE_LINKER_FLAGS_SPDLOG "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_SPDLOG_LIST "")
set(CONAN_CXX_FLAGS_SPDLOG_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_SPDLOG_LIST "")
set(CONAN_EXE_LINKER_FLAGS_SPDLOG_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_SPDLOG "${CONAN_FRAMEWORKS_SPDLOG}" "_SPDLOG" "")
# Append to aggregated values variable
set(CONAN_LIBS_SPDLOG ${CONAN_PKG_LIBS_SPDLOG} ${CONAN_SYSTEM_LIBS_SPDLOG} ${CONAN_FRAMEWORKS_FOUND_SPDLOG})


#################
###  GMP
#################
set(CONAN_GMP_ROOT "/home/fernando/.conan/data/gmp/6.2.1/_/_/package/f7d295909e4a77ea9afe0e22daa8e7d48da26066")
set(CONAN_INCLUDE_DIRS_GMP "/home/fernando/.conan/data/gmp/6.2.1/_/_/package/f7d295909e4a77ea9afe0e22daa8e7d48da26066/include")
set(CONAN_LIB_DIRS_GMP "/home/fernando/.conan/data/gmp/6.2.1/_/_/package/f7d295909e4a77ea9afe0e22daa8e7d48da26066/lib")
set(CONAN_BIN_DIRS_GMP )
set(CONAN_RES_DIRS_GMP )
set(CONAN_SRC_DIRS_GMP )
set(CONAN_BUILD_DIRS_GMP "/home/fernando/.conan/data/gmp/6.2.1/_/_/package/f7d295909e4a77ea9afe0e22daa8e7d48da26066/")
set(CONAN_FRAMEWORK_DIRS_GMP )
set(CONAN_LIBS_GMP gmpxx gmp)
set(CONAN_PKG_LIBS_GMP gmpxx gmp)
set(CONAN_SYSTEM_LIBS_GMP )
set(CONAN_FRAMEWORKS_GMP )
set(CONAN_FRAMEWORKS_FOUND_GMP "")  # Will be filled later
set(CONAN_DEFINES_GMP )
set(CONAN_BUILD_MODULES_PATHS_GMP )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_GMP )

set(CONAN_C_FLAGS_GMP "")
set(CONAN_CXX_FLAGS_GMP "")
set(CONAN_SHARED_LINKER_FLAGS_GMP "")
set(CONAN_EXE_LINKER_FLAGS_GMP "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_GMP_LIST "")
set(CONAN_CXX_FLAGS_GMP_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_GMP_LIST "")
set(CONAN_EXE_LINKER_FLAGS_GMP_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_GMP "${CONAN_FRAMEWORKS_GMP}" "_GMP" "")
# Append to aggregated values variable
set(CONAN_LIBS_GMP ${CONAN_PKG_LIBS_GMP} ${CONAN_SYSTEM_LIBS_GMP} ${CONAN_FRAMEWORKS_FOUND_GMP})


#################
###  ZLIB
#################
set(CONAN_ZLIB_ROOT "/home/fernando/.conan/data/zlib/1.2.11/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646")
set(CONAN_INCLUDE_DIRS_ZLIB "/home/fernando/.conan/data/zlib/1.2.11/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646/include")
set(CONAN_LIB_DIRS_ZLIB "/home/fernando/.conan/data/zlib/1.2.11/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646/lib")
set(CONAN_BIN_DIRS_ZLIB )
set(CONAN_RES_DIRS_ZLIB )
set(CONAN_SRC_DIRS_ZLIB )
set(CONAN_BUILD_DIRS_ZLIB "/home/fernando/.conan/data/zlib/1.2.11/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646/")
set(CONAN_FRAMEWORK_DIRS_ZLIB )
set(CONAN_LIBS_ZLIB z)
set(CONAN_PKG_LIBS_ZLIB z)
set(CONAN_SYSTEM_LIBS_ZLIB )
set(CONAN_FRAMEWORKS_ZLIB )
set(CONAN_FRAMEWORKS_FOUND_ZLIB "")  # Will be filled later
set(CONAN_DEFINES_ZLIB )
set(CONAN_BUILD_MODULES_PATHS_ZLIB )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_ZLIB )

set(CONAN_C_FLAGS_ZLIB "")
set(CONAN_CXX_FLAGS_ZLIB "")
set(CONAN_SHARED_LINKER_FLAGS_ZLIB "")
set(CONAN_EXE_LINKER_FLAGS_ZLIB "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_ZLIB_LIST "")
set(CONAN_CXX_FLAGS_ZLIB_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_ZLIB_LIST "")
set(CONAN_EXE_LINKER_FLAGS_ZLIB_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_ZLIB "${CONAN_FRAMEWORKS_ZLIB}" "_ZLIB" "")
# Append to aggregated values variable
set(CONAN_LIBS_ZLIB ${CONAN_PKG_LIBS_ZLIB} ${CONAN_SYSTEM_LIBS_ZLIB} ${CONAN_FRAMEWORKS_FOUND_ZLIB})


#################
###  BZIP2
#################
set(CONAN_BZIP2_ROOT "/home/fernando/.conan/data/bzip2/1.0.8/_/_/package/c32092bf4d4bb47cf962af898e02823f499b017e")
set(CONAN_INCLUDE_DIRS_BZIP2 "/home/fernando/.conan/data/bzip2/1.0.8/_/_/package/c32092bf4d4bb47cf962af898e02823f499b017e/include")
set(CONAN_LIB_DIRS_BZIP2 "/home/fernando/.conan/data/bzip2/1.0.8/_/_/package/c32092bf4d4bb47cf962af898e02823f499b017e/lib")
set(CONAN_BIN_DIRS_BZIP2 "/home/fernando/.conan/data/bzip2/1.0.8/_/_/package/c32092bf4d4bb47cf962af898e02823f499b017e/bin")
set(CONAN_RES_DIRS_BZIP2 )
set(CONAN_SRC_DIRS_BZIP2 )
set(CONAN_BUILD_DIRS_BZIP2 "/home/fernando/.conan/data/bzip2/1.0.8/_/_/package/c32092bf4d4bb47cf962af898e02823f499b017e/"
			"/home/fernando/.conan/data/bzip2/1.0.8/_/_/package/c32092bf4d4bb47cf962af898e02823f499b017e/lib/cmake")
set(CONAN_FRAMEWORK_DIRS_BZIP2 )
set(CONAN_LIBS_BZIP2 bz2)
set(CONAN_PKG_LIBS_BZIP2 bz2)
set(CONAN_SYSTEM_LIBS_BZIP2 )
set(CONAN_FRAMEWORKS_BZIP2 )
set(CONAN_FRAMEWORKS_FOUND_BZIP2 "")  # Will be filled later
set(CONAN_DEFINES_BZIP2 )
set(CONAN_BUILD_MODULES_PATHS_BZIP2 )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_BZIP2 )

set(CONAN_C_FLAGS_BZIP2 "")
set(CONAN_CXX_FLAGS_BZIP2 "")
set(CONAN_SHARED_LINKER_FLAGS_BZIP2 "")
set(CONAN_EXE_LINKER_FLAGS_BZIP2 "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_BZIP2_LIST "")
set(CONAN_CXX_FLAGS_BZIP2_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_BZIP2_LIST "")
set(CONAN_EXE_LINKER_FLAGS_BZIP2_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_BZIP2 "${CONAN_FRAMEWORKS_BZIP2}" "_BZIP2" "")
# Append to aggregated values variable
set(CONAN_LIBS_BZIP2 ${CONAN_PKG_LIBS_BZIP2} ${CONAN_SYSTEM_LIBS_BZIP2} ${CONAN_FRAMEWORKS_FOUND_BZIP2})


#################
###  LIBBACKTRACE
#################
set(CONAN_LIBBACKTRACE_ROOT "/home/fernando/.conan/data/libbacktrace/cci.20210118/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646")
set(CONAN_INCLUDE_DIRS_LIBBACKTRACE "/home/fernando/.conan/data/libbacktrace/cci.20210118/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646/include")
set(CONAN_LIB_DIRS_LIBBACKTRACE "/home/fernando/.conan/data/libbacktrace/cci.20210118/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646/lib")
set(CONAN_BIN_DIRS_LIBBACKTRACE )
set(CONAN_RES_DIRS_LIBBACKTRACE )
set(CONAN_SRC_DIRS_LIBBACKTRACE )
set(CONAN_BUILD_DIRS_LIBBACKTRACE "/home/fernando/.conan/data/libbacktrace/cci.20210118/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646/")
set(CONAN_FRAMEWORK_DIRS_LIBBACKTRACE )
set(CONAN_LIBS_LIBBACKTRACE backtrace)
set(CONAN_PKG_LIBS_LIBBACKTRACE backtrace)
set(CONAN_SYSTEM_LIBS_LIBBACKTRACE )
set(CONAN_FRAMEWORKS_LIBBACKTRACE )
set(CONAN_FRAMEWORKS_FOUND_LIBBACKTRACE "")  # Will be filled later
set(CONAN_DEFINES_LIBBACKTRACE )
set(CONAN_BUILD_MODULES_PATHS_LIBBACKTRACE )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_LIBBACKTRACE )

set(CONAN_C_FLAGS_LIBBACKTRACE "")
set(CONAN_CXX_FLAGS_LIBBACKTRACE "")
set(CONAN_SHARED_LINKER_FLAGS_LIBBACKTRACE "")
set(CONAN_EXE_LINKER_FLAGS_LIBBACKTRACE "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_LIBBACKTRACE_LIST "")
set(CONAN_CXX_FLAGS_LIBBACKTRACE_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_LIBBACKTRACE_LIST "")
set(CONAN_EXE_LINKER_FLAGS_LIBBACKTRACE_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_LIBBACKTRACE "${CONAN_FRAMEWORKS_LIBBACKTRACE}" "_LIBBACKTRACE" "")
# Append to aggregated values variable
set(CONAN_LIBS_LIBBACKTRACE ${CONAN_PKG_LIBS_LIBBACKTRACE} ${CONAN_SYSTEM_LIBS_LIBBACKTRACE} ${CONAN_FRAMEWORKS_FOUND_LIBBACKTRACE})


#################
###  FMT
#################
set(CONAN_FMT_ROOT "/home/fernando/.conan/data/fmt/8.0.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9")
set(CONAN_INCLUDE_DIRS_FMT "/home/fernando/.conan/data/fmt/8.0.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include")
set(CONAN_LIB_DIRS_FMT )
set(CONAN_BIN_DIRS_FMT )
set(CONAN_RES_DIRS_FMT )
set(CONAN_SRC_DIRS_FMT )
set(CONAN_BUILD_DIRS_FMT "/home/fernando/.conan/data/fmt/8.0.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/")
set(CONAN_FRAMEWORK_DIRS_FMT )
set(CONAN_LIBS_FMT )
set(CONAN_PKG_LIBS_FMT )
set(CONAN_SYSTEM_LIBS_FMT )
set(CONAN_FRAMEWORKS_FMT )
set(CONAN_FRAMEWORKS_FOUND_FMT "")  # Will be filled later
set(CONAN_DEFINES_FMT "-DFMT_HEADER_ONLY=1")
set(CONAN_BUILD_MODULES_PATHS_FMT )
# COMPILE_DEFINITIONS are equal to CONAN_DEFINES without -D, for targets
set(CONAN_COMPILE_DEFINITIONS_FMT "FMT_HEADER_ONLY=1")

set(CONAN_C_FLAGS_FMT "")
set(CONAN_CXX_FLAGS_FMT "")
set(CONAN_SHARED_LINKER_FLAGS_FMT "")
set(CONAN_EXE_LINKER_FLAGS_FMT "")

# For modern cmake targets we use the list variables (separated with ;)
set(CONAN_C_FLAGS_FMT_LIST "")
set(CONAN_CXX_FLAGS_FMT_LIST "")
set(CONAN_SHARED_LINKER_FLAGS_FMT_LIST "")
set(CONAN_EXE_LINKER_FLAGS_FMT_LIST "")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND_FMT "${CONAN_FRAMEWORKS_FMT}" "_FMT" "")
# Append to aggregated values variable
set(CONAN_LIBS_FMT ${CONAN_PKG_LIBS_FMT} ${CONAN_SYSTEM_LIBS_FMT} ${CONAN_FRAMEWORKS_FOUND_FMT})


### Definition of global aggregated variables ###

set(CONAN_PACKAGE_NAME None)
set(CONAN_PACKAGE_VERSION None)

set(CONAN_SETTINGS_ARCH "x86_64")
set(CONAN_SETTINGS_ARCH_BUILD "x86_64")
set(CONAN_SETTINGS_BUILD_TYPE "Release")
set(CONAN_SETTINGS_COMPILER "gcc")
set(CONAN_SETTINGS_COMPILER_LIBCXX "libstdc++11")
set(CONAN_SETTINGS_COMPILER_VERSION "11")
set(CONAN_SETTINGS_OS "Linux")
set(CONAN_SETTINGS_OS_BUILD "Linux")

set(CONAN_DEPENDENCIES c-api node blockchain network database consensus lmdb domain algorithm infrastructure secp256k1 boost spdlog gmp zlib bzip2 libbacktrace fmt)
# Storing original command line args (CMake helper) flags
set(CONAN_CMD_CXX_FLAGS ${CONAN_CXX_FLAGS})

set(CONAN_CMD_SHARED_LINKER_FLAGS ${CONAN_SHARED_LINKER_FLAGS})
set(CONAN_CMD_C_FLAGS ${CONAN_C_FLAGS})
# Defining accumulated conan variables for all deps

set(CONAN_INCLUDE_DIRS "/home/fernando/.conan/data/c-api/0.22.1/kth/staging/package/98069248353d6ac271695922fd1d0d12469c7e24/include"
			"/home/fernando/.conan/data/node/0.22.0/kth/staging/package/cd90a97627504e847813354ae7dfdcf26a1903ef/include"
			"/home/fernando/.conan/data/blockchain/0.17.0/kth/staging/package/0a0f078456c6b77fa2113533c5567c10753ecaf0/include"
			"/home/fernando/.conan/data/network/0.20.0/kth/staging/package/89babca437eca3f3abf7dd2ea71ee56d242e3dc3/include"
			"/home/fernando/.conan/data/database/0.16.0/kth/staging/package/819cae77b0c1d51b96016fd74618ff5c35115fd0/include"
			"/home/fernando/.conan/data/consensus/0.12.0/kth/staging/package/fac4b53985ca1cefd528bb5be8ab9f7bcec012ee/include"
			"/home/fernando/.conan/data/lmdb/0.9.24/kth/stable/package/39aaeb71f358f382684814bdb6f04e3fc669ee47/include"
			"/home/fernando/.conan/data/domain/0.17.0/kth/staging/package/6c0e803888d15a2b9405b081594a94706d76d588/include"
			"/home/fernando/.conan/data/algorithm/0.1.239/tao/stable/package/4840973d2444cde9f04ae865e38e3aaa6dc0f094/include"
			"/home/fernando/.conan/data/infrastructure/0.14.0/kth/staging/package/a8347adac4c3fe9e912308eeca2e397bd9055b76/include"
			"/home/fernando/.conan/data/secp256k1/0.8.0/kth/staging/package/94868bdef937e4065e64dbe74f919be7aaf0042a/include"
			"/home/fernando/.conan/data/boost/1.76.0/_/_/package/dc8aedd23a0f0a773a5fcdcfe1ae3e89c4205978/include"
			"/home/fernando/.conan/data/spdlog/1.9.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include"
			"/home/fernando/.conan/data/gmp/6.2.1/_/_/package/f7d295909e4a77ea9afe0e22daa8e7d48da26066/include"
			"/home/fernando/.conan/data/zlib/1.2.11/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646/include"
			"/home/fernando/.conan/data/bzip2/1.0.8/_/_/package/c32092bf4d4bb47cf962af898e02823f499b017e/include"
			"/home/fernando/.conan/data/libbacktrace/cci.20210118/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646/include"
			"/home/fernando/.conan/data/fmt/8.0.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/include" ${CONAN_INCLUDE_DIRS})
set(CONAN_LIB_DIRS "/home/fernando/.conan/data/c-api/0.22.1/kth/staging/package/98069248353d6ac271695922fd1d0d12469c7e24/lib"
			"/home/fernando/.conan/data/node/0.22.0/kth/staging/package/cd90a97627504e847813354ae7dfdcf26a1903ef/lib"
			"/home/fernando/.conan/data/blockchain/0.17.0/kth/staging/package/0a0f078456c6b77fa2113533c5567c10753ecaf0/lib"
			"/home/fernando/.conan/data/network/0.20.0/kth/staging/package/89babca437eca3f3abf7dd2ea71ee56d242e3dc3/lib"
			"/home/fernando/.conan/data/database/0.16.0/kth/staging/package/819cae77b0c1d51b96016fd74618ff5c35115fd0/lib"
			"/home/fernando/.conan/data/consensus/0.12.0/kth/staging/package/fac4b53985ca1cefd528bb5be8ab9f7bcec012ee/lib"
			"/home/fernando/.conan/data/lmdb/0.9.24/kth/stable/package/39aaeb71f358f382684814bdb6f04e3fc669ee47/lib"
			"/home/fernando/.conan/data/domain/0.17.0/kth/staging/package/6c0e803888d15a2b9405b081594a94706d76d588/lib"
			"/home/fernando/.conan/data/infrastructure/0.14.0/kth/staging/package/a8347adac4c3fe9e912308eeca2e397bd9055b76/lib"
			"/home/fernando/.conan/data/secp256k1/0.8.0/kth/staging/package/94868bdef937e4065e64dbe74f919be7aaf0042a/lib"
			"/home/fernando/.conan/data/boost/1.76.0/_/_/package/dc8aedd23a0f0a773a5fcdcfe1ae3e89c4205978/lib"
			"/home/fernando/.conan/data/gmp/6.2.1/_/_/package/f7d295909e4a77ea9afe0e22daa8e7d48da26066/lib"
			"/home/fernando/.conan/data/zlib/1.2.11/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646/lib"
			"/home/fernando/.conan/data/bzip2/1.0.8/_/_/package/c32092bf4d4bb47cf962af898e02823f499b017e/lib"
			"/home/fernando/.conan/data/libbacktrace/cci.20210118/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646/lib" ${CONAN_LIB_DIRS})
set(CONAN_BIN_DIRS "/home/fernando/.conan/data/bzip2/1.0.8/_/_/package/c32092bf4d4bb47cf962af898e02823f499b017e/bin" ${CONAN_BIN_DIRS})
set(CONAN_RES_DIRS  ${CONAN_RES_DIRS})
set(CONAN_FRAMEWORK_DIRS  ${CONAN_FRAMEWORK_DIRS})
set(CONAN_LIBS kth-c-api kth-node kth-blockchain kth-network kth-database kth-consensus lmdb kth-domain kth-infrastructure pthread secp256k1 boost_contract boost_coroutine boost_fiber_numa boost_fiber boost_context boost_graph boost_iostreams boost_json boost_log_setup boost_log boost_locale boost_math_c99 boost_math_c99f boost_math_c99l boost_math_tr1 boost_math_tr1f boost_math_tr1l boost_nowide boost_program_options boost_random boost_regex boost_stacktrace_addr2line boost_stacktrace_backtrace boost_stacktrace_basic boost_stacktrace_noop boost_timer boost_type_erasure boost_thread boost_atomic boost_chrono boost_container boost_date_time boost_unit_test_framework boost_prg_exec_monitor boost_test_exec_monitor boost_exception boost_wave boost_filesystem boost_wserialization boost_serialization gmpxx gmp z bz2 backtrace ${CONAN_LIBS})
set(CONAN_PKG_LIBS kth-c-api kth-node kth-blockchain kth-network kth-database kth-consensus lmdb kth-domain kth-infrastructure pthread secp256k1 boost_contract boost_coroutine boost_fiber_numa boost_fiber boost_context boost_graph boost_iostreams boost_json boost_log_setup boost_log boost_locale boost_math_c99 boost_math_c99f boost_math_c99l boost_math_tr1 boost_math_tr1f boost_math_tr1l boost_nowide boost_program_options boost_random boost_regex boost_stacktrace_addr2line boost_stacktrace_backtrace boost_stacktrace_basic boost_stacktrace_noop boost_timer boost_type_erasure boost_thread boost_atomic boost_chrono boost_container boost_date_time boost_unit_test_framework boost_prg_exec_monitor boost_test_exec_monitor boost_exception boost_wave boost_filesystem boost_wserialization boost_serialization gmpxx gmp z bz2 backtrace ${CONAN_PKG_LIBS})
set(CONAN_SYSTEM_LIBS dl rt pthread ${CONAN_SYSTEM_LIBS})
set(CONAN_FRAMEWORKS  ${CONAN_FRAMEWORKS})
set(CONAN_FRAMEWORKS_FOUND "")  # Will be filled later
set(CONAN_DEFINES "-DFMT_HEADER_ONLY=1"
			"-DSPDLOG_FMT_EXTERNAL"
			"-DBOOST_STACKTRACE_ADDR2LINE_LOCATION=\"/usr/bin/addr2line\""
			"-DBOOST_STACKTRACE_USE_ADDR2LINE"
			"-DBOOST_STACKTRACE_USE_BACKTRACE"
			"-DBOOST_STACKTRACE_USE_NOOP"
			"-DKI_STATIC"
			"-DKD_STATIC" ${CONAN_DEFINES})
set(CONAN_BUILD_MODULES_PATHS  ${CONAN_BUILD_MODULES_PATHS})
set(CONAN_CMAKE_MODULE_PATH "/home/fernando/.conan/data/c-api/0.22.1/kth/staging/package/98069248353d6ac271695922fd1d0d12469c7e24/"
			"/home/fernando/.conan/data/node/0.22.0/kth/staging/package/cd90a97627504e847813354ae7dfdcf26a1903ef/"
			"/home/fernando/.conan/data/blockchain/0.17.0/kth/staging/package/0a0f078456c6b77fa2113533c5567c10753ecaf0/"
			"/home/fernando/.conan/data/network/0.20.0/kth/staging/package/89babca437eca3f3abf7dd2ea71ee56d242e3dc3/"
			"/home/fernando/.conan/data/database/0.16.0/kth/staging/package/819cae77b0c1d51b96016fd74618ff5c35115fd0/"
			"/home/fernando/.conan/data/consensus/0.12.0/kth/staging/package/fac4b53985ca1cefd528bb5be8ab9f7bcec012ee/"
			"/home/fernando/.conan/data/lmdb/0.9.24/kth/stable/package/39aaeb71f358f382684814bdb6f04e3fc669ee47/"
			"/home/fernando/.conan/data/domain/0.17.0/kth/staging/package/6c0e803888d15a2b9405b081594a94706d76d588/"
			"/home/fernando/.conan/data/algorithm/0.1.239/tao/stable/package/4840973d2444cde9f04ae865e38e3aaa6dc0f094/"
			"/home/fernando/.conan/data/infrastructure/0.14.0/kth/staging/package/a8347adac4c3fe9e912308eeca2e397bd9055b76/"
			"/home/fernando/.conan/data/secp256k1/0.8.0/kth/staging/package/94868bdef937e4065e64dbe74f919be7aaf0042a/"
			"/home/fernando/.conan/data/boost/1.76.0/_/_/package/dc8aedd23a0f0a773a5fcdcfe1ae3e89c4205978/"
			"/home/fernando/.conan/data/spdlog/1.9.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/"
			"/home/fernando/.conan/data/gmp/6.2.1/_/_/package/f7d295909e4a77ea9afe0e22daa8e7d48da26066/"
			"/home/fernando/.conan/data/zlib/1.2.11/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646/"
			"/home/fernando/.conan/data/bzip2/1.0.8/_/_/package/c32092bf4d4bb47cf962af898e02823f499b017e/"
			"/home/fernando/.conan/data/bzip2/1.0.8/_/_/package/c32092bf4d4bb47cf962af898e02823f499b017e/lib/cmake"
			"/home/fernando/.conan/data/libbacktrace/cci.20210118/_/_/package/dfbe50feef7f3c6223a476cd5aeadb687084a646/"
			"/home/fernando/.conan/data/fmt/8.0.1/_/_/package/5ab84d6acfe1f23c4fae0ab88f26e3a396351ac9/" ${CONAN_CMAKE_MODULE_PATH})

set(CONAN_CXX_FLAGS " ${CONAN_CXX_FLAGS}")
set(CONAN_SHARED_LINKER_FLAGS " ${CONAN_SHARED_LINKER_FLAGS}")
set(CONAN_EXE_LINKER_FLAGS " ${CONAN_EXE_LINKER_FLAGS}")
set(CONAN_C_FLAGS " ${CONAN_C_FLAGS}")

# Apple Frameworks
conan_find_apple_frameworks(CONAN_FRAMEWORKS_FOUND "${CONAN_FRAMEWORKS}" "" "")
# Append to aggregated values variable: Use CONAN_LIBS instead of CONAN_PKG_LIBS to include user appended vars
set(CONAN_LIBS ${CONAN_LIBS} ${CONAN_SYSTEM_LIBS} ${CONAN_FRAMEWORKS_FOUND})


###  Definition of macros and functions ###

macro(conan_define_targets)
    if(${CMAKE_VERSION} VERSION_LESS "3.1.2")
        message(FATAL_ERROR "TARGETS not supported by your CMake version!")
    endif()  # CMAKE > 3.x
    set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} ${CONAN_CMD_CXX_FLAGS}")
    set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} ${CONAN_CMD_C_FLAGS}")
    set(CMAKE_SHARED_LINKER_FLAGS "${CMAKE_SHARED_LINKER_FLAGS} ${CONAN_CMD_SHARED_LINKER_FLAGS}")


    set(_CONAN_PKG_LIBS_C-API_DEPENDENCIES "${CONAN_SYSTEM_LIBS_C-API} ${CONAN_FRAMEWORKS_FOUND_C-API} CONAN_PKG::node")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_C-API_DEPENDENCIES "${_CONAN_PKG_LIBS_C-API_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_C-API}" "${CONAN_LIB_DIRS_C-API}"
                                  CONAN_PACKAGE_TARGETS_C-API "${_CONAN_PKG_LIBS_C-API_DEPENDENCIES}"
                                  "" c-api)
    set(_CONAN_PKG_LIBS_C-API_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_C-API_DEBUG} ${CONAN_FRAMEWORKS_FOUND_C-API_DEBUG} CONAN_PKG::node")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_C-API_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_C-API_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_C-API_DEBUG}" "${CONAN_LIB_DIRS_C-API_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_C-API_DEBUG "${_CONAN_PKG_LIBS_C-API_DEPENDENCIES_DEBUG}"
                                  "debug" c-api)
    set(_CONAN_PKG_LIBS_C-API_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_C-API_RELEASE} ${CONAN_FRAMEWORKS_FOUND_C-API_RELEASE} CONAN_PKG::node")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_C-API_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_C-API_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_C-API_RELEASE}" "${CONAN_LIB_DIRS_C-API_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_C-API_RELEASE "${_CONAN_PKG_LIBS_C-API_DEPENDENCIES_RELEASE}"
                                  "release" c-api)
    set(_CONAN_PKG_LIBS_C-API_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_C-API_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_C-API_RELWITHDEBINFO} CONAN_PKG::node")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_C-API_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_C-API_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_C-API_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_C-API_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_C-API_RELWITHDEBINFO "${_CONAN_PKG_LIBS_C-API_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" c-api)
    set(_CONAN_PKG_LIBS_C-API_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_C-API_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_C-API_MINSIZEREL} CONAN_PKG::node")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_C-API_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_C-API_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_C-API_MINSIZEREL}" "${CONAN_LIB_DIRS_C-API_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_C-API_MINSIZEREL "${_CONAN_PKG_LIBS_C-API_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" c-api)

    add_library(CONAN_PKG::c-api INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::c-api PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_C-API} ${_CONAN_PKG_LIBS_C-API_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_C-API_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_C-API_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_C-API_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_C-API_RELEASE} ${_CONAN_PKG_LIBS_C-API_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_C-API_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_C-API_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_C-API_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_C-API_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_C-API_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_C-API_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_C-API_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_C-API_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_C-API_MINSIZEREL} ${_CONAN_PKG_LIBS_C-API_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_C-API_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_C-API_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_C-API_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_C-API_DEBUG} ${_CONAN_PKG_LIBS_C-API_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_C-API_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_C-API_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_C-API_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::c-api PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_C-API}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_C-API_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_C-API_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_C-API_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_C-API_DEBUG}>)
    set_property(TARGET CONAN_PKG::c-api PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_C-API}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_C-API_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_C-API_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_C-API_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_C-API_DEBUG}>)
    set_property(TARGET CONAN_PKG::c-api PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_C-API_LIST} ${CONAN_CXX_FLAGS_C-API_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_C-API_RELEASE_LIST} ${CONAN_CXX_FLAGS_C-API_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_C-API_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_C-API_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_C-API_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_C-API_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_C-API_DEBUG_LIST}  ${CONAN_CXX_FLAGS_C-API_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_NODE_DEPENDENCIES "${CONAN_SYSTEM_LIBS_NODE} ${CONAN_FRAMEWORKS_FOUND_NODE} CONAN_PKG::blockchain CONAN_PKG::network")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_NODE_DEPENDENCIES "${_CONAN_PKG_LIBS_NODE_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_NODE}" "${CONAN_LIB_DIRS_NODE}"
                                  CONAN_PACKAGE_TARGETS_NODE "${_CONAN_PKG_LIBS_NODE_DEPENDENCIES}"
                                  "" node)
    set(_CONAN_PKG_LIBS_NODE_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_NODE_DEBUG} ${CONAN_FRAMEWORKS_FOUND_NODE_DEBUG} CONAN_PKG::blockchain CONAN_PKG::network")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_NODE_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_NODE_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_NODE_DEBUG}" "${CONAN_LIB_DIRS_NODE_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_NODE_DEBUG "${_CONAN_PKG_LIBS_NODE_DEPENDENCIES_DEBUG}"
                                  "debug" node)
    set(_CONAN_PKG_LIBS_NODE_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_NODE_RELEASE} ${CONAN_FRAMEWORKS_FOUND_NODE_RELEASE} CONAN_PKG::blockchain CONAN_PKG::network")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_NODE_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_NODE_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_NODE_RELEASE}" "${CONAN_LIB_DIRS_NODE_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_NODE_RELEASE "${_CONAN_PKG_LIBS_NODE_DEPENDENCIES_RELEASE}"
                                  "release" node)
    set(_CONAN_PKG_LIBS_NODE_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_NODE_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_NODE_RELWITHDEBINFO} CONAN_PKG::blockchain CONAN_PKG::network")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_NODE_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_NODE_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_NODE_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_NODE_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_NODE_RELWITHDEBINFO "${_CONAN_PKG_LIBS_NODE_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" node)
    set(_CONAN_PKG_LIBS_NODE_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_NODE_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_NODE_MINSIZEREL} CONAN_PKG::blockchain CONAN_PKG::network")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_NODE_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_NODE_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_NODE_MINSIZEREL}" "${CONAN_LIB_DIRS_NODE_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_NODE_MINSIZEREL "${_CONAN_PKG_LIBS_NODE_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" node)

    add_library(CONAN_PKG::node INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::node PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_NODE} ${_CONAN_PKG_LIBS_NODE_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NODE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NODE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_NODE_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_NODE_RELEASE} ${_CONAN_PKG_LIBS_NODE_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NODE_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NODE_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_NODE_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_NODE_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_NODE_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NODE_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NODE_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_NODE_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_NODE_MINSIZEREL} ${_CONAN_PKG_LIBS_NODE_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NODE_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NODE_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_NODE_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_NODE_DEBUG} ${_CONAN_PKG_LIBS_NODE_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NODE_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NODE_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_NODE_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::node PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_NODE}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_NODE_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_NODE_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_NODE_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_NODE_DEBUG}>)
    set_property(TARGET CONAN_PKG::node PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_NODE}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_NODE_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_NODE_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_NODE_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_NODE_DEBUG}>)
    set_property(TARGET CONAN_PKG::node PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_NODE_LIST} ${CONAN_CXX_FLAGS_NODE_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_NODE_RELEASE_LIST} ${CONAN_CXX_FLAGS_NODE_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_NODE_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_NODE_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_NODE_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_NODE_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_NODE_DEBUG_LIST}  ${CONAN_CXX_FLAGS_NODE_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES "${CONAN_SYSTEM_LIBS_BLOCKCHAIN} ${CONAN_FRAMEWORKS_FOUND_BLOCKCHAIN} CONAN_PKG::database CONAN_PKG::consensus")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES "${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BLOCKCHAIN}" "${CONAN_LIB_DIRS_BLOCKCHAIN}"
                                  CONAN_PACKAGE_TARGETS_BLOCKCHAIN "${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES}"
                                  "" blockchain)
    set(_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_BLOCKCHAIN_DEBUG} ${CONAN_FRAMEWORKS_FOUND_BLOCKCHAIN_DEBUG} CONAN_PKG::database CONAN_PKG::consensus")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BLOCKCHAIN_DEBUG}" "${CONAN_LIB_DIRS_BLOCKCHAIN_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_BLOCKCHAIN_DEBUG "${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_DEBUG}"
                                  "debug" blockchain)
    set(_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_BLOCKCHAIN_RELEASE} ${CONAN_FRAMEWORKS_FOUND_BLOCKCHAIN_RELEASE} CONAN_PKG::database CONAN_PKG::consensus")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BLOCKCHAIN_RELEASE}" "${CONAN_LIB_DIRS_BLOCKCHAIN_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_BLOCKCHAIN_RELEASE "${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_RELEASE}"
                                  "release" blockchain)
    set(_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_BLOCKCHAIN_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_BLOCKCHAIN_RELWITHDEBINFO} CONAN_PKG::database CONAN_PKG::consensus")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BLOCKCHAIN_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_BLOCKCHAIN_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_BLOCKCHAIN_RELWITHDEBINFO "${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" blockchain)
    set(_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_BLOCKCHAIN_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_BLOCKCHAIN_MINSIZEREL} CONAN_PKG::database CONAN_PKG::consensus")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BLOCKCHAIN_MINSIZEREL}" "${CONAN_LIB_DIRS_BLOCKCHAIN_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_BLOCKCHAIN_MINSIZEREL "${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" blockchain)

    add_library(CONAN_PKG::blockchain INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::blockchain PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_BLOCKCHAIN} ${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BLOCKCHAIN_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BLOCKCHAIN_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BLOCKCHAIN_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_BLOCKCHAIN_RELEASE} ${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BLOCKCHAIN_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BLOCKCHAIN_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BLOCKCHAIN_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_BLOCKCHAIN_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BLOCKCHAIN_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BLOCKCHAIN_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BLOCKCHAIN_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_BLOCKCHAIN_MINSIZEREL} ${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BLOCKCHAIN_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BLOCKCHAIN_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BLOCKCHAIN_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_BLOCKCHAIN_DEBUG} ${_CONAN_PKG_LIBS_BLOCKCHAIN_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BLOCKCHAIN_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BLOCKCHAIN_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BLOCKCHAIN_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::blockchain PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_BLOCKCHAIN}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_BLOCKCHAIN_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_BLOCKCHAIN_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_BLOCKCHAIN_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_BLOCKCHAIN_DEBUG}>)
    set_property(TARGET CONAN_PKG::blockchain PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_BLOCKCHAIN}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_BLOCKCHAIN_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_BLOCKCHAIN_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_BLOCKCHAIN_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_BLOCKCHAIN_DEBUG}>)
    set_property(TARGET CONAN_PKG::blockchain PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_BLOCKCHAIN_LIST} ${CONAN_CXX_FLAGS_BLOCKCHAIN_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_BLOCKCHAIN_RELEASE_LIST} ${CONAN_CXX_FLAGS_BLOCKCHAIN_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_BLOCKCHAIN_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_BLOCKCHAIN_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_BLOCKCHAIN_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_BLOCKCHAIN_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_BLOCKCHAIN_DEBUG_LIST}  ${CONAN_CXX_FLAGS_BLOCKCHAIN_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES "${CONAN_SYSTEM_LIBS_NETWORK} ${CONAN_FRAMEWORKS_FOUND_NETWORK} CONAN_PKG::domain")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_NETWORK_DEPENDENCIES "${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_NETWORK}" "${CONAN_LIB_DIRS_NETWORK}"
                                  CONAN_PACKAGE_TARGETS_NETWORK "${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES}"
                                  "" network)
    set(_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_NETWORK_DEBUG} ${CONAN_FRAMEWORKS_FOUND_NETWORK_DEBUG} CONAN_PKG::domain")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_NETWORK_DEBUG}" "${CONAN_LIB_DIRS_NETWORK_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_NETWORK_DEBUG "${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_DEBUG}"
                                  "debug" network)
    set(_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_NETWORK_RELEASE} ${CONAN_FRAMEWORKS_FOUND_NETWORK_RELEASE} CONAN_PKG::domain")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_NETWORK_RELEASE}" "${CONAN_LIB_DIRS_NETWORK_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_NETWORK_RELEASE "${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_RELEASE}"
                                  "release" network)
    set(_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_NETWORK_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_NETWORK_RELWITHDEBINFO} CONAN_PKG::domain")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_NETWORK_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_NETWORK_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_NETWORK_RELWITHDEBINFO "${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" network)
    set(_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_NETWORK_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_NETWORK_MINSIZEREL} CONAN_PKG::domain")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_NETWORK_MINSIZEREL}" "${CONAN_LIB_DIRS_NETWORK_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_NETWORK_MINSIZEREL "${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" network)

    add_library(CONAN_PKG::network INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::network PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_NETWORK} ${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NETWORK_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NETWORK_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_NETWORK_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_NETWORK_RELEASE} ${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NETWORK_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NETWORK_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_NETWORK_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_NETWORK_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NETWORK_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NETWORK_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_NETWORK_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_NETWORK_MINSIZEREL} ${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NETWORK_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NETWORK_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_NETWORK_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_NETWORK_DEBUG} ${_CONAN_PKG_LIBS_NETWORK_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NETWORK_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_NETWORK_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_NETWORK_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::network PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_NETWORK}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_NETWORK_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_NETWORK_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_NETWORK_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_NETWORK_DEBUG}>)
    set_property(TARGET CONAN_PKG::network PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_NETWORK}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_NETWORK_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_NETWORK_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_NETWORK_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_NETWORK_DEBUG}>)
    set_property(TARGET CONAN_PKG::network PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_NETWORK_LIST} ${CONAN_CXX_FLAGS_NETWORK_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_NETWORK_RELEASE_LIST} ${CONAN_CXX_FLAGS_NETWORK_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_NETWORK_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_NETWORK_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_NETWORK_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_NETWORK_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_NETWORK_DEBUG_LIST}  ${CONAN_CXX_FLAGS_NETWORK_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES "${CONAN_SYSTEM_LIBS_DATABASE} ${CONAN_FRAMEWORKS_FOUND_DATABASE} CONAN_PKG::lmdb CONAN_PKG::domain")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_DATABASE_DEPENDENCIES "${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_DATABASE}" "${CONAN_LIB_DIRS_DATABASE}"
                                  CONAN_PACKAGE_TARGETS_DATABASE "${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES}"
                                  "" database)
    set(_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_DATABASE_DEBUG} ${CONAN_FRAMEWORKS_FOUND_DATABASE_DEBUG} CONAN_PKG::lmdb CONAN_PKG::domain")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_DATABASE_DEBUG}" "${CONAN_LIB_DIRS_DATABASE_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_DATABASE_DEBUG "${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_DEBUG}"
                                  "debug" database)
    set(_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_DATABASE_RELEASE} ${CONAN_FRAMEWORKS_FOUND_DATABASE_RELEASE} CONAN_PKG::lmdb CONAN_PKG::domain")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_DATABASE_RELEASE}" "${CONAN_LIB_DIRS_DATABASE_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_DATABASE_RELEASE "${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_RELEASE}"
                                  "release" database)
    set(_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_DATABASE_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_DATABASE_RELWITHDEBINFO} CONAN_PKG::lmdb CONAN_PKG::domain")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_DATABASE_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_DATABASE_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_DATABASE_RELWITHDEBINFO "${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" database)
    set(_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_DATABASE_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_DATABASE_MINSIZEREL} CONAN_PKG::lmdb CONAN_PKG::domain")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_DATABASE_MINSIZEREL}" "${CONAN_LIB_DIRS_DATABASE_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_DATABASE_MINSIZEREL "${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" database)

    add_library(CONAN_PKG::database INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::database PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_DATABASE} ${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DATABASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DATABASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_DATABASE_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_DATABASE_RELEASE} ${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DATABASE_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DATABASE_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_DATABASE_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_DATABASE_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DATABASE_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DATABASE_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_DATABASE_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_DATABASE_MINSIZEREL} ${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DATABASE_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DATABASE_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_DATABASE_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_DATABASE_DEBUG} ${_CONAN_PKG_LIBS_DATABASE_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DATABASE_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DATABASE_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_DATABASE_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::database PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_DATABASE}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_DATABASE_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_DATABASE_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_DATABASE_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_DATABASE_DEBUG}>)
    set_property(TARGET CONAN_PKG::database PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_DATABASE}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_DATABASE_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_DATABASE_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_DATABASE_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_DATABASE_DEBUG}>)
    set_property(TARGET CONAN_PKG::database PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_DATABASE_LIST} ${CONAN_CXX_FLAGS_DATABASE_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_DATABASE_RELEASE_LIST} ${CONAN_CXX_FLAGS_DATABASE_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_DATABASE_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_DATABASE_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_DATABASE_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_DATABASE_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_DATABASE_DEBUG_LIST}  ${CONAN_CXX_FLAGS_DATABASE_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES "${CONAN_SYSTEM_LIBS_CONSENSUS} ${CONAN_FRAMEWORKS_FOUND_CONSENSUS} CONAN_PKG::boost CONAN_PKG::secp256k1")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES "${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_CONSENSUS}" "${CONAN_LIB_DIRS_CONSENSUS}"
                                  CONAN_PACKAGE_TARGETS_CONSENSUS "${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES}"
                                  "" consensus)
    set(_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_CONSENSUS_DEBUG} ${CONAN_FRAMEWORKS_FOUND_CONSENSUS_DEBUG} CONAN_PKG::boost CONAN_PKG::secp256k1")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_CONSENSUS_DEBUG}" "${CONAN_LIB_DIRS_CONSENSUS_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_CONSENSUS_DEBUG "${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_DEBUG}"
                                  "debug" consensus)
    set(_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_CONSENSUS_RELEASE} ${CONAN_FRAMEWORKS_FOUND_CONSENSUS_RELEASE} CONAN_PKG::boost CONAN_PKG::secp256k1")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_CONSENSUS_RELEASE}" "${CONAN_LIB_DIRS_CONSENSUS_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_CONSENSUS_RELEASE "${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_RELEASE}"
                                  "release" consensus)
    set(_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_CONSENSUS_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_CONSENSUS_RELWITHDEBINFO} CONAN_PKG::boost CONAN_PKG::secp256k1")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_CONSENSUS_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_CONSENSUS_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_CONSENSUS_RELWITHDEBINFO "${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" consensus)
    set(_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_CONSENSUS_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_CONSENSUS_MINSIZEREL} CONAN_PKG::boost CONAN_PKG::secp256k1")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_CONSENSUS_MINSIZEREL}" "${CONAN_LIB_DIRS_CONSENSUS_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_CONSENSUS_MINSIZEREL "${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" consensus)

    add_library(CONAN_PKG::consensus INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::consensus PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_CONSENSUS} ${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_CONSENSUS_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_CONSENSUS_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_CONSENSUS_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_CONSENSUS_RELEASE} ${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_CONSENSUS_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_CONSENSUS_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_CONSENSUS_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_CONSENSUS_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_CONSENSUS_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_CONSENSUS_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_CONSENSUS_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_CONSENSUS_MINSIZEREL} ${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_CONSENSUS_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_CONSENSUS_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_CONSENSUS_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_CONSENSUS_DEBUG} ${_CONAN_PKG_LIBS_CONSENSUS_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_CONSENSUS_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_CONSENSUS_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_CONSENSUS_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::consensus PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_CONSENSUS}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_CONSENSUS_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_CONSENSUS_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_CONSENSUS_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_CONSENSUS_DEBUG}>)
    set_property(TARGET CONAN_PKG::consensus PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_CONSENSUS}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_CONSENSUS_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_CONSENSUS_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_CONSENSUS_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_CONSENSUS_DEBUG}>)
    set_property(TARGET CONAN_PKG::consensus PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_CONSENSUS_LIST} ${CONAN_CXX_FLAGS_CONSENSUS_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_CONSENSUS_RELEASE_LIST} ${CONAN_CXX_FLAGS_CONSENSUS_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_CONSENSUS_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_CONSENSUS_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_CONSENSUS_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_CONSENSUS_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_CONSENSUS_DEBUG_LIST}  ${CONAN_CXX_FLAGS_CONSENSUS_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_LMDB_DEPENDENCIES "${CONAN_SYSTEM_LIBS_LMDB} ${CONAN_FRAMEWORKS_FOUND_LMDB} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_LMDB_DEPENDENCIES "${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_LMDB}" "${CONAN_LIB_DIRS_LMDB}"
                                  CONAN_PACKAGE_TARGETS_LMDB "${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES}"
                                  "" lmdb)
    set(_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_LMDB_DEBUG} ${CONAN_FRAMEWORKS_FOUND_LMDB_DEBUG} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_LMDB_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_LMDB_DEBUG}" "${CONAN_LIB_DIRS_LMDB_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_LMDB_DEBUG "${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_DEBUG}"
                                  "debug" lmdb)
    set(_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_LMDB_RELEASE} ${CONAN_FRAMEWORKS_FOUND_LMDB_RELEASE} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_LMDB_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_LMDB_RELEASE}" "${CONAN_LIB_DIRS_LMDB_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_LMDB_RELEASE "${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_RELEASE}"
                                  "release" lmdb)
    set(_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_LMDB_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_LMDB_RELWITHDEBINFO} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_LMDB_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_LMDB_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_LMDB_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_LMDB_RELWITHDEBINFO "${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" lmdb)
    set(_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_LMDB_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_LMDB_MINSIZEREL} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_LMDB_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_LMDB_MINSIZEREL}" "${CONAN_LIB_DIRS_LMDB_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_LMDB_MINSIZEREL "${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" lmdb)

    add_library(CONAN_PKG::lmdb INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::lmdb PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_LMDB} ${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LMDB_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LMDB_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_LMDB_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_LMDB_RELEASE} ${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LMDB_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LMDB_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_LMDB_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_LMDB_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LMDB_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LMDB_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_LMDB_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_LMDB_MINSIZEREL} ${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LMDB_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LMDB_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_LMDB_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_LMDB_DEBUG} ${_CONAN_PKG_LIBS_LMDB_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LMDB_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LMDB_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_LMDB_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::lmdb PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_LMDB}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_LMDB_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_LMDB_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_LMDB_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_LMDB_DEBUG}>)
    set_property(TARGET CONAN_PKG::lmdb PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_LMDB}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_LMDB_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_LMDB_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_LMDB_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_LMDB_DEBUG}>)
    set_property(TARGET CONAN_PKG::lmdb PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_LMDB_LIST} ${CONAN_CXX_FLAGS_LMDB_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_LMDB_RELEASE_LIST} ${CONAN_CXX_FLAGS_LMDB_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_LMDB_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_LMDB_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_LMDB_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_LMDB_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_LMDB_DEBUG_LIST}  ${CONAN_CXX_FLAGS_LMDB_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES "${CONAN_SYSTEM_LIBS_DOMAIN} ${CONAN_FRAMEWORKS_FOUND_DOMAIN} CONAN_PKG::algorithm CONAN_PKG::secp256k1 CONAN_PKG::infrastructure")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES "${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_DOMAIN}" "${CONAN_LIB_DIRS_DOMAIN}"
                                  CONAN_PACKAGE_TARGETS_DOMAIN "${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES}"
                                  "" domain)
    set(_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_DOMAIN_DEBUG} ${CONAN_FRAMEWORKS_FOUND_DOMAIN_DEBUG} CONAN_PKG::algorithm CONAN_PKG::secp256k1 CONAN_PKG::infrastructure")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_DOMAIN_DEBUG}" "${CONAN_LIB_DIRS_DOMAIN_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_DOMAIN_DEBUG "${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_DEBUG}"
                                  "debug" domain)
    set(_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_DOMAIN_RELEASE} ${CONAN_FRAMEWORKS_FOUND_DOMAIN_RELEASE} CONAN_PKG::algorithm CONAN_PKG::secp256k1 CONAN_PKG::infrastructure")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_DOMAIN_RELEASE}" "${CONAN_LIB_DIRS_DOMAIN_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_DOMAIN_RELEASE "${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_RELEASE}"
                                  "release" domain)
    set(_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_DOMAIN_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_DOMAIN_RELWITHDEBINFO} CONAN_PKG::algorithm CONAN_PKG::secp256k1 CONAN_PKG::infrastructure")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_DOMAIN_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_DOMAIN_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_DOMAIN_RELWITHDEBINFO "${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" domain)
    set(_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_DOMAIN_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_DOMAIN_MINSIZEREL} CONAN_PKG::algorithm CONAN_PKG::secp256k1 CONAN_PKG::infrastructure")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_DOMAIN_MINSIZEREL}" "${CONAN_LIB_DIRS_DOMAIN_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_DOMAIN_MINSIZEREL "${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" domain)

    add_library(CONAN_PKG::domain INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::domain PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_DOMAIN} ${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DOMAIN_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DOMAIN_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_DOMAIN_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_DOMAIN_RELEASE} ${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DOMAIN_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DOMAIN_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_DOMAIN_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_DOMAIN_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DOMAIN_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DOMAIN_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_DOMAIN_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_DOMAIN_MINSIZEREL} ${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DOMAIN_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DOMAIN_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_DOMAIN_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_DOMAIN_DEBUG} ${_CONAN_PKG_LIBS_DOMAIN_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DOMAIN_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_DOMAIN_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_DOMAIN_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::domain PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_DOMAIN}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_DOMAIN_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_DOMAIN_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_DOMAIN_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_DOMAIN_DEBUG}>)
    set_property(TARGET CONAN_PKG::domain PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_DOMAIN}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_DOMAIN_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_DOMAIN_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_DOMAIN_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_DOMAIN_DEBUG}>)
    set_property(TARGET CONAN_PKG::domain PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_DOMAIN_LIST} ${CONAN_CXX_FLAGS_DOMAIN_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_DOMAIN_RELEASE_LIST} ${CONAN_CXX_FLAGS_DOMAIN_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_DOMAIN_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_DOMAIN_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_DOMAIN_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_DOMAIN_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_DOMAIN_DEBUG_LIST}  ${CONAN_CXX_FLAGS_DOMAIN_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES "${CONAN_SYSTEM_LIBS_ALGORITHM} ${CONAN_FRAMEWORKS_FOUND_ALGORITHM} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES "${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_ALGORITHM}" "${CONAN_LIB_DIRS_ALGORITHM}"
                                  CONAN_PACKAGE_TARGETS_ALGORITHM "${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES}"
                                  "" algorithm)
    set(_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_ALGORITHM_DEBUG} ${CONAN_FRAMEWORKS_FOUND_ALGORITHM_DEBUG} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_ALGORITHM_DEBUG}" "${CONAN_LIB_DIRS_ALGORITHM_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_ALGORITHM_DEBUG "${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_DEBUG}"
                                  "debug" algorithm)
    set(_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_ALGORITHM_RELEASE} ${CONAN_FRAMEWORKS_FOUND_ALGORITHM_RELEASE} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_ALGORITHM_RELEASE}" "${CONAN_LIB_DIRS_ALGORITHM_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_ALGORITHM_RELEASE "${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_RELEASE}"
                                  "release" algorithm)
    set(_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_ALGORITHM_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_ALGORITHM_RELWITHDEBINFO} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_ALGORITHM_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_ALGORITHM_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_ALGORITHM_RELWITHDEBINFO "${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" algorithm)
    set(_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_ALGORITHM_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_ALGORITHM_MINSIZEREL} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_ALGORITHM_MINSIZEREL}" "${CONAN_LIB_DIRS_ALGORITHM_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_ALGORITHM_MINSIZEREL "${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" algorithm)

    add_library(CONAN_PKG::algorithm INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::algorithm PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_ALGORITHM} ${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ALGORITHM_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ALGORITHM_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_ALGORITHM_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_ALGORITHM_RELEASE} ${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ALGORITHM_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ALGORITHM_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_ALGORITHM_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_ALGORITHM_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ALGORITHM_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ALGORITHM_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_ALGORITHM_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_ALGORITHM_MINSIZEREL} ${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ALGORITHM_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ALGORITHM_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_ALGORITHM_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_ALGORITHM_DEBUG} ${_CONAN_PKG_LIBS_ALGORITHM_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ALGORITHM_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ALGORITHM_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_ALGORITHM_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::algorithm PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_ALGORITHM}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_ALGORITHM_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_ALGORITHM_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_ALGORITHM_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_ALGORITHM_DEBUG}>)
    set_property(TARGET CONAN_PKG::algorithm PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_ALGORITHM}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_ALGORITHM_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_ALGORITHM_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_ALGORITHM_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_ALGORITHM_DEBUG}>)
    set_property(TARGET CONAN_PKG::algorithm PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_ALGORITHM_LIST} ${CONAN_CXX_FLAGS_ALGORITHM_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_ALGORITHM_RELEASE_LIST} ${CONAN_CXX_FLAGS_ALGORITHM_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_ALGORITHM_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_ALGORITHM_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_ALGORITHM_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_ALGORITHM_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_ALGORITHM_DEBUG_LIST}  ${CONAN_CXX_FLAGS_ALGORITHM_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES "${CONAN_SYSTEM_LIBS_INFRASTRUCTURE} ${CONAN_FRAMEWORKS_FOUND_INFRASTRUCTURE} CONAN_PKG::secp256k1 CONAN_PKG::boost CONAN_PKG::fmt CONAN_PKG::spdlog")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES "${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_INFRASTRUCTURE}" "${CONAN_LIB_DIRS_INFRASTRUCTURE}"
                                  CONAN_PACKAGE_TARGETS_INFRASTRUCTURE "${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES}"
                                  "" infrastructure)
    set(_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_INFRASTRUCTURE_DEBUG} ${CONAN_FRAMEWORKS_FOUND_INFRASTRUCTURE_DEBUG} CONAN_PKG::secp256k1 CONAN_PKG::boost CONAN_PKG::fmt CONAN_PKG::spdlog")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_INFRASTRUCTURE_DEBUG}" "${CONAN_LIB_DIRS_INFRASTRUCTURE_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_INFRASTRUCTURE_DEBUG "${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_DEBUG}"
                                  "debug" infrastructure)
    set(_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_INFRASTRUCTURE_RELEASE} ${CONAN_FRAMEWORKS_FOUND_INFRASTRUCTURE_RELEASE} CONAN_PKG::secp256k1 CONAN_PKG::boost CONAN_PKG::fmt CONAN_PKG::spdlog")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_INFRASTRUCTURE_RELEASE}" "${CONAN_LIB_DIRS_INFRASTRUCTURE_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_INFRASTRUCTURE_RELEASE "${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_RELEASE}"
                                  "release" infrastructure)
    set(_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_INFRASTRUCTURE_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_INFRASTRUCTURE_RELWITHDEBINFO} CONAN_PKG::secp256k1 CONAN_PKG::boost CONAN_PKG::fmt CONAN_PKG::spdlog")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_INFRASTRUCTURE_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_INFRASTRUCTURE_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_INFRASTRUCTURE_RELWITHDEBINFO "${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" infrastructure)
    set(_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_INFRASTRUCTURE_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_INFRASTRUCTURE_MINSIZEREL} CONAN_PKG::secp256k1 CONAN_PKG::boost CONAN_PKG::fmt CONAN_PKG::spdlog")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_INFRASTRUCTURE_MINSIZEREL}" "${CONAN_LIB_DIRS_INFRASTRUCTURE_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_INFRASTRUCTURE_MINSIZEREL "${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" infrastructure)

    add_library(CONAN_PKG::infrastructure INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::infrastructure PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_INFRASTRUCTURE} ${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_INFRASTRUCTURE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_INFRASTRUCTURE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_INFRASTRUCTURE_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_INFRASTRUCTURE_RELEASE} ${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_INFRASTRUCTURE_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_INFRASTRUCTURE_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_INFRASTRUCTURE_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_INFRASTRUCTURE_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_INFRASTRUCTURE_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_INFRASTRUCTURE_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_INFRASTRUCTURE_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_INFRASTRUCTURE_MINSIZEREL} ${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_INFRASTRUCTURE_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_INFRASTRUCTURE_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_INFRASTRUCTURE_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_INFRASTRUCTURE_DEBUG} ${_CONAN_PKG_LIBS_INFRASTRUCTURE_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_INFRASTRUCTURE_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_INFRASTRUCTURE_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_INFRASTRUCTURE_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::infrastructure PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_INFRASTRUCTURE}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_INFRASTRUCTURE_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_INFRASTRUCTURE_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_INFRASTRUCTURE_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_INFRASTRUCTURE_DEBUG}>)
    set_property(TARGET CONAN_PKG::infrastructure PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_INFRASTRUCTURE}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_INFRASTRUCTURE_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_INFRASTRUCTURE_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_INFRASTRUCTURE_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_INFRASTRUCTURE_DEBUG}>)
    set_property(TARGET CONAN_PKG::infrastructure PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_INFRASTRUCTURE_LIST} ${CONAN_CXX_FLAGS_INFRASTRUCTURE_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_INFRASTRUCTURE_RELEASE_LIST} ${CONAN_CXX_FLAGS_INFRASTRUCTURE_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_INFRASTRUCTURE_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_INFRASTRUCTURE_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_INFRASTRUCTURE_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_INFRASTRUCTURE_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_INFRASTRUCTURE_DEBUG_LIST}  ${CONAN_CXX_FLAGS_INFRASTRUCTURE_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES "${CONAN_SYSTEM_LIBS_SECP256K1} ${CONAN_FRAMEWORKS_FOUND_SECP256K1} CONAN_PKG::gmp")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES "${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_SECP256K1}" "${CONAN_LIB_DIRS_SECP256K1}"
                                  CONAN_PACKAGE_TARGETS_SECP256K1 "${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES}"
                                  "" secp256k1)
    set(_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_SECP256K1_DEBUG} ${CONAN_FRAMEWORKS_FOUND_SECP256K1_DEBUG} CONAN_PKG::gmp")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_SECP256K1_DEBUG}" "${CONAN_LIB_DIRS_SECP256K1_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_SECP256K1_DEBUG "${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_DEBUG}"
                                  "debug" secp256k1)
    set(_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_SECP256K1_RELEASE} ${CONAN_FRAMEWORKS_FOUND_SECP256K1_RELEASE} CONAN_PKG::gmp")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_SECP256K1_RELEASE}" "${CONAN_LIB_DIRS_SECP256K1_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_SECP256K1_RELEASE "${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_RELEASE}"
                                  "release" secp256k1)
    set(_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_SECP256K1_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_SECP256K1_RELWITHDEBINFO} CONAN_PKG::gmp")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_SECP256K1_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_SECP256K1_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_SECP256K1_RELWITHDEBINFO "${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" secp256k1)
    set(_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_SECP256K1_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_SECP256K1_MINSIZEREL} CONAN_PKG::gmp")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_SECP256K1_MINSIZEREL}" "${CONAN_LIB_DIRS_SECP256K1_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_SECP256K1_MINSIZEREL "${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" secp256k1)

    add_library(CONAN_PKG::secp256k1 INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::secp256k1 PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_SECP256K1} ${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SECP256K1_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SECP256K1_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_SECP256K1_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_SECP256K1_RELEASE} ${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SECP256K1_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SECP256K1_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_SECP256K1_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_SECP256K1_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SECP256K1_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SECP256K1_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_SECP256K1_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_SECP256K1_MINSIZEREL} ${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SECP256K1_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SECP256K1_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_SECP256K1_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_SECP256K1_DEBUG} ${_CONAN_PKG_LIBS_SECP256K1_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SECP256K1_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SECP256K1_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_SECP256K1_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::secp256k1 PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_SECP256K1}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_SECP256K1_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_SECP256K1_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_SECP256K1_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_SECP256K1_DEBUG}>)
    set_property(TARGET CONAN_PKG::secp256k1 PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_SECP256K1}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_SECP256K1_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_SECP256K1_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_SECP256K1_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_SECP256K1_DEBUG}>)
    set_property(TARGET CONAN_PKG::secp256k1 PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_SECP256K1_LIST} ${CONAN_CXX_FLAGS_SECP256K1_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_SECP256K1_RELEASE_LIST} ${CONAN_CXX_FLAGS_SECP256K1_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_SECP256K1_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_SECP256K1_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_SECP256K1_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_SECP256K1_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_SECP256K1_DEBUG_LIST}  ${CONAN_CXX_FLAGS_SECP256K1_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_BOOST_DEPENDENCIES "${CONAN_SYSTEM_LIBS_BOOST} ${CONAN_FRAMEWORKS_FOUND_BOOST} CONAN_PKG::zlib CONAN_PKG::bzip2 CONAN_PKG::libbacktrace")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BOOST_DEPENDENCIES "${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BOOST}" "${CONAN_LIB_DIRS_BOOST}"
                                  CONAN_PACKAGE_TARGETS_BOOST "${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES}"
                                  "" boost)
    set(_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_BOOST_DEBUG} ${CONAN_FRAMEWORKS_FOUND_BOOST_DEBUG} CONAN_PKG::zlib CONAN_PKG::bzip2 CONAN_PKG::libbacktrace")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BOOST_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BOOST_DEBUG}" "${CONAN_LIB_DIRS_BOOST_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_BOOST_DEBUG "${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_DEBUG}"
                                  "debug" boost)
    set(_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_BOOST_RELEASE} ${CONAN_FRAMEWORKS_FOUND_BOOST_RELEASE} CONAN_PKG::zlib CONAN_PKG::bzip2 CONAN_PKG::libbacktrace")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BOOST_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BOOST_RELEASE}" "${CONAN_LIB_DIRS_BOOST_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_BOOST_RELEASE "${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_RELEASE}"
                                  "release" boost)
    set(_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_BOOST_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_BOOST_RELWITHDEBINFO} CONAN_PKG::zlib CONAN_PKG::bzip2 CONAN_PKG::libbacktrace")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BOOST_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BOOST_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_BOOST_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_BOOST_RELWITHDEBINFO "${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" boost)
    set(_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_BOOST_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_BOOST_MINSIZEREL} CONAN_PKG::zlib CONAN_PKG::bzip2 CONAN_PKG::libbacktrace")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BOOST_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BOOST_MINSIZEREL}" "${CONAN_LIB_DIRS_BOOST_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_BOOST_MINSIZEREL "${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" boost)

    add_library(CONAN_PKG::boost INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::boost PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_BOOST} ${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BOOST_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BOOST_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BOOST_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_BOOST_RELEASE} ${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BOOST_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BOOST_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BOOST_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_BOOST_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BOOST_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BOOST_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BOOST_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_BOOST_MINSIZEREL} ${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BOOST_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BOOST_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BOOST_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_BOOST_DEBUG} ${_CONAN_PKG_LIBS_BOOST_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BOOST_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BOOST_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BOOST_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::boost PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_BOOST}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_BOOST_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_BOOST_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_BOOST_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_BOOST_DEBUG}>)
    set_property(TARGET CONAN_PKG::boost PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_BOOST}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_BOOST_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_BOOST_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_BOOST_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_BOOST_DEBUG}>)
    set_property(TARGET CONAN_PKG::boost PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_BOOST_LIST} ${CONAN_CXX_FLAGS_BOOST_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_BOOST_RELEASE_LIST} ${CONAN_CXX_FLAGS_BOOST_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_BOOST_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_BOOST_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_BOOST_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_BOOST_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_BOOST_DEBUG_LIST}  ${CONAN_CXX_FLAGS_BOOST_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES "${CONAN_SYSTEM_LIBS_SPDLOG} ${CONAN_FRAMEWORKS_FOUND_SPDLOG} CONAN_PKG::fmt")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES "${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_SPDLOG}" "${CONAN_LIB_DIRS_SPDLOG}"
                                  CONAN_PACKAGE_TARGETS_SPDLOG "${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES}"
                                  "" spdlog)
    set(_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_SPDLOG_DEBUG} ${CONAN_FRAMEWORKS_FOUND_SPDLOG_DEBUG} CONAN_PKG::fmt")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_SPDLOG_DEBUG}" "${CONAN_LIB_DIRS_SPDLOG_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_SPDLOG_DEBUG "${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_DEBUG}"
                                  "debug" spdlog)
    set(_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_SPDLOG_RELEASE} ${CONAN_FRAMEWORKS_FOUND_SPDLOG_RELEASE} CONAN_PKG::fmt")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_SPDLOG_RELEASE}" "${CONAN_LIB_DIRS_SPDLOG_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_SPDLOG_RELEASE "${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_RELEASE}"
                                  "release" spdlog)
    set(_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_SPDLOG_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_SPDLOG_RELWITHDEBINFO} CONAN_PKG::fmt")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_SPDLOG_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_SPDLOG_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_SPDLOG_RELWITHDEBINFO "${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" spdlog)
    set(_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_SPDLOG_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_SPDLOG_MINSIZEREL} CONAN_PKG::fmt")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_SPDLOG_MINSIZEREL}" "${CONAN_LIB_DIRS_SPDLOG_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_SPDLOG_MINSIZEREL "${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" spdlog)

    add_library(CONAN_PKG::spdlog INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::spdlog PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_SPDLOG} ${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SPDLOG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SPDLOG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_SPDLOG_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_SPDLOG_RELEASE} ${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SPDLOG_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SPDLOG_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_SPDLOG_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_SPDLOG_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SPDLOG_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SPDLOG_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_SPDLOG_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_SPDLOG_MINSIZEREL} ${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SPDLOG_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SPDLOG_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_SPDLOG_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_SPDLOG_DEBUG} ${_CONAN_PKG_LIBS_SPDLOG_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SPDLOG_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_SPDLOG_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_SPDLOG_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::spdlog PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_SPDLOG}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_SPDLOG_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_SPDLOG_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_SPDLOG_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_SPDLOG_DEBUG}>)
    set_property(TARGET CONAN_PKG::spdlog PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_SPDLOG}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_SPDLOG_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_SPDLOG_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_SPDLOG_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_SPDLOG_DEBUG}>)
    set_property(TARGET CONAN_PKG::spdlog PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_SPDLOG_LIST} ${CONAN_CXX_FLAGS_SPDLOG_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_SPDLOG_RELEASE_LIST} ${CONAN_CXX_FLAGS_SPDLOG_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_SPDLOG_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_SPDLOG_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_SPDLOG_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_SPDLOG_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_SPDLOG_DEBUG_LIST}  ${CONAN_CXX_FLAGS_SPDLOG_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_GMP_DEPENDENCIES "${CONAN_SYSTEM_LIBS_GMP} ${CONAN_FRAMEWORKS_FOUND_GMP} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_GMP_DEPENDENCIES "${_CONAN_PKG_LIBS_GMP_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_GMP}" "${CONAN_LIB_DIRS_GMP}"
                                  CONAN_PACKAGE_TARGETS_GMP "${_CONAN_PKG_LIBS_GMP_DEPENDENCIES}"
                                  "" gmp)
    set(_CONAN_PKG_LIBS_GMP_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_GMP_DEBUG} ${CONAN_FRAMEWORKS_FOUND_GMP_DEBUG} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_GMP_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_GMP_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_GMP_DEBUG}" "${CONAN_LIB_DIRS_GMP_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_GMP_DEBUG "${_CONAN_PKG_LIBS_GMP_DEPENDENCIES_DEBUG}"
                                  "debug" gmp)
    set(_CONAN_PKG_LIBS_GMP_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_GMP_RELEASE} ${CONAN_FRAMEWORKS_FOUND_GMP_RELEASE} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_GMP_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_GMP_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_GMP_RELEASE}" "${CONAN_LIB_DIRS_GMP_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_GMP_RELEASE "${_CONAN_PKG_LIBS_GMP_DEPENDENCIES_RELEASE}"
                                  "release" gmp)
    set(_CONAN_PKG_LIBS_GMP_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_GMP_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_GMP_RELWITHDEBINFO} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_GMP_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_GMP_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_GMP_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_GMP_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_GMP_RELWITHDEBINFO "${_CONAN_PKG_LIBS_GMP_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" gmp)
    set(_CONAN_PKG_LIBS_GMP_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_GMP_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_GMP_MINSIZEREL} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_GMP_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_GMP_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_GMP_MINSIZEREL}" "${CONAN_LIB_DIRS_GMP_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_GMP_MINSIZEREL "${_CONAN_PKG_LIBS_GMP_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" gmp)

    add_library(CONAN_PKG::gmp INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::gmp PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_GMP} ${_CONAN_PKG_LIBS_GMP_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GMP_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GMP_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_GMP_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_GMP_RELEASE} ${_CONAN_PKG_LIBS_GMP_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GMP_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GMP_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_GMP_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_GMP_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_GMP_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GMP_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GMP_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_GMP_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_GMP_MINSIZEREL} ${_CONAN_PKG_LIBS_GMP_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GMP_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GMP_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_GMP_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_GMP_DEBUG} ${_CONAN_PKG_LIBS_GMP_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GMP_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_GMP_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_GMP_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::gmp PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_GMP}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_GMP_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_GMP_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_GMP_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_GMP_DEBUG}>)
    set_property(TARGET CONAN_PKG::gmp PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_GMP}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_GMP_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_GMP_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_GMP_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_GMP_DEBUG}>)
    set_property(TARGET CONAN_PKG::gmp PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_GMP_LIST} ${CONAN_CXX_FLAGS_GMP_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_GMP_RELEASE_LIST} ${CONAN_CXX_FLAGS_GMP_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_GMP_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_GMP_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_GMP_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_GMP_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_GMP_DEBUG_LIST}  ${CONAN_CXX_FLAGS_GMP_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES "${CONAN_SYSTEM_LIBS_ZLIB} ${CONAN_FRAMEWORKS_FOUND_ZLIB} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_ZLIB_DEPENDENCIES "${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_ZLIB}" "${CONAN_LIB_DIRS_ZLIB}"
                                  CONAN_PACKAGE_TARGETS_ZLIB "${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES}"
                                  "" zlib)
    set(_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_ZLIB_DEBUG} ${CONAN_FRAMEWORKS_FOUND_ZLIB_DEBUG} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_ZLIB_DEBUG}" "${CONAN_LIB_DIRS_ZLIB_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_ZLIB_DEBUG "${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_DEBUG}"
                                  "debug" zlib)
    set(_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_ZLIB_RELEASE} ${CONAN_FRAMEWORKS_FOUND_ZLIB_RELEASE} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_ZLIB_RELEASE}" "${CONAN_LIB_DIRS_ZLIB_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_ZLIB_RELEASE "${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_RELEASE}"
                                  "release" zlib)
    set(_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_ZLIB_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_ZLIB_RELWITHDEBINFO} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_ZLIB_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_ZLIB_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_ZLIB_RELWITHDEBINFO "${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" zlib)
    set(_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_ZLIB_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_ZLIB_MINSIZEREL} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_ZLIB_MINSIZEREL}" "${CONAN_LIB_DIRS_ZLIB_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_ZLIB_MINSIZEREL "${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" zlib)

    add_library(CONAN_PKG::zlib INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::zlib PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_ZLIB} ${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ZLIB_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ZLIB_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_ZLIB_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_ZLIB_RELEASE} ${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ZLIB_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ZLIB_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_ZLIB_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_ZLIB_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ZLIB_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ZLIB_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_ZLIB_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_ZLIB_MINSIZEREL} ${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ZLIB_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ZLIB_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_ZLIB_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_ZLIB_DEBUG} ${_CONAN_PKG_LIBS_ZLIB_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ZLIB_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_ZLIB_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_ZLIB_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::zlib PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_ZLIB}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_ZLIB_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_ZLIB_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_ZLIB_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_ZLIB_DEBUG}>)
    set_property(TARGET CONAN_PKG::zlib PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_ZLIB}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_ZLIB_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_ZLIB_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_ZLIB_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_ZLIB_DEBUG}>)
    set_property(TARGET CONAN_PKG::zlib PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_ZLIB_LIST} ${CONAN_CXX_FLAGS_ZLIB_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_ZLIB_RELEASE_LIST} ${CONAN_CXX_FLAGS_ZLIB_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_ZLIB_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_ZLIB_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_ZLIB_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_ZLIB_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_ZLIB_DEBUG_LIST}  ${CONAN_CXX_FLAGS_ZLIB_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES "${CONAN_SYSTEM_LIBS_BZIP2} ${CONAN_FRAMEWORKS_FOUND_BZIP2} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BZIP2_DEPENDENCIES "${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BZIP2}" "${CONAN_LIB_DIRS_BZIP2}"
                                  CONAN_PACKAGE_TARGETS_BZIP2 "${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES}"
                                  "" bzip2)
    set(_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_BZIP2_DEBUG} ${CONAN_FRAMEWORKS_FOUND_BZIP2_DEBUG} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BZIP2_DEBUG}" "${CONAN_LIB_DIRS_BZIP2_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_BZIP2_DEBUG "${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_DEBUG}"
                                  "debug" bzip2)
    set(_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_BZIP2_RELEASE} ${CONAN_FRAMEWORKS_FOUND_BZIP2_RELEASE} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BZIP2_RELEASE}" "${CONAN_LIB_DIRS_BZIP2_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_BZIP2_RELEASE "${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_RELEASE}"
                                  "release" bzip2)
    set(_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_BZIP2_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_BZIP2_RELWITHDEBINFO} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BZIP2_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_BZIP2_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_BZIP2_RELWITHDEBINFO "${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" bzip2)
    set(_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_BZIP2_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_BZIP2_MINSIZEREL} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_BZIP2_MINSIZEREL}" "${CONAN_LIB_DIRS_BZIP2_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_BZIP2_MINSIZEREL "${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" bzip2)

    add_library(CONAN_PKG::bzip2 INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::bzip2 PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_BZIP2} ${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BZIP2_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BZIP2_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BZIP2_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_BZIP2_RELEASE} ${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BZIP2_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BZIP2_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BZIP2_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_BZIP2_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BZIP2_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BZIP2_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BZIP2_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_BZIP2_MINSIZEREL} ${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BZIP2_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BZIP2_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BZIP2_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_BZIP2_DEBUG} ${_CONAN_PKG_LIBS_BZIP2_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BZIP2_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_BZIP2_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_BZIP2_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::bzip2 PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_BZIP2}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_BZIP2_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_BZIP2_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_BZIP2_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_BZIP2_DEBUG}>)
    set_property(TARGET CONAN_PKG::bzip2 PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_BZIP2}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_BZIP2_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_BZIP2_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_BZIP2_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_BZIP2_DEBUG}>)
    set_property(TARGET CONAN_PKG::bzip2 PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_BZIP2_LIST} ${CONAN_CXX_FLAGS_BZIP2_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_BZIP2_RELEASE_LIST} ${CONAN_CXX_FLAGS_BZIP2_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_BZIP2_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_BZIP2_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_BZIP2_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_BZIP2_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_BZIP2_DEBUG_LIST}  ${CONAN_CXX_FLAGS_BZIP2_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES "${CONAN_SYSTEM_LIBS_LIBBACKTRACE} ${CONAN_FRAMEWORKS_FOUND_LIBBACKTRACE} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES "${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_LIBBACKTRACE}" "${CONAN_LIB_DIRS_LIBBACKTRACE}"
                                  CONAN_PACKAGE_TARGETS_LIBBACKTRACE "${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES}"
                                  "" libbacktrace)
    set(_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_LIBBACKTRACE_DEBUG} ${CONAN_FRAMEWORKS_FOUND_LIBBACKTRACE_DEBUG} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_LIBBACKTRACE_DEBUG}" "${CONAN_LIB_DIRS_LIBBACKTRACE_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_LIBBACKTRACE_DEBUG "${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_DEBUG}"
                                  "debug" libbacktrace)
    set(_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_LIBBACKTRACE_RELEASE} ${CONAN_FRAMEWORKS_FOUND_LIBBACKTRACE_RELEASE} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_LIBBACKTRACE_RELEASE}" "${CONAN_LIB_DIRS_LIBBACKTRACE_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_LIBBACKTRACE_RELEASE "${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_RELEASE}"
                                  "release" libbacktrace)
    set(_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_LIBBACKTRACE_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_LIBBACKTRACE_RELWITHDEBINFO} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_LIBBACKTRACE_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_LIBBACKTRACE_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_LIBBACKTRACE_RELWITHDEBINFO "${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" libbacktrace)
    set(_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_LIBBACKTRACE_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_LIBBACKTRACE_MINSIZEREL} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_LIBBACKTRACE_MINSIZEREL}" "${CONAN_LIB_DIRS_LIBBACKTRACE_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_LIBBACKTRACE_MINSIZEREL "${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" libbacktrace)

    add_library(CONAN_PKG::libbacktrace INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::libbacktrace PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_LIBBACKTRACE} ${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LIBBACKTRACE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LIBBACKTRACE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_LIBBACKTRACE_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_LIBBACKTRACE_RELEASE} ${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LIBBACKTRACE_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LIBBACKTRACE_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_LIBBACKTRACE_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_LIBBACKTRACE_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LIBBACKTRACE_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LIBBACKTRACE_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_LIBBACKTRACE_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_LIBBACKTRACE_MINSIZEREL} ${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LIBBACKTRACE_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LIBBACKTRACE_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_LIBBACKTRACE_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_LIBBACKTRACE_DEBUG} ${_CONAN_PKG_LIBS_LIBBACKTRACE_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LIBBACKTRACE_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_LIBBACKTRACE_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_LIBBACKTRACE_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::libbacktrace PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_LIBBACKTRACE}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_LIBBACKTRACE_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_LIBBACKTRACE_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_LIBBACKTRACE_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_LIBBACKTRACE_DEBUG}>)
    set_property(TARGET CONAN_PKG::libbacktrace PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_LIBBACKTRACE}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_LIBBACKTRACE_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_LIBBACKTRACE_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_LIBBACKTRACE_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_LIBBACKTRACE_DEBUG}>)
    set_property(TARGET CONAN_PKG::libbacktrace PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_LIBBACKTRACE_LIST} ${CONAN_CXX_FLAGS_LIBBACKTRACE_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_LIBBACKTRACE_RELEASE_LIST} ${CONAN_CXX_FLAGS_LIBBACKTRACE_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_LIBBACKTRACE_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_LIBBACKTRACE_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_LIBBACKTRACE_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_LIBBACKTRACE_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_LIBBACKTRACE_DEBUG_LIST}  ${CONAN_CXX_FLAGS_LIBBACKTRACE_DEBUG_LIST}>)


    set(_CONAN_PKG_LIBS_FMT_DEPENDENCIES "${CONAN_SYSTEM_LIBS_FMT} ${CONAN_FRAMEWORKS_FOUND_FMT} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_FMT_DEPENDENCIES "${_CONAN_PKG_LIBS_FMT_DEPENDENCIES}")
    conan_package_library_targets("${CONAN_PKG_LIBS_FMT}" "${CONAN_LIB_DIRS_FMT}"
                                  CONAN_PACKAGE_TARGETS_FMT "${_CONAN_PKG_LIBS_FMT_DEPENDENCIES}"
                                  "" fmt)
    set(_CONAN_PKG_LIBS_FMT_DEPENDENCIES_DEBUG "${CONAN_SYSTEM_LIBS_FMT_DEBUG} ${CONAN_FRAMEWORKS_FOUND_FMT_DEBUG} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_FMT_DEPENDENCIES_DEBUG "${_CONAN_PKG_LIBS_FMT_DEPENDENCIES_DEBUG}")
    conan_package_library_targets("${CONAN_PKG_LIBS_FMT_DEBUG}" "${CONAN_LIB_DIRS_FMT_DEBUG}"
                                  CONAN_PACKAGE_TARGETS_FMT_DEBUG "${_CONAN_PKG_LIBS_FMT_DEPENDENCIES_DEBUG}"
                                  "debug" fmt)
    set(_CONAN_PKG_LIBS_FMT_DEPENDENCIES_RELEASE "${CONAN_SYSTEM_LIBS_FMT_RELEASE} ${CONAN_FRAMEWORKS_FOUND_FMT_RELEASE} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_FMT_DEPENDENCIES_RELEASE "${_CONAN_PKG_LIBS_FMT_DEPENDENCIES_RELEASE}")
    conan_package_library_targets("${CONAN_PKG_LIBS_FMT_RELEASE}" "${CONAN_LIB_DIRS_FMT_RELEASE}"
                                  CONAN_PACKAGE_TARGETS_FMT_RELEASE "${_CONAN_PKG_LIBS_FMT_DEPENDENCIES_RELEASE}"
                                  "release" fmt)
    set(_CONAN_PKG_LIBS_FMT_DEPENDENCIES_RELWITHDEBINFO "${CONAN_SYSTEM_LIBS_FMT_RELWITHDEBINFO} ${CONAN_FRAMEWORKS_FOUND_FMT_RELWITHDEBINFO} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_FMT_DEPENDENCIES_RELWITHDEBINFO "${_CONAN_PKG_LIBS_FMT_DEPENDENCIES_RELWITHDEBINFO}")
    conan_package_library_targets("${CONAN_PKG_LIBS_FMT_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_FMT_RELWITHDEBINFO}"
                                  CONAN_PACKAGE_TARGETS_FMT_RELWITHDEBINFO "${_CONAN_PKG_LIBS_FMT_DEPENDENCIES_RELWITHDEBINFO}"
                                  "relwithdebinfo" fmt)
    set(_CONAN_PKG_LIBS_FMT_DEPENDENCIES_MINSIZEREL "${CONAN_SYSTEM_LIBS_FMT_MINSIZEREL} ${CONAN_FRAMEWORKS_FOUND_FMT_MINSIZEREL} ")
    string(REPLACE " " ";" _CONAN_PKG_LIBS_FMT_DEPENDENCIES_MINSIZEREL "${_CONAN_PKG_LIBS_FMT_DEPENDENCIES_MINSIZEREL}")
    conan_package_library_targets("${CONAN_PKG_LIBS_FMT_MINSIZEREL}" "${CONAN_LIB_DIRS_FMT_MINSIZEREL}"
                                  CONAN_PACKAGE_TARGETS_FMT_MINSIZEREL "${_CONAN_PKG_LIBS_FMT_DEPENDENCIES_MINSIZEREL}"
                                  "minsizerel" fmt)

    add_library(CONAN_PKG::fmt INTERFACE IMPORTED)

    # Property INTERFACE_LINK_FLAGS do not work, necessary to add to INTERFACE_LINK_LIBRARIES
    set_property(TARGET CONAN_PKG::fmt PROPERTY INTERFACE_LINK_LIBRARIES ${CONAN_PACKAGE_TARGETS_FMT} ${_CONAN_PKG_LIBS_FMT_DEPENDENCIES}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_FMT_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_FMT_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_FMT_LIST}>

                                                                 $<$<CONFIG:Release>:${CONAN_PACKAGE_TARGETS_FMT_RELEASE} ${_CONAN_PKG_LIBS_FMT_DEPENDENCIES_RELEASE}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_FMT_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_FMT_RELEASE_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_FMT_RELEASE_LIST}>>

                                                                 $<$<CONFIG:RelWithDebInfo>:${CONAN_PACKAGE_TARGETS_FMT_RELWITHDEBINFO} ${_CONAN_PKG_LIBS_FMT_DEPENDENCIES_RELWITHDEBINFO}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_FMT_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_FMT_RELWITHDEBINFO_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_FMT_RELWITHDEBINFO_LIST}>>

                                                                 $<$<CONFIG:MinSizeRel>:${CONAN_PACKAGE_TARGETS_FMT_MINSIZEREL} ${_CONAN_PKG_LIBS_FMT_DEPENDENCIES_MINSIZEREL}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_FMT_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_FMT_MINSIZEREL_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_FMT_MINSIZEREL_LIST}>>

                                                                 $<$<CONFIG:Debug>:${CONAN_PACKAGE_TARGETS_FMT_DEBUG} ${_CONAN_PKG_LIBS_FMT_DEPENDENCIES_DEBUG}
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,SHARED_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_FMT_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,MODULE_LIBRARY>:${CONAN_SHARED_LINKER_FLAGS_FMT_DEBUG_LIST}>
                                                                 $<$<STREQUAL:$<TARGET_PROPERTY:TYPE>,EXECUTABLE>:${CONAN_EXE_LINKER_FLAGS_FMT_DEBUG_LIST}>>)
    set_property(TARGET CONAN_PKG::fmt PROPERTY INTERFACE_INCLUDE_DIRECTORIES ${CONAN_INCLUDE_DIRS_FMT}
                                                                      $<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_FMT_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_FMT_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_FMT_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_FMT_DEBUG}>)
    set_property(TARGET CONAN_PKG::fmt PROPERTY INTERFACE_COMPILE_DEFINITIONS ${CONAN_COMPILE_DEFINITIONS_FMT}
                                                                      $<$<CONFIG:Release>:${CONAN_COMPILE_DEFINITIONS_FMT_RELEASE}>
                                                                      $<$<CONFIG:RelWithDebInfo>:${CONAN_COMPILE_DEFINITIONS_FMT_RELWITHDEBINFO}>
                                                                      $<$<CONFIG:MinSizeRel>:${CONAN_COMPILE_DEFINITIONS_FMT_MINSIZEREL}>
                                                                      $<$<CONFIG:Debug>:${CONAN_COMPILE_DEFINITIONS_FMT_DEBUG}>)
    set_property(TARGET CONAN_PKG::fmt PROPERTY INTERFACE_COMPILE_OPTIONS ${CONAN_C_FLAGS_FMT_LIST} ${CONAN_CXX_FLAGS_FMT_LIST}
                                                                  $<$<CONFIG:Release>:${CONAN_C_FLAGS_FMT_RELEASE_LIST} ${CONAN_CXX_FLAGS_FMT_RELEASE_LIST}>
                                                                  $<$<CONFIG:RelWithDebInfo>:${CONAN_C_FLAGS_FMT_RELWITHDEBINFO_LIST} ${CONAN_CXX_FLAGS_FMT_RELWITHDEBINFO_LIST}>
                                                                  $<$<CONFIG:MinSizeRel>:${CONAN_C_FLAGS_FMT_MINSIZEREL_LIST} ${CONAN_CXX_FLAGS_FMT_MINSIZEREL_LIST}>
                                                                  $<$<CONFIG:Debug>:${CONAN_C_FLAGS_FMT_DEBUG_LIST}  ${CONAN_CXX_FLAGS_FMT_DEBUG_LIST}>)

    set(CONAN_TARGETS CONAN_PKG::c-api CONAN_PKG::node CONAN_PKG::blockchain CONAN_PKG::network CONAN_PKG::database CONAN_PKG::consensus CONAN_PKG::lmdb CONAN_PKG::domain CONAN_PKG::algorithm CONAN_PKG::infrastructure CONAN_PKG::secp256k1 CONAN_PKG::boost CONAN_PKG::spdlog CONAN_PKG::gmp CONAN_PKG::zlib CONAN_PKG::bzip2 CONAN_PKG::libbacktrace CONAN_PKG::fmt)

endmacro()


macro(conan_basic_setup)
    set(options TARGETS NO_OUTPUT_DIRS SKIP_RPATH KEEP_RPATHS SKIP_STD SKIP_FPIC)
    cmake_parse_arguments(ARGUMENTS "${options}" "${oneValueArgs}" "${multiValueArgs}" ${ARGN} )

    if(CONAN_EXPORTED)
        conan_message(STATUS "Conan: called by CMake conan helper")
    endif()

    if(CONAN_IN_LOCAL_CACHE)
        conan_message(STATUS "Conan: called inside local cache")
    endif()

    if(NOT ARGUMENTS_NO_OUTPUT_DIRS)
        conan_message(STATUS "Conan: Adjusting output directories")
        conan_output_dirs_setup()
    endif()

    if(NOT ARGUMENTS_TARGETS)
        conan_message(STATUS "Conan: Using cmake global configuration")
        conan_global_flags()
    else()
        conan_message(STATUS "Conan: Using cmake targets configuration")
        conan_define_targets()
    endif()

    if(ARGUMENTS_SKIP_RPATH)
        # Change by "DEPRECATION" or "SEND_ERROR" when we are ready
        conan_message(WARNING "Conan: SKIP_RPATH is deprecated, it has been renamed to KEEP_RPATHS")
    endif()

    if(NOT ARGUMENTS_SKIP_RPATH AND NOT ARGUMENTS_KEEP_RPATHS)
        # Parameter has renamed, but we keep the compatibility with old SKIP_RPATH
        conan_set_rpath()
    endif()

    if(NOT ARGUMENTS_SKIP_STD)
        conan_set_std()
    endif()

    if(NOT ARGUMENTS_SKIP_FPIC)
        conan_set_fpic()
    endif()

    conan_check_compiler()
    conan_set_libcxx()
    conan_set_vs_runtime()
    conan_set_find_paths()
    conan_include_build_modules()
    conan_set_find_library_paths()
endmacro()


macro(conan_set_find_paths)
    # CMAKE_MODULE_PATH does not have Debug/Release config, but there are variables
    # CONAN_CMAKE_MODULE_PATH_DEBUG to be used by the consumer
    # CMake can find findXXX.cmake files in the root of packages
    set(CMAKE_MODULE_PATH ${CONAN_CMAKE_MODULE_PATH} ${CMAKE_MODULE_PATH})

    # Make find_package() to work
    set(CMAKE_PREFIX_PATH ${CONAN_CMAKE_MODULE_PATH} ${CMAKE_PREFIX_PATH})

    # Set the find root path (cross build)
    set(CMAKE_FIND_ROOT_PATH ${CONAN_CMAKE_FIND_ROOT_PATH} ${CMAKE_FIND_ROOT_PATH})
    if(CONAN_CMAKE_FIND_ROOT_PATH_MODE_PROGRAM)
        set(CMAKE_FIND_ROOT_PATH_MODE_PROGRAM ${CONAN_CMAKE_FIND_ROOT_PATH_MODE_PROGRAM})
    endif()
    if(CONAN_CMAKE_FIND_ROOT_PATH_MODE_LIBRARY)
        set(CMAKE_FIND_ROOT_PATH_MODE_LIBRARY ${CONAN_CMAKE_FIND_ROOT_PATH_MODE_LIBRARY})
    endif()
    if(CONAN_CMAKE_FIND_ROOT_PATH_MODE_INCLUDE)
        set(CMAKE_FIND_ROOT_PATH_MODE_INCLUDE ${CONAN_CMAKE_FIND_ROOT_PATH_MODE_INCLUDE})
    endif()
endmacro()


macro(conan_set_find_library_paths)
    # CMAKE_INCLUDE_PATH, CMAKE_LIBRARY_PATH does not have Debug/Release config, but there are variables
    # CONAN_INCLUDE_DIRS_DEBUG/RELEASE CONAN_LIB_DIRS_DEBUG/RELEASE to be used by the consumer
    # For find_library
    set(CMAKE_INCLUDE_PATH ${CONAN_INCLUDE_DIRS} ${CMAKE_INCLUDE_PATH})
    set(CMAKE_LIBRARY_PATH ${CONAN_LIB_DIRS} ${CMAKE_LIBRARY_PATH})
endmacro()


macro(conan_set_vs_runtime)
    if(CONAN_LINK_RUNTIME)
        conan_get_policy(CMP0091 policy_0091)
        if(policy_0091 STREQUAL "NEW")
            if(CONAN_LINK_RUNTIME MATCHES "MTd")
                set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreadedDebug")
            elseif(CONAN_LINK_RUNTIME MATCHES "MDd")
                set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreadedDebugDLL")
            elseif(CONAN_LINK_RUNTIME MATCHES "MT")
                set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreaded")
            elseif(CONAN_LINK_RUNTIME MATCHES "MD")
                set(CMAKE_MSVC_RUNTIME_LIBRARY "MultiThreadedDLL")
            endif()
        else()
            foreach(flag CMAKE_C_FLAGS_RELEASE CMAKE_CXX_FLAGS_RELEASE
                         CMAKE_C_FLAGS_RELWITHDEBINFO CMAKE_CXX_FLAGS_RELWITHDEBINFO
                         CMAKE_C_FLAGS_MINSIZEREL CMAKE_CXX_FLAGS_MINSIZEREL)
                if(DEFINED ${flag})
                    string(REPLACE "/MD" ${CONAN_LINK_RUNTIME} ${flag} "${${flag}}")
                endif()
            endforeach()
            foreach(flag CMAKE_C_FLAGS_DEBUG CMAKE_CXX_FLAGS_DEBUG)
                if(DEFINED ${flag})
                    string(REPLACE "/MDd" ${CONAN_LINK_RUNTIME} ${flag} "${${flag}}")
                endif()
            endforeach()
        endif()
    endif()
endmacro()


macro(conan_flags_setup)
    # Macro maintained for backwards compatibility
    conan_set_find_library_paths()
    conan_global_flags()
    conan_set_rpath()
    conan_set_vs_runtime()
    conan_set_libcxx()
endmacro()


function(conan_message MESSAGE_OUTPUT)
    if(NOT CONAN_CMAKE_SILENT_OUTPUT)
        message(${ARGV${0}})
    endif()
endfunction()


function(conan_get_policy policy_id policy)
    if(POLICY "${policy_id}")
        cmake_policy(GET "${policy_id}" _policy)
        set(${policy} "${_policy}" PARENT_SCOPE)
    else()
        set(${policy} "" PARENT_SCOPE)
    endif()
endfunction()


function(conan_find_libraries_abs_path libraries package_libdir libraries_abs_path)
    foreach(_LIBRARY_NAME ${libraries})
        find_library(CONAN_FOUND_LIBRARY NAME ${_LIBRARY_NAME} PATHS ${package_libdir}
                     NO_DEFAULT_PATH NO_CMAKE_FIND_ROOT_PATH)
        if(CONAN_FOUND_LIBRARY)
            conan_message(STATUS "Library ${_LIBRARY_NAME} found ${CONAN_FOUND_LIBRARY}")
            set(CONAN_FULLPATH_LIBS ${CONAN_FULLPATH_LIBS} ${CONAN_FOUND_LIBRARY})
        else()
            conan_message(STATUS "Library ${_LIBRARY_NAME} not found in package, might be system one")
            set(CONAN_FULLPATH_LIBS ${CONAN_FULLPATH_LIBS} ${_LIBRARY_NAME})
        endif()
        unset(CONAN_FOUND_LIBRARY CACHE)
    endforeach()
    set(${libraries_abs_path} ${CONAN_FULLPATH_LIBS} PARENT_SCOPE)
endfunction()


function(conan_package_library_targets libraries package_libdir libraries_abs_path deps build_type package_name)
    unset(_CONAN_ACTUAL_TARGETS CACHE)
    unset(_CONAN_FOUND_SYSTEM_LIBS CACHE)
    foreach(_LIBRARY_NAME ${libraries})
        find_library(CONAN_FOUND_LIBRARY NAME ${_LIBRARY_NAME} PATHS ${package_libdir}
                     NO_DEFAULT_PATH NO_CMAKE_FIND_ROOT_PATH)
        if(CONAN_FOUND_LIBRARY)
            conan_message(STATUS "Library ${_LIBRARY_NAME} found ${CONAN_FOUND_LIBRARY}")
            set(_LIB_NAME CONAN_LIB::${package_name}_${_LIBRARY_NAME}${build_type})
            add_library(${_LIB_NAME} UNKNOWN IMPORTED)
            set_target_properties(${_LIB_NAME} PROPERTIES IMPORTED_LOCATION ${CONAN_FOUND_LIBRARY})
            set(CONAN_FULLPATH_LIBS ${CONAN_FULLPATH_LIBS} ${_LIB_NAME})
            set(_CONAN_ACTUAL_TARGETS ${_CONAN_ACTUAL_TARGETS} ${_LIB_NAME})
        else()
            conan_message(STATUS "Library ${_LIBRARY_NAME} not found in package, might be system one")
            set(CONAN_FULLPATH_LIBS ${CONAN_FULLPATH_LIBS} ${_LIBRARY_NAME})
            set(_CONAN_FOUND_SYSTEM_LIBS "${_CONAN_FOUND_SYSTEM_LIBS};${_LIBRARY_NAME}")
        endif()
        unset(CONAN_FOUND_LIBRARY CACHE)
    endforeach()

    # Add all dependencies to all targets
    string(REPLACE " " ";" deps_list "${deps}")
    foreach(_CONAN_ACTUAL_TARGET ${_CONAN_ACTUAL_TARGETS})
        set_property(TARGET ${_CONAN_ACTUAL_TARGET} PROPERTY INTERFACE_LINK_LIBRARIES "${_CONAN_FOUND_SYSTEM_LIBS};${deps_list}")
    endforeach()

    set(${libraries_abs_path} ${CONAN_FULLPATH_LIBS} PARENT_SCOPE)
endfunction()


macro(conan_set_libcxx)
    if(DEFINED CONAN_LIBCXX)
        conan_message(STATUS "Conan: C++ stdlib: ${CONAN_LIBCXX}")
        if(CONAN_COMPILER STREQUAL "clang" OR CONAN_COMPILER STREQUAL "apple-clang")
            if(CONAN_LIBCXX STREQUAL "libstdc++" OR CONAN_LIBCXX STREQUAL "libstdc++11" )
                set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -stdlib=libstdc++")
            elseif(CONAN_LIBCXX STREQUAL "libc++")
                set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -stdlib=libc++")
            endif()
        endif()
        if(CONAN_COMPILER STREQUAL "sun-cc")
            if(CONAN_LIBCXX STREQUAL "libCstd")
                set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -library=Cstd")
            elseif(CONAN_LIBCXX STREQUAL "libstdcxx")
                set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -library=stdcxx4")
            elseif(CONAN_LIBCXX STREQUAL "libstlport")
                set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -library=stlport4")
            elseif(CONAN_LIBCXX STREQUAL "libstdc++")
                set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -library=stdcpp")
            endif()
        endif()
        if(CONAN_LIBCXX STREQUAL "libstdc++11")
            add_definitions(-D_GLIBCXX_USE_CXX11_ABI=1)
        elseif(CONAN_LIBCXX STREQUAL "libstdc++")
            add_definitions(-D_GLIBCXX_USE_CXX11_ABI=0)
        endif()
    endif()
endmacro()


macro(conan_set_std)
    conan_message(STATUS "Conan: Adjusting language standard")
    # Do not warn "Manually-specified variables were not used by the project"
    set(ignorevar "${CONAN_STD_CXX_FLAG}${CONAN_CMAKE_CXX_STANDARD}${CONAN_CMAKE_CXX_EXTENSIONS}")
    if (CMAKE_VERSION VERSION_LESS "3.1" OR
        (CMAKE_VERSION VERSION_LESS "3.12" AND ("${CONAN_CMAKE_CXX_STANDARD}" STREQUAL "20" OR "${CONAN_CMAKE_CXX_STANDARD}" STREQUAL "gnu20")))
        if(CONAN_STD_CXX_FLAG)
            conan_message(STATUS "Conan setting CXX_FLAGS flags: ${CONAN_STD_CXX_FLAG}")
            set(CMAKE_CXX_FLAGS "${CONAN_STD_CXX_FLAG} ${CMAKE_CXX_FLAGS}")
        endif()
    else()
        if(CONAN_CMAKE_CXX_STANDARD)
            conan_message(STATUS "Conan setting CPP STANDARD: ${CONAN_CMAKE_CXX_STANDARD} WITH EXTENSIONS ${CONAN_CMAKE_CXX_EXTENSIONS}")
            set(CMAKE_CXX_STANDARD ${CONAN_CMAKE_CXX_STANDARD})
            set(CMAKE_CXX_EXTENSIONS ${CONAN_CMAKE_CXX_EXTENSIONS})
        endif()
    endif()
endmacro()


macro(conan_set_rpath)
    conan_message(STATUS "Conan: Adjusting default RPATHs Conan policies")
    if(APPLE)
        # https://cmake.org/Wiki/CMake_RPATH_handling
        # CONAN GUIDE: All generated libraries should have the id and dependencies to other
        # dylibs without path, just the name, EX:
        # libMyLib1.dylib:
        #     libMyLib1.dylib (compatibility version 0.0.0, current version 0.0.0)
        #     libMyLib0.dylib (compatibility version 0.0.0, current version 0.0.0)
        #     /usr/lib/libc++.1.dylib (compatibility version 1.0.0, current version 120.0.0)
        #     /usr/lib/libSystem.B.dylib (compatibility version 1.0.0, current version 1197.1.1)
        # AVOID RPATH FOR *.dylib, ALL LIBS BETWEEN THEM AND THE EXE
        # SHOULD BE ON THE LINKER RESOLVER PATH (./ IS ONE OF THEM)
        set(CMAKE_SKIP_RPATH 1 CACHE BOOL "rpaths" FORCE)
        # Policy CMP0068
        # We want the old behavior, in CMake >= 3.9 CMAKE_SKIP_RPATH won't affect the install_name in OSX
        set(CMAKE_INSTALL_NAME_DIR "")
    endif()
endmacro()


macro(conan_set_fpic)
    if(DEFINED CONAN_CMAKE_POSITION_INDEPENDENT_CODE)
        conan_message(STATUS "Conan: Adjusting fPIC flag (${CONAN_CMAKE_POSITION_INDEPENDENT_CODE})")
        set(CMAKE_POSITION_INDEPENDENT_CODE ${CONAN_CMAKE_POSITION_INDEPENDENT_CODE})
    endif()
endmacro()


macro(conan_output_dirs_setup)
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}/bin)
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_RELEASE ${CMAKE_RUNTIME_OUTPUT_DIRECTORY})
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_RELWITHDEBINFO ${CMAKE_RUNTIME_OUTPUT_DIRECTORY})
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_MINSIZEREL ${CMAKE_RUNTIME_OUTPUT_DIRECTORY})
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_DEBUG ${CMAKE_RUNTIME_OUTPUT_DIRECTORY})

    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}/lib)
    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY_RELEASE ${CMAKE_ARCHIVE_OUTPUT_DIRECTORY})
    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY_RELWITHDEBINFO ${CMAKE_ARCHIVE_OUTPUT_DIRECTORY})
    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY_MINSIZEREL ${CMAKE_ARCHIVE_OUTPUT_DIRECTORY})
    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY_DEBUG ${CMAKE_ARCHIVE_OUTPUT_DIRECTORY})

    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}/lib)
    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_RELEASE ${CMAKE_LIBRARY_OUTPUT_DIRECTORY})
    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_RELWITHDEBINFO ${CMAKE_LIBRARY_OUTPUT_DIRECTORY})
    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_MINSIZEREL ${CMAKE_LIBRARY_OUTPUT_DIRECTORY})
    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY_DEBUG ${CMAKE_LIBRARY_OUTPUT_DIRECTORY})
endmacro()


macro(conan_split_version VERSION_STRING MAJOR MINOR)
    #make a list from the version string
    string(REPLACE "." ";" VERSION_LIST "${VERSION_STRING}")

    #write output values
    list(LENGTH VERSION_LIST _version_len)
    list(GET VERSION_LIST 0 ${MAJOR})
    if(${_version_len} GREATER 1)
        list(GET VERSION_LIST 1 ${MINOR})
    endif()
endmacro()


macro(conan_error_compiler_version)
    message(FATAL_ERROR "Detected a mismatch for the compiler version between your conan profile settings and CMake: \n"
                        "Compiler version specified in your conan profile: ${CONAN_COMPILER_VERSION}\n"
                        "Compiler version detected in CMake: ${VERSION_MAJOR}.${VERSION_MINOR}\n"
                        "Please check your conan profile settings (conan profile show [default|your_profile_name])\n"
                        "P.S. You may set CONAN_DISABLE_CHECK_COMPILER CMake variable in order to disable this check."
           )
endmacro()

set(_CONAN_CURRENT_DIR ${CMAKE_CURRENT_LIST_DIR})

function(conan_get_compiler CONAN_INFO_COMPILER CONAN_INFO_COMPILER_VERSION)
    conan_message(STATUS "Current conanbuildinfo.cmake directory: " ${_CONAN_CURRENT_DIR})
    if(NOT EXISTS ${_CONAN_CURRENT_DIR}/conaninfo.txt)
        conan_message(STATUS "WARN: conaninfo.txt not found")
        return()
    endif()

    file (READ "${_CONAN_CURRENT_DIR}/conaninfo.txt" CONANINFO)

    # MATCHALL will match all, including the last one, which is the full_settings one
    string(REGEX MATCH "full_settings.*" _FULL_SETTINGS_MATCHED ${CONANINFO})
    string(REGEX MATCH "compiler=([-A-Za-z0-9_ ]+)" _MATCHED ${_FULL_SETTINGS_MATCHED})
    if(DEFINED CMAKE_MATCH_1)
        string(STRIP "${CMAKE_MATCH_1}" _CONAN_INFO_COMPILER)
        set(${CONAN_INFO_COMPILER} ${_CONAN_INFO_COMPILER} PARENT_SCOPE)
    endif()

    string(REGEX MATCH "compiler.version=([-A-Za-z0-9_.]+)" _MATCHED ${_FULL_SETTINGS_MATCHED})
    if(DEFINED CMAKE_MATCH_1)
        string(STRIP "${CMAKE_MATCH_1}" _CONAN_INFO_COMPILER_VERSION)
        set(${CONAN_INFO_COMPILER_VERSION} ${_CONAN_INFO_COMPILER_VERSION} PARENT_SCOPE)
    endif()
endfunction()


function(check_compiler_version)
    conan_split_version(${CMAKE_CXX_COMPILER_VERSION} VERSION_MAJOR VERSION_MINOR)
    if(DEFINED CONAN_SETTINGS_COMPILER_TOOLSET)
       conan_message(STATUS "Conan: Skipping compiler check: Declared 'compiler.toolset'")
       return()
    endif()
    if(CMAKE_CXX_COMPILER_ID MATCHES MSVC)
        # MSVC_VERSION is defined since 2.8.2 at least
        # https://cmake.org/cmake/help/v2.8.2/cmake.html#variable:MSVC_VERSION
        # https://cmake.org/cmake/help/v3.14/variable/MSVC_VERSION.html
        if(
            # 1930 = VS 17.0 (v143 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "17" AND NOT((MSVC_VERSION EQUAL 1930) OR (MSVC_VERSION GREATER 1930))) OR
            # 1920-1929 = VS 16.0 (v142 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "16" AND NOT((MSVC_VERSION GREATER 1919) AND (MSVC_VERSION LESS 1930))) OR
            # 1910-1919 = VS 15.0 (v141 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "15" AND NOT((MSVC_VERSION GREATER 1909) AND (MSVC_VERSION LESS 1920))) OR
            # 1900      = VS 14.0 (v140 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "14" AND NOT(MSVC_VERSION EQUAL 1900)) OR
            # 1800      = VS 12.0 (v120 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "12" AND NOT VERSION_MAJOR STREQUAL "18") OR
            # 1700      = VS 11.0 (v110 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "11" AND NOT VERSION_MAJOR STREQUAL "17") OR
            # 1600      = VS 10.0 (v100 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "10" AND NOT VERSION_MAJOR STREQUAL "16") OR
            # 1500      = VS  9.0 (v90 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "9" AND NOT VERSION_MAJOR STREQUAL "15") OR
            # 1400      = VS  8.0 (v80 toolset)
            (CONAN_COMPILER_VERSION STREQUAL "8" AND NOT VERSION_MAJOR STREQUAL "14") OR
            # 1310      = VS  7.1, 1300      = VS  7.0
            (CONAN_COMPILER_VERSION STREQUAL "7" AND NOT VERSION_MAJOR STREQUAL "13") OR
            # 1200      = VS  6.0
            (CONAN_COMPILER_VERSION STREQUAL "6" AND NOT VERSION_MAJOR STREQUAL "12") )
            conan_error_compiler_version()
        endif()
    elseif(CONAN_COMPILER STREQUAL "gcc")
        conan_split_version(${CONAN_COMPILER_VERSION} CONAN_COMPILER_MAJOR CONAN_COMPILER_MINOR)
        set(_CHECK_VERSION ${VERSION_MAJOR}.${VERSION_MINOR})
        set(_CONAN_VERSION ${CONAN_COMPILER_MAJOR}.${CONAN_COMPILER_MINOR})
        if(NOT ${CONAN_COMPILER_VERSION} VERSION_LESS 5.0)
            conan_message(STATUS "Conan: Compiler GCC>=5, checking major version ${CONAN_COMPILER_VERSION}")
            conan_split_version(${CONAN_COMPILER_VERSION} CONAN_COMPILER_MAJOR CONAN_COMPILER_MINOR)
            if("${CONAN_COMPILER_MINOR}" STREQUAL "")
                set(_CHECK_VERSION ${VERSION_MAJOR})
                set(_CONAN_VERSION ${CONAN_COMPILER_MAJOR})
            endif()
        endif()
        conan_message(STATUS "Conan: Checking correct version: ${_CHECK_VERSION}")
        if(NOT ${_CHECK_VERSION} VERSION_EQUAL ${_CONAN_VERSION})
            conan_error_compiler_version()
        endif()
    elseif(CONAN_COMPILER STREQUAL "clang")
        conan_split_version(${CONAN_COMPILER_VERSION} CONAN_COMPILER_MAJOR CONAN_COMPILER_MINOR)
        set(_CHECK_VERSION ${VERSION_MAJOR}.${VERSION_MINOR})
        set(_CONAN_VERSION ${CONAN_COMPILER_MAJOR}.${CONAN_COMPILER_MINOR})
        if(NOT ${CONAN_COMPILER_VERSION} VERSION_LESS 8.0)
            conan_message(STATUS "Conan: Compiler Clang>=8, checking major version ${CONAN_COMPILER_VERSION}")
            if("${CONAN_COMPILER_MINOR}" STREQUAL "")
                set(_CHECK_VERSION ${VERSION_MAJOR})
                set(_CONAN_VERSION ${CONAN_COMPILER_MAJOR})
            endif()
        endif()
        conan_message(STATUS "Conan: Checking correct version: ${_CHECK_VERSION}")
        if(NOT ${_CHECK_VERSION} VERSION_EQUAL ${_CONAN_VERSION})
            conan_error_compiler_version()
        endif()
    elseif(CONAN_COMPILER STREQUAL "apple-clang" OR CONAN_COMPILER STREQUAL "sun-cc" OR CONAN_COMPILER STREQUAL "mcst-lcc")
        conan_split_version(${CONAN_COMPILER_VERSION} CONAN_COMPILER_MAJOR CONAN_COMPILER_MINOR)
        if(${CONAN_COMPILER_MAJOR} VERSION_GREATER_EQUAL "13" AND "${CONAN_COMPILER_MINOR}" STREQUAL "" AND ${CONAN_COMPILER_MAJOR} VERSION_EQUAL ${VERSION_MAJOR})
           # This is correct,  13.X is considered 13
        elseif(NOT ${VERSION_MAJOR}.${VERSION_MINOR} VERSION_EQUAL ${CONAN_COMPILER_MAJOR}.${CONAN_COMPILER_MINOR})
           conan_error_compiler_version()
        endif()
    elseif(CONAN_COMPILER STREQUAL "intel")
        conan_split_version(${CONAN_COMPILER_VERSION} CONAN_COMPILER_MAJOR CONAN_COMPILER_MINOR)
        if(NOT ${CONAN_COMPILER_VERSION} VERSION_LESS 19.1)
            if(NOT ${VERSION_MAJOR}.${VERSION_MINOR} VERSION_EQUAL ${CONAN_COMPILER_MAJOR}.${CONAN_COMPILER_MINOR})
               conan_error_compiler_version()
            endif()
        else()
            if(NOT ${VERSION_MAJOR} VERSION_EQUAL ${CONAN_COMPILER_MAJOR})
               conan_error_compiler_version()
            endif()
        endif()
    else()
        conan_message(STATUS "WARN: Unknown compiler '${CONAN_COMPILER}', skipping the version check...")
    endif()
endfunction()


function(conan_check_compiler)
    if(CONAN_DISABLE_CHECK_COMPILER)
        conan_message(STATUS "WARN: Disabled conan compiler checks")
        return()
    endif()
    if(NOT DEFINED CMAKE_CXX_COMPILER_ID)
        if(DEFINED CMAKE_C_COMPILER_ID)
            conan_message(STATUS "This project seems to be plain C, using '${CMAKE_C_COMPILER_ID}' compiler")
            set(CMAKE_CXX_COMPILER_ID ${CMAKE_C_COMPILER_ID})
            set(CMAKE_CXX_COMPILER_VERSION ${CMAKE_C_COMPILER_VERSION})
        else()
            message(FATAL_ERROR "This project seems to be plain C, but no compiler defined")
        endif()
    endif()
    if(NOT CMAKE_CXX_COMPILER_ID AND NOT CMAKE_C_COMPILER_ID)
        # This use case happens when compiler is not identified by CMake, but the compilers are there and work
        conan_message(STATUS "*** WARN: CMake was not able to identify a C or C++ compiler ***")
        conan_message(STATUS "*** WARN: Disabling compiler checks. Please make sure your settings match your environment ***")
        return()
    endif()
    if(NOT DEFINED CONAN_COMPILER)
        conan_get_compiler(CONAN_COMPILER CONAN_COMPILER_VERSION)
        if(NOT DEFINED CONAN_COMPILER)
            conan_message(STATUS "WARN: CONAN_COMPILER variable not set, please make sure yourself that "
                          "your compiler and version matches your declared settings")
            return()
        endif()
    endif()

    if(NOT CMAKE_HOST_SYSTEM_NAME STREQUAL ${CMAKE_SYSTEM_NAME})
        set(CROSS_BUILDING 1)
    endif()

    # If using VS, verify toolset
    if (CONAN_COMPILER STREQUAL "Visual Studio")
        if (CONAN_SETTINGS_COMPILER_TOOLSET MATCHES "LLVM" OR
            CONAN_SETTINGS_COMPILER_TOOLSET MATCHES "llvm" OR
            CONAN_SETTINGS_COMPILER_TOOLSET MATCHES "clang" OR
            CONAN_SETTINGS_COMPILER_TOOLSET MATCHES "Clang")
            set(EXPECTED_CMAKE_CXX_COMPILER_ID "Clang")
        elseif (CONAN_SETTINGS_COMPILER_TOOLSET MATCHES "Intel")
            set(EXPECTED_CMAKE_CXX_COMPILER_ID "Intel")
        else()
            set(EXPECTED_CMAKE_CXX_COMPILER_ID "MSVC")
        endif()

        if (NOT CMAKE_CXX_COMPILER_ID MATCHES ${EXPECTED_CMAKE_CXX_COMPILER_ID})
            message(FATAL_ERROR "Incorrect '${CONAN_COMPILER}'. Toolset specifies compiler as '${EXPECTED_CMAKE_CXX_COMPILER_ID}' "
                                "but CMake detected '${CMAKE_CXX_COMPILER_ID}'")
        endif()

    # Avoid checks when cross compiling, apple-clang crashes because its APPLE but not apple-clang
    # Actually CMake is detecting "clang" when you are using apple-clang, only if CMP0025 is set to NEW will detect apple-clang
    elseif((CONAN_COMPILER STREQUAL "gcc" AND NOT CMAKE_CXX_COMPILER_ID MATCHES "GNU") OR
        (CONAN_COMPILER STREQUAL "apple-clang" AND NOT CROSS_BUILDING AND (NOT APPLE OR NOT CMAKE_CXX_COMPILER_ID MATCHES "Clang")) OR
        (CONAN_COMPILER STREQUAL "clang" AND NOT CMAKE_CXX_COMPILER_ID MATCHES "Clang") OR
        (CONAN_COMPILER STREQUAL "sun-cc" AND NOT CMAKE_CXX_COMPILER_ID MATCHES "SunPro") )
        message(FATAL_ERROR "Incorrect '${CONAN_COMPILER}', is not the one detected by CMake: '${CMAKE_CXX_COMPILER_ID}'")
    endif()


    if(NOT DEFINED CONAN_COMPILER_VERSION)
        conan_message(STATUS "WARN: CONAN_COMPILER_VERSION variable not set, please make sure yourself "
                             "that your compiler version matches your declared settings")
        return()
    endif()
    check_compiler_version()
endfunction()


macro(conan_set_flags build_type)
    set(CMAKE_CXX_FLAGS${build_type} "${CMAKE_CXX_FLAGS${build_type}} ${CONAN_CXX_FLAGS${build_type}}")
    set(CMAKE_C_FLAGS${build_type} "${CMAKE_C_FLAGS${build_type}} ${CONAN_C_FLAGS${build_type}}")
    set(CMAKE_SHARED_LINKER_FLAGS${build_type} "${CMAKE_SHARED_LINKER_FLAGS${build_type}} ${CONAN_SHARED_LINKER_FLAGS${build_type}}")
    set(CMAKE_EXE_LINKER_FLAGS${build_type} "${CMAKE_EXE_LINKER_FLAGS${build_type}} ${CONAN_EXE_LINKER_FLAGS${build_type}}")
endmacro()


macro(conan_global_flags)
    if(CONAN_SYSTEM_INCLUDES)
        include_directories(SYSTEM ${CONAN_INCLUDE_DIRS}
                                   "$<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_RELEASE}>"
                                   "$<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_RELWITHDEBINFO}>"
                                   "$<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_MINSIZEREL}>"
                                   "$<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_DEBUG}>")
    else()
        include_directories(${CONAN_INCLUDE_DIRS}
                            "$<$<CONFIG:Release>:${CONAN_INCLUDE_DIRS_RELEASE}>"
                            "$<$<CONFIG:RelWithDebInfo>:${CONAN_INCLUDE_DIRS_RELWITHDEBINFO}>"
                            "$<$<CONFIG:MinSizeRel>:${CONAN_INCLUDE_DIRS_MINSIZEREL}>"
                            "$<$<CONFIG:Debug>:${CONAN_INCLUDE_DIRS_DEBUG}>")
    endif()

    link_directories(${CONAN_LIB_DIRS})

    conan_find_libraries_abs_path("${CONAN_LIBS_DEBUG}" "${CONAN_LIB_DIRS_DEBUG}"
                                  CONAN_LIBS_DEBUG)
    conan_find_libraries_abs_path("${CONAN_LIBS_RELEASE}" "${CONAN_LIB_DIRS_RELEASE}"
                                  CONAN_LIBS_RELEASE)
    conan_find_libraries_abs_path("${CONAN_LIBS_RELWITHDEBINFO}" "${CONAN_LIB_DIRS_RELWITHDEBINFO}"
                                  CONAN_LIBS_RELWITHDEBINFO)
    conan_find_libraries_abs_path("${CONAN_LIBS_MINSIZEREL}" "${CONAN_LIB_DIRS_MINSIZEREL}"
                                  CONAN_LIBS_MINSIZEREL)

    add_compile_options(${CONAN_DEFINES}
                        "$<$<CONFIG:Debug>:${CONAN_DEFINES_DEBUG}>"
                        "$<$<CONFIG:Release>:${CONAN_DEFINES_RELEASE}>"
                        "$<$<CONFIG:RelWithDebInfo>:${CONAN_DEFINES_RELWITHDEBINFO}>"
                        "$<$<CONFIG:MinSizeRel>:${CONAN_DEFINES_MINSIZEREL}>")

    conan_set_flags("")
    conan_set_flags("_RELEASE")
    conan_set_flags("_DEBUG")

endmacro()


macro(conan_target_link_libraries target)
    if(CONAN_TARGETS)
        target_link_libraries(${target} ${CONAN_TARGETS})
    else()
        target_link_libraries(${target} ${CONAN_LIBS})
        foreach(_LIB ${CONAN_LIBS_RELEASE})
            target_link_libraries(${target} optimized ${_LIB})
        endforeach()
        foreach(_LIB ${CONAN_LIBS_DEBUG})
            target_link_libraries(${target} debug ${_LIB})
        endforeach()
    endif()
endmacro()


macro(conan_include_build_modules)
    if(CMAKE_BUILD_TYPE)
        if(${CMAKE_BUILD_TYPE} MATCHES "Debug")
            set(CONAN_BUILD_MODULES_PATHS ${CONAN_BUILD_MODULES_PATHS_DEBUG} ${CONAN_BUILD_MODULES_PATHS})
        elseif(${CMAKE_BUILD_TYPE} MATCHES "Release")
            set(CONAN_BUILD_MODULES_PATHS ${CONAN_BUILD_MODULES_PATHS_RELEASE} ${CONAN_BUILD_MODULES_PATHS})
        elseif(${CMAKE_BUILD_TYPE} MATCHES "RelWithDebInfo")
            set(CONAN_BUILD_MODULES_PATHS ${CONAN_BUILD_MODULES_PATHS_RELWITHDEBINFO} ${CONAN_BUILD_MODULES_PATHS})
        elseif(${CMAKE_BUILD_TYPE} MATCHES "MinSizeRel")
            set(CONAN_BUILD_MODULES_PATHS ${CONAN_BUILD_MODULES_PATHS_MINSIZEREL} ${CONAN_BUILD_MODULES_PATHS})
        endif()
    endif()

    foreach(_BUILD_MODULE_PATH ${CONAN_BUILD_MODULES_PATHS})
        include(${_BUILD_MODULE_PATH})
    endforeach()
endmacro()


### Definition of user declared vars (user_info) ###

set(CONAN_USER_BOOST_stacktrace_addr2line_available "True")