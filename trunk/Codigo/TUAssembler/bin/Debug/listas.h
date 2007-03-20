//    ___________________________________________________________________________________
//   (********/*(*************************************************************)*\********)
//  (********/*(***************************************************************)*\********)
// (********/*(*****************************************************************)*\********) 
//(**********(***********************[DEFINICIONES]******************************)**********)
//(**********(*******************************************************************)**********)
// (********\*(*****************************************************************)*/********) 
//  (********\*(***************************************************************)*/********)
//   (********\*(*************************************************************)*/********)
//    -----------------------------------------------------------------------------------
#include <stdio.h>

#ifndef LIBRERIALISTAS
#define LIBRERIALISTAS

#define bool int
#define true 1
#define false 0

#define listaSinProblemas 0
#define listaCircular 1
#define punteroInvalido 2

struct Listaint
{
   int nodo;
   struct Listaint *siguiente;
};

struct Listabool
{
   int nodo;
   struct Listabool *siguiente;
};

struct Listalonglong{
   long long int nodo;
   struct Listalonglong *siguiente;
};

struct Listashort
{
   short nodo;
   struct Listashort *siguiente;
};


struct Listachar
{
   char nodo;
   struct Listachar *siguiente;
};

struct Listafloat
{
   float nodo;
   struct Listafloat *siguiente;
};

struct Listadouble
{
   double nodo;
   struct Listadouble *siguiente;
};

struct ListaPuntero
{
   void * nodo;
   struct ListaPuntero *siguiente;
};

//    ***********************************************************************************
//   *************************************************************************************
//  ************************FUNCIONES PARA TRABAJAR CON LISTAS DE PUNTEROS*****************
// ***************************************************************************************** 
//*******************************************************************************************

int esta(struct ListaPuntero *cabeza, void *valor){
   struct ListaPuntero *actual;
   int ret_value = 0;
   actual = cabeza;
   while (actual!=NULL && !ret_value){
	ret_value = actual->nodo == valor;
	actual = actual->siguiente;
   }
   return actual != NULL;
}

void insertarPuntero(struct ListaPuntero **cabeza,  void *valor){
   struct ListaPuntero *predecesor,*sucesor,*nuevo;
   nuevo = (struct ListaPuntero *) malloc(sizeof(struct ListaPuntero));
   nuevo->nodo = valor;
   if (*cabeza == NULL){ //Vacia
      nuevo->siguiente = *cabeza;
      *cabeza = nuevo;
   }else{
      predecesor = *cabeza;
      sucesor = predecesor->siguiente;
      while (sucesor!=NULL){
	 predecesor = sucesor;
	 sucesor = sucesor->siguiente;
      }
      predecesor->siguiente = nuevo;
      nuevo->siguiente = sucesor;
   }
}

//    ***********************************************************************************
//   *************************************************************************************
//  **************************INTEGER-INTEGER-INTEGER**************************************
// *****************************************************************************************
//*******************************************************************************************

void crearint(struct Listaint **cabeza)
{
   *cabeza = NULL;
}

int vaciaint(struct Listaint *cabeza)
{
  return (cabeza==NULL);
}
/*
void insertar(struct Listaint **cabeza)
{
   struct Listaint *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listaint *) malloc(sizeof(struct Listaint));
   printf("Introduzca nodo: ");
   scanf("%d",&nuevo->nodo); fflush(stdin);
   if (vaciaint(*cabeza)){
      nuevo->siguiente = *cabeza;
      *cabeza = nuevo;
   }else{
      predecesor = *cabeza;
      sucesor = predecesor->siguiente;
      while (sucesor!=NULL){
	 predecesor = sucesor;
	 sucesor = sucesor->siguiente;
      }
      predecesor->siguiente = nuevo;
      nuevo->siguiente = sucesor;
   }
}
*/
void insertarint(struct Listaint **cabeza, int valor, bool contabilizarPedidoMemoria){
   struct Listaint *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listaint *) malloc2(sizeof(struct Listaint),contabilizarPedidoMemoria);
   nuevo->nodo = valor;
   if (vaciaint(*cabeza)){
      nuevo->siguiente = *cabeza;
      *cabeza = nuevo;
   }else{
      predecesor = *cabeza;
      sucesor = predecesor->siguiente;
      while (sucesor!=NULL){
	 predecesor = sucesor;
	 sucesor = sucesor->siguiente;
      }
      predecesor->siguiente = nuevo;
      nuevo->siguiente = sucesor;
   }
}

void liberarint(struct Listaint **cabeza)
{
   struct Listaint *actual,*sucesor;
   actual = *cabeza;
   while (actual!=NULL){
      sucesor = actual->siguiente;
      free2(actual);  
      actual = sucesor;
   }
   *cabeza = NULL;
}

