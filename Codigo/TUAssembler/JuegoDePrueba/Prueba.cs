using System;

namespace TUAssembler.JuegoDePrueba
{
    [Serializable()]
    public class Prueba
    {
        #region Variables miembro
        private Parametro[] entrada;
        private Parametro salida;
        #endregion

        #region Propiedades
        public Parametro[] Entrada
        {
            get
            {
                return entrada;
            }
            set
            {
                entrada = value;
            }
        }

        public Parametro Salida
        {
            get
            {
                return salida;
            }
            set
            {
                salida = value;
            }
        }
        #endregion

        #region Métodos
        #endregion
    }
}