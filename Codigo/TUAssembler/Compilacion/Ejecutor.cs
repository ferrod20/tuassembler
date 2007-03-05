using System;
using System.CodeDom.Compiler;
using System.IO;

namespace TUAssembler.Compilacion
{
    internal class Ejecutor
    {
        #region Variables miembro
        public static string ArchivoSalida;
        public static string ArchivoError;
        #endregion

        public static void Ejecutar( string comando )
        {
            TempFileCollection archivosTemporales = new TempFileCollection();
            try
            {
                File.Delete( ArchivoSalida );
                File.Delete( ArchivoError );
                Executor.ExecWaitWithCapture( comando, archivosTemporales, ref ArchivoSalida, ref ArchivoError );
            }
            catch( Exception e )
            {
                throw new Exception( Mensajes.ErrorAlEjecutar( e ) );
            }
        }
        public static string ObtenerSalida()
        {
            string salida;
            StreamReader sr = new StreamReader( ArchivoSalida );
            //Tiro las 2 primeras lineas q son basura!
            sr.ReadLine();
            sr.ReadLine();
            salida = sr.ReadToEnd();
            return salida;
        }
        public static void BorrarArchivosTemporales( bool borrarSalida )
        {
            if( borrarSalida )
                File.Delete( ArchivoSalida );
            File.Delete( ArchivoError );
        }
    }
}