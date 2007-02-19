using System;
using System.IO;
using TUAssembler.Auxiliares;

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

        #region Métodos
        public override void Instanciar(StreamWriter escritor)
        {
            string instanciacion;
            instanciacion = Definicion.Nombre + " = { ";
            foreach (Elem elemento in Elementos)
                instanciacion += elemento.Valor + ", ";
            instanciacion += " }";
            escritor.WriteLine( instanciacion );
        }
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
        public override void CompararValor( EscritorC escritor )
        {
            if (Definicion.EsVector)
            {
                for (int i = 0; i < Elementos.Length; i++)
                {
                    escritor.WriteLine("if ( " + Definicion.Nombre + "[" + i + "] != " + this[i].Valor + " )");
                    escritor.WriteLine(Mensajes.PrintfValorDistintoCadena(Definicion.Nombre, this[i].Valor, i));
                }
            }
        }
        #endregion
    }
}