/*int igualdadint(struct Listaint *cabeza1, struct Listaint *cabeza2){
   struct Listaint *actual1, *actual2;
   int ret_value = 1;
   actual1 = cabeza1;
   actual2 = cabeza2;
   while (actual1!=NULL && actual2!=NULL && ret_value){
      ret_value = actual1->nodo == actual2->nodo;
      actual1 = actual1->siguiente;
      actual2 = actual2->siguiente;
   }
   return (actual1 == actual2) && ret_value;
}
*/

int igualdadint(struct Listaint *cabeza1, struct Listaint *cabeza2){
   struct Listaint *actual1, *actual2;
   int ret_value = 1, tamanio1=0, tamanio2=0;
   actual1 = cabeza1;
   actual2 = cabeza2;
   while ((actual1!=NULL || actual2!=NULL)){
	if (actual1!=NULL){
		if (actual2!=NULL){
			if(actual1->nodo != actual2->nodo){
				ret_value = 0;
				printf("El nodo %d en la primer lista tiene valor %d mientras que en la segunda tiene valor %d. \n Diferencia: %d\n", tamanio1, actual1->nodo, actual2->nodo, actual1->nodo - actual2->nodo);
			}
		}else{
			ret_value = 0;
			printf("La primera lista tiene el valor %d en la posicion %d mientras que la segunda ya no tiene elementos \n", actual1->nodo, tamanio1);
		}
		
	}else{
		ret_value = 0;
		printf("La segunda lista tiene el valor %d en la posicion %d mientras que la primera ya no tiene elementos \n", actual2->nodo, tamanio2);
	}
      if(actual1 != NULL){
      	actual1 = actual1->siguiente;
	tamanio1++;
      }
      if(actual2 != NULL){
      	actual2 = actual2->siguiente;
	tamanio2++;
      }
   }
   if(tamanio1 != tamanio2)
   	printf("El tamanio de las listas es distinto. Una tiene longitud %d y la otra %d \n",tamanio1,tamanio2);
   return ret_value;
}

int PunteroInvalidoOListaCircularint(struct Listaint *cabeza){
   struct Listaint *actual;
   struct ListaPuntero *listaDePunteros = NULL;
   int ret_value=0;
   actual = cabeza;
   while (actual!=NULL && !ret_value && fuePedido(actual)){
	ret_value = esta(listaDePunteros, (void*)actual);
	insertarPuntero(&listaDePunteros, (void*)actual);
	actual = actual->siguiente;
   }
   if (ret_value) 
   	return listaCircular;
   else
	if(actual != NULL)
   		return punteroInvalido;
   return listaSinProblemas;
}

/*int TienePunterosInvalidosint(struct Listaint *cabeza){
   struct ListaPuntero *listaDePunteros = NULL;
   struct Listaint *actual;
   actual = cabeza;
   //printf(">>>(%d)\n", actual);
   while (actual!=NULL && fuePedido(actual)){
	actual = actual->siguiente;
	//printf("(%d)\n", actual);
   }
   return actual != NULL;
}
*/

//    ***********************************************************************************
//   *************************************************************************************
//  **************************SHORT-SHORT-SHORT-SHORT-SHORT********************************
// *****************************************************************************************
//*******************************************************************************************

void crearshort(struct Listashort **cabeza)
{
   *cabeza = NULL;
}

int vaciashort(struct Listashort *cabeza)
{
  return (cabeza==NULL);
}

void insertarshort(struct Listashort **cabeza, short valor, bool contabilizarPedidoMemoria){
   struct Listashort *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listashort *) malloc2(sizeof(struct Listashort), contabilizarPedidoMemoria);
   nuevo->nodo = valor;
   if (vaciashort(*cabeza)){
      nuevo->siguiente = *cabeza;
      *cabeza = nuevo;
   }else{
      predecesor = *cabeza;
      sucesor = predecesor->siguiente;
      while (sucesor!=NULL){
	 predecesor = sucesor;
	 sucesor = sucesor->siguiente;
      }
      predecesor->siguiente = nuevo;
      nuevo->siguiente = sucesor;
   }
}

void liberarshort(struct Listashort **cabeza)
{
   struct Listashort *actual,*sucesor;
   actual = *cabeza;
   while (actual!=NULL){
      sucesor = actual->siguiente;
      free2(actual);
      actual = sucesor;
   }
   *cabeza = NULL;
}

