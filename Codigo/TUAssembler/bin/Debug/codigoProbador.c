#include <stdio.h>
#include "libreria.h"
#define bool int
#define true 1
#define false 0
extern unsigned int funcion1( struct Listaint * );

#define todoBien 0
#define liberarPosMemNoValida 1
#define escrituraFueraDelBuffer 2

int cantPedidosMemoria = 0;
char* pedidos[sizeof(int)*10000];
int tamanioPedidos[sizeof(int)*10000];
bool fueLiberado[sizeof(bool)*10000];

char* malloc2(int cantBytes){
   int i;
   char* ret_value;
   ret_value = malloc(cantBytes + 8 + 8);	//8 bytes antes y 8 bytes despues para controlar que no se pase de la longitud del buffer
   pedidos[cantPedidosMemoria] = ret_value;
   tamanioPedidos[cantPedidosMemoria] = cantBytes;
   fueLiberado[cantPedidosMemoria] = false;
   for(i=0; i<8; i++){
       ((char*)ret_value)[i] = 'A';
       ((char*)ret_value)[cantBytes + 8 + i] = 'A';
   }
   cantPedidosMemoria++;
   return ret_value + 8;
}

int free2(char* punteroABloque)
{
   int pos, i;
   int salida = todoBien;
   for(pos=0; pos<cantPedidosMemoria && pedidos[pos]!=punteroABloque-8; pos++);
       if(pedidos[pos] !=punteroABloque-8)
           salida = liberarPosMemNoValida;//printf("Se intento liberar una posicion de memoria no valida");
       else{
           fueLiberado[pos] = true;
           for (i = 0; i < 8; i++)
               if(((char*)punteroABloque-8)[i] != 'A'|| ((char*)punteroABloque)[tamanioPedidos[pos] + i] != 'A'){
                   salida = escrituraFueraDelBuffer;//printf("Se ha escrito fuera del buffer");
                   break;
               }
       }
       return salida;
}

void free2all(){
   int i;
   int bytesNoLiberados = 0;
   for(i=0; i<cantPedidosMemoria; i++){
       free(pedidos[i]);
       if(fueLiberado[i]== false)
           bytesNoLiberados = bytesNoLiberados + tamanioPedidos[i];
   }
   if(bytesNoLiberados >0)
       printf("No se han liberado %d bytes de memoria", bytesNoLiberados);
}

int pruebaVector()
{
	//------------Variables comunes------------------
	int salidaFree2;
	long long tiempoDeEjecucion=0;
	//------------Parametros-------------------------
	unsigned int salida;
	struct Listaint  *lista = NULL;
	int cantErrores = 0;
	//------------Pedir memoria----------------------
	//------------Instanciacion----------------------
	insertarint(&lista,5);
	insertarint(&lista,7);
	insertarint(&lista,9);
	//------------LlamadaFuncion---------------------
	tiempoDeEjecucion = timer();
	salida = funcion1( &lista );
	tiempoDeEjecucion = timer() - tiempoDeEjecucion;
	printf("Tardo: %d ciclos \n ", tiempoDeEjecucion);
	//------------Comparacion de valores-------------
	//salida
	if( salida != 8 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento salida:%d es distinto al valor esperado: 8" ,salida );
		printf( "\nDiferencia: %d\n" ,salida - 8 );
		cantErrores++;
	}
	//lista
	struct Listaint *listaaux;
	crearint(&listaaux);
	insertarint(&listaaux, 1);
	insertarint(&listaaux, 2);
	insertarint(&listaaux, 3);
	if( ListaCircularint(lista) )
	{
		printf( "La prueba pruebaVector es una Lista Circular" );
		return 1;
	}
	if( !igualdadint(lista, listaaux) )
	{
		    cantErrores++;
	}
	//------------Liberar memoria--------------------
	liberarint(&listaaux);
	liberarint(&lista);
	//------------Informar cant. de errores----------
	printf( "La prueba pruebaVector ha concluido con %d errores" ,cantErrores );
	return cantErrores;
}
int main()
{
	/*------------Parametros-------------------------*/
	int cantErrores = 0;
	/*------------Llamada a pruebas------------------*/
	if( cantErrores == 0 )
	{
		cantErrores = pruebaVector();
	}
	return 0;
}
