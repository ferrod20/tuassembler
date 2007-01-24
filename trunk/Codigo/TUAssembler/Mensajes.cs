using System;
using System.Collections.Generic;
using System.Text;

namespace TUAssembler
{
    class Mensajes
    {
        public const string CantidadParametrosEntradaNoCoincideConDefinicion = "La cantidad de parametros de entrada no coincide con la definición de la función";
        public const string CantidadDeParametrosNoCoincidenConDefinicion = "La cantidad de parametros no coincide con la definición de la función";
        public static string TipoIncorrectoMatriz( int f, int c )
        {
            return "El tipo del elemento de la matriz, en la posición " + f + "," + c + " es incorrecto.";
        }
        public static string TipoIncorrectoVector( int i )
        {
            return "El tipo del elemento del vector, en la posición " + i + " es incorrecto.";
        }
    }
}
