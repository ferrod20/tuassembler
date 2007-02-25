#include <stdio.h>
#define bool int
#define true 1
#define false 0
extern unsigned long int funcion1( unsigned char*  );

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
	/*------------Variables comunes------------------*/
	int salidaFree2;
	/*------------Parametros-------------------------*/
	unsigned long salida;
	unsigned char *vector;
	int cantErrores = 0;
	/*------------Pedir memoria----------------------*/
	vector = malloc2( 3 );
	/*------------Instanciacion----------------------*/
	vector[0] = 5;
	vector[1] = 7;
	vector[2] = 9;
	/*------------LlamadaFuncion---------------------*/
	salida = funcion1( vector );
	/*------------Comparacion de valores-------------*/
	//salida
	if( salida != 8 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento salida:%d es distinto al valor esperado: 8" ,salida );
		printf( "\nDiferencia: %d" ,salida - 8 );
		cantErrores++;
	}
	//vector
	if( vector[0] != 1 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento vector[0]:%d es distinto al valor esperado: 1" ,vector[0] );
		printf( "\nDiferencia: %d" ,vector[0] - 1 );
		cantErrores++;
	}
	if( vector[1] != 2 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento vector[1]:%d es distinto al valor esperado: 2" ,vector[1] );
		printf( "\nDiferencia: %d" ,vector[1] - 2 );
		cantErrores++;
	}
	if( vector[2] != 3 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento vector[2]:%d es distinto al valor esperado: 3" ,vector[2] );
		printf( "\nDiferencia: %d" ,vector[2] - 3 );
		cantErrores++;
	}
	/*------------Comparacion de valores-------------*/
	salidaFree2 = free2( vector );
	if( salidaFree2 == escrituraFueraDelBuffer )
	{
		printf( "Prueba pruebaVector: Se ha escrito fuera del buffer en el parámetro vector" );
		cantErrores++;
	}
	if( salidaFree2 == liberarPosMemNoValida )
	{
		printf( "Prueba pruebaVector: Se ha cambiado la dirección del parámetro vector por una dirección inválida." );
		cantErrores++;
	}

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
}
