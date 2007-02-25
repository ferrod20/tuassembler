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
            string declaracion = string.Empty;
            switch (Definicion.Tipo)
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
            declaracion += "**" + Definicion.Nombre + ";";
            escritor.WriteLine(declaracion);
        }
        public override void PedirMemoria( EscritorC escritor )
        {
            string pedido;
            int cantMemoria;
            cantMemoria = cantColumnas* cantFilas * MA.CuantosBytes(Definicion.Tipo);
            pedido = Definicion.Nombre + " = " + "malloc2( " + cantMemoria + " );";
            escritor.WriteLine(pedido);
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
            escritor.WriteLine("salidaFree2 = free2( " + Definicion.Nombre + " );");
            escritor.If("salidaFree2 == escrituraFueraDelBuffer");
            escritor.PrintfEscrituraFueraDelBuffer(Definicion.Nombre);
            escritor.WriteLine("cantErrores++;");
            escritor.FinIf();
            escritor.If("salidaFree2 == liberarPosMemNoValida");
            escritor.CambioDeDireccionDelPuntero(Definicion.Nombre);
            escritor.WriteLine("cantErrores++;");
            escritor.FinIf();
        }
        #endregion
        #endregion
    }
}