using System;
using System.IO;
using System.Xml;
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
        private static string erroresDeValidacion;
        #endregion

        #region Propiedades
        [XmlIgnore()]
        private static string ErroresDeValidacion
        {
            get
            {
                return erroresDeValidacion;
            }
            set
            {
                erroresDeValidacion = value;
            }
        }

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
            DefinicionFuncion defFuncion;
            XmlReader lectorDefinicionXml;
            FileStream fs = new FileStream( archivo, FileMode.Open );
            lectorDefinicionXml = XmlReader.Create( fs );

            try
            {
                xml = new XmlSerializer( typeof( DefinicionFuncion ) );
                defFuncion = (DefinicionFuncion) xml.Deserialize( lectorDefinicionXml );
            }
            catch( Exception e )
            {
                throw new Exception( Mensajes.ErrorLecturaDefinicion( archivo, e ) );
            }
            finally
            {
                lectorDefinicionXml.Close();
                fs.Close();
            }
            return defFuncion;
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
        public static void VerificarDefinicion( string archivo )
        {
            XmlValidatingReader lectorEsquema;
            XmlSchemaCollection esquemas = new XmlSchemaCollection();
            XmlReader lectorDefinicionXml;
            ErroresDeValidacion = string.Empty;
            FileStream fs = new FileStream( archivo, FileMode.Open );
            lectorDefinicionXml = XmlReader.Create( fs );
            try
            {
                lectorEsquema = new XmlValidatingReader( lectorDefinicionXml );
                esquemas.Add( null, "esquema.xsd" );

                lectorEsquema.ValidationType = ValidationType.Schema;
                lectorEsquema.Schemas.Add( esquemas );
                lectorEsquema.ValidationEventHandler += ValidacionXml;

                while( lectorEsquema.Read() )
                {
                }
            }
            catch( Exception e )
            {
                throw new Exception( Mensajes.ErrorVerificacionDefinicion( archivo, e ) );
            }
            finally
            {
                lectorDefinicionXml.Close();
                fs.Close();
            }

            if( ErroresDeValidacion!=string.Empty )
                throw new Exception( Mensajes.ErrorVerificacionDeDefinicion + "\n" + ErroresDeValidacion );
        }
        private static void ValidacionXml( object sender, ValidationEventArgs args )
        {
            if( args.Severity==XmlSeverityType.Error )
                ErroresDeValidacion += ( "Error: " + args.Message + "\n" );
        }
        public void VerificarUnSoloTipo()
        {
            foreach( DefParametro parametro in DefParametrosEntrada )
                parametro.VerificarUnSoloTipo();
            DefParametroSalida.VerificarUnSoloTipo();
        }
        #endregion

        public void VerificarValorOReferencia()
        {
            foreach( DefParametro parametro in DefParametrosEntrada )
                parametro.VerificarValorOReferencia();
            DefParametroSalida.VerificarValorOReferencia();
        }
    }
}