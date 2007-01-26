using System;

namespace TUAssembler
{
    internal class Mensajes
    {
        public const string CantidadParametrosEntradaNoCoincideConDefinicion =
            "La cantidad de parametros de entrada no coincide con la definición de la función";

        public const string CantidadDeParametrosNoCoincidenConDefinicion =
            "La cantidad de parametros no coincide con la definición de la función";

        public static string TipoIncorrectoMatriz( int f, int c )
        {
            return "El tipo del elemento de la matriz, en la posición " + f + "," + c + " es incorrecto.";
        }
        public static string TipoIncorrectoVector( int i )
        {
            return "El tipo del elemento del vector, en la posición " + i + " es incorrecto.";
        }
        public static string PrintfValorDistinto( string variable, string valorEsperado )
        {
            return "printf( \"El valor del parametro " + variable + ": %i es distinto al valor esperado: " + valorEsperado  + "\", "+valorEsperado +" );";
        }
        public static string PrintfValorDistintoCadena( string variable, string valorEsperado, int i )
        {
            return "printf( \"El valor de la cadena " + variable + ": de la posicion " + i + " %i es distinto al valor esperado: " + valorEsperado + "\", " + valorEsperado + " );";
        }
    }
}