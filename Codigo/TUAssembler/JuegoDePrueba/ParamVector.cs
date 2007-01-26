using System;
using TUAssembler.Definicion;

namespace TUAssembler.JuegoDePrueba
{
    [Serializable()]
    public class ParamVector: Parametro
    {
        #region Variables miembro
        private Elem[] elem;
        #endregion

        #region Propiedades
        public Elem[] Elementos
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

        public Elem this[ int indice ]
        {
            get
            {
                return Elementos[indice];
            }
            set
            {
                Elementos[indice] = value;
            }
        }
        #endregion

        #region Constructores
        public ParamVector( int longitud )
        {
            Elementos = new Elem[longitud];
        }
        #endregion

        public void Leer( string linea, Tipo tipo )
        {
            int longitud;
            string[] parametros;

            longitud = Elementos.Length;
            parametros = linea.Split( ' ' );
            if( parametros.Length!=longitud )
                throw new Exception( Mensajes.CantidadDeParametrosNoCoincidenConDefinicion );

            for( int i = 0; i < longitud; i++ )
            {
                Elem elem = new Elem( parametros[i] );
                if( !elem.TipoCorrecto( tipo ) )
                    throw new Exception( Mensajes.TipoIncorrectoVector( i ) );
                Elementos[i] = elem;
            }
        }
    }
}