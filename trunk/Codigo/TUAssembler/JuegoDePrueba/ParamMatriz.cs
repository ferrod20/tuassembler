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
        /*
        public void EstablecerFilasYColumnas(int cantFilas, int cantColumnas)
        {
            this.cantFilas = cantFilas;
            this.cantColumnas = cantColumnas;

            Filas = new ParamVector[cantFilas];
            for (int i = 0; i < Filas.Length; i++)
                Filas[i] = new ParamVector(cantColumnas);
        }
        */
        public override void Leer( StreamReader lector )
        {
            string[] filas;
            filas = MA.LeerMatriz( lector );
            Filas = new ParamVector[filas.Length];
            int f = 0;

            foreach( string fila in filas )
            {
                ParamVector vector = new ParamVector();
                vector.EstablecerValor( fila );
                Filas[f] = vector;
                f++;
            }
        }
        public override void Instanciar(StreamWriter escritor)
        {
            string instanciacion = string.Empty;
            instanciacion = Definicion.Nombre + " = { ";
            foreach( ParamVector fila in Filas )
                foreach( Elem elemento in fila.Elementos )
                    instanciacion = elemento.Valor + ", ";
            instanciacion += " }";
            
            escritor.WriteLine( instanciacion );            
        }
        public override void CompararValor( EscritorC escritor )
        {
            
        }
        #endregion
    }
}