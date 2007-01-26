extern unsigned short funcion1( unsigned char );

 void main()
{

/*------------Parametros-------------------------*/

unsigned short UInt8a;
unsigned char UInt8b;

/*------------Instanciacion----------------------*/

UInt8a = 8;
UInt8b = 54;

/*------------LlamadaFuncion---------------------*/

UInt8a = funcion1( UInt8b );

/*------------Comparacion de valores-------------*/


if ( UInt8b != 34 )
printf( "El valor del parametro UInt8b: %i es distinto al valor esperado: 34", 34 );


}
