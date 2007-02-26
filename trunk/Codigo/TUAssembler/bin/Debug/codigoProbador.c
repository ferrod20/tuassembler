#include <stdio.h>
#define bool int
#define true 1
#define false 0
extern unsigned long int funcion1( unsigned char**  );

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
	unsigned char **matriz;
	int cantErrores = 0;
	/*------------Pedir memoria----------------------*/
	matriz = malloc2( sizeof(unsigned char*)*3 );
	int matrizFila;
	for( matrizFila = 0; matrizFila < 3; matrizFila++ )
	{
		matriz[matrizFila] = malloc2( sizeof(unsigned char)*3);
	}
	/*------------Instanciacion----------------------*/
	matriz[0][0] = 1;
	matriz[0][1] = 2;
	matriz[0][2] = 3;
	matriz[1][0] = 4;
	matriz[1][1] = 5;
	matriz[1][2] = 6;
	matriz[2][0] = 7;
	matriz[2][1] = 8;
	matriz[2][2] = 9;
	/*------------LlamadaFuncion---------------------*/
	salida = funcion1( matriz );
	/*------------Comparacion de valores-------------*/
	//salida
	if( salida != 8 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento salida:%d es distinto al valor esperado: 8" ,salida );
		printf( "\nDiferencia: %d" ,salida - 8 );
		cantErrores++;
	}
	if( matriz[0][0] != 1 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento matriz[0][0]:%d es distinto al valor esperado: 1" ,matriz[0][0] );
		printf( "\nDiferencia: %d" ,matriz[0][0] - 1 );
		cantErrores++;
	}
	if( matriz[0][1] != 2 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento matriz[0][1]:%d es distinto al valor esperado: 2" ,matriz[0][1] );
		printf( "\nDiferencia: %d" ,matriz[0][1] - 2 );
		cantErrores++;
	}
	if( matriz[0][2] != 3 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento matriz[0][2]:%d es distinto al valor esperado: 3" ,matriz[0][2] );
		printf( "\nDiferencia: %d" ,matriz[0][2] - 3 );
		cantErrores++;
	}
	if( matriz[1][0] != 5 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento matriz[1][0]:%d es distinto al valor esperado: 5" ,matriz[1][0] );
		printf( "\nDiferencia: %d" ,matriz[1][0] - 5 );
		cantErrores++;
	}
	if( matriz[1][1] != 7 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento matriz[1][1]:%d es distinto al valor esperado: 7" ,matriz[1][1] );
		printf( "\nDiferencia: %d" ,matriz[1][1] - 7 );
		cantErrores++;
	}
	if( matriz[1][2] != 9 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento matriz[1][2]:%d es distinto al valor esperado: 9" ,matriz[1][2] );
		printf( "\nDiferencia: %d" ,matriz[1][2] - 9 );
		cantErrores++;
	}
	if( matriz[2][0] != 2 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento matriz[2][0]:%d es distinto al valor esperado: 2" ,matriz[2][0] );
		printf( "\nDiferencia: %d" ,matriz[2][0] - 2 );
		cantErrores++;
	}
	if( matriz[2][1] != 6 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento matriz[2][1]:%d es distinto al valor esperado: 6" ,matriz[2][1] );
		printf( "\nDiferencia: %d" ,matriz[2][1] - 6 );
		cantErrores++;
	}
	if( matriz[2][2] != 8 )
	{
		printf( "Prueba pruebaVector: El valor del parametro/elemento matriz[2][2]:%d es distinto al valor esperado: 8" ,matriz[2][2] );
		printf( "\nDiferencia: %d" ,matriz[2][2] - 8 );
		cantErrores++;
	}
	/*------------Liberar memoria--------------------*/
	salidaFree2 = free2( matriz );
	if( salidaFree2 == escrituraFueraDelBuffer )
	{
		printf( "Prueba pruebaVector: Se ha escrito fuera del buffer en el parámetro matriz" );
		cantErrores++;
	}
	if( salidaFree2 == liberarPosMemNoValida )
	{
		printf( "Prueba pruebaVector: Se ha cambiado la dirección del parámetro matriz por una dirección inválida." );
		cantErrores++;
	}
	/*------------Informar cant. de errores----------*/
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
