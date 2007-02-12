using System;
using System.CodeDom.Compiler;
using System.IO;

namespace TUAssembler.Compilacion
{
    internal class Ejecutor
    {
        public static void Ejecutar( string comando )
        {
            string salida = "salidaEjecucion.txt";
            string error = "errorEjecucion.txt";

            TempFileCollection archivosTemporales = new TempFileCollection();

            try
            {
                File.Delete( salida );
                File.Delete(error);
                Executor.ExecWaitWithCapture( comando, archivosTemporales, ref salida, ref error );
            }
            catch( Exception e )
            {
                throw new Exception( Mensajes.ErrorAlEjecutar( e ) );
            }
        }
    }
}