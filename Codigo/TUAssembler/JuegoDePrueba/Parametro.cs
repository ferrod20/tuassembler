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
        public void CompararValor( ref StreamWriter escritor )
        {
            string precision = "0";
                //Este parametro,usado por Float y Double,deberia leerse desde el archivo, pero por ahora lo hace desde aqui

            Elem elemento;
            ParamVector vector;
            ParamMatriz matriz;
            //(!this.EsDeSalidaOEntradaSalida);

            string aux = "AUX" + Definicion.Nombre;
            string auxPrecision = "PR" + Definicion.Nombre;
            string iterador = "IT" + Definicion.Nombre;

            escritor.WriteLine();
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
                    case Tipo.Char:
                        escritor.WriteLine( "if ( " + Definicion.Nombre + " != " + elemento.Valor + " )" );
                        escritor.WriteLine( "    " + Mensajes.PrintfValorDistinto( Definicion.Nombre, elemento.Valor ) );
                        break;
                        /*   case Tipo.Booleano:
                        escritor.WriteLine("if ( (" + Definicion.Nombre + " == "  + "0 && " + elemento.Valor +"!=0)||(" + Definicion.Nombre + " != " + "0 && " + elemento.Valor + "==0)" + " )");
                        escritor.WriteLine("    " + Mensajes.PrintfValorDistinto(Definicion.Nombre, elemento.Valor));
*/
                    case Tipo.Float32:
                        // Realiza la resta entre ambos operandos y si la misma dio un resultado menor que
                        // 10^precision entonces los considera iguales
                        escritor.WriteLine( "float " + aux + " = " + Definicion.Nombre + " - " + elemento.Valor + ";" );
                        escritor.WriteLine( aux + " = (" + aux + " >= 0) ? " + aux + " : -" + aux + ";" );
                        escritor.WriteLine( "float " + auxPrecision + " = pow((float)10, " + precision + ");" );
                        escritor.WriteLine( "if (" + aux + " < " + auxPrecision + ")" );
                        escritor.WriteLine( "    " + Mensajes.PrintfValorDistinto( Definicion.Nombre, elemento.Valor ) );
                        break;
                    case Tipo.Float64:
                        escritor.WriteLine( "double " + aux + " = " + Definicion.Nombre + " - " + elemento.Valor + ";" );
                        escritor.WriteLine( aux + " = (" + aux + " >= 0) ? " + aux + " : -" + aux + ";" );
                        escritor.WriteLine( "double " + auxPrecision + " = pow((double)10, " + precision + ");" );
                        escritor.WriteLine( "if (" + aux + " < " + auxPrecision + ")" );
                        escritor.WriteLine( "    " + Mensajes.PrintfValorDistinto( Definicion.Nombre, elemento.Valor ) );
                        break;
                    case Tipo.CadenaC:
                        escritor.WriteLine( "char* " + aux + " = \"" + elemento.Valor + "\";" );
                        escritor.WriteLine( "int " + iterador + ";" );
                        escritor.WriteLine( "for(" + iterador + "=0;" + aux + "[" + iterador + "]!=0 && " +
                            Definicion.Nombre + "[" + iterador + "]!=0 ;" + iterador + "++)" );
                        escritor.WriteLine( "if ( " + Definicion.Nombre + "[" + iterador + "] != " + aux + "[" +
                            iterador + "]" + " )" );
                        escritor.WriteLine( "    printf( \"El valor de la cadena " + Definicion.Nombre +
                            ": de la posicion %n es distinto al valor esperado: %c \"," + iterador + ", " + aux + "[" +
                                iterador + "]);" );
                        /*  for(int i = 0; i < elemento.Valor[i]; i++){
                            escritor.WriteLine("if ( " + Definicion.Nombre + "[" + i + "] != '" + elemento.Valor[i] + "' )");
                            escritor.WriteLine(Mensajes.PrintfValorDistintoCadena(Definicion.Nombre, elemento.Valor, i));
                        }*/
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
            escritor.WriteLine();
        }
    }
}