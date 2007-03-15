#include "libreria.h"


int main(){
       struct Listadouble * lista1, *lista2;
	creardouble(&lista1);
	insertardouble(&lista1, 11);
	insertardouble(&lista1, 12);
	insertardouble(&lista1, 13);
	insertardouble(&lista1, 13.5);
//	insertardouble(&lista1, 15);
//	insertardouble(&lista1, 16);
//	insertardouble(&lista1, 17);

	creardouble(&lista2);
	insertardouble(&lista2, 11);
	insertardouble(&lista2, 12);
	insertardouble(&lista2, 13);
	insertardouble(&lista2, 13.6);
	insertardouble(&lista2, 15);
	
	
	if(igualdaddouble(lista1, lista2))
		printf("Son Iguales");
	else
		printf("Son distintos");
	
	liberardouble(&lista1);
	liberardouble(&lista2);
	return 0;
}

