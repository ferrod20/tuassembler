using System;
using System.Collections.Generic;
using System.Text;

namespace TUAssembler
{
    /// <summary>
    /// Métodos auxiliares
    /// </summary>
    public class MA
    {
        public static bool SoloEnteros( string cadena )
        {
            bool salida = true;
            foreach (char c in cadena)
                salida &= char.IsNumber( c );

            return salida;
        }
        /// <summary>
        /// Comienza con un signo
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public static bool SoloEnterosConSigno( string cadena )
        {            
            bool salida = cadena[0] == '-' || cadena[0] == '+';
            salida &= SoloEnteros( cadena.Substring( 1 ) );
            return salida;
        }

        /// <summary>
        /// Comienza con el signo y puede tener una coma
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public static bool EsPtoFlotante( string cadena )
        {
            bool encontreComa = false;
            bool salida = cadena[0] == '-' || cadena[0] == '+';//Signo                    
            cadena = cadena.Substring( 1 );

            foreach (char c in cadena)
            {
                encontreComa = c==',';
                    
                if (!encontreComa)
                    salida &= (char.IsNumber(c) || c == ',');
                else
                    salida &= char.IsNumber(c);
            }
            return salida;

        }
        /// <summary>
        /// Todos ceros y al final un 1 o 0
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public static bool EsBool( string cadena )
        {
            char ultimoDigito = cadena[cadena.Length - 1];
            bool salida = ultimoDigito == '0' || ultimoDigito == '1';

            cadena = cadena.Substring( 0, cadena.Length - 1 );
            foreach (char c in cadena)
                salida &= c == '0';

            return salida;            
        }
        public static bool EntreComillas( string cadena )
        {
            return cadena[0] == '"' && cadena[cadena.Length -1 ] == '"';
        }
    }
    
    
}
