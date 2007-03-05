using System;
using System.Xml.Serialization;
using TUAssembler.JuegoDePrueba;

namespace TUAssembler.Definicion
{
    [Serializable()]
    public class DefParametro
    {
        #region Variables miembro
        private string nombre;
        private ValorOReferencia tipoDeAcceso;
        private EntradaSalida entradaSalida;
        private Tipo tipo;
        private bool esVector;
        private bool esMatriz;
        private bool esLista;
        private bool esElemento;
        private int precision;
        #endregion

        #region Propiedades
        [XmlAttribute()]
        public int Precision
        {
            get
            {
                return precision;
            }
            set
            {
                precision = value;
            }
        }

        [XmlAttribute()]
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }

        [XmlAttribute()]
        public ValorOReferencia TipoDeAcceso
        {
            get
            {
                return tipoDeAcceso;
            }
            set
            {
                tipoDeAcceso = value;
            }
        }

        [XmlAttribute()]
        public EntradaSalida EntradaSalida
        {
            get
            {
                return entradaSalida;
            }
            set
            {
                entradaSalida = value;
            }
        }

        [XmlAttribute()]
        public Tipo Tipo
        {
            get
            {
                return tipo;
            }
            set
            {
                tipo = value;
            }
        }

        [XmlAttribute()]
        public bool EsVector
        {
            get
            {
                return esVector;
            }
            set
            {
                esVector = value;
            } //CrearNombre();
        }

        [XmlAttribute()]
        public bool EsMatriz
        {
            get
            {
                return esMatriz;
            }
            set
            {
                esMatriz = value;
            } //CrearNombre();
        }

        [XmlAttribute()]
        public bool EsLista
        {
            get
            {
                return esLista;
            }
            set
            {
                esLista = value;
            } //CrearNombre();
        }

        [XmlAttribute()]
        public bool EsElemento
        {
            get
            {
                return esElemento;
            }
            set
            {
                esElemento = value;
            } //CrearNombre();
        }
        #endregion

        #region Constructores
        public DefParametro()
        {
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            string salida = string.Empty;
            switch( Tipo )
            {
                case Tipo.UInt8:
                    salida = "unsigned char";
                    break;
                case Tipo.UInt16:
                    salida = "unsigned short";
                    break;
                case Tipo.UInt32:
                    salida = "unsigned int";
                    break;
                case Tipo.UInt64:
                    salida = "unsigned long int";
                    break;
                case Tipo.Int8:
                    salida = "char";
                    break;
                case Tipo.Int16:
                    salida = "short";
                    break;
                case Tipo.Int32:
                    salida = "int";
                    break;
                case Tipo.Int64:
                    salida = "long int";
                    break;
                case Tipo.Float32:
                    salida = "float";
                    break;
                case Tipo.Float64:
                    salida = "double";
                    break;
                case Tipo.Booleano:
                    salida = "bool";
                    break;
                case Tipo.Char:
                    salida = "char";
                    break;
                case Tipo.CadenaC:
                    salida = "char*";
                    break;
            }

            if( esVector )
                salida += "* "; //longitud
            if( esMatriz )
                salida += "** "; //cantFilas, cantColumnas
            if( !esVector && !esMatriz && TipoDeAcceso==ValorOReferencia.R )
                salida += "*";
            //Por ahora solo soporta INT, CHAR, FLOAT Y DOUBLE
            if( EsLista )
                switch( Tipo )
                {
                    case Tipo.UInt8:
                    case Tipo.Int8:
                        salida = "struct Listachar *";
                        break;
                    case Tipo.UInt32:
                    case Tipo.Int32:
                        salida = "struct Listaint *";
                        break;
                    case Tipo.Float32:
                        salida = "struct Listafloat *";
                        break;
                    case Tipo.Float64:
                        salida = "struct Listadouble *";
                        break;
                    default:
                        throw new Exception( "Tipo de lista no valido" );
                }
            return salida;
        }
        //Genera un parametro segun el tipo que sea.
        public Parametro GenerarParametro()
        {
            Parametro salida = null;

            if( EsMatriz )
                salida = new ParamMatriz();
            if( EsVector )
                salida = new ParamVector();
            if( EsLista )
                salida = new ParamLista();
            if( EsElemento )
                salida = new Elem();
            salida.Definicion = this;
            return salida;
        }
        #endregion

        public string ObtenerNombreDelTipoParaC()
        {
            string nombre = string.Empty;
            switch( Tipo )
            {
                case Tipo.UInt8:
                    nombre = "unsigned char";
                    break;
                case Tipo.UInt16:
                    nombre = "unsigned short";
                    break;
                case Tipo.UInt32:
                    nombre = "unsigned int";
                    break;
                case Tipo.UInt64:
                    nombre = "unsigned long";
                    break;
                case Tipo.Int8:
                    nombre = "char";
                    break;
                case Tipo.Int16:
                    nombre = "short";
                    break;
                case Tipo.Int32:
                    nombre = "int";
                    break;
                case Tipo.Int64:
                    nombre = "long long int";
                    // el tipo "long long int" define(al menos en GCC) el entero de 64 bits
                    break;
                case Tipo.Float32:
                    nombre = "float";
                    break;
                case Tipo.Float64:
                    nombre = "double";
                    break;
                case Tipo.Booleano:
                    nombre = "bool";
                    break;
                case Tipo.Char:
                    nombre = "char";
                    break;
                case Tipo.CadenaC:
                    nombre = "char";
                    break;
                case Tipo.CadenaPascal:
                    nombre = "char";
                    break;
            }
            return nombre;
        }
        public void VerificarUnSoloTipo()
        {
            int cuantosTipos = 0;
            if( EsElemento )
                cuantosTipos++;
            if( EsMatriz )
                cuantosTipos++;
            if( EsLista )
                cuantosTipos++;
            if( EsVector )
                cuantosTipos++;

            if( cuantosTipos!=1 )
                throw new Exception( Mensajes.ParametroDeUnSoloTipo( Nombre ) );
        }
    }

    [Serializable()]
    public enum Tipo
    {
        UInt8,
        UInt16,
        UInt32,
        UInt64,
        Int8,
        Int16,
        Int32,
        Int64,
        Float32,
        Float64,
        Booleano,
        Char,
        CadenaC,
        CadenaPascal
    }

    [Serializable()]
    public enum EntradaSalida
    {
        E,
        S,
        ES
    }

    [Serializable()]
    public enum ValorOReferencia
    {
        V,
        R
    }
}