using System;
using TUAssembler.Compilacion;

namespace TUAssembler
{
    public class Mensajes
    {
        public static string NombreDePrueba;

        #region Constantes
        public static string ErrorVerificacionDeDefinicion = "La definición de la función no es válida.";

        public static string CantidadParametrosEntradaNoCoincideConDefinicion =
            "La cantidad de parametros de entrada de la prueba " + NombreDePrueba +
                " no coincide con la definición de la función";

        public static string CantidadDeParametrosNoCoincidenConDefinicion =
            "La cantidad de parametros de la prueba " + NombreDePrueba + " no coincide con la definición de la función";

        public static string NombrePruebaNoPermitido =
            "El nombre que intenta poner a la prueba no esta permitido. Debe ser un nombre con las mismas caracteristicas que tiene el nombre de una función de C.";

        public static string FaltaPuntoYComa = "Debe haberse olvidado de poner un ';' en algún lado.";
        public static string CadenaNoSoportadaParaEstaOpcion = "Esta opción no soporta cadenas como tipo básico.";

        public static string ParametroCantidadDePruebasIncorrecto =
            "El primer parametro debe ser un numerico serguido de ; que indica la cantidad de pruebas.";

        public static string FinDePruebaIncorrecto = "Al finalizar la prueba debe escribir 'FinDePrueba'";
        #endregion

        #region Métodos
        public static string ErrorAlLeerParametro( string nombre, Exception e )
        {
            return "Se ha producido un error al leer el parámetro " + nombre + "\n" + e.Message;
        }
        public static string ErrorAlLeerParametroDeEntrada( Exception e )
        {
            string mensaje;
            mensaje = "Se produjo un error al leer los parámetros de entrada de la función en la prueba:" +
                NombreDePrueba + "\n";
            ;
            mensaje += "Descripción: \n" + MA.ExcepcionCompleta( e ) + "\n\n";
            return mensaje;
        }
        public static string ErrorAlLeerParametroDeSalida( Exception e )
        {
            string mensaje;
            mensaje = "Se produjo un error al leer los parámetros de salida de la función en la prueba: " +
                NombreDePrueba + "\n";
            mensaje += "Descripción: \n" + MA.ExcepcionCompleta( e ) + "\n\n";
            return mensaje;
        }
        public static string ErrorLecturaDefinicion( string archivo, Exception e )
        {
            string mensaje = string.Empty;
            mensaje = "Se produjo un error en la lectura de la definición de la función.\n";
            mensaje += "Archivo: " + archivo + "\n";
            mensaje += "Descripción: \n" + MA.ExcepcionCompleta( e ) + "\n\n";
            return mensaje;
        }
        public static string ErrorAlEjecutar( Exception e )
        {
            return ErrorAlEjecutar( MA.ExcepcionCompleta( e ) );
        }
        public static string ErrorAlEjecutar( string descripcion )
        {
            return "Error al ejecutar el código probador: \n" + descripcion;
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
            mensaje += "\nDescripción: \n" + descripcion + "\n\n";

            return mensaje;
        }
        public static string TipoIncorrectoMatriz( int f, int c )
        {
            return
                "Prueba " + NombreDePrueba + ": El tipo del elemento de la matriz, en la posición " + f + "," + c +
                    " de la prueba " + NombreDePrueba + " es incorrecto.";
        }
        public static string TipoIncorrectoVector( int i )
        {
            return
                "Prueba " + NombreDePrueba + ": El tipo del elemento del vector, en la posición " + i + " de la prueba " +
                    NombreDePrueba + " es incorrecto.";
        }
        /*
        public static string PrintfValorDistinto( string variable, string valorEsperado )
        {
            //%10.2f Para los float 10 digitos, 2 de precision
            string salida =
                "printf( \"Prueba " + NombreDePrueba + ": El valor del parametro " + variable +
                    ":%d es distinto al valor esperado: " + valorEsperado +
                        "\", " + variable + ");";
            salida += "\nprintf( \"\\nDiferencia: %d \", " + variable + "-" + valorEsperado + " );";
            return salida;
        }
        public static string PrintfValorDistintoCadena( string variable, string valorEsperado, int i )
        {
            return
                "printf( \"Prueba " + NombreDePrueba + ": El valor de la cadena " + variable + ": de la posicion " + i +
                    " %i es distinto al valor esperado: " + valorEsperado + "\", " + valorEsperado + " );";
        }
         * */
        #endregion

        public static string ElementoDeTipoIncorrectoEnElVector( string nombreVector, int posicion )
        {
            return
                "El elemento del vector " + nombreVector + ", en la posición " + posicion + " no es del tipo correcto.";
        }
        public static string MatrizTieneFilaEnDeDistintaLongitud( string nombreMatriz, int fila )
        {
            return
                "La fila " + fila + " de la matriz " + nombreMatriz + " tiene longitud diferente a las demás.";
        }
        public static string ElementoDeTipoIncorrectoEnLaMatriz( string nombreMatriz, int fila, int col )
        {
            return
                "El elemento de la matriz " + nombreMatriz + ", en la posición " + fila + " " + col +
                    " no es del tipo correcto.";
        }
        public static string ErrorVerificacionDefinicion( string archivo, Exception e )
        {
            string mensaje = string.Empty;
            mensaje = ErrorVerificacionDeDefinicion + "\n";
            mensaje += "Archivo: " + archivo + "\n";
            mensaje += "Descripción: \n" + MA.ExcepcionCompleta( e ) + "\n\n";
            return mensaje;
        }
    }
}