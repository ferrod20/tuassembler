%define NULL    0

global _Circular

extern _insertarint

section .data

	puntero           dd        NULL
	
	struc Listaint
		.nodo      resd     1
		.siguiente  resd    1
	endstruc
	

section .text

 _Circular:
	push ebp
	mov ebp,esp
	push esi
	push edi
	push ebx

	; void insertarint(struct Listaint **cabeza, int valor)
	
	
;	push dword 1
;	push dword puntero
;	call _insertarint
;	add esp, 8
	
;	mov eax, [puntero]
;	mov dword [eax + Listaint.siguiente], eax ; Aca lo hago circular

;	mov dword[ebp], eax
	
	pop ebx
	pop edi
	pop esi
	pop ebp
	ret
