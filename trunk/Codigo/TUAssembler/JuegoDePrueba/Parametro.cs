using System.IO;
using TUAssembler.Auxiliares;
using TUAssembler.Definicion;

namespace TUAssembler.JuegoDePrueba
{
    public class Parametro
    {
        #region Variables miembro
        private DefParametro definicion;
        #endregion

        #region Propiedades
        public DefParametro Definicion
        {
            get
            {
                if( definicion==null )
                    definicion = new DefParametro();
                return definicion;
            }
            set
            {
                definicion = value;
            }
        }

        public bool EsDeSalidaOEntradaSalida
        {
            get
            {
                return Definicion.EntradaSalida!=EntradaSalida.E;
            }
        }
        #endregion

        #region Métodos
        #region Escritura código C
        public string Declarar()
        {
            string declaracion = string.Empty;
            switch( Definicion.Tipo )
            {
                case Tipo.UInt8:
                    declaracion = "unsigned char ";
                    break;
                case Tipo.UInt16:
                    declaracion = "unsigned short ";
                    break;
                case Tipo.UInt32:
                    declaracion = "unsigned int ";
                    break;
                case Tipo.UInt64:
                    declaracion = "unsigned long ";
                    break;
                case Tipo.Int8:
                    declaracion = "char ";
                    break;
                case Tipo.Int16:
                    declaracion = "short ";
                    break;
                case Tipo.Int32:
                    declaracion = "int ";
                    break;
                case Tipo.Int64:
                    declaracion = "long long int ";
                    // el tipo "long long int" define(al menos en GCC) el entero de 64 bits
                    break;
                case Tipo.Float32:
                    declaracion = "float ";
                    break;
                case Tipo.Float64:
                    declaracion = "double ";
                    break;
                case Tipo.Booleano:
                    declaracion = "bool ";
                    break;
                case Tipo.Char:
                    declaracion = "char ";
                    break;
                case Tipo.CadenaC:
                    declaracion = "char* ";
                    break;
                case Tipo.CadenaPascal:
                    declaracion = "char* ";
                    break;
            }
            if( Definicion.TipoDeAcceso==ValorOReferencia.R )
                declaracion += "*";
            if( Definicion.EsElemento )
                declaracion += Definicion.Nombre + ";";
            /*if( Definicion.EsVector )
                declaracion += Definicion.Nombre + "[" + Definicion.Longitud + "];";
             * */
            /* if( Definicion.EsMatriz )
                declaracion += Definicion.Nombre + "[" + Definicion.CantFilas + "][" + Definicion.CantColumnas + "];";
            */
            return declaracion;
        }
        public virtual void Instanciar( StreamWriter escritor )
        {            
        }
        public virtual void CompararValor(  EscritorC escritor )
        {            
        }
        #endregion
        //No borrar, sirve para definir el metodo en las clases hijas( ParamVector, ParamMatriz y Elem )
        public virtual void Leer( StreamReader lector )
        {
        }
        #endregion
    }
}