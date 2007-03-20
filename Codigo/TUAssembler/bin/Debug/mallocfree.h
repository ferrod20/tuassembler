#include <stdio.h>

#ifndef LIBRERIAMALLOCFREE
#define LIBRERIAMALLOCFREE

#define bool int
#define true 1
#define false 0
#define todoBien 0
#define liberarPosMemNoValida 1
#define escrituraFueraDelBuffer 2
#define dosFreeDelMismoBuffer 3

#define cantMaxPedidos 10000
int cantPedidosMemoria = 0;
//const int cantMaxPedidos = 10000;
char* pedidos[sizeof(int)*cantMaxPedidos];
int tamanioPedidos[sizeof(int)*cantMaxPedidos];
bool fueLiberado[sizeof(bool)*cantMaxPedidos];
bool debeContabilizarse[sizeof(bool)*cantMaxPedidos];

char* malloc2(int cantBytes, bool debeContabil){
   int i;
   char* ret_value;
   if (cantPedidosMemoria >= cantMaxPedidos){
	printf("Se ha superado la cantidad maxima de pedidos de memoria permitidos. El programa finalizara.\n");
	exit(1);
   }
   ret_value = (char *)malloc(cantBytes + 8 + 8);	//8 bytes antes y 8 bytes despues para controlar que no se pase de la longitud del buffer
   pedidos[cantPedidosMemoria] = ret_value;
   tamanioPedidos[cantPedidosMemoria] = cantBytes;
   fueLiberado[cantPedidosMemoria] = false;
    debeContabilizarse[cantPedidosMemoria] = debeContabil;
   for(i=0; i<8; i++){
       ((char*)ret_value)[i] = 'A';
       ((char*)ret_value)[cantBytes + 8 + i] = 'A';
   }
   cantPedidosMemoria++;
   //printf("[%d]\n", ret_value + 8);
   return ret_value + 8;
}

void free2all(){
   int i;
   int bytesNoLiberados = 0;
   for(i=0; i<cantPedidosMemoria; i++){
       free(pedidos[i]);
       if(fueLiberado[i]== false && debeContabilizarse[i])
           bytesNoLiberados = bytesNoLiberados + tamanioPedidos[i];
   }
   if(bytesNoLiberados >0){
       printf("No se han liberado %d bytes de memoria", bytesNoLiberados);
       exit(0);
   }
}

int free2(char* punteroABloque)
{
   static int cantLlamadosCorrectos = 0;
   int pos, i;
   for(pos=0; pos<cantPedidosMemoria && pedidos[pos]!=punteroABloque-8; pos++);
       if(pedidos[pos] !=punteroABloque-8){
           	//printf("Se intento liberar una posicion de memoria no valida Anteriormente se llamo exitosamente a free %d veces ", cantLlamadosCorrectos);
           	free2all();
       		return liberarPosMemNoValida;;    
       }else{
           if (fueLiberado[pos]){  
		//printf("Se intentaron hacer 2 free del mismo buffer. Anteriormente se llamo exitosamente a free %d veces\n", cantLlamadosCorrectos);  
		free2all();  
		return dosFreeDelMismoBuffer;    
           } 
           fueLiberado[pos] = true;
           for (i = 0; i < 8; i++)
               if(((char*)punteroABloque-8)[i] != 'A'|| ((char*)punteroABloque)[tamanioPedidos[pos] + i] != 'A'){
			//printf("Se ha escrito fuera del buffer");                  
			free2all();  
			return escrituraFueraDelBuffer;
               }
       }
       cantLlamadosCorrectos++; 
       return todoBien;
}

bool fuePedido(char* punteroABloque)
{
	bool ret_value= false;
	int i;
	for(i=0; i<cantPedidosMemoria; i++){
		ret_value = ret_value || ((pedidos[i]+8) == punteroABloque);
		//printf("<%d>\n", pedidos[i]);
	}
	return ret_value;

}


#endif