int igualdadshort(struct Listashort *cabeza1, struct Listashort *cabeza2){
   struct Listashort *actual1, *actual2;
   int ret_value = 1, tamanio1=0, tamanio2=0;
   actual1 = cabeza1;
   actual2 = cabeza2;
   while ((actual1!=NULL || actual2!=NULL)){
	if (actual1!=NULL){
		if (actual2!=NULL){
			if(actual1->nodo != actual2->nodo){
				ret_value = 0;
				printf("El nodo %d en la primer lista tiene valor %d mientras que en la segunda tiene valor %d. \n Diferencia: %d\n", tamanio1, actual1->nodo, actual2->nodo, actual1->nodo - actual2->nodo);
			}
		}else{
			ret_value = 0;
			printf("La primera lista tiene el valor %d en la posicion %d mientras que la segunda ya no tiene elementos \n", actual1->nodo, tamanio1);
		}
		
	}else{
		ret_value = 0;
		printf("La segunda lista tiene el valor %d en la posicion %d mientras que la primera ya no tiene elementos \n", actual2->nodo, tamanio2);
	}
      if(actual1 != NULL){
      	actual1 = actual1->siguiente;
	tamanio1++;
      }
      if(actual2 != NULL){
      	actual2 = actual2->siguiente;
	tamanio2++;
      }
   }
   if(tamanio1 != tamanio2)
   	printf("El tamanio de las listas es distinto. Una tiene longitud %d y la otra %d \n",tamanio1,tamanio2);
   return ret_value;
}


int PunteroInvalidoOListaCircularshort(struct Listashort *cabeza){
   struct Listashort *actual;
   struct ListaPuntero *listaDePunteros = NULL;
   int ret_value=0;
   actual = cabeza;
   while (actual!=NULL && !ret_value && fuePedido(actual)){
	ret_value = esta(listaDePunteros, (void*)actual);
	insertarPuntero(&listaDePunteros, (void*)actual);
	actual = actual->siguiente;
   }
   if (ret_value) 
   	return listaCircular;
   else
	if(actual != NULL)
   		return punteroInvalido;
   return listaSinProblemas;
}

/*
int ListaCircularshort(struct Listashort *cabeza){
   struct Listashort *actual;
   struct ListaPuntero *listaDePunteros = NULL;
   int ret_value=0;
   actual = cabeza;
   while (actual!=NULL && !ret_value){
	ret_value = esta(listaDePunteros, (void*)actual);
	insertarPuntero(&listaDePunteros, (void*)actual);
	actual = actual->siguiente;
   }
   return ret_value;
}

int TienePunterosInvalidosshort(struct Listashort *cabeza){
   struct ListaPuntero *listaDePunteros = NULL;
   struct Listashort *actual;
   actual = cabeza;
   while (actual!=NULL && fuePedido(actual))
	actual = actual->siguiente;
   return actual != NULL;
}
*/
//    ***********************************************************************************
//   *************************************************************************************
//  **************************BOOLEAN-BOOLEAN-BOOLEAN-BOOLEAN******************************
// *****************************************************************************************
//*******************************************************************************************

void crearbool(struct Listabool **cabeza)
{
   *cabeza = NULL;
}

int vaciabool(struct Listabool *cabeza)
{
  return (cabeza==NULL);
}

void insertarbool(struct Listabool **cabeza, int valor, bool contabilizarPedidoMemoria){
   struct Listabool *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listabool *) malloc2(sizeof(struct Listabool), contabilizarPedidoMemoria);
   nuevo->nodo = (valor==0)?0:1;
   if (vaciabool(*cabeza)){
      nuevo->siguiente = *cabeza;
      *cabeza = nuevo;
   }else{
      predecesor = *cabeza;
      sucesor = predecesor->siguiente;
      while (sucesor!=NULL){
	 predecesor = sucesor;
	 sucesor = sucesor->siguiente;
      }
      predecesor->siguiente = nuevo;
      nuevo->siguiente = sucesor;
   }
}

void liberarbool(struct Listabool **cabeza)
{
   struct Listabool *actual,*sucesor;
   actual = *cabeza;
   while (actual!=NULL){
      sucesor = actual->siguiente;
      free2(actual);
      actual = sucesor;
   }
   *cabeza = NULL;
}

