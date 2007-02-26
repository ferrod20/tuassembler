using System;
using System.IO;
using TUAssembler.Auxiliares;
using TUAssembler.Definicion;

namespace TUAssembler.JuegoDePrueba
{
    [Serializable()]
    public class ParamMatriz: Parametro
    {
        #region Variables miembro
        private int cantFilas;
        private int cantColumnas;
        private ParamVector[] filas; //Cada fila es un vector
        #endregion

        #region Propiedades
        public ParamVector[] Filas
        {
            get
            {
                return filas;
            }
            set
            {
                filas = value;
            }
        }
        #endregion

        #region Constructores
        public ParamMatriz()
        {
        }
        #endregion

        #region Métodos
        public override void Leer( StreamReader lector )
        {
            string[] filas;
            string[] elemsFila;
            int f = 0;
            cantColumnas = -1;
            filas = MA.LeerMatriz( lector );
            cantFilas = filas.Length;
            Filas = new ParamVector[cantFilas];
            
            foreach( string fila in filas )
            {
                ParamVector vector = new ParamVector();

                elemsFila = MA.ObtenerElementosDeLaFila( fila );
                if (cantColumnas == -1)
                    cantColumnas = elemsFila.Length;
                else
                    if (elemsFila.Length != cantColumnas)
                        throw new Exception( Mensajes.MatrizTieneFilaEnDeDistintaLongitud( Definicion.Nombre, f ) );

                VerificarTiposCorrectos( f, elemsFila );                
                vector.EstablecerValor( elemsFila );
                Filas[f] = vector;
                f++;
            }
        }
        private void VerificarTiposCorrectos( int fila, string[] elemsFila )
        {
            int col = 0;
            foreach (string elemento in elemsFila)
            {
                Elem elem = new Elem( elemento );
                if( !elem.TipoCorrecto( Definicion.Tipo ) )
                    throw new Exception( Mensajes.ElementoDeTipoIncorrectoEnLaMatriz( Definicion.Nombre, fila, col ) );
                col++;
            }
        }
        #region Métodos de código C
        public override void Declarar( EscritorC escritor )
        {
            string declaracion = Definicion.ObtenerNombreDelTipoParaC() + " ";
            declaracion += "**" + Definicion.Nombre + ";";
            escritor.WriteLine(declaracion);
        }
        public override void PedirMemoria( EscritorC escritor )
        {
            string pedido;
            string varFila = Definicion.Nombre + "Fila";
            pedido = Definicion.Nombre + " = " + "malloc2( sizeof(" + Definicion.ObtenerNombreDelTipoParaC() + "*)*" + cantFilas + " );";            
            escritor.WriteLine(pedido);
            escritor.WriteLine( "int " + varFila + ";");
            escritor.For( varFila + " = 0", varFila + " < " + cantFilas,varFila + "++");
            escritor.WriteLine(Definicion.Nombre + "[" + varFila + "] = malloc2( sizeof(" + Definicion.ObtenerNombreDelTipoParaC() + ")*" + cantColumnas + ");");
            escritor.FinFor();          
        }
        public override void Instanciar( EscritorC escritor )
        {
            string instanciacion;
            int fil = 0, col = 0;
            
            foreach (ParamVector fila in Filas)
            {
                foreach (Elem elemento in fila.Elementos)
                {
                    instanciacion = Definicion.Nombre + "[" + fil + "]["  + col + "] = " + elemento.Valor + ";";
                    escritor.WriteLine(instanciacion);
                    col++;
                }
                fil++;
                col = 0;
            }            
        }
        public override void CompararValor( EscritorC escritor )
        {
            Elem elem;
            
            for(int fila =0; fila <cantFilas; fila++)
                for(int col=0; col<cantColumnas; col ++ ) 
                {
                    elem = Filas[fila].Elementos[col];
                    elem.CompararValor(escritor, Definicion.Nombre + "[" + fila + "]" + "[" + col +"]");                    
                }
        }
        public override void LiberarMemoria( EscritorC escritor )
        {
            string pedido;
            string varFila = Definicion.Nombre + "Fila";
            //Libera cada una de las filas
            escritor.For(varFila + " = 0", varFila + " < " + cantFilas, varFila + "++");
            escritor.WriteLine( "salidaFree2 = free2( " + Definicion.Nombre + "[" + varFila + "] );" );
            escritor.If("salidaFree2 == escrituraFueraDelBuffer");
            escritor.PrintfEscrituraFueraDelBufferEnFilaDeMatriz( Definicion.Nombre, varFila );
            escritor.WriteLine("cantErrores++;");
            escritor.FinIf();
            escritor.If("salidaFree2 == liberarPosMemNoValida");
            escritor.PrintfCambioDeDireccionDelPunteroEnFilaDeMatriz(Definicion.Nombre, varFila);
            escritor.WriteLine("cantErrores++;");
            escritor.FinIf();
            escritor.FinFor();

            //Libera el arreglo de punteros
            escritor.WriteLine("salidaFree2 = free2( " + Definicion.Nombre + " );");
            escritor.If("salidaFree2 == escrituraFueraDelBuffer");
            escritor.PrintfEscrituraFueraDelBuffer(Definicion.Nombre );
            escritor.WriteLine("cantErrores++;");
            escritor.FinIf();
            escritor.If("salidaFree2 == liberarPosMemNoValida");
            escritor.PrintfCambioDeDireccionDelPuntero(Definicion.Nombre);
            escritor.WriteLine("cantErrores++;");
            escritor.FinIf();                                    
        }
        #endregion
        #endregion
    }
}