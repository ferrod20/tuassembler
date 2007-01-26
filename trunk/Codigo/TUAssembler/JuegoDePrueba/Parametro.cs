using System;
using System.IO;
using TUAssembler.Definicion;

namespace TUAssembler.JuegoDePrueba
{
    [Serializable()]
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
                    declaracion = "long ";
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
            }
            if( Definicion.TipoDeAcceso==ValorOReferencia.R )
                declaracion += "*";
            if( Definicion.EsElemento )
                declaracion += Definicion.Nombre + ";";
            if( Definicion.EsVector )
                declaracion += Definicion.Nombre + "[" + Definicion.Longitud + "];";
            if( Definicion.EsMatriz )
                declaracion += Definicion.Nombre + "[" + Definicion.CantFilas + "][" + Definicion.CantColumnas + "];";

            return declaracion;
        }
        public string Instanciar()
        {
            ParamMatriz paramMatriz;
            ParamVector paramVector;
            string instanciacion = string.Empty;
            Elem elem;
            if( Definicion.EsElemento )
            {
                elem = (Elem) this;
                switch( Definicion.Tipo )
                {
                    case Tipo.UInt8:
                    case Tipo.UInt16:
                    case Tipo.UInt32:
                    case Tipo.UInt64:
                    case Tipo.Int8:
                    case Tipo.Int16:
                    case Tipo.Int32:
                    case Tipo.Int64:
                    case Tipo.Float32:
                    case Tipo.Float64:
                    case Tipo.CadenaC:
                        instanciacion = Definicion.Nombre + " = " + elem.Valor + ";";
                        break;
                    case Tipo.Booleano:
                        instanciacion = Definicion.Nombre + " = ";
                        instanciacion += elem.UltimoElementoUno()? "true;" : "false;";
                        break;
                    case Tipo.Char:
                        instanciacion = Definicion.Nombre + " = '" + elem.Valor + "';";
                        break;
                }
            }
            if( Definicion.EsVector )
            {
                paramVector = (ParamVector) this;
                instanciacion = Definicion.Nombre + " = { ";
                foreach( Elem elemento in paramVector.Elementos )
                    instanciacion += elemento.Valor + ", ";
                instanciacion += " }";
            }
            if( Definicion.EsMatriz )
            {
                paramMatriz = (ParamMatriz) this;
                instanciacion = Definicion.Nombre + " = { ";
                foreach( ParamVector fila in paramMatriz.Filas )
                    foreach( Elem elemento in fila.Elementos )
                        instanciacion = elemento.Valor + ", ";
                instanciacion += " }";
            }
            return instanciacion;
        }
        public void CompararValor( ref StreamWriter escritor )
        {
            Elem elemento;
            ParamVector vector;
            ParamMatriz matriz;
            
            escritor.WriteLine( );
            if (Definicion.EsElemento)
            {
                elemento = (Elem) this;
                switch (Definicion.Tipo)
                {
                    case Tipo.UInt8:
                    case Tipo.UInt16:
                    case Tipo.UInt32:
                    case Tipo.UInt64:
                    case Tipo.Int8:
                    case Tipo.Int16:
                    case Tipo.Int32:
                    case Tipo.Int64:
                    case Tipo.Float32:
                    case Tipo.Float64:
                    case Tipo.Booleano:
                    case Tipo.Char:                        
                        escritor.WriteLine( "if ( " + Definicion.Nombre + " != " + elemento.Valor + " )" );
                        escritor.WriteLine(Mensajes.PrintfValorDistinto( Definicion.Nombre, elemento.Valor ));
                        break;
                    case Tipo.CadenaC:
                        for (int i = 0; i < elemento.Valor[i]; i++)
                        {
                            escritor.WriteLine("if ( " + Definicion.Nombre + "[" + i + "] != '" + elemento.Valor[i] + "' )");
                            escritor.WriteLine(Mensajes.PrintfValorDistintoCadena(Definicion.Nombre, elemento.Valor, i));
                        }
                            break;
                }
            }            
            if (Definicion.EsVector)
            {
                vector = (ParamVector) this;
                for (int i = 0; i < vector.Elementos.Length; i++)
                {
                    escritor.WriteLine("if ( " + Definicion.Nombre + "[" + i + "] != " + vector[i].Valor + " )");
                    escritor.WriteLine(Mensajes.PrintfValorDistintoCadena(Definicion.Nombre, vector[i].Valor, i));
                }                
            }
            escritor.WriteLine();
        }
    }
}