using System;
using System.IO;
using TUAssembler.Auxiliares;
using TUAssembler.Definicion;

namespace TUAssembler.JuegoDePrueba
{
    public class Elem: Parametro
    {
        #region Variables miembro
        private string elem;
        #endregion

        #region Propiedades
        public string Valor
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
        #endregion

        #region Constructores
        public Elem()
        {
        }
        public Elem( string elem )
        {
            this.elem = elem;
        }
        #endregion

        #region Métodos

        #region Escritura código C
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
            if( Definicion.TipoDeAcceso==ValorOReferencia.R )
                declaracion += "*";
            declaracion += Definicion.Nombre + ";";
            escritor.WriteLine( declaracion );
        }
        public override void Instanciar( EscritorC escritor )
        {
            string instanciacion = string.Empty;
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
                    if( Definicion.TipoDeAcceso==ValorOReferencia.R )
                        instanciacion = "*";
                    instanciacion += Definicion.Nombre + " = " + Valor + ";";
                    break;
                case Tipo.CadenaPascal:
                case Tipo.CadenaC:
                    instanciacion = Definicion.Nombre + " = \"" + Valor + "\";";
                    break;
                case Tipo.Booleano:
                    if( Definicion.TipoDeAcceso==ValorOReferencia.R )
                        instanciacion = "*";
                    instanciacion += Definicion.Nombre + " = " + ( UltimoElementoUno()? "true;" : "false;" );
                    break;
                case Tipo.Char:
                    if( Definicion.TipoDeAcceso==ValorOReferencia.R )
                        instanciacion = "*";
                    instanciacion += Definicion.Nombre + " = '" + Valor + "';";
                    break;
            }
            escritor.WriteLine( instanciacion );
        }
        public override void CompararValor( EscritorC escritor )
        {
            string precision = "0";
            //Este parametro,usado por Float y Double,deberia leerse desde el archivo, pero por ahora lo hace desde aqui

            //(!this.EsDeSalidaOEntradaSalida);
            string variable = string.Empty;
            string diferencia = "AUX" + Definicion.Nombre;
            string varPrecision = "PR" + Definicion.Nombre;
            string iterador = "IT" + Definicion.Nombre;

            escritor.WriteLine( "//" + Definicion.Nombre );
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
                    if( Definicion.TipoDeAcceso==ValorOReferencia.R )
                        variable = "*";
                    variable += Definicion.Nombre;
                    escritor.If( variable + " != " + Valor );
                    escritor.PrintfValorDistintoConDiferencia( variable, Valor );
                    escritor.WriteLine( "cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.Char:
                    if( Definicion.TipoDeAcceso==ValorOReferencia.R )
                        variable = "*";
                    variable += Definicion.Nombre;
                    escritor.If( variable + " != " + Valor );
                    escritor.PrintfValorDistinto( variable, Valor );
                    escritor.WriteLine( "cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.Booleano:
                    if( Definicion.TipoDeAcceso==ValorOReferencia.R )
                        variable = "*";
                    variable += Definicion.Nombre;
                    escritor.If( "(" + variable + " == " + "0 && " + Valor + "!=0)||(" + variable + " != " +
                        "0 && " + Valor + "==0)" );
                    escritor.PrintfValorDistinto( variable, Valor );
                    escritor.WriteLine( "cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.Float32:
                    // Realiza la resta entre ambos operandos y si la misma dio un resultado menor que
                    // 10^precision entonces los considera iguales
                    if( Definicion.TipoDeAcceso==ValorOReferencia.R )
                        variable = "*";
                    variable += Definicion.Nombre;
                    escritor.WriteLine( "float " + diferencia + " = " + variable + " - " + Valor + ";" );
                    escritor.WriteLine( diferencia + " = (" + diferencia + " >= 0) ? " + diferencia + " : -" +
                        diferencia + ";" );
                    escritor.WriteLine( "float " + varPrecision + " = pow((float)10, " + precision + ");" );
                    escritor.If( diferencia + " < " + varPrecision );
                    escritor.PrintfValorDistintoConDiferencia( variable, Valor, diferencia );
                    escritor.WriteLine( "cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.Float64:
                    if( Definicion.TipoDeAcceso==ValorOReferencia.R )
                        variable = "*";
                    variable += Definicion.Nombre;
                    escritor.WriteLine( "double " + diferencia + " = " + variable + " - " + Valor + ";" );
                    escritor.WriteLine( diferencia + " = (" + diferencia + " >= 0) ? " + diferencia + " : -" +
                        diferencia + ";" );
                    escritor.WriteLine( "double " + varPrecision + " = pow((double)10, " + precision + ");" );
                    escritor.If( diferencia + " < " + varPrecision );
                    escritor.PrintfValorDistintoConDiferencia( variable, Valor, diferencia );
                    escritor.WriteLine( "cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.CadenaC:
                    escritor.WriteLine( "char* " + diferencia + " = \"" + Valor + "\";" );
                    escritor.WriteLine( "int " + iterador + ";" );
                    escritor.For( iterador + "=0", diferencia + "[" + iterador + "]!=0 && " +
                        Definicion.Nombre + "[" + iterador + "]!=0", iterador + "++" );
                    escritor.If( Definicion.Nombre + "[" + iterador + "] != " + diferencia + "[" +
                        iterador + "]" );
                    escritor.WriteLine( "printf( \"El valor de la cadena " + Definicion.Nombre +
                        ": de la posicion %n es distinto al valor esperado: %c \"," + iterador + ", " + diferencia +
                            "[" +
                                iterador + "]);" );
                    escritor.WriteLine( "cantErrores++;" );
                    escritor.FinIf();
                    escritor.FinFor();
                    break;
                case Tipo.CadenaPascal:
                    break;
            }
        }
        #endregion

        public void EstablecerValor( string elem )
        {
            this.elem = elem;
        }
        public bool TipoCorrecto( Tipo tipo )
        {
            bool salida = false;
            switch( tipo )
            {
                case Tipo.UInt8:
                case Tipo.UInt16:
                case Tipo.UInt32:
                case Tipo.UInt64:
                    salida = MA.SoloEnteros( elem );
                    break;
                case Tipo.Int8:
                case Tipo.Int16:
                case Tipo.Int32:
                case Tipo.Int64:
                    salida = MA.SoloEnterosConSigno( elem );
                    break;
                case Tipo.Float32:
                case Tipo.Float64:
                    salida = MA.EsPtoFlotante( elem );
                    break;
                case Tipo.Booleano:
                    salida = MA.EsBool( elem );
                    break;
                case Tipo.Char:
                    salida = elem.Length==1;
                    break;
                case Tipo.CadenaC:
                    salida = MA.EntreComillas( elem );
                    break;
            }
            return salida;
        }
        public bool UltimoElementoUno()
        {
            return elem[elem.Length - 1]=='1';
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
            if( parametros.Length!=1 )
                throw new Exception( Mensajes.CantidadDeParametrosNoCoincidenConDefinicion );
            EstablecerValor( parametros[0] );
        }
        #endregion
    }
}