//#include "listas.h"
#define bool int
#define true 1
#define false 0


extern struct Listaint
{
   int nodo;
   struct Listaint *siguiente;
};
extern void crearint(struct Listaint **cabeza);
extern void insertarint(struct Listaint **cabeza, int valor, bool contabilizarPedidoMemoria);
extern void liberarint(struct Listaint **cabeza);

//a)
unsigned int Circular( struct Listaint ** l ){
//	liberarint(l);
	//crearint(l);
//	insertarint(l, 11, true);
	(*l)->siguiente = *l;	//Circularidad	
	return 0;
}

unsigned int Circular2( struct Listaint ** l ){
	liberarint(l);
	crearint(l);
	insertarint(l, 11, true);
	insertarint(l, 12, true);
	insertarint(l, 13, true);
	(*l)->siguiente->siguiente->siguiente = *l;	//Circularidad	
	return 0;
}



//b)
unsigned int LugarBasura( struct Listaint ** l ){
	liberarint(l);
	*l = 111111; //Posicion basura
	return 0;
}

//c)


//d), e) y f)
unsigned int ListaOriginal( struct Listaint ** l ){
	liberarint(l);
	crearint(l);
	insertarint(l, 1, true);
	insertarint(l, 2, true);
	insertarint(l, 3, true);
	
	return 0;
}
unsigned int ListaNodoMenos( struct Listaint ** l ){
	liberarint(l);
	crearint(l);
	insertarint(l, 1, true);
	insertarint(l, 2, true);
	return 0;
}

unsigned int ListaNodoMas( struct Listaint ** l ){
	liberarint(l);
	crearint(l);
	insertarint(l, 1, true);
	insertarint(l, 2, true);
	insertarint(l, 3, true);
	insertarint(l, 4, true);
	return 0;
}
unsigned int ListaNodoDistinto( struct Listaint ** l ){
	liberarint(l);
	crearint(l);
	insertarint(l, 1, true);
	insertarint(l, 2, true);
	insertarint(l, 1000, true);
	return 0;
}




