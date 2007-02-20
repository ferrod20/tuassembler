namespace TUAssembler.Compilacion
{
    public class CompiladorC: Compilador
    {
        #region Constructores
        public CompiladorC( string directorioDelCompilador, string nombreDelCompilador )
            : base( directorioDelCompilador, nombreDelCompilador, "SalidaC.txt", "ErrorC.txt" )
        {
        }
        #endregion

        #region Métodos
        public void Compilar( string opciones, string nombreArchivoACompilar )
        {
            Compilar( opciones + " " + nombreArchivoACompilar );
        }
        #endregion
    }
}