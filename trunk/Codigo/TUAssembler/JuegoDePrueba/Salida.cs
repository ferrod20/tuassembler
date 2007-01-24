using System;

namespace TUAssembler.JuegoDePrueba
{
    [Serializable()]
    public class Salida
    {
        #region Variables miembro
        private Parametro parametro;
        #endregion

        #region Propiedades
        public Parametro Parametro
        {
            get
            {
                return parametro;
            }
            set
            {
                parametro = value;
            }
        }
        #endregion
    }
}