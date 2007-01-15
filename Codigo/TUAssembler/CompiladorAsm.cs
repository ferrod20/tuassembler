namespace TUAssembler
{
    public class CompiladorAsm: Compilador
    {
        public CompiladorAsm( string directorioDelCompilador, string nombreDelCompilador )
            : base( directorioDelCompilador, nombreDelCompilador, "SalidaAsm.txt", "ErrorAsm.txt" )
        {
        }
        //[-@ response file] [-o outfile] [-f format] [-l listfile] [options...] [--] filename
        public void Compilar( string archivoRespuesta, string archivoSalida, string formato, string listaArchivos,
                              string opciones, string nombreArchivoACompilar )
        {
            string comando = string.Empty;
            comando += GenerarComando( "-@ ", archivoRespuesta );
            comando += GenerarComando( " -o ", archivoSalida );
            comando += GenerarComando( " -f ", formato );
            comando += GenerarComando( " -l ", listaArchivos );

            comando += " " + opciones + " " + nombreArchivoACompilar;
            Compilar( comando );
        }
        private string GenerarComando( string token, string atributo )
        {
            string salida = string.Empty;
            if( atributo!=string.Empty )
                salida = token + atributo;
            return salida;
        }
        public void Compilar( string opciones, string nombreArchivoACompilar )
        {
            Compilar( "", "", "", "", opciones, nombreArchivoACompilar );
        }
    }
}