global _funcionAsm
global _funcion1
extern _liberarint
extern _crearint
extern _insertarint
section .data
	formatoChar db 'Esto es un Byte(char)= %c',10,0

section .text

 _funcion1:
	push ebp
	mov ebp,esp
	push esi
	push edi
	push ebx
			
	push dword [ebp+8]
	call _liberarint
	add esp, 4
	
	push dword [ebp+8]
	call _crearint
	add esp, 4

	push dword 1
	push dword [ebp+8]
	call _insertarint
	add esp, 8

	push dword 2
	push dword [ebp+8]	
	call _insertarint
	add esp, 8

	push dword 3
	push dword [ebp+8]	
	call _insertarint
	add esp, 8

	mov eax, [ebp+8]

	mov eax, 8

	pop ebx
	pop edi
	pop esi
	pop ebp
	ret
