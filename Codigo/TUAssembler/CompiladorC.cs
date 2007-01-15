namespace TUAssembler
{
    public class CompiladorC: Compilador
    {
        public CompiladorC( string directorioDelCompilador, string nombreDelCompilador )
            : base( directorioDelCompilador, nombreDelCompilador, "SalidaC.txt", "ErrorC.txt" )
        {
        }
        public void Compilar( string opciones, string nombreArchivoACompilar )
        {
            Compilar( opciones + " " + nombreArchivoACompilar );
        }
    }
}