int igualdadbool(struct Listabool *cabeza1, struct Listabool *cabeza2){
   struct Listabool *actual1, *actual2;
   int ret_value = 1, tamanio1=0, tamanio2=0;
   actual1 = cabeza1;
   actual2 = cabeza2;
   while ((actual1!=NULL || actual2!=NULL)){
	if (actual1!=NULL){
		if (actual2!=NULL){
			if(actual1->nodo==0 && actual2->nodo!=0){
				ret_value = 0;
				printf("El nodo %d en la primer lista tiene valor False mientras que en la segunda tiene valor True. \n", tamanio1);
			}
			if(actual1->nodo!=0 && actual2->nodo==0){
				ret_value = 0;
				printf("El nodo %d en la primer lista tiene valor True mientras que en la segunda tiene valor False. \n", tamanio1);
			}
		}else{
			ret_value = 0;
			printf("La primera lista tiene el valor %d en la posicion %d mientras que la segunda ya no tiene elementos \n", actual1->nodo, tamanio1);
		}
		
	}else{
		ret_value = 0;
		printf("La segunda lista tiene el valor %d en la posicion %d mientras que la primera ya no tiene elementos \n", actual2->nodo, tamanio2);
	}
      if(actual1 != NULL){
      	actual1 = actual1->siguiente;
	tamanio1++;
      }
      if(actual2 != NULL){
      	actual2 = actual2->siguiente;
	tamanio2++;
      }
   }
   if(tamanio1 != tamanio2)
   	printf("El tamanio de las listas es distinto. Una tiene longitud %d y la otra %d \n",tamanio1,tamanio2);
   return ret_value;
}


int PunteroInvalidoOListaCircularbool(struct Listabool *cabeza){
   struct Listabool *actual;
   struct ListaPuntero *listaDePunteros = NULL;
   int ret_value=0;
   actual = cabeza;
   while (actual!=NULL && !ret_value && fuePedido(actual)){
	ret_value = esta(listaDePunteros, (void*)actual);
	insertarPuntero(&listaDePunteros, (void*)actual);
	actual = actual->siguiente;
   }
   if (ret_value) 
   	return listaCircular;
   else
	if(actual != NULL)
   		return punteroInvalido;
   return listaSinProblemas;
}
/*
int ListaCircularbool(struct Listabool *cabeza){
   struct Listabool *actual;
   struct ListaPuntero *listaDePunteros = NULL;
   int ret_value=0;
   actual = cabeza;
   while (actual!=NULL && !ret_value){
	ret_value = esta(listaDePunteros, (void*)actual);
	insertarPuntero(&listaDePunteros, (void*)actual);
	actual = actual->siguiente;
   }
   return ret_value;
}

int TienePunterosInvalidosbool(struct Listabool *cabeza){
   struct ListaPuntero *listaDePunteros = NULL;
   struct Listabool *actual;
   actual = cabeza;
   while (actual!=NULL && fuePedido(actual))
	actual = actual->siguiente;
   return actual == NULL;
}
*/

//    ***********************************************************************************
//   *************************************************************************************
//  **************************INT64-INT64-INT64-INT64-INT64-INT64**************************
// *****************************************************************************************
//*******************************************************************************************

void crearlonglong(struct Listalonglong **cabeza)
{
   *cabeza = NULL;
}

int vacialonglong(struct Listalonglong *cabeza)
{
  return (cabeza==NULL);
}

void insertarlonglong(struct Listalonglong **cabeza, int valor, bool contabilizarPedidoMemoria){
   struct Listalonglong *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listalonglong *) malloc2(sizeof(struct Listalonglong), contabilizarPedidoMemoria);
   nuevo->nodo = (valor==0)?0:1;
   if (vacialonglong(*cabeza)){
      nuevo->siguiente = *cabeza;
      *cabeza = nuevo;
   }else{
      predecesor = *cabeza;
      sucesor = predecesor->siguiente;
      while (sucesor!=NULL){
	 predecesor = sucesor;
	 sucesor = sucesor->siguiente;
      }
      predecesor->siguiente = nuevo;
      nuevo->siguiente = sucesor;
   }
}

void liberarlonglong(struct Listalonglong **cabeza)
{
   struct Listalonglong *actual,*sucesor;
   actual = *cabeza;
   while (actual!=NULL){
      sucesor = actual->siguiente;
      free2(actual);
      actual = sucesor;
   }
   *cabeza = NULL;
}

int igualdadlonglong(struct Listalonglong *cabeza1, struct Listalonglong *cabeza2){
   struct Listalonglong *actual1, *actual2;
   int ret_value = 1, tamanio1=0, tamanio2=0;
   actual1 = cabeza1;
   actual2 = cabeza2;
   while ((actual1!=NULL || actual2!=NULL)){
	if (actual1!=NULL){
		if (actual2!=NULL){
			if(actual1->nodo != actual2->nodo){
				ret_value = 0;
				printf("El nodo %d en la primer lista tiene valor %d mientras que en la segunda tiene valor %d. \n Diferencia: %d\n", tamanio1, actual1->nodo, actual2->nodo, actual1->nodo - actual2->nodo);
			}
		}else{
			ret_value = 0;
			printf("La primera lista tiene el valor %d en la posicion %d mientras que la segunda ya no tiene elementos \n", actual1->nodo, tamanio1);
		}
		
	}else{
		ret_value = 0;
		printf("La segunda lista tiene el valor %d en la posicion %d mientras que la primera ya no tiene elementos \n", actual2->nodo, tamanio2);
	}
      if(actual1 != NULL){
      	actual1 = actual1->siguiente;
	tamanio1++;
      }
      if(actual2 != NULL){
      	actual2 = actual2->siguiente;
	tamanio2++;
      }
   }
   if(tamanio1 != tamanio2)
   	printf("El tamanio de las listas es distinto. Una tiene longitud %d y la otra %d \n",tamanio1,tamanio2);
   return ret_value;
}

