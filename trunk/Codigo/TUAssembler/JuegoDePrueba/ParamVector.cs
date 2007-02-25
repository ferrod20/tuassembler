using System;
using System.IO;
using TUAssembler.Auxiliares;
using TUAssembler.Definicion;

namespace TUAssembler.JuegoDePrueba
{
    [Serializable()]
    public class ParamVector: Parametro
    {
        #region Variables miembro
        private Elem[] elem;
        #endregion

        #region Propiedades
        public Elem[] Elementos
        {
            get
            {
                return elem;
            }
            set
            {
                elem = value;
            }
        }

        public Elem this[ int indice ]
        {
            get
            {
                return Elementos[indice];
            }
            set
            {
                Elementos[indice] = value;
            }
        }

        public int Longitud
        {
            get
            {
                return Elementos.Length;
            }
        }
        #endregion

        #region Constructores
        public ParamVector()
        {
        }
        public ParamVector( int longitud )
        {
            Elementos = new Elem[longitud];
        }
        public void EstablecerLongitud( int longitud )
        {
            Elementos = new Elem[longitud];
        }
        #endregion

        #region Métodos
        public void EstablecerValor( string[] fila )
        {
            EstablecerLongitud( fila.Length );
            int i = 0;

            foreach( string elemento in fila )
            {
                Elem elem = new Elem( elemento );
                if( !elem.TipoCorrecto( Definicion.Tipo ) )
                    throw new Exception( Mensajes.ElementoDeTipoIncorrectoEnElVector( Definicion.Nombre, i ) );
                Elementos[i] = elem;
                i++;
            }
        }
        public override void Leer( StreamReader lector )
        {
            string[] parametros;
            try
            {
                parametros = MA.Leer( lector );
            }
            catch( Exception e )
            {
                throw new Exception( Mensajes.ErrorAlLeerParametro( Definicion.Nombre, e ) );
            }

            EstablecerValor( parametros );
        }

        #region Métodos de código C
        public override void Declarar( EscritorC escritor )
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
                    declaracion = "char ";
                    break;
                case Tipo.CadenaPascal:
                    declaracion = "char ";
                    break;
            }
            declaracion += "*" + Definicion.Nombre + ";";
            escritor.WriteLine( declaracion );
        }
        public override void PedirMemoria( EscritorC escritor )
        {
            string pedido;
            int cantMemoria;
            cantMemoria = Longitud*MA.CuantosBytes( Definicion.Tipo );
            pedido = Definicion.Nombre + " = " + "malloc2( " + cantMemoria + " );";
            escritor.WriteLine( pedido );
        }
        public override void Instanciar( EscritorC escritor )
        {
            string instanciacion;
            int i = 0;

            foreach( Elem elemento in Elementos )
            {
                instanciacion = Definicion.Nombre + "[" + i + "] = " + elemento.Valor + ";";
                escritor.WriteLine( instanciacion );
                i++;
            }
        }
        public override void CompararValor( EscritorC escritor )
        {
            escritor.WriteLine( "//" + Definicion.Nombre );
            for( int i = 0; i < Elementos.Length; i++ )
                Elementos[i].CompararValor( escritor, Definicion.Nombre + "[" + i + "]" );
        }
        public override void LiberarMemoria( EscritorC escritor )
        {
            escritor.WriteLine( "salidaFree2 = free2( " + Definicion.Nombre + " );" );
            escritor.If( "salidaFree2 == escrituraFueraDelBuffer" );
            escritor.PrintfEscrituraFueraDelBuffer( Definicion.Nombre );
            escritor.WriteLine( "cantErrores++;" );
            escritor.FinIf();
            escritor.If( "salidaFree2 == liberarPosMemNoValida" );
            escritor.CambioDeDireccionDelPuntero( Definicion.Nombre );
            escritor.WriteLine( "cantErrores++;" );
            escritor.FinIf();
        }
        #endregion

        #endregion
    }
}