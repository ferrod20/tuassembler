global funcionAsm

extern printf

section .data
formato db '%s %d',10,0

section .text
funcionAsm:
push ebp
mov ebp,esp
push esi
push edi
push ebx
push dword [ebp+12] ;param2
push dword [ebp+8] ;param1
push dword formato
call printf
add esp,12
xor eax,eax ;devuelve 0
pop ebx
pop edi
pop esi
pop ebp
ret