using System;
using System.IO;
using System.Xml.Schema;
using System.Xml.Serialization;
using TUAssembler.Definicion;

namespace TUAssembler
{
    [Serializable()]
    public class DefinicionFuncion
    {
        #region Variables miembro
        private string nombre;
        private DefParametro parametroSalida;
        private DefParametro[] parametrosEntrada;
        #endregion

        #region Propiedades
        [XmlAttribute()]
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }

        public DefParametro DefParametroSalida
        {
            get
            {
                return parametroSalida;
            }
            set
            {
                parametroSalida = value;
            }
        }

        public DefParametro[] DefParametrosEntrada
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
        #endregion

        #region Métodos
        public string GenerarPrototipo()
        {
            string prototipo;
            prototipo = parametroSalida.ToString();
            prototipo += " " + nombre + "( ";
            foreach( DefParametro paramEntrada in parametrosEntrada )
                prototipo += paramEntrada + ", ";

            if( parametrosEntrada.Length > 0 ) //Si la funcion tiene parametros de entrada.
                prototipo = prototipo.Remove( prototipo.Length - 2, 2 );
            prototipo += " )";

            return prototipo;
        }
        public static DefinicionFuncion Leer( string archivo )
        {
            XmlSerializer xml;
            DefinicionFuncion defFuncion = null;
            FileStream fs;

            
                
            try
            {
                fs = new FileStream( archivo, FileMode.Open );
                xml = new XmlSerializer( typeof( DefinicionFuncion ) );

                XmlSchema esquema = XmlSchema.Read(fs, ValidacionXml);
                //XmlSchemaValidator validador = new XmlSchemaValidator();
                //validador.AddSchema(esquema);            

                defFuncion = (DefinicionFuncion) xml.Deserialize( fs );
            }
            catch( Exception e )
            {
                throw new Exception( Mensajes.ErrorLecturaDefinicion( archivo, e ) );
            }
            return defFuncion;
        }

        static void ValidacionXml(object sender, ValidationEventArgs args)
        {
        /*    if (args.Severity == XmlSeverityType.Warning)
                Console.Write("WARNING: ");
            else if (args.Severity == XmlSeverityType.Error)
                Console.Write("ERROR: ");

            Console.WriteLine(args.Message);
         * */
        }

        public void CrearInstanciaDePrueba()
        {
            //            nombre = "funcion1";
            parametroSalida = new DefParametro();
            parametroSalida.TipoDeAcceso = ValorOReferencia.R;
            DefParametroSalida.EntradaSalida = EntradaSalida.ES;
            DefParametroSalida.Tipo = Tipo.UInt16;

            DefParametro parametroEntrada1 = new DefParametro();
            parametroEntrada1.TipoDeAcceso = ValorOReferencia.V;
            parametrosEntrada = new DefParametro[1];
            parametrosEntrada[0] = parametroEntrada1;
        }
        public int CuantosParametrosESoS()
        {
            int cuantos = 0;
            foreach( DefParametro defParam in DefParametrosEntrada )
                if( defParam.EntradaSalida==EntradaSalida.ES || defParam.EntradaSalida==EntradaSalida.S )
                    cuantos++;
            return cuantos;
        }
        public DefParametro[] ObtenerDefParametrosESoS()
        {
            DefParametro[] defParametros = new DefParametro[CuantosParametrosESoS()];
            int i = 0;
            foreach( DefParametro defParam in DefParametrosEntrada )
                if( defParam.EntradaSalida==EntradaSalida.ES || defParam.EntradaSalida==EntradaSalida.S )
                {
                    defParametros[i] = defParam;
                    i++;
                }
            return defParametros;
        }
        #endregion
    }

    internal class XmlTextReader
    {
    }
}