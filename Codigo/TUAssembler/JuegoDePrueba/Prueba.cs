using System;
using System.IO;
using TUAssembler.Definicion;

namespace TUAssembler.JuegoDePrueba
{
    public class Prueba
    {
        #region Variables miembro
        private Parametro[] parametrosEntrada;
        private Parametro[] parametrosSalida;
        private string nombre;
        #endregion

        #region Propiedades
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

        public string Prototipo
        {
            get
            {
                return "int " + Nombre + "()";
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

        #region Generacion de parametros
        //En base a la definicion crea los parametro correspondientes; hace un new de Matriz, Vector o Elem para luego poder ser leidos.
        public void GenerarParametros( DefinicionFuncion definicion )
        {
            GenerarParametrosDeSalida( definicion );
            GenerarParametrosDeEntrada( definicion );
        }
        private void GenerarParametrosDeEntrada( DefinicionFuncion definicion )
        {
            DefParametro[] defParametros = definicion.DefParametrosEntrada;
            ParametrosEntrada = new Parametro[defParametros.Length];

            for( int i = 0; i < ParametrosEntrada.Length; i++ )
                ParametrosEntrada[i] = defParametros[i].GenerarParametro();
        }
        private void GenerarParametrosDeSalida( DefinicionFuncion definicion )
        {
            DefParametro[] defParametrosSoES;
            int cuantos;

            cuantos = definicion.CuantosParametrosESoS();
            ParametrosSalida = new Parametro[cuantos + 1];
            ParametrosSalida[0] = definicion.DefParametroSalida.GenerarParametro();
            defParametrosSoES = definicion.ObtenerDefParametrosESoS();

            for( int i = 1; i < cuantos + 1; i++ )
                ParametrosSalida[i] = defParametrosSoES[i - 1].GenerarParametro();
        }
        #endregion

        #region Lectura de parametros
        public void LeerParametros( StreamReader lector )
        {
            try
            {
                LeerParametrosSalida( lector );
            }
            catch( Exception e )
            {
                throw new Exception( Mensajes.ErrorAlLeerParametroDeSalida( e ) );
            }
            try
            {
                LeerParametrosEntrada( lector );
            }
            catch( Exception e )
            {
                throw new Exception( Mensajes.ErrorAlLeerParametroDeEntrada( e ) );
            }
        }
        private void LeerParametrosSalida( StreamReader lector )
        {
            foreach( Parametro parametro in ParametrosSalida )
                parametro.Leer( lector );
        }
        private void LeerParametrosEntrada( StreamReader lector )
        {
            foreach( Parametro parametro in ParametrosEntrada )
                parametro.Leer( lector );
        }
        #endregion

        public void LeerNombre( StreamReader lector )
        {
            string[] nombrePrueba = MA.Leer( lector );
            if( nombrePrueba.Length > 1 )
                throw new Exception( Mensajes.NombrePruebaNoPermitido );
            Nombre = nombrePrueba[0];
        }
    }
}