int PunteroInvalidoOListaCircularlonglong(struct Listalonglong *cabeza){
   struct Listalonglong *actual;
   struct ListaPuntero *listaDePunteros = NULL;
   int ret_value=0;
   actual = cabeza;
   while (actual!=NULL && !ret_value && fuePedido(actual)){
	ret_value = esta(listaDePunteros, (void*)actual);
	insertarPuntero(&listaDePunteros, (void*)actual);
	actual = actual->siguiente;
   }
   if (ret_value) 
   	return listaCircular;
   else
	if(actual != NULL)
   		return punteroInvalido;
   return listaSinProblemas;
}

//    ***********************************************************************************
//   *************************************************************************************
//  **************************CHAR-CHAR-CHAR-CHAR-CHAR*************************************
// *****************************************************************************************
//*******************************************************************************************

void crearchar(struct Listachar **cabeza)
{
   *cabeza = NULL;
}

int vaciachar(struct Listachar *cabeza)
{
  return (cabeza==NULL);
}
/*
void insertar(struct Listachar **cabeza)
{
   struct Listachar *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listachar *) malloc(sizeof(struct Listachar));
   printf("Introduzca nodo: ");
   scanf("%c",&nuevo->nodo); fflush(stdin);
   if (vaciachar(*cabeza)){
      nuevo->siguiente = *cabeza;
      *cabeza = nuevo;
   }else{
      predecesor = *cabeza;
      sucesor = predecesor->siguiente;
      while (sucesor!=NULL){
	 predecesor = sucesor;
	 sucesor = sucesor->siguiente;
      }
      predecesor->siguiente = nuevo;
      nuevo->siguiente = sucesor;
   }
}
*/
void insertarchar(struct Listachar **cabeza, char valor, bool contabilizarPedidoMemoria){
   struct Listachar *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listachar *) malloc2(sizeof(struct Listachar), contabilizarPedidoMemoria);
   nuevo->nodo = valor;
   if (vaciachar(*cabeza)){
      nuevo->siguiente = *cabeza;
      *cabeza = nuevo;
   }else{
      predecesor = *cabeza;
      sucesor = predecesor->siguiente;
      while (sucesor!=NULL){
	 predecesor = sucesor;
	 sucesor = sucesor->siguiente;
      }
      predecesor->siguiente = nuevo;
      nuevo->siguiente = sucesor;
   }
}

void liberarchar(struct Listachar **cabeza)
{
   struct Listachar *actual,*sucesor;
   actual = *cabeza;
   while (actual!=NULL){
      sucesor = actual->siguiente;
      free2(actual);
      actual = sucesor;
   }
   *cabeza = NULL;
}
/*
int igualdadchar(struct Listachar *cabeza1, struct Listachar *cabeza2){
   struct Listachar *actual1, *actual2;
   int ret_value = 1;
   actual1 = cabeza1;
   actual2 = cabeza2;
   while (actual1!=NULL && actual2!=NULL && ret_value){
      ret_value = actual1->nodo == actual2->nodo;
      actual1 = actual1->siguiente;
      actual2 = actual2->siguiente;
   }
   return (actual1 == actual2) && ret_value;
}
*/
int igualdadchar(struct Listachar *cabeza1, struct Listachar *cabeza2){
   struct Listachar *actual1, *actual2;
   int ret_value = 1, tamanio1=0, tamanio2=0;
   actual1 = cabeza1;
   actual2 = cabeza2;
   while ((actual1!=NULL || actual2!=NULL)){
	if (actual1!=NULL){
		if (actual2!=NULL){
			if(actual1->nodo != actual2->nodo){
				ret_value = 0;
				printf("El nodo %d en la primer lista tiene valor %c mientras que en la segunda tiene valor %c. \n Diferencia: %c\n", tamanio1, actual1->nodo, actual2->nodo, actual1->nodo - actual2->nodo);
			}
		}else{
			ret_value = 0;
			printf("La primera lista tiene el valor %c en la posicion %d mientras que la segunda ya no tiene elementos \n", actual1->nodo, tamanio1);
		}
		
	}else{
		ret_value = 0;
		printf("La segunda lista tiene el valor %c en la posicion %d mientras que la primera ya no tiene elementos \n", actual2->nodo, tamanio2);
	}
      if(actual1 != NULL){
      	actual1 = actual1->siguiente;
	tamanio1++;
      }
      if(actual2 != NULL){
      	actual2 = actual2->siguiente;
	tamanio2++;
      }
   }
   if(tamanio1 != tamanio2)
   	printf("El tamanio de las listas es distinto. Una tiene longitud %d y la otra %d \n",tamanio1,tamanio2);
   return ret_value;
}

