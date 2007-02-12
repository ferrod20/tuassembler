using System;
using TUAssembler.Compilacion;

namespace TUAssembler
{
    internal class Mensajes
    {
        public const string CantidadParametrosEntradaNoCoincideConDefinicion =
            "La cantidad de parametros de entrada no coincide con la definición de la función";

        public const string CantidadDeParametrosNoCoincidenConDefinicion =
            "La cantidad de parametros no coincide con la definición de la función";

        public static string ErrorAlLeerParametroDeEntrada( Exception e )
        {
            string mensaje = string.Empty;
            mensaje = "Se produjo un error al leer los parámetros de entrada de la función:\n";
            mensaje += "Descripción: " + MA.ExcepcionCompleta( e ) + "\n\n";
            return mensaje;
        }
        public static string ErrorAlLeerParametroDeSalida( Exception e )
        {
            string mensaje = string.Empty;
            mensaje = "Se produjo un error al leer los parámetros de salida de la función:\n";
            mensaje += "Descripción: " + MA.ExcepcionCompleta( e ) + "\n\n";
            return mensaje;
        }
        public static string ErrorLecturaDefinicion( string archivo, Exception e )
        {
            string mensaje = string.Empty;
            mensaje = "Se produjo un error en la lectura de la definición de la función:\n";
            mensaje += "Archivo: " + archivo + "\n";
            mensaje += "Descripción: " + MA.ExcepcionCompleta( e ) + "\n\n";
            return mensaje;
        }
        public static string ErrorAlEjecutar( Exception e )
        {
            return ErrorAlEjecutar( MA.ExcepcionCompleta( e ) );
        }
        public static string ErrorAlEjecutar( string descripcion )
        {
            return "Error al ejecutar el código probador: " + descripcion;
        }
        public static string ErrorAlCompilar( Compilador compilador, string comando, Exception excepcion )
        {
            return ErrorAlCompilar( compilador, comando, MA.ExcepcionCompleta( excepcion ) );
        }
        public static string ErrorAlCompilar( Compilador compilador, string comando, string descripcion )
        {
            string mensaje = string.Empty;

            if( compilador is CompiladorAsm )
                mensaje = "Error de compilación ASM: ";
            if( compilador is CompiladorC )
                mensaje = "Error de compilación C: ";
            if( mensaje==string.Empty )
                mensaje = "Error en enlace: ";
            mensaje += "\nComando: " + comando;
            mensaje += "\nDescripción: " + descripcion + "\n\n";

            return mensaje;
        }
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
            return
                "printf( \"El valor del parametro " + variable + ":  es distinto al valor esperado: " + valorEsperado +
                    "\" );";
        }
        /*      public static string PrintfValorDistinto(string variable, string valorEsperado)
        {
            return "printf( \"El valor del parametro " + variable + ": %i es distinto al valor esperado: " + valorEsperado + "\", " + valorEsperado + " );";
        }

*/
        public static string PrintfValorDistintoCadena( string variable, string valorEsperado, int i )
        {
            return
                "printf( \"El valor de la cadena " + variable + ": de la posicion " + i +
                    " %i es distinto al valor esperado: " + valorEsperado + "\", " + valorEsperado + " );";
        }
    }
}