using TUAssembler.Definicion;

namespace TUAssembler.JuegoDePrueba
{
    public class Elem: Parametro
    {
        #region Variables miembro
        private string elem;
        #endregion

        #region Propiedades
        public string Valor
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
        #endregion

        public Elem( string elem )
        {
            this.elem = elem;
        }
        public bool TipoCorrecto( Tipo tipo )
        {
            bool salida = false;
            switch( tipo )
            {
                case Tipo.UInt8:
                case Tipo.UInt16:
                case Tipo.UInt32:
                case Tipo.UInt64:
                    salida = MA.SoloEnteros( elem );
                    break;

                case Tipo.Int8:
                case Tipo.Int16:
                case Tipo.Int32:
                case Tipo.Int64:
                    salida = MA.SoloEnterosConSigno( elem );
                    break;

                case Tipo.Float32:
                case Tipo.Float64:
                    salida = MA.EsPtoFlotante( elem );
                    break;

                case Tipo.Booleano:
                    salida = MA.EsBool( elem );
                    break;

                case Tipo.Char:
                    salida = elem.Length==1;
                    break;

                case Tipo.CadenaC:
                    salida = MA.EntreComillas( elem );
                    break;
            }
            return salida;
        }
        public bool UltimoElementoUno()
        {
            return elem[elem.Length - 1]=='1';
        }
    }
}