int PunteroInvalidoOListaCircularchar(struct Listachar *cabeza){
   struct Listachar *actual;
   struct ListaPuntero *listaDePunteros = NULL;
   int ret_value=0;
   actual = cabeza;
   while (actual!=NULL && !ret_value && fuePedido(actual)){
	ret_value = esta(listaDePunteros, (void*)actual);
	insertarPuntero(&listaDePunteros, (void*)actual);
	actual = actual->siguiente;
   }
   if (ret_value) 
   	return listaCircular;
   else
	if(actual != NULL)
   		return punteroInvalido;
   return listaSinProblemas;
}
/*
int ListaCircularchar(struct Listachar *cabeza){
   struct Listachar *actual;
   struct ListaPuntero *listaDePunteros = NULL;
   int ret_value=0;
   actual = cabeza;
   while (actual!=NULL && !ret_value){
	ret_value = esta(listaDePunteros, (void*)actual);
	insertarPuntero(&listaDePunteros, (void*)actual);
	actual = actual->siguiente;
   }
   return ret_value;
}

int TienePunterosInvalidoschar(struct Listachar *cabeza){
   struct ListaPuntero *listaDePunteros = NULL;
   struct Listachar *actual;
   actual = cabeza;
   while (actual!=NULL && fuePedido(actual))
	actual = actual->siguiente;
   return actual == NULL;
}
*/

//    ***********************************************************************************
//   *************************************************************************************
//  **************************FLOAT-FLOAT-FLOAT-FLOAT-FLOAT********************************
// *****************************************************************************************
//*******************************************************************************************

void crearfloat(struct Listafloat **cabeza)
{
   *cabeza = NULL;
}

int vaciafloat(struct Listafloat *cabeza)
{
  return (cabeza==NULL);
}


/*void insertar(struct Listafloat **cabeza)
{
   struct Listafloat *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listafloat *) malloc(sizeof(struct Listafloat));
   printf("Introduzca nodo: ");
   scanf("%f",&nuevo->nodo); fflush(stdin);
   if (vaciafloat(*cabeza)){
      nuevo->siguiente = *cabeza;
      *cabeza = nuevo;
   }else{
      predecesor = *cabeza;
      sucesor = predecesor->siguiente;
      while (sucesor!=NULL){
	 predecesor = sucesor;
	 sucesor = sucesor->siguiente;
      }
      predecesor->siguiente = nuevo;
      nuevo->siguiente = sucesor;
   }
}
*/

void insertarfloat(struct Listafloat **cabeza, float valor, bool contabilizarPedidoMemoria){
   struct Listafloat *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listafloat *) malloc2(sizeof(struct Listafloat), contabilizarPedidoMemoria);
   nuevo->nodo = valor;
   if (vaciafloat(*cabeza)){
      nuevo->siguiente = *cabeza;
      *cabeza = nuevo;
   }else{
      predecesor = *cabeza;
      sucesor = predecesor->siguiente;
      while (sucesor!=NULL){
	 predecesor = sucesor;
	 sucesor = sucesor->siguiente;
      }
      predecesor->siguiente = nuevo;
      nuevo->siguiente = sucesor;
   }
}

void liberarfloat(struct Listafloat **cabeza)
{
   struct Listafloat *actual,*sucesor;
   actual = *cabeza;
   while (actual!=NULL){
      sucesor = actual->siguiente;
      free2(actual);
      actual = sucesor;
   }
   *cabeza = NULL;
}
/*
int igualdadfloat(struct Listafloat *cabeza1, struct Listafloat *cabeza2){
   struct Listafloat *actual1, *actual2;
   int ret_value = 1;
   actual1 = cabeza1;
   actual2 = cabeza2;
   while (actual1!=NULL && actual2!=NULL && ret_value){
      ret_value = actual1->nodo == actual2->nodo;
      actual1 = actual1->siguiente;
      actual2 = actual2->siguiente;
   }
   return (actual1 == actual2) && ret_value;
}
*/

bool equalfloat(float valor1, float valor2, int precision){
    float diferencia  = valor1 - valor2;
    diferencia = (diferencia >= 0) ? diferencia : -diferencia;
    float varPrecision = pow((float)10, -precision);
    return diferencia < varPrecision;
}

