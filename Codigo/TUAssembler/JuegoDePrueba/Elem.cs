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

        #region Escritura código C
        public override void Declarar( EscritorC escritor )
        {
            string declaracion = string.Empty;
            declaracion = Definicion.ObtenerNombreDelTipoParaC() + " ";

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
            string variable = string.Empty;

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
                case Tipo.Char:
                case Tipo.Booleano:
                case Tipo.Float32:
                case Tipo.Float64:
                    if( Definicion.TipoDeAcceso==ValorOReferencia.R )
                        variable = "*";
                    break;
                case Tipo.CadenaC:
                case Tipo.CadenaPascal:
                    break;
            }
            variable += Definicion.Nombre;

            CompararValor( escritor, variable );
        }
        public void CompararValor( EscritorC escritor, string variable )
        {
            //(!this.EsDeSalidaOEntradaSalida);
            string diferencia = "AUX" + variable;
            string varPrecision = "PR" + variable;
            string iterador = "IT" + variable;

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
                    escritor.If( variable + " != " + Valor );
                    escritor.PrintfValorDistintoConDiferencia( variable, Valor );
                    escritor.WriteLine( "cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.Char:
                    escritor.If( variable + " != " + Valor );
                    escritor.PrintfValorDistinto( variable, Valor );
                    escritor.WriteLine( "cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.Booleano:
                    escritor.If( "(" + variable + " == " + "0 && " + Valor + "!=0)||(" + variable + " != " +
                        "0 && " + Valor + "==0)" );
                    escritor.PrintfValorDistinto( variable, Valor );
                    escritor.WriteLine( "cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.Float32:
                    // Realiza la resta entre ambos operandos y si la misma dio un resultado menor que
                    // 10^precision entonces los considera iguales
                    escritor.WriteLine( "float " + diferencia + " = " + variable + " - " + Valor + ";" );
                    escritor.WriteLine( diferencia + " = (" + diferencia + " >= 0) ? " + diferencia + " : -" +
                        diferencia + ";" );
                    escritor.WriteLine( "float " + varPrecision + " = pow((float)10, " + Definicion.Precision + ");" );
                    escritor.If( diferencia + " < " + varPrecision );
                    escritor.PrintfValorDistintoConDiferencia( variable, Valor, diferencia );
                    escritor.WriteLine( "cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.Float64:
                    escritor.WriteLine( "double " + diferencia + " = " + variable + " - " + Valor + ";" );
                    escritor.WriteLine( diferencia + " = (" + diferencia + " >= 0) ? " + diferencia + " : -" +
                        diferencia + ";" );
                    escritor.WriteLine( "double " + varPrecision + " = pow((double)10, " + Definicion.Precision + ");" );
                    escritor.If( diferencia + " < " + varPrecision );
                    escritor.PrintfValorDistintoConDiferencia( variable, Valor, diferencia );
                    escritor.WriteLine( "cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.CadenaC:
                    escritor.WriteLine( "char* " + diferencia + " = \"" + Valor + "\";" );
                    escritor.WriteLine( "int " + iterador + ";" );
                    escritor.For( iterador + "=0", diferencia + "[" + iterador + "]!=0 && " +
                        variable + "[" + iterador + "]!=0", iterador + "++" );
                    escritor.If( variable + "[" + iterador + "] != " + diferencia + "[" +
                        iterador + "]" );
                    escritor.WriteLine( "printf( \"El valor de la cadena " + variable +
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

        #endregion
    }
}