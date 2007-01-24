using System;
using TUAssembler.Definicion;

namespace TUAssembler.JuegoDePrueba
{
    [Serializable()]
    public class Matriz: Parametro
    {
        #region Variables miembro
        private int cantFilas;
        private int cantColumnas;
        private Vector[] filas;//Cada fila es un vector
        #endregion

        #region Propiedades
        public Vector[] Filas
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
        public Matriz( int cantFilas, int cantColumnas)
        {
            this.cantFilas = cantFilas;
            this.cantColumnas = cantColumnas;
            
            Filas = new Vector[cantFilas];
            for (int i = 0; i < Filas.Length; i++)            
                Filas[i] = new Vector(cantColumnas);
            
        }
        #endregion

        public void Leer( string linea, Tipo tipo )
        {
            int indice;
            string[] parametros;
            
            parametros = linea.Split( ' ' );
            if( parametros.Length != cantFilas * cantColumnas)
                throw new Exception( Mensajes.CantidadDeParametrosNoCoincidenConDefinicion );            
            
            for (int f = 0; f < cantFilas; f++)
                for (int c = 0; c < cantColumnas; c++)
                {
                    indice = f * cantColumnas + c;
                    Elem elem = new Elem( parametros[indice] );
                    if( !elem.TipoCorrecto( tipo ))
                        throw new Exception(Mensajes.TipoIncorrectoMatriz( f, c) );
                    Filas[f][c] = elem;
                }                    
        }
    }
}