int igualdadfloat(struct Listafloat *cabeza1, struct Listafloat *cabeza2, int precision){
   struct Listafloat *actual1, *actual2;
   int ret_value = 1, tamanio1=0, tamanio2=0;
   actual1 = cabeza1;
   actual2 = cabeza2;
   while ((actual1!=NULL || actual2!=NULL)){
	if (actual1!=NULL){
		if (actual2!=NULL){
			if(!equalfloat(actual1->nodo, actual2->nodo, precision)){
				ret_value = 0;
				printf("El nodo %d en la primer lista tiene valor %f mientras que en la segunda tiene valor %f. \n Diferencia: %f\n", tamanio1, actual1->nodo, actual2->nodo, actual1->nodo - actual2->nodo);
			}
		}else{
			ret_value = 0;
			printf("La primera lista tiene el valor %f en la posicion %d mientras que la segunda ya no tiene elementos \n", actual1->nodo, tamanio1);
		}		
	}else{
		ret_value = 0;
		printf("La segunda lista tiene el valor %f en la posicion %d mientras que la primera ya no tiene elementos \n", actual2->nodo, tamanio2);
	}
      if(actual1 != NULL){
      	actual1 = actual1->siguiente;
	tamanio1++;
      }
      if(actual2 != NULL){
      	actual2 = actual2->siguiente;
	tamanio2++;
      }
   }
   if(tamanio1 != tamanio2)
   	printf("El tamanio de las listas es distinto. Una tiene longitud %d y la otra %d \n",tamanio1,tamanio2);
   return ret_value;
}

int PunteroInvalidoOListaCircularfloat(struct Listafloat *cabeza){
   struct Listafloat *actual;
   struct ListaPuntero *listaDePunteros = NULL;
   int ret_value=0;
   actual = cabeza;
   while (actual!=NULL && !ret_value && fuePedido(actual)){
	ret_value = esta(listaDePunteros, (void*)actual);
	insertarPuntero(&listaDePunteros, (void*)actual);
	actual = actual->siguiente;
   }
   if (ret_value) 
   	return listaCircular;
   else
	if(actual != NULL)
   		return punteroInvalido;
   return listaSinProblemas;
}
/*
int ListaCircularfloat(struct Listafloat *cabeza){
   struct Listafloat *actual;
   struct ListaPuntero *listaDePunteros = NULL;
   int ret_value=0;
   actual = cabeza;
   while (actual!=NULL && !ret_value){
	ret_value = esta(listaDePunteros, (void*)actual);
	insertarPuntero(&listaDePunteros, (void*)actual);
	actual = actual->siguiente;
   }
   return ret_value;
}

int TienePunterosInvalidosfloat(struct Listafloat *cabeza){
   struct ListaPuntero *listaDePunteros = NULL;
   struct Listafloat *actual;
   actual = cabeza;
   while (actual!=NULL && fuePedido(actual))
	actual = actual->siguiente;
   return actual == NULL;
}
*/

//    ***********************************************************************************
//   *************************************************************************************
//  **************************DOUBLE-DOUBLE-DOUBLE-DOUBLE**********************************
// *****************************************************************************************
//*******************************************************************************************

void creardouble(struct Listadouble **cabeza)
{
   *cabeza = NULL;
}

int vaciadouble(struct Listadouble *cabeza)
{
  return (cabeza==NULL);
}

/*void insertar(struct Listadouble **cabeza)
{
   struct Listadouble *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listadouble *) malloc(sizeof(struct Listadouble));
   printf("Introduzca nodo: ");
   scanf("%lf",&nuevo->nodo); fflush(stdin);
   if (vaciadouble(*cabeza)){
      nuevo->siguiente = *cabeza;
      *cabeza = nuevo;
   }else{
      predecesor = *cabeza;
      sucesor = predecesor->siguiente;
      while (sucesor!=NULL){
	 predecesor = sucesor;
	 sucesor = sucesor->siguiente;
      }
      predecesor->siguiente = nuevo;
      nuevo->siguiente = sucesor;
   }
}*/

void insertardouble(struct Listadouble **cabeza, double valor, bool contabilizarPedidoMemoria){
   struct Listadouble *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listadouble *) malloc2(sizeof(struct Listadouble), contabilizarPedidoMemoria);
   nuevo->nodo = valor;
   if (vaciadouble(*cabeza)){
      nuevo->siguiente = *cabeza;
      *cabeza = nuevo;
   }else{
      predecesor = *cabeza;
      sucesor = predecesor->siguiente;
      while (sucesor!=NULL){
	 predecesor = sucesor;
	 sucesor = sucesor->siguiente;
      }
      predecesor->siguiente = nuevo;
      nuevo->siguiente = sucesor;
   }
}

