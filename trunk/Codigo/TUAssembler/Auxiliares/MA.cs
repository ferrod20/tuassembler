using System;
using System.IO;
using TUAssembler.Definicion;

namespace TUAssembler
{
    /// <summary>
    /// Métodos auxiliares
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
            bool salida = cadena[0]=='-' || char.IsNumber( cadena[0] );
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
            bool encontreComa;
            bool salida = cadena[0]=='-' || char.IsNumber( cadena[0] ); //Signo                    
            cadena = cadena.Substring( 1 );
            int cantComas = 0;

            foreach( char c in cadena )
            {
                encontreComa = c=='.';

                if( !encontreComa )
                    salida &= char.IsNumber( c );
                else
                    cantComas++;
            }
            return salida && cantComas < 2;
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
        public static bool EntreComillasSimples( string cadena )
        {
            return cadena[0]=='\'' && cadena[cadena.Length - 1]=='\'';
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
        public static int CuantosBytes( Tipo tipo )
        {
            int cantBytes = 0;
            switch( tipo )
            {
                case Tipo.UInt8:
                case Tipo.Int8:
                case Tipo.Char:
                    cantBytes = 1;
                    break;
                case Tipo.Int16:
                case Tipo.UInt16:
                    cantBytes = 2;
                    break;
                case Tipo.Booleano:
                case Tipo.Float32:
                case Tipo.Int32:
                case Tipo.UInt32:
                    cantBytes = 4;
                    break;
                case Tipo.Int64:
                case Tipo.UInt64:
                case Tipo.Float64:
                    cantBytes = 8;
                    break;
                case Tipo.CadenaC:
                case Tipo.CadenaPascal:
                    throw new Exception( Mensajes.CadenaNoSoportadaParaEstaOpcion );
            }
            return cantBytes;
        }
        public static void EliminarAsteriscos( ref string cadena )
        {
            int desde;
            desde = cadena.IndexOf( '*' );
            if( desde >= 0 )
                cadena = cadena.Remove( desde, 1 );
        }
        public static void EliminarCorchetes( ref string cadena )
        {
            int desde, hasta;
            desde = cadena.IndexOf( '[' );
            if( desde >= 0 )
                cadena = cadena.Remove( desde, 1 );
            hasta = cadena.IndexOf( ']' );
            if( hasta >= 0 )
                cadena = cadena.Remove( hasta, 1 );
        }
    }
}