@echo off
del TestTiposBasicos.o
del a.*
nasm -fcoff TestTiposBasicos.asm
gcc test.c TestTiposBasicos.o
a.exe
@echo on
