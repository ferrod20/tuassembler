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
            if( !TipoCorrecto( Definicion.Tipo ) )
                throw new Exception( Mensajes.TipoIncorrectoElem( Definicion.Nombre ) );
            if( Definicion.Tipo==Tipo.Char )
                this.elem = elem[1].ToString();
            if( Definicion.Tipo==Tipo.CadenaC )
            {
                this.elem = this.elem.Remove( 0, 1 );
                this.elem = this.elem.Remove( this.elem.Length - 1, 1 );
            }
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
                    salida = MA.EntreComillasSimples( elem );
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
        public override void TamanioOValorParaMedicion( EscritorC escritor )
        {
            escritor.Write( Valor );
        }
        public override void Declarar( EscritorC escritor )
        {
            string declaracion = string.Empty;
            declaracion = Definicion.ObtenerNombreDelTipoParaC() + " ";
            if( Definicion.Tipo!=Tipo.CadenaC && Definicion.Tipo!=Tipo.CadenaPascal )
            {
                if( Definicion.TipoDeAcceso==ValorOReferencia.R )
                    declaracion += "*";
                declaracion += Definicion.Nombre + ";";
                escritor.WriteLine( declaracion );
            }
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
                    if (Definicion.TipoDeAcceso == ValorOReferencia.R)
                        instanciacion = "*";
                    instanciacion += Definicion.Nombre + " = " + Valor + ";";
                    break;                    
                case Tipo.Float32:
                case Tipo.Float64:
                    if (Definicion.TipoDeAcceso == ValorOReferencia.R)
                        instanciacion = "*";
                    instanciacion += Definicion.Nombre + " = 0;\n";
                    if (Definicion.TipoDeAcceso == ValorOReferencia.R)
                        instanciacion += "*";
                    instanciacion += Definicion.Nombre + " += " + Valor + ";";
                    break;
                case Tipo.CadenaPascal:
                case Tipo.CadenaC:
                    instanciacion = Definicion.ObtenerNombreDelTipoParaC() + " " + Definicion.Nombre + "[] = \"" + Valor +
                        "\";";
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
        public override void PedirMemoria( EscritorC escritor )
        {
            string pedido;
            int cantMemoria;
            if( Definicion.Tipo!=Tipo.CadenaC && Definicion.Tipo!=Tipo.CadenaPascal )
            {
                cantMemoria = MA.CuantosBytes( Definicion.Tipo );
                pedido = Definicion.Nombre + " = " + "malloc2( " + cantMemoria + ",true );";
                escritor.WriteLine( pedido );
            }
        }
        public override void LiberarMemoria( EscritorC escritor )
        {
            if( Definicion.Tipo!=Tipo.CadenaC && Definicion.Tipo!=Tipo.CadenaPascal )
            {
                escritor.WriteLine( "salidaFree2 = free2( " + Definicion.Nombre + " );" );
                escritor.If( "salidaFree2 == escrituraFueraDelBuffer" );
                escritor.PrintfEscrituraFueraDelBuffer( Definicion.Nombre );
                escritor.WriteLine( "cantErrores++;" );
                escritor.FinIf();
                escritor.If( "salidaFree2 == liberarPosMemNoValida" );
                escritor.PrintfCambioDeDireccionDelPuntero( Definicion.Nombre );
                escritor.WriteLine( "cantErrores++;" );
                escritor.FinIf();
                escritor.If( "salidaFree2 == dosFreeDelMismoBuffer" );
                escritor.PrintfDosFreeAlMismoParam( Definicion.Nombre );
                escritor.WriteLine( "cantErrores++;" );
                escritor.FinIf();
            }
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
                    escritor.If( variable + " != " + "'" + Valor + "'" );
                    escritor.PrintfValorDistintoChar( variable, Valor );
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
                    MA.EliminarAsteriscos( ref diferencia );
                    MA.EliminarCorchetes( ref diferencia );
                    MA.EliminarAsteriscos( ref varPrecision );
                    MA.EliminarCorchetes( ref varPrecision );

                    escritor.WriteLine( "float " + diferencia + " = (" + variable + ") - (" + Valor + ");" );
                    escritor.WriteLine( diferencia + " = (" + diferencia + " >= 0) ? " + diferencia + " : -" +
                        diferencia + ";" );
                    escritor.WriteLine( "float " + varPrecision + " = pow((float)10, -" + Definicion.Precision + ");" );
                    escritor.If( diferencia + " >= " + varPrecision );
                    escritor.PrintfValorDistintoFloatConDiferencia( variable, Valor, diferencia );
                    escritor.WriteLine( "cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.Float64:
                    MA.EliminarAsteriscos( ref diferencia );
                    MA.EliminarCorchetes( ref diferencia );
                    MA.EliminarAsteriscos( ref varPrecision );
                    MA.EliminarCorchetes( ref varPrecision );

                    escritor.WriteLine( "float " + diferencia + " = (" + variable + ") - (" + Valor + ");" );
                    escritor.WriteLine( diferencia + " = (" + diferencia + " >= 0) ? " + diferencia + " : -" +
                        diferencia + ";" );
                    escritor.WriteLine( "double " + varPrecision + " = pow((double)10, -" + Definicion.Precision + ");" );
                    escritor.If( diferencia + " >= " + varPrecision );
                    escritor.PrintfValorDistintoFloatConDiferencia( variable, Valor, diferencia );
                    escritor.WriteLine( "cantErrores++;" );
                    escritor.FinIf();
                    break;
                case Tipo.CadenaC:
                    escritor.WriteLine( "char " + diferencia + "[] = \"" + Valor + "\";" );
                    escritor.WriteLine( "int " + iterador + ";" );
                    escritor.For( iterador + "=0", diferencia + "[" + iterador + "]!=0 && " +
                        variable + "[" + iterador + "]!=0", iterador + "++" );
                    escritor.If( variable + "[" + iterador + "] != " + diferencia + "[" +
                        iterador + "]" );
                    escritor.PrintfValorDeStringDistintos( variable, iterador, diferencia );
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