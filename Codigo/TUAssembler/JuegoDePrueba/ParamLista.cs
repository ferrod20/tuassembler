using System;
using System.IO;
using TUAssembler.Auxiliares;
using TUAssembler.Definicion;

namespace TUAssembler.JuegoDePrueba
{
    [Serializable()]
    public class ParamLista : Parametro
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

        public Elem this[int indice]
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

        public int Longitud
        {
            get
            {
                return Elementos.Length;
            }
        }
        #endregion

        #region Constructores
        public ParamLista()
        {
        }
        public ParamLista(int longitud)
        {
            Elementos = new Elem[longitud];
        }
        public void EstablecerLongitud(int longitud)
        {
            Elementos = new Elem[longitud];
        }
        #endregion

        #region Métodos
        public override void TamanioOValorParaMedicion(EscritorC escritor)
        {
            escritor.Write(Longitud * MA.CuantosBytes(Definicion.Tipo));
        }
        public void EstablecerValor(string[] fila)
        {
            EstablecerLongitud(fila.Length);
            int i = 0;

            foreach (string elemento in fila)
            {
                Elem elem = new Elem(elemento);
                if (!elem.TipoCorrecto(Definicion.Tipo))
                    throw new Exception(Mensajes.ElementoDeTipoIncorrectoEnElVector(Definicion.Nombre, i));
                Elementos[i] = elem;
                i++;
            }
        }
        public override void Leer(StreamReader lector)
        {
            string[] parametros;
            try
            {
                parametros = MA.Leer(lector);
            }
            catch (Exception e)
            {
                throw new Exception(Mensajes.ErrorAlLeerParametro(Definicion.Nombre, e));
            }
            EstablecerValor(parametros);
        }

