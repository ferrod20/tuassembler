using System;

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
        public ParamVector()
        {
        }
        public ParamVector( int longitud )
        {
            Elementos = new Elem[longitud];
        }
        public void EstablecerLongitud( int longitud )
        {
            Elementos = new Elem[longitud];
        }
        #endregion

        public void EstablecerValor( string fila )
        {
            string[] elementos;
            elementos = MA.ObtenerElementosDeLaFila( fila );
            EstablecerLongitud( elementos.Length );
            int i = 0;

            foreach( string elemento in elementos )
            {
                Elem elem = new Elem( elemento );
                /*if (!elem.TipoCorrecto(Definicion.Tipo))
                        throw new Exception(Mensajes.TipoIncorrecto );
                     * */
                Elementos[i] = elem;
                i++;
            }
        }
    }
}