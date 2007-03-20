using System.IO;

namespace TUAssembler.Auxiliares
{
    public class EscritorC: StreamWriter
    {
        #region Variables miembro
        private int identacion;
        private bool identacionActiva;
        #endregion

        #region Constructores
        public EscritorC( string path )
            : base( path )
        {
            identacion = 0;
        }
        #endregion

        #region Métodos
        public bool IdentacionActiva
        {
            get
            {
                return identacionActiva;
            }
            set
            {
                identacionActiva = value;
            }
        }

        private string Espacios()
        {
            int i = 0;
            string espacios = string.Empty;
            if( IdentacionActiva )
                while( identacion > i )
                {
                    espacios += "\t";
                    i++;
                }

            return espacios;
        }

        #region Escritura general
        public void EntreCorchetes( string valor )
        {
            WriteLine( "{" );
            identacion ++;
            WriteLine( valor );
            identacion --;
            WriteLine( "}" );
        }
        public void EntreCorchetes( params string[] lineas )
        {
            WriteLine( "{" );
            identacion++;
            foreach( string linea in lineas )
                WriteLine( linea );
            identacion--;
            WriteLine( "}" );
        }
        public override void WriteLine( string valor )
        {
            valor = Espacios() + valor;
            base.WriteLine( valor );
        }
        #endregion

        #region Instrucciones
        public void AbrirCorchetes()
        {
            WriteLine( "{" );
            identacion++;
        }
        public void CerrarCorchetes()
        {
            identacion--;
            WriteLine( "}" );
        }
        public void If( string condicion )
        {
            WriteLine( "if( " + condicion + " )" );
            AbrirCorchetes();
        }
        public void ElseIf()
        {
            CerrarCorchetes();
            WriteLine( "else" );
            AbrirCorchetes();
        }
        public void FinIf()
        {
            CerrarCorchetes();
        }
        public void For( string inicializacion, string condicion, string asignacion )
        {
            WriteLine( "for( " + inicializacion + "; " + condicion + "; " + asignacion + " )" );
            AbrirCorchetes();
        }
        public void FinFor()
        {
            CerrarCorchetes();
        }
        public void While( string condicion )
        {
            WriteLine( "while( " + condicion + " )" );
            AbrirCorchetes();
        }
        public void FinWhile()
        {
            CerrarCorchetes();
        }
        #endregion

        #region PrintF
        public void PrintfNoSePudoAbrirElArchivo( string archivo )
        {
            Printf( "No se pudo abrir el archivo " + archivo + ".\\n" );
        }
        public void PrintfPruebasConcluidas()
        {
            Printf( "Todas las pruebas han concluido." );
        }
        public void PrintfPruebaConcluida()
        {
            Printf( "La prueba " + Mensajes.NombreDePrueba + " ha concluido con %d errores.\\n", "cantErrores" );
        }
        public void PrintfValorDistintoChar( string variable, string valorEsperado )
        {
            PrintfError(
                "El valor de " + variable +
                    ": %c(%d) es distinto al valor esperado: " + valorEsperado + "(%d)\\n", variable, variable,
                "'" + valorEsperado + "'" );
        }
        public void PrintfValorDistinto( string variable, string valorEsperado )
        {
            PrintfError(
                "El valor de " + variable +
                    ": %d es distinto al valor esperado: " + valorEsperado + "\\n", variable );
        }
        public void PrintfValorDistintoConDiferencia( string variable, string valorEsperado, string varDiferencia )
        {
            PrintfValorDistinto( variable, valorEsperado );
            Printf( "Diferencia: %d\\n", varDiferencia );
        }
        public void PrintfValorDistintoConDiferencia( string variable, string valorEsperado )
        {
            PrintfValorDistinto( variable, valorEsperado );
            Printf( "Diferencia: %d\\n", variable + " - " + valorEsperado );
        }
        public void PrintfEscrituraFueraDelBuffer( string nombreVariable )
        {
            string texto = "Se ha escrito fuera del buffer en el parámetro " +
                nombreVariable + ".\\n";

            PrintfError( texto );
        }
        public void PrintfCambioDeDireccionDelPuntero( string nombreVariable )
        {
            string texto = "Se ha cambiado la dirección del parámetro " +
                nombreVariable + " por una dirección inválida.\\n";

            PrintfError( texto );
        }
        public void PrintfEscrituraFueraDelBufferEnFilaDeMatriz( string nombreMatriz, string varFila )
        {
            string texto = "Se ha escrito fuera del buffer en el parámetro " +
                nombreMatriz + " en la fila %d\\n";

            PrintfError( texto, varFila );
        }
        public void PrintfCambioDeDireccionDelPunteroEnFilaDeMatriz( string nombreMatriz, string varFila )
        {
            string texto = "Se ha cambiado la dirección del parámetro " +
                nombreMatriz + " por una dirección inválida, en la fila: %d\\n";

            PrintfError( texto, varFila );
        }
        public void PrintListaCircular()
        {
            PrintfError( "es una Lista Circular." );
        }
        public void PrintPunterosInvalidos()
        {
            Printf("La prueba " + Mensajes.NombreDePrueba + " tiene punteros invalidos");
        }
        public void PrintfDosFreeAlMismoParam( string nombre )
        {
            PrintfError( "Se hicieron 2 free al mismo parámetro: " + nombre + "\\n" );
        }
        public void PrintfDosFreeAlMismoParamEnFilaDeMatriz( string nombreMatriz, string varFila )
        {
            string texto = "Se han hecho dos free al mismo parámetro " +
                nombreMatriz + " , en la fila: %d\\n";

            PrintfError( texto, varFila );
        }
        public void PrintfValorDeStringDistintos( string variable, string iterador, string diferencia )
        {
            string cEsperado = diferencia +
                "[" + iterador + "]";
            string cObtenido = variable +
                "[" + iterador + "]";
            PrintfError( "El valor de la cadena " + variable +
                ": %c(%d) de la posicion %d es distinto al valor esperado: %c(%d) \\n", cObtenido, cObtenido, iterador,
                         cEsperado, cEsperado );
        }
        private void PrintfError( string texto, params string[] variables )
        {
            Printf( "Error " + Mensajes.NombreDePrueba + ": " + texto, variables );
        }
        public void Printf( string texto, params string[] variables )
        {
            Write( Espacios() );
            Write( "printf( \"" + texto + "\"" );
            foreach( string variable in variables )
                Write( " ," + variable );
            Write( " );" );
            WriteLine();
        }
        #endregion

        #endregion

        public void PrintfValorDistintoFloatConDiferencia( string variable, string valorEsperado, string varDiferencia )
        {
            //%10.2f Para los float 10 digitos, 2 de precision
            PrintfError(
                "El valor de " + variable +
                    ": %f es distinto al valor esperado: " + valorEsperado + "\\n", variable );
            Printf( "Diferencia: %f\\n", varDiferencia );
        }
    }
}