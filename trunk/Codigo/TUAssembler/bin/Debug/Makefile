all: timer.o codigoProbador.o
	@echo ......................................................
	@echo ...........ORGANIZACION.DEL.COMPUTADOR.ii.............
	@echo ...............Trabajo.Practico.Final.................
	@echo ..............Makefile.Version.Linux..................
	@echo ......................................................
	gcc -o prueba.exe timer.o codigoProbador.o
clean:
	rm *.o prueba
timer.o: timer.asm
	nasm -fcoff timer.asm
codigoProbador.o: codigoProbador.c
	gcc -c -o codigoProbador.o codigoProbador.c

#TPLinux: tpbmp.o
#	ld -s tpbmp.o -o prueba
#main.o: main.c
#	gcc -c -O2 -o main.o main.c
#tpbmp.o: tpbmp.asm macrosLinux.asm macrosC.asm
#	nasm -f elf -o tpbmp.o tpbmp.asm
