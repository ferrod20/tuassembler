global _TomaUnInt8
global _TomaUnInt16
global _TomaUnInt32
global _TomaUnIEEE32
global _TomaUnIEEE64
global _TomaUnBool

extern _printf
extern _floatToDouble

section .data
	formatoChar db 'Esto es un Byte(char)= %c',10,0
	formatoShort db 'Esto es un Word= %d',10,0
	formatoInt db 'Esto es un DWord= %d',10,0
	formatoFloat db 'Esto es un IEEE32= %f',10,0
	formatoDouble db 'Esto es un IEEE64= %f',10,0
	formatBoolTrue db 'Este booleano es = True',10,0
	formatBoolFalse db 'Este booleano es = False',10,0

section .text
  _TomaUnInt8:
	push ebp
	mov ebp,esp
	push esi
	push edi
	push ebx
	
	push word [ebp+8]
	push dword formatoChar
	call _printf
	add esp,6	; quito el puntero al formato y un word(la minima unidad para pushear-popear es 16 bits, por lo que el byte a mostrar tiene otro de desperdicio)
	
	pop ebx
	pop edi
	pop esi
	pop ebp
	ret


  _TomaUnInt16:
	push ebp
	mov ebp,esp
	push esi
	push edi
	push ebx
	
	; La cuestion es que printf mirara todo el dword y no el word bajo. Por eso debe limpiarse la parte alta.
	; Ademas si es negativo debe convertirse a complemento a 2.
	mov eax, [ebp+8]
	and eax, 0FFFFh
	test eax, 08000h 
	je EsPositivo
		neg eax
		and eax, 0FFFFh
		neg eax
	EsPositivo:

	push eax
	push dword formatoShort
	call _printf
	add esp,8
	
	pop ebx
	pop edi
	pop esi
	pop ebp
	ret



  _TomaUnInt32:
	push ebp
	mov ebp,esp
	push esi
	push edi
	push ebx
	
	push dword [ebp+8]
	push dword formatoInt
	call _printf
	add esp,8
	
	pop ebx
	pop edi
	pop esi
	pop ebp
	ret


  _TomaUnIEEE32:
	push ebp
	mov ebp,esp
	push esi
	push edi
	push ebx
	
	push dword [ebp+8]
	call _floatToDouble
	add esp, 4

	push dword edx 
	push dword ebx
	push dword formatoFloat
	call _printf
	add esp,12

	pop ebx
	pop edi
	pop esi
	pop ebp
	ret


  _TomaUnIEEE64:
	push ebp
	mov ebp,esp
	push esi
	push edi
	push ebx
	
	push dword [ebp+12]
	push dword [ebp+8]
	push dword formatoDouble
	call _printf
	add esp,12
	
	pop ebx
	pop edi
	pop esi
	pop ebp
	ret

  _TomaUnBool:
	push ebp
	mov ebp,esp
	push esi
	push edi
	push ebx
	
	; de esta manera un lenguaje de alto nivel verifica si es true o false(estoy casi seguro :-P)
	mov eax, [ebp+8]
	cmp eax, 0
	je esFalse
		push dword formatBoolTrue
		jmp mostrarBool
	esFalse:
		push dword formatBoolFalse
	mostrarBool:

	call _printf
	add esp,4
	
	pop ebx
	pop edi
	pop esi
	pop ebp
	ret

