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

struct Listaint
{
   int nodo;
   struct Listaint *siguiente;
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
void insertarint(struct Listaint **cabeza, int valor){
   struct Listaint *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listaint *) malloc(sizeof(struct Listaint));
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
      free(actual);
      actual = sucesor;
   }
   *cabeza = NULL;
}

int igualdadint(struct Listaint *cabeza1, struct Listaint *cabeza2){
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

int ListaCircularint(struct Listaint *cabeza){
   struct Listaint *actual;
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
void insertarchar(struct Listachar **cabeza, char valor){
   struct Listachar *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listachar *) malloc(sizeof(struct Listachar));
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
      free(actual);
      actual = sucesor;
   }
   *cabeza = NULL;
}

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

void insertarfloat(struct Listafloat **cabeza, float valor){
   struct Listafloat *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listafloat *) malloc(sizeof(struct Listafloat));
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
      free(actual);
      actual = sucesor;
   }
   *cabeza = NULL;
}

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

void insertar(struct Listadouble **cabeza)
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
}

void insertardouble(struct Listadouble **cabeza, double valor){
   struct Listadouble *predecesor,*sucesor,*nuevo;
   nuevo = (struct Listadouble *) malloc(sizeof(struct Listadouble));
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
      free(actual);
      actual = sucesor;
   }
   *cabeza = NULL;
}

int igualdaddouble(struct Listadouble *cabeza1, struct Listadouble *cabeza2){
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
