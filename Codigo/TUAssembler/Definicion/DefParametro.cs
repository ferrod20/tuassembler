using System;
using System.Xml.Serialization;

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
        private int longitud;
        private int cantFilas;
        private int cantColumnas;
        private bool esVector;
        private bool esMatriz;
        private bool esElemento;
        #endregion

        #region Propiedades
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
        public int Longitud
        {
            get
            {
                return longitud;
            }
            set
            {
                longitud = value;
            }
        }

        [XmlAttribute()]
        public int CantFilas
        {
            get
            {
                return cantFilas;
            }
            set
            {
                cantFilas = value;
            }
        }

        [XmlAttribute()]
        public int CantColumnas
        {
            get
            {
                return cantColumnas;
            }
            set
            {
                cantColumnas = value;
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
                //CrearNombre();
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
                //CrearNombre();
            }
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
                //CrearNombre();
            }
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
                //CrearNombre();
            }
        }
        #endregion

        #region Constructores
        public DefParametro()
        {
        }
        #endregion

        #region Métodos
        /*private void CrearNombre()
        {
            Nombre = "";
            if( EsMatriz )
                Nombre = "matriz";
            if( EsVector )
                Nombre = "vector";
            Nombre += Tipo.ToString() + GetHashCode().ToString();
        }
         * */
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
                salida += "* , int "; //longitud

            if( esMatriz )
                salida += "**  , int , int "; //cantFilas, cantColumnas
            if( TipoDeAcceso==ValorOReferencia.R )
                salida += "*";
            return salida;
        }
        #endregion
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