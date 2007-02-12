global _funcionAsm
global _funcion1

extern _printf

section .data
	formatoChar db 'Esto es un Byte(char)= %c',10,0

section .text

 _funcion1:
	push ebp
	mov ebp,esp
	push esi
	push edi
	push ebx
	
	push word [ebp+8]
	push dword formatoChar
	call _printf
	add esp,6	; quito el puntero al formato y un word(la minima unidad para pushear-popear es 16 bits, por lo que el byte a mostrar tiene otro de desperdicio)

	mov eax, 100
	pop ebx
	pop edi
	pop esi
	pop ebp
	ret
