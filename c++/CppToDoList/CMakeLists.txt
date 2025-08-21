cmake_minimum_required(VERSION 3.21)
project(helloworld LANGUAGES CXX)
set(CMAKE_CXX_STANDARD 17)

find_package(wxWidgets CONFIG REQUIRED)

add_executable(helloworld WIN32 src/main.cpp)
target_link_libraries(helloworld PRIVATE wx::core wx::base)