        #region Métodos de código C
        public override void Declarar(EscritorC escritor)
        {
            string declaracion = string.Empty;
            switch (Definicion.Tipo)
            {
                case Tipo.Char:
                case Tipo.UInt8:
                case Tipo.Int8:
                    declaracion = "char ";
                    break;
                case Tipo.Int16:
                case Tipo.UInt16:
                    declaracion = "short ";
                    break;
                case Tipo.UInt32:
                case Tipo.Int32:
                    declaracion = "int ";
                    break;
                case Tipo.Booleano:
                    declaracion = "bool ";
                    break;
                case Tipo.UInt64:
                case Tipo.Int64:
                    declaracion = "longlong ";
                    break;
                case Tipo.Float32:
                    declaracion = "float ";
                    break;
                case Tipo.Float64:
                    declaracion = "double ";
                    break;
                default:
                    throw new Exception(Mensajes.TipoIncorrectoListas);
            }
            declaracion = "struct Lista" + declaracion + " *" + Definicion.Nombre + " = NULL;";
            escritor.WriteLine(declaracion);
        }
        public override void PedirMemoria(EscritorC escritor)
        {
        }
        public override void Instanciar(EscritorC escritor)
        {
            string instanciacion = string.Empty;
            foreach (Elem elemento in Elementos)
            {
                switch (Definicion.Tipo)
                {
                    case Tipo.UInt8:
                    case Tipo.Int8:
                    case Tipo.Char:
                        instanciacion = "insertarchar(&" + Definicion.Nombre + "," + elemento.Valor + ", false);";
                        break;
                    case Tipo.Int16:
                    case Tipo.UInt16:
                        instanciacion = "insertarshort(&" + Definicion.Nombre + "," + elemento.Valor + ", false);";
                        break;
                    case Tipo.UInt32:
                    case Tipo.Int32:
                        instanciacion = "insertarint(&" + Definicion.Nombre + "," + elemento.Valor + ", false);";
                        break;
                    case Tipo.Booleano:
                        instanciacion = "insertarbool(&" + Definicion.Nombre + "," + elemento.Valor + ", false);";
                        break;
                    case Tipo.UInt64:
                    case Tipo.Int64:
                        instanciacion = "insertarlonglong(&" + Definicion.Nombre + "," + elemento.Valor + ", false);";
                        break;
                    case Tipo.Float32:
                        instanciacion = "insertarfloat(&" + Definicion.Nombre + "," + elemento.Valor + ", false);";
                        break;
                    case Tipo.Float64:
                        instanciacion = "insertardouble(&" + Definicion.Nombre + "," + elemento.Valor + ", false);";
                        break;
                    default:
                        throw new Exception(Mensajes.TipoIncorrectoListas);
                }
                escritor.WriteLine(instanciacion);
            }
        }
        public override void CompararValor(EscritorC escritor)
        {
            escritor.WriteLine("//" + Definicion.Nombre);
            switch (Definicion.Tipo)
            {
                case Tipo.UInt8:
                case Tipo.Int8:
                case Tipo.Char:
                    escritor.WriteLine("struct Listachar *" + Definicion.Nombre + "listaaux;");
                    escritor.WriteLine("crearchar(&" + Definicion.Nombre + "listaaux);");
                    foreach (Elem elemento in Elementos)
                        escritor.WriteLine("insertarchar(&" + Definicion.Nombre + "listaaux, " + elemento.Valor + ", false);");
                    /*escritor.If("TienePunterosInvalidoschar(" + Definicion.Nombre + ")");
                    escritor.PrintPunterosInvalidos();
                    escritor.WriteLine("return 1;");
                    escritor.FinIf();
                    escritor.If("ListaCircularchar(" + Definicion.Nombre + ")");
                    escritor.PrintListaCircular();
                    escritor.WriteLine("return 1;");
                    escritor.FinIf();
                    */
                    escritor.WriteLine("switch(PunteroInvalidoOListaCircularchar(" + Definicion.Nombre + ")){");
                    escritor.WriteLine("case listaCircular:");
                    escritor.PrintListaCircular();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("case punteroInvalido:");
                    escritor.PrintPunterosInvalidos();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("}");

                    escritor.If("!igualdadchar(" + Definicion.Nombre + ", " + Definicion.Nombre + "listaaux)");
                    escritor.WriteLine("   cantErrores++;");
                    escritor.FinIf();
                    break;
                case Tipo.Int16:
                case Tipo.UInt16:
                    escritor.WriteLine("struct Listashort *" + Definicion.Nombre + "listaaux;");
                    escritor.WriteLine("crearshort(&" + Definicion.Nombre + "listaaux);");
                    foreach (Elem elemento in Elementos)
                        escritor.WriteLine("insertarshort(&" + Definicion.Nombre + "listaaux, " + elemento.Valor + ", false);");
                    /*                    escritor.If("TienePunterosInvalidosshort(" + Definicion.Nombre + ")");
                                        escritor.PrintPunterosInvalidos();
                                        escritor.WriteLine("return 1;");
                                        escritor.FinIf();
                                        escritor.If("ListaCircularshort(" + Definicion.Nombre + ")");
                                        escritor.PrintListaCircular();
                                        escritor.WriteLine("return 1;");
                                        escritor.FinIf();*/
                    escritor.WriteLine("switch(PunteroInvalidoOListaCircularshort(" + Definicion.Nombre + ")){");
                    escritor.WriteLine("case listaCircular:");
                    escritor.PrintListaCircular();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("case punteroInvalido:");
                    escritor.PrintPunterosInvalidos();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("}");
                    escritor.If("!igualdadshort(" + Definicion.Nombre + ", " + Definicion.Nombre + "listaaux)");
                    escritor.WriteLine("    cantErrores++;");
                    escritor.FinIf();
                    break;
                case Tipo.UInt32:
                case Tipo.Int32:
                    escritor.WriteLine("struct Listaint *" + Definicion.Nombre + "listaaux;");
                    escritor.WriteLine("crearint(&" + Definicion.Nombre + "listaaux);");
                    foreach (Elem elemento in Elementos)
                        escritor.WriteLine("insertarint(&" + Definicion.Nombre + "listaaux, " + elemento.Valor + ", false);");
                    /*escritor.If("TienePunterosInvalidosint(" + Definicion.Nombre + ")");
                    escritor.PrintPunterosInvalidos();
                    escritor.WriteLine("return 1;");
                    escritor.FinIf();
                    escritor.If("ListaCircularint(" + Definicion.Nombre + ")");
                    escritor.PrintListaCircular();
                    escritor.WriteLine("return 1;");
                    escritor.FinIf();*/
                    escritor.WriteLine("switch(PunteroInvalidoOListaCircularint(" + Definicion.Nombre + ")){");
                    escritor.WriteLine("case listaCircular:");
                    escritor.PrintListaCircular();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("case punteroInvalido:");
                    escritor.PrintPunterosInvalidos();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("}");
                    escritor.If("!igualdadint(" + Definicion.Nombre + ", " + Definicion.Nombre + "listaaux)");
                    escritor.WriteLine("    cantErrores++;");
                    escritor.FinIf();
                    break;
                case Tipo.Booleano:
                    escritor.WriteLine("struct Listabool *" + Definicion.Nombre + "listaaux;");
                    escritor.WriteLine("crearbool(&" + Definicion.Nombre + "listaaux);");
                    foreach (Elem elemento in Elementos)
                        escritor.WriteLine("insertarbool(&" + Definicion.Nombre + "listaaux, " + elemento.Valor + ", false);");
                    /*escritor.If("TienePunterosInvalidosbool(" + Definicion.Nombre + ")");
                    escritor.PrintPunterosInvalidos();
                    escritor.WriteLine("return 1;");
                    escritor.FinIf();
                    escritor.If("ListaCircularbool(" + Definicion.Nombre + ")");
                    escritor.PrintListaCircular();
                    escritor.WriteLine("return 1;");
                    escritor.FinIf();*/
                    escritor.WriteLine("switch(PunteroInvalidoOListaCircularbool(" + Definicion.Nombre + ")){");
                    escritor.WriteLine("case listaCircular:");
                    escritor.PrintListaCircular();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("case punteroInvalido:");
                    escritor.PrintPunterosInvalidos();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("}");
                    escritor.If("!igualdadbool(" + Definicion.Nombre + ", " + Definicion.Nombre + "listaaux)");
                    escritor.WriteLine("    cantErrores++;");
                    escritor.FinIf();
                    break;
                case Tipo.UInt64:
                case Tipo.Int64:
                    escritor.WriteLine("struct Listalonglong *" + Definicion.Nombre + "listaaux;");
                    escritor.WriteLine("crearlonglong(&" + Definicion.Nombre + "listaaux);");
                    foreach (Elem elemento in Elementos)
                        escritor.WriteLine("insertarlonglong(&" + Definicion.Nombre + "listaaux, " + elemento.Valor + ", false);");
                    escritor.WriteLine("switch(PunteroInvalidoOListaCircularlonglong(" + Definicion.Nombre + ")){");
                    escritor.WriteLine("case listaCircular:");
                    escritor.PrintListaCircular();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("case punteroInvalido:");
                    escritor.PrintPunterosInvalidos();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("}");
                    escritor.If("!igualdadlonglong(" + Definicion.Nombre + ", " + Definicion.Nombre + "listaaux)");
                    escritor.WriteLine("    cantErrores++;");
                    escritor.FinIf();
                    break;
                case Tipo.Float32:
                    escritor.WriteLine("struct Listafloat *" + Definicion.Nombre + "listaaux;");
                    escritor.WriteLine("crearfloat(&" + Definicion.Nombre + "listaaux);");
                    foreach (Elem elemento in Elementos)
                        escritor.WriteLine("insertarfloat(&" + Definicion.Nombre + "listaaux, " + elemento.Valor + ", false);");
                    /*escritor.If("TienePunterosInvalidosfloat(" + Definicion.Nombre + ")");
                    escritor.PrintPunterosInvalidos();
                    escritor.WriteLine("return 1;");
                    escritor.FinIf();
                    escritor.If("ListaCircularfloat(" + Definicion.Nombre + ")");
                    escritor.PrintListaCircular();
                    escritor.WriteLine("return 1;");
                    escritor.FinIf();*/
                    escritor.WriteLine("switch(PunteroInvalidoOListaCircularfloat(" + Definicion.Nombre + ")){");
                    escritor.WriteLine("case listaCircular:");
                    escritor.PrintListaCircular();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("case punteroInvalido:");
                    escritor.PrintPunterosInvalidos();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("}");
                    escritor.If("!igualdadfloat(" + Definicion.Nombre + ", " + Definicion.Nombre + "listaaux," + Definicion.Precision + ")");
                    escritor.WriteLine("    cantErrores++;");
                    escritor.FinIf();
                    break;
                case Tipo.Float64:
                    escritor.WriteLine("struct Listadouble *" + Definicion.Nombre + "listaaux;");
                    escritor.WriteLine("creardouble(&" + Definicion.Nombre + "listaaux);");
                    foreach (Elem elemento in Elementos)
                        escritor.WriteLine("insertardouble(&" + Definicion.Nombre + "listaaux, " + elemento.Valor + ", false);");
                    /*escritor.If("TienePunterosInvalidosdouble(" + Definicion.Nombre + ")");
                    escritor.PrintPunterosInvalidos();
                    escritor.WriteLine("return 1;");
                    escritor.FinIf();
                    escritor.If("ListaCirculardouble(" + Definicion.Nombre + ")");
                    escritor.PrintListaCircular();
                    escritor.WriteLine("return 1;");
                    escritor.FinIf();*/
                    escritor.WriteLine("switch(PunteroInvalidoOListaCirculardouble(" + Definicion.Nombre + ")){");
                    escritor.WriteLine("case listaCircular:");
                    escritor.PrintListaCircular();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("case punteroInvalido:");
                    escritor.PrintPunterosInvalidos();
                    escritor.WriteLine("return 1;");
                    escritor.WriteLine("}");
                    escritor.If("!igualdaddouble(" + Definicion.Nombre + ", " + Definicion.Nombre + "listaaux, " + Definicion.Precision + ")");
                    escritor.WriteLine("    cantErrores++;");
                    escritor.FinIf();
                    break;
                default:
                    throw new Exception(Mensajes.TipoIncorrectoListas);
            }
        }
        public override void LiberarMemoria(EscritorC escritor)
        {
            switch (Definicion.Tipo)
            {
                case Tipo.UInt8:
                case Tipo.Int8:
                case Tipo.Char:
                    if (this.Definicion.EntradaSalida != EntradaSalida.E)
                        escritor.WriteLine("liberarchar(&" + Definicion.Nombre + "listaaux);");
                    escritor.WriteLine("liberarchar(&" + Definicion.Nombre + ");");
                    break;
                case Tipo.UInt16:
                case Tipo.Int16:
                    if (this.Definicion.EntradaSalida != EntradaSalida.E)
                        escritor.WriteLine("liberarshort(&" + Definicion.Nombre + "listaaux);");
                    escritor.WriteLine("liberarshort(&" + Definicion.Nombre + ");");
                    break;
                case Tipo.UInt32:
                case Tipo.Int32:
                    if (this.Definicion.EntradaSalida != EntradaSalida.E)
                        escritor.WriteLine("liberarint(&" + Definicion.Nombre + "listaaux);");
                    escritor.WriteLine("liberarint(&" + Definicion.Nombre + ");");
                    break;
                case Tipo.Booleano:
                    if (this.Definicion.EntradaSalida != EntradaSalida.E)
                        escritor.WriteLine("liberarbool(&" + Definicion.Nombre + "listaaux);");
                    escritor.WriteLine("liberarbool(&" + Definicion.Nombre + ");");
                    break;
                case Tipo.UInt64:
                case Tipo.Int64:
                    if (this.Definicion.EntradaSalida != EntradaSalida.E)
                        escritor.WriteLine("liberarlonglong(&" + Definicion.Nombre + "listaaux);");
                    escritor.WriteLine("liberarlonglong(&" + Definicion.Nombre + ");");
                    break;
                case Tipo.Float32:
                    if (this.Definicion.EntradaSalida != EntradaSalida.E)
                        escritor.WriteLine("liberarfloat(&" + Definicion.Nombre + "listaaux);");
                    escritor.WriteLine("liberarfloat(&" + Definicion.Nombre + ");");
                    break;
                case Tipo.Float64:
                    if (this.Definicion.EntradaSalida != EntradaSalida.E)
                        escritor.WriteLine("liberardouble(&" + Definicion.Nombre + "listaaux);");
                    escritor.WriteLine("liberardouble(&" + Definicion.Nombre + ");");
                    break;
                default:
                    throw new Exception(Mensajes.TipoIncorrectoListas);
            }
        }
        #endregion

        #endregion
    }
}