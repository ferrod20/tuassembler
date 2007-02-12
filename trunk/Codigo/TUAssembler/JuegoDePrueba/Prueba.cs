using System;
using TUAssembler.Definicion;

namespace TUAssembler.JuegoDePrueba
{
    [Serializable()]
    public class Prueba
    {
        #region Variables miembro
        private Parametro[] parametrosEntrada;
        private Parametro[] parametrosSalida;
        #endregion

        #region Propiedades
        public Parametro[] ParametrosEntrada
        {
            get
            {
                return parametrosEntrada;
            }
            set
            {
                parametrosEntrada = value;
            }
        }

        public Parametro[] ParametrosSalida
        {
            get
            {
                return parametrosSalida;
            }
            set
            {
                parametrosSalida = value;
            }
        }
        #endregion

        #region Métodos
        public int CuantosParametrosSonDeESoS()
        {
            int cuantos = 0;
            foreach( Parametro parametro in ParametrosEntrada )
                if( parametro.Definicion.EntradaSalida==EntradaSalida.S ||
                    parametro.Definicion.EntradaSalida==EntradaSalida.ES )
                    cuantos++;
            return cuantos;
        }
        public Parametro[] ObtenerParametrosESoS()
        {
            Parametro param;
            Parametro[] salida = new Parametro[CuantosParametrosSonDeESoS()];

            for( int i = 0; i < ParametrosEntrada.Length; i++ )
            {
                param = ParametrosEntrada[i];
                if( param.Definicion.EntradaSalida==EntradaSalida.S || param.Definicion.EntradaSalida==EntradaSalida.ES
                    )
                    salida[i] = param;
            }
            return salida;
        }
        #endregion
    }
}