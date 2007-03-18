using System.IO;
using TUAssembler.Auxiliares;
using TUAssembler.Definicion;

namespace TUAssembler.JuegoDePrueba
{
    public class Parametro
    {
        #region Variables miembro
        private DefParametro definicion;
        #endregion

        #region Propiedades
        public DefParametro Definicion
        {
            get
            {
                if( definicion==null )
                    definicion = new DefParametro();
                return definicion;
            }
            set
            {
                definicion = value;
            }
        }

        public bool EsDeSalidaOEntradaSalida
        {
            get
            {
                return Definicion.EntradaSalida!=EntradaSalida.E;
            }
        }
        #endregion

        #region Métodos

        #region Escritura código C
        //No borrar, sirven para definir los metodos en las clases hijas( ParamVector, ParamMatriz y Elem )
        public virtual void Declarar( EscritorC escritor )
        {
        }
        public virtual void Instanciar( EscritorC escritor )
        {
        }
        public virtual void CompararValor( EscritorC escritor )
        {
        }
        public virtual void PedirMemoria( EscritorC escritor )
        {
        }
        public virtual void LiberarMemoria( EscritorC escritor )
        {
        }
        public virtual void TamanioOValorParaMedicion( EscritorC escritor )
        {
        }
        #endregion

        //No borrar, sirve para definir el metodo en las clases hijas( ParamVector, ParamMatriz y Elem )
        public virtual void Leer( StreamReader lector )
        {
        }
        #endregion
    }
}