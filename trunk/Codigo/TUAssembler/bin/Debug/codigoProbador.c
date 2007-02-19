#include <stdio.h>
#define bool int
#define true 1
#define false 0
extern unsigned long int funcion1( unsigned char, unsigned long int*, char*, bool*, float*, double* );

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

void free2(char* punteroABloque)
{
   int pos, i;
   for(pos=0; pos<cantPedidosMemoria && pedidos[pos]!=punteroABloque-8; pos++);
       if(pedidos[pos] !=punteroABloque-8)
           printf("Se intento liberar una posicion de memoria no valida");
       else{
           fueLiberado[pos] = true;
           for (i = 0; i < 8; i++)
               if(((char*)punteroABloque-8)[i] != 'A'|| ((char*)punteroABloque)[tamanioPedidos[pos] + i] != 'A'){
                   printf("Se ha escrito fuera del buffer");
                   break;
               }
       }
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

int PruebaUno()
{
	/*------------Parametros-------------------------*/
	unsigned long salida;
	unsigned char p1UI8;
	unsigned long *p2UI16;
	char *p3Char;
	bool *p4Bool;
	float *p5Float32;
	double *p6Float64;
	int cantErrores = 0;
	/*------------Instanciacion----------------------*/
	p1UI8 = 7;
	*p2UI16 = 8;
	*p3Char = '9';
	*p4Bool = false;
	*p5Float32 = 11.2;
	*p6Float64 = 12.3;
	/*------------LlamadaFuncion---------------------*/
	salida = funcion1( p1UI8, p2UI16, p3Char, p4Bool, p5Float32, p6Float64 );
	/*------------Comparacion de valores-------------*/
	//salida
	if( salida != 1 )
	{
		printf( "El valor del parametro salida:%d es distinto al valor esperado: 1" ,salida);
		printf( "\nDiferencia: %d" ,salida - 1);
		cantErrores++;
	}
	//p2UI16
	if( *p2UI16 != 2 )
	{
		printf( "El valor del parametro *p2UI16:%d es distinto al valor esperado: 2" ,*p2UI16);
		printf( "\nDiferencia: %d" ,*p2UI16 - 2);
		cantErrores++;
	}
	//p3Char
	if( *p3Char != 3 )
	{
		printf( "El valor del parametro *p3Char:%d es distinto al valor esperado: 3" ,*p3Char);
		cantErrores++;
	}
	//p4Bool
	if( (*p4Bool == 0 && 0!=0)||(*p4Bool != 0 && 0==0) )
	{
		printf( "El valor del parametro *p4Bool:%d es distinto al valor esperado: 0" ,*p4Bool);
		cantErrores++;
	}
	//p5Float32
	float AUXp5Float32 = *p5Float32 - 5.0;
	AUXp5Float32 = (AUXp5Float32 >= 0) ? AUXp5Float32 : -AUXp5Float32;
	float PRp5Float32 = pow((float)10, 0);
	if( AUXp5Float32 < PRp5Float32 )
	{
		printf( "El valor del parametro *p5Float32:%d es distinto al valor esperado: 5.0" ,*p5Float32);
		printf( "\nDiferencia: %d" ,AUXp5Float32);
		cantErrores++;
	}
	//p6Float64
	double AUXp6Float64 = *p6Float64 - 6.0;
	AUXp6Float64 = (AUXp6Float64 >= 0) ? AUXp6Float64 : -AUXp6Float64;
	double PRp6Float64 = pow((double)10, 0);
	if( AUXp6Float64 < PRp6Float64 )
	{
		printf( "El valor del parametro *p6Float64:%d es distinto al valor esperado: 6.0" ,*p6Float64);
		printf( "\nDiferencia: %d" ,AUXp6Float64);
		cantErrores++;
	}

	printf( "\nLa prueba PruebaUno ha concluido con %d errores", cantErrores ); 
	return cantErrores;
}
int main()
{
	/*------------Parametros-------------------------*/
	int cantErrores = 0;
	/*------------Llamada a pruebas------------------*/
	if( cantErrores == 0 )
	{
		cantErrores = PruebaUno();
	}
}
