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

	mov eax, [ebp+8]
	add eax, [ebp+12]
	
	pop ebx
	pop edi
	pop esi
	pop ebp
	ret
