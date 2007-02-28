using System;
using System.IO;
using TUAssembler.Auxiliares;
using TUAssembler.Definicion;

namespace TUAssembler.JuegoDePrueba
{
    [Serializable()]
    public class ParamLista: Parametro
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
        public ParamLista()
        {
        }
        public ParamLista( int longitud )
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
                case Tipo.Char:
                case Tipo.UInt8:
                case Tipo.Int8:
                    declaracion = "char ";
                    break;
                case Tipo.UInt32:
                case Tipo.Int32:
                    declaracion = "int ";
                    break;
                case Tipo.Float32:
                    declaracion = "float ";
                    break;
                case Tipo.Float64:
                    declaracion = "double ";
                    break;
                default:
                    throw new Exception( "Tipo de lista no valido" );
            }
            declaracion = "struct Lista" + declaracion + " *" + Definicion.Nombre + " = NULL;";
            escritor.WriteLine( declaracion );
        }
        public override void PedirMemoria( EscritorC escritor )
        {
        }
        public override void Instanciar( EscritorC escritor )
        {
            string instanciacion = null;
            foreach( Elem elemento in Elementos )
            {
                switch( Definicion.Tipo )
                {
                    case Tipo.Char:
                    case Tipo.UInt8:
                    case Tipo.Int8:
                        instanciacion = "insertarchar(&" + Definicion.Nombre + "," + elemento.Valor + ");";
                        break;
                    case Tipo.UInt32:
                    case Tipo.Int32:
                        instanciacion = "insertarint(&" + Definicion.Nombre + "," + elemento.Valor + ");";
                        break;
                    case Tipo.Float32:
                        instanciacion = "insertarfloat(&" + Definicion.Nombre + "," + elemento.Valor + ");";
                        break;
                    case Tipo.Float64:
                        instanciacion = "insertardouble(&" + Definicion.Nombre + "," + elemento.Valor + ");";
                        break;
                    default:
                        throw new Exception( "Tipo no valido para listas" );
                }
                escritor.WriteLine( instanciacion );
            }
        }
        public override void CompararValor( EscritorC escritor )
        {
            escritor.WriteLine( "//" + Definicion.Nombre );
            switch( Definicion.Tipo )
            {
                case Tipo.UInt8:
                case Tipo.Int8:
                case Tipo.Char:
                    escritor.WriteLine( "struct Listachar *listaaux;" );
                    escritor.WriteLine( "crearchar(&listaaux);" );
                    foreach( Elem elemento in Elementos )
                        escritor.WriteLine( "insertarchar(&listaaux, " + elemento.Valor + ");" );
                    escritor.If( "ListaCircularchar(" + Definicion.Nombre + ")" );
                    escritor.PrintListaCircular();
                    escritor.WriteLine( "return 1;" );
                    escritor.FinIf();
                    escritor.If( "!igualdadchar(" + Definicion.Nombre + ", listaaux)" );
                    escritor.WriteLine( "   cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.UInt32:
                case Tipo.Int32:
                    escritor.WriteLine( "struct Listaint *listaaux;" );
                    escritor.WriteLine( "crearint(&listaaux);" );
                    foreach( Elem elemento in Elementos )
                        escritor.WriteLine( "insertarint(&listaaux, " + elemento.Valor + ");" );
                    escritor.If( "ListaCircularint(" + Definicion.Nombre + ")" );
                    escritor.PrintListaCircular();
                    escritor.WriteLine( "return 1;" );
                    escritor.FinIf();
                    escritor.If( "!igualdadint(" + Definicion.Nombre + ", listaaux)" );
                    escritor.WriteLine( "    cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.Float32:
                    escritor.WriteLine( "struct Listafloat *listaaux;" );
                    escritor.WriteLine( "crearfloat(&listaaux);" );
                    foreach( Elem elemento in Elementos )
                        escritor.WriteLine( "insertarfloat(&listaaux, " + elemento.Valor + ");" );
                    escritor.If( "ListaCircularfloat(" + Definicion.Nombre + ")" );
                    escritor.PrintListaCircular();
                    escritor.WriteLine( "return 1;" );
                    escritor.FinIf();
                    escritor.If( "!igualdadfloat(" + Definicion.Nombre + ", listaaux)" );
                    escritor.WriteLine( "    cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.Float64:
                    escritor.WriteLine( "struct Listadouble *listaaux;" );
                    escritor.WriteLine( "creardouble(&listaaux);" );
                    foreach( Elem elemento in Elementos )
                        escritor.WriteLine( "insertardouble(&listaaux, " + elemento.Valor + ");" );
                    escritor.If( "ListaCirculardouble(" + Definicion.Nombre + ")" );
                    escritor.PrintListaCircular();
                    escritor.WriteLine( "return 1;" );
                    escritor.FinIf();
                    escritor.If( "!igualdaddouble(" + Definicion.Nombre + ", listaaux)" );
                    escritor.WriteLine( "    cantErrores++;" );
                    escritor.FinIf();
                    break;
                default:
                    throw new Exception( "Tipo no valido para listas" );
            }
        }
        public override void LiberarMemoria( EscritorC escritor )
        {
            switch( Definicion.Tipo )
            {
                case Tipo.UInt8:
                case Tipo.Int8:
                case Tipo.Char:
                    escritor.WriteLine( "liberarchar(&listaaux);" );
                    escritor.WriteLine( "liberarchar(&" + Definicion.Nombre + ");" );
                    break;
                case Tipo.UInt32:
                case Tipo.Int32:
                    escritor.WriteLine( "liberarint(&listaaux);" );
                    escritor.WriteLine( "liberarint(&" + Definicion.Nombre + ");" );
                    break;
                case Tipo.Float32:
                    escritor.WriteLine( "liberarfloat(&listaaux);" );
                    escritor.WriteLine( "liberarfloat(&" + Definicion.Nombre + ");" );
                    break;
                case Tipo.Float64:
                    escritor.WriteLine( "liberardouble(&listaaux);" );
                    escritor.WriteLine( "liberardouble(&" + Definicion.Nombre + ");" );
                    break;
                default:
                    throw new Exception( "El tipo de lista no es valido" );
            }
        }
        #endregion

        #endregion
    }
}