using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TUAssembler.Auxiliares
{
    public class EscritorC:StreamWriter
    {
        #region Variables miembro
        private int identacion;
        private bool identacionActiva;
        #endregion
        public EscritorC( string path ): base( path )
        {
            identacion = 0;
        }

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

        public void EntreCorchetes( string valor )
        {
            WriteLine( "{" );
            identacion ++;
            WriteLine( valor );
            identacion --;
            WriteLine( "}" );
        }
        public void EntreCorchetes( params string[] lineas)
        {
            WriteLine("{");
            identacion++;
            foreach( string linea in lineas )
                WriteLine(linea);
            identacion--;
            WriteLine("}");
        }

        public void If(string condicion)
        {
            WriteLine("if( " + condicion + " )");
            AbrirCorchetes();
        }
        
        public void Printf( string texto, params string[] variables )
        {
            Write( Espacios() );
            Write("printf( \"" + texto + "\"");
            foreach (string variable in variables )
                Write( " ," + variable);
            Write( ");" );
            WriteLine();            
        }
        
        public override void WriteLine( string valor )
        {
            valor = Espacios() + valor;
            base.WriteLine( valor );
        }
        
        private string Espacios()
        {
            int i = 0;
            string espacios = string .Empty;
            if( IdentacionActiva )
                while ( identacion > i)
                {
                    espacios += "\t";
                    //espacios += "    ";
                    i++;
                }
         
            return espacios;
        }
        public void AbrirCorchetes()
        {
            WriteLine( "{");
            identacion++;
        }
        public void CerrarCorchetes()
        {
            identacion--;
            WriteLine("}");            
        }
        public void FinIf()
        {
            CerrarCorchetes();
        }
        public void For( string inicializacion, string condicion, string asignacion )
        {
            WriteLine("for( " + inicializacion + "; " + condicion+"; " + asignacion + " )");
            AbrirCorchetes();
        }
        public void FinFor()
        {
            CerrarCorchetes();
        }
        public void PrintfValorDistinto( string variable, string valorEsperado )
        {
            //%10.2f Para los float 10 digitos, 2 de precision
            Printf( 
                "El valor del parametro " + variable + ":%d es distinto al valor esperado: " + valorEsperado,  variable );                     
        }
        public void PrintfValorDistintoConDiferencia( string variable, string valorEsperado, string varDiferencia )
        {     
            PrintfValorDistinto( variable, valorEsperado );
            Printf("\\nDiferencia: %d", varDiferencia );
        }
    }
}
