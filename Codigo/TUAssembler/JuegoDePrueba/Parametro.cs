using System;
using System.IO;
using TUAssembler.Auxiliares;
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
                        instanciacion = Definicion.Nombre + " = " + elem.Valor + ";";
                        break;
                    case Tipo.CadenaPascal:
                    case Tipo.CadenaC:
                        instanciacion = Definicion.Nombre + " = \"" + elem.Valor + "\";";
                        break;
                    case Tipo.Booleano:
                        instanciacion = Definicion.Nombre + " = " + ( elem.UltimoElementoUno()? "true;" : "false;" );
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
        public void CompararValor( ref EscritorC escritor )
        {
            string precision = "0";
            //Este parametro,usado por Float y Double,deberia leerse desde el archivo, pero por ahora lo hace desde aqui

            Elem elemento;
            ParamVector vector;
            ParamMatriz matriz;
            //(!this.EsDeSalidaOEntradaSalida);

            string diferencia = "AUX" + Definicion.Nombre;
            string varPrecision = "PR" + Definicion.Nombre;
            string iterador = "IT" + Definicion.Nombre;
            
            escritor.WriteLine("//" + Definicion.Nombre);
            if( Definicion.EsElemento )
            {
                elemento = (Elem) this;
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
                       /* escritor.If(Definicion.Nombre + " != " + elemento.Valor);
                        escritor.PrintfValorDistintoConDiferencia( Definicion.Nombre, elemento.Valor );
                        escritor.WriteLine("cantErrores++;");
                        escritor.FinIf();
                        break;
                        * */
                    case Tipo.Char:                                                                        
                        escritor.If( Definicion.Nombre + " != " + elemento.Valor );
                        escritor.PrintfValorDistinto(Definicion.Nombre, elemento.Valor);
                        escritor.WriteLine( "cantErrores++;" );
                        escritor.FinIf();
                        break;
                    case Tipo.Booleano:                                                                        
                        escritor.If("(" + Definicion.Nombre + " == "  + "0 && " + elemento.Valor +"!=0)||(" + Definicion.Nombre + " != " + "0 && " + elemento.Valor + "==0)");
                        escritor.PrintfValorDistinto(Definicion.Nombre, elemento.Valor);
                        escritor.WriteLine(  "cantErrores++;" );
                        escritor.FinIf();
                        break;
                    case Tipo.Float32:
                        // Realiza la resta entre ambos operandos y si la misma dio un resultado menor que
                        // 10^precision entonces los considera iguales
                        escritor.WriteLine( "float " + diferencia + " = " + Definicion.Nombre + " - " + elemento.Valor + ";" );
                        escritor.WriteLine( diferencia + " = (" + diferencia + " >= 0) ? " + diferencia + " : -" + diferencia + ";" );
                        escritor.WriteLine( "float " + varPrecision + " = pow((float)10, " + precision + ");" );                        
                        escritor.If( diferencia + " < " + varPrecision );
                        escritor.PrintfValorDistintoConDiferencia(Definicion.Nombre, elemento.Valor, diferencia );
                        escritor.WriteLine( "cantErrores++;" );
                        escritor.FinIf();                        
                        break;
                    case Tipo.Float64:
                        escritor.WriteLine( "double " + diferencia + " = " + Definicion.Nombre + " - " + elemento.Valor + ";" );
                        escritor.WriteLine( diferencia + " = (" + diferencia + " >= 0) ? " + diferencia + " : -" + diferencia + ";" );
                        escritor.WriteLine( "double " + varPrecision + " = pow((double)10, " + precision + ");" );
                        escritor.If( diferencia + " < " + varPrecision );
                        escritor.PrintfValorDistintoConDiferencia(Definicion.Nombre, elemento.Valor, diferencia);
                        escritor.WriteLine( "cantErrores++;" );                        
                        escritor.FinIf();
                        break;
                    case Tipo.CadenaC:
                        escritor.WriteLine( "char* " + diferencia + " = \"" + elemento.Valor + "\";" );
                        escritor.WriteLine( "int " + iterador + ";" );
                        escritor.For( iterador + "=0", diferencia + "[" + iterador + "]!=0 && " +
                            Definicion.Nombre + "[" + iterador + "]!=0",  iterador + "++" );
                        escritor.If( Definicion.Nombre + "[" + iterador + "] != " + diferencia + "[" +
                            iterador + "]" );                        
                        escritor.WriteLine( "printf( \"El valor de la cadena " + Definicion.Nombre +
                            ": de la posicion %n es distinto al valor esperado: %c \"," + iterador + ", " + diferencia + "[" +
                                iterador + "]);" );
                        escritor.WriteLine("cantErrores++;");
                        escritor.FinIf();
                        escritor.FinFor();
                        break;
                    case Tipo.CadenaPascal:
                        break;
                }
            }
            if( Definicion.EsVector )
            {
                vector = (ParamVector) this;
                for( int i = 0; i < vector.Elementos.Length; i++ )
                {
                    escritor.WriteLine( "if ( " + Definicion.Nombre + "[" + i + "] != " + vector[i].Valor + " )" );
                    escritor.WriteLine( Mensajes.PrintfValorDistintoCadena( Definicion.Nombre, vector[i].Valor, i ) );
                }
            }            
        }
    }
}