void liberardouble(struct Listadouble **cabeza)
{
   struct Listadouble *actual,*sucesor;
   actual = *cabeza;
   while (actual!=NULL){
      sucesor = actual->siguiente;
      free2(actual);
      actual = sucesor;
   }
   *cabeza = NULL;
}

/*int igualdaddouble(struct Listadouble *cabeza1, struct Listadouble *cabeza2){
   struct Listadouble *actual1, *actual2;
   int ret_value = 1;
   actual1 = cabeza1;
   actual2 = cabeza2;
   while (actual1!=NULL && actual2!=NULL && ret_value){
      ret_value = actual1->nodo == actual2->nodo;
      actual1 = actual1->siguiente;
      actual2 = actual2->siguiente;
   }
   return (actual1 == actual2) && ret_value;
}
*/

bool equaldouble(double valor1, double valor2, int precision){
    double diferencia  = valor1 - valor2;
    diferencia = (diferencia >= 0) ? diferencia : -diferencia;
    double varPrecision = pow((float)10, -precision);
    return diferencia < varPrecision;
}


int igualdaddouble(struct Listadouble *cabeza1, struct Listadouble *cabeza2, int precision){
   struct Listadouble *actual1, *actual2;
   int ret_value = 1, tamanio1=0, tamanio2=0;
   actual1 = cabeza1;
   actual2 = cabeza2;
   while ((actual1!=NULL || actual2!=NULL)){
	if (actual1!=NULL){
		if (actual2!=NULL){
			if(!equaldouble(actual1->nodo, actual2->nodo, precision)){
				ret_value = 0;
				printf("El nodo %d en la primer lista tiene valor %f mientras que en la segunda tiene valor %f. \n Diferencia: %f \n", tamanio1, actual1->nodo, actual2->nodo, actual1->nodo - actual2->nodo);
			}
		}else{
			ret_value = 0;
			printf("La primera lista tiene el valor %f en la posicion %d mientras que la segunda ya no tiene elementos \n", actual1->nodo, tamanio1);
		}
		
	}else{
		ret_value = 0;
		printf("La segunda lista tiene el valor %f en la posicion %d mientras que la primera ya no tiene elementos \n", actual2->nodo, tamanio2);
	}
      if(actual1 != NULL){
      	actual1 = actual1->siguiente;
	tamanio1++;
      }
      if(actual2 != NULL){
      	actual2 = actual2->siguiente;
	tamanio2++;
      }
   }
   if(tamanio1 != tamanio2)
   	printf("El tamanio de las listas es distinto. Una tiene longitud %d y la otra %d \n",tamanio1,tamanio2);
   return ret_value;
}

int PunteroInvalidoOListaCirculardouble(struct Listadouble *cabeza){
   struct Listadouble *actual;
   struct ListaPuntero *listaDePunteros = NULL;
   int ret_value=0;
   actual = cabeza;
   while (actual!=NULL && !ret_value && fuePedido(actual)){
	ret_value = esta(listaDePunteros, (void*)actual);
	insertarPuntero(&listaDePunteros, (void*)actual);
	actual = actual->siguiente;
   }
   if (ret_value) 
   	return listaCircular;
   else
	if(actual != NULL)
   		return punteroInvalido;
   return listaSinProblemas;
}
/*
int ListaCirculardouble(struct Listadouble *cabeza){
   struct Listadouble *actual;
   struct ListaPuntero *listaDePunteros = NULL;
   int ret_value=0;
   actual = cabeza;
   while (actual!=NULL && !ret_value){
	ret_value = esta(listaDePunteros, (void*)actual);
	insertarPuntero(&listaDePunteros, (void*)actual);
	actual = actual->siguiente;
   }
   return ret_value;
}

int TienePunterosInvalidosdouble(struct Listadouble *cabeza){
   struct ListaPuntero *listaDePunteros = NULL;
   struct Listadouble *actual;
   actual = cabeza;
   while (actual!=NULL && fuePedido(actual))
	actual = actual->siguiente;
   return actual == NULL;
}
*/
#endif


//** * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * **
// *****************************************************************************************
//  ***************************************************************************************
//   *************************************************************************************
//    ***********************************************************************************
//     *********************************************************************************
//      *******************************************************************************
//       *****************************************************************************
//        ***************************************************************************
//	   \									   /
//	    \									  /
//	     \									 /
//	      \									/
//	       \							       /
//	        \							      /
//	         \							     /
//	          \							    /
//	           \							   /
//	            \							  /
//	             \							 /
//	              \							/
//	               \					       /
//	                 \					     /
//	                   \					   /
//	                     \					 /
//	                       \			       /
//				-------------------------------
