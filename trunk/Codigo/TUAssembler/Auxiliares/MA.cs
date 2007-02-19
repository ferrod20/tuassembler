using System;
using System.IO;

namespace TUAssembler
{
    /// <summary>
    /// M�todos auxiliares
    /// </summary>
    public class MA
    {
        public static string[] Leer( StreamReader lector )
        {
            string[] parametros, parametrosReal;
            int r = 0;
            char letraActual;
            string salida = string.Empty;
            char[] deCorte = {' ', '\n', '\r', '\t'};

            letraActual = (char) lector.Read();
            while( letraActual!=';' )
            {
                if( lector.EndOfStream )
                    throw new Exception( Mensajes.FaltaPuntoYComa );
                salida += letraActual;
                letraActual = (char) lector.Read();
            }

            parametros = salida.Split( deCorte );
            int longReal = parametros.Length;
            foreach( string s in parametros )
                if( s==string.Empty )
                    longReal--;

            parametrosReal = new string[longReal];
            for( int i = 0; i < parametros.Length; i++ )
                if( parametros[i]!=string.Empty )
                {
                    parametrosReal[r] = parametros[i];
                    r++;
                }
            return parametrosReal;
        }
        public static bool SoloEnteros( string cadena )
        {
            bool salida = true;
            foreach( char c in cadena )
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
            bool salida = cadena[0]=='-' || cadena[0]=='+';
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
            bool salida = cadena[0]=='-' || cadena[0]=='+'; //Signo                    
            cadena = cadena.Substring( 1 );

            foreach( char c in cadena )
            {
                encontreComa = c==',';

                if( !encontreComa )
                    salida &= ( char.IsNumber( c ) || c==',' );
                else
                    salida &= char.IsNumber( c );
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
            bool salida = ultimoDigito=='0' || ultimoDigito=='1';

            cadena = cadena.Substring( 0, cadena.Length - 1 );
            foreach( char c in cadena )
                salida &= c=='0';

            return salida;
        }
        public static bool EntreComillas( string cadena )
        {
            return cadena[0]=='"' && cadena[cadena.Length - 1]=='"';
        }
        public static string ExcepcionCompleta( Exception e )
        {
            string salida = string.Empty;
            while( e!=null )
            {
                salida += e.Message;
                e = e.InnerException;
            }
            return salida;
        }
        public static string[] ObtenerElementosDeLaFila( string fila )
        {
            string[] parametros, parametrosReal;
            int r = 0;
            char[] deCorte = {' ', '\n', '\r', '\t'};

            parametros = fila.Split( deCorte );
            int longReal = parametros.Length;
            foreach( string s in parametros )
                if( s==string.Empty )
                    longReal--;

            parametrosReal = new string[longReal];
            for( int i = 0; i < parametros.Length; i++ )
                if( parametros[i]!=string.Empty )
                {
                    parametrosReal[r] = parametros[i];
                    r++;
                }
            return parametrosReal;
        }
        //Devuelve todas las filas, separadas por ":"
        public static string[] LeerMatriz( StreamReader lector )
        {
            string[] parametros, parametrosReal;
            int r = 0;
            char letraActual;
            string salida = string.Empty;
            char[] deCorte = {':'};

            letraActual = (char) lector.Read();
            while( letraActual!=';' )
            {
                if( lector.EndOfStream )
                    throw new Exception( Mensajes.FaltaPuntoYComa );
                salida += letraActual;
                letraActual = (char) lector.Read();
            }

            parametros = salida.Split( deCorte );
            int longReal = parametros.Length;
            foreach( string s in parametros )
                if( s==string.Empty )
                    longReal--;

            parametrosReal = new string[longReal];
            for( int i = 0; i < parametros.Length; i++ )
                if( parametros[i]!=string.Empty )
                {
                    parametrosReal[r] = parametros[i];
                    r++;
                }
            return parametrosReal;
        }
    }
}