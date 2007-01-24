using System.CodeDom.Compiler;

namespace TUAssembler.Compilacion
{
    internal class Ejecutor
    {
        public static void Ejecutar( string comando )
        {
            TempFileCollection archivosTemporales = new TempFileCollection();
            string salida = "salida.txt";
            string error = "error.txt";
            Executor.ExecWaitWithCapture( comando, archivosTemporales, ref salida, ref error );
        }
    }
}