namespace TUAssembler
{
    public class Enlazador: Compilador
    {
        public Enlazador( string directorio, string nombre )
            : base( directorio, nombre, "salidaE.txt", "archivoErrorE.txt" )
        {
        }
        public void Enlazar( string ejecutable, params string[] archivosObjeto )
        {
            string comando;
            comando = "-o " + ejecutable;
            foreach( string archivo in archivosObjeto )
                comando += " " + archivo;

            Compilar( comando );
        }
    }
}