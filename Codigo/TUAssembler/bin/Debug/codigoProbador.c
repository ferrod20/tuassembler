#include <stdio.h>
#define bool int
#define true 1
#define false 0
extern unsigned long int funcion1(  );

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


int main()
{

/*------------Parametros-------------------------*/

unsigned long salida;
int cantErrores = 0;

/*------------Instanciacion----------------------*/


/*------------LlamadaFuncion---------------------*/

salida = funcion1(  );

/*------------Comparacion de valores-------------*/


if ( salida != 101 )
{
    printf( "El valor del parametro salida:%d es distinto al valor esperado: 101", salida);
printf( "\nDiferencia: %d ", salida-101 );
cantErrores++;
}


printf( "\nLa prueba ha concluido con %d errores", cantErrores ); 

return 0;

}
