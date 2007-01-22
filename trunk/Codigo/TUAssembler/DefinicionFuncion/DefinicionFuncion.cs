
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TUAssembler
{
    [Serializable()]
    public class DefinicionFuncion
    {
        #region Variables miembro
        private string nombre;
        private Parametro parametroSalida;
        private Parametro[] parametrosEntrada;
        #endregion

        #region Propiedades
        [XmlAttribute()]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public Parametro ParametroSalida
        {
            get { return parametroSalida; }
            set { parametroSalida = value; }
        }

        public Parametro[] ParametrosEntrada
        {
            get { return parametrosEntrada; }
            set { parametrosEntrada = value; }
        }

        #endregion

        public string GenerarPrototipo()
        {
            string prototipo;
            prototipo = parametroSalida.ToString();
            prototipo += " " + nombre + "( ";
            foreach ( Parametro paramEntrada in parametrosEntrada)            
                prototipo += paramEntrada + ", ";

            prototipo.Remove(prototipo.Length - 2, 2);//Elimino la última coma.
            prototipo += " )";

            return prototipo;
        }

        public static DefinicionFuncion Leer(string archivo)
        {
            XmlSerializer xml;
            DefinicionFuncion defFuncion = null;            
            FileStream fs;
            try
            {
                fs = new FileStream(archivo, FileMode.Open);
                xml = new XmlSerializer(typeof(DefinicionFuncion));
                defFuncion = (DefinicionFuncion)xml.Deserialize(fs);
            }
            catch (Exception e)
            {
                archivo = e.Message;
            }
            return defFuncion;
        }

        public void CrearInstanciaDePrueba()
        {
            nombre = "funcion1";
            parametroSalida = new Parametro();
            parametroSalida.TipoDeAcceso = ValorOReferencia.R;
            ParametroSalida.EntradaSalida = EntradaSalida.ES;
            ParametroSalida.Tipo = Tipo.UInt16;

            Parametro parametroEntrada1 = new Parametro();
            parametroEntrada1.TipoDeAcceso = ValorOReferencia.V;
            parametrosEntrada = new Parametro[1];
            parametrosEntrada[0] = parametroEntrada1;                        
        }
    }
}
