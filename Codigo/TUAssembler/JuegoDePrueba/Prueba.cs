using System;
using System.IO;
using TUAssembler.Auxiliares;
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
        //Lee el nombre de la prueba
        public void LeerNombre( StreamReader lector )
        {
            string[] nombrePrueba = MA.Leer( lector );
            if( nombrePrueba.Length!=1 )
                throw new Exception( Mensajes.NombrePruebaNoPermitido );
            Nombre = nombrePrueba[0];
        }

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

        #region Escritura de código C
        public void DeclararParametros( EscritorC escritor )
        {
            ParametrosSalida[0].Declarar( escritor );
            foreach( Parametro param in ParametrosEntrada )
                param.Declarar( escritor );
        }
        public void InstanciarParametros( EscritorC escritor )
        {
            //Instancio el valor de cada uno de los parametros de entrada que no son de salida para pasarselo a la función.
            foreach( Parametro param in ParametrosEntrada )
                if( param.Definicion.EntradaSalida!=EntradaSalida.S )
                    param.Instanciar( escritor );
        }
        public void CompararValoresDevueltos( EscritorC escritor )
        {
            //Comparo los valores de los parametros de salida y ES
            foreach( Parametro param in ParametrosSalida )
                param.CompararValor( escritor );
        }
        public void PedirMemoria( EscritorC escritor )
        {
            //Pido memoria para todos los parametros de referencia.
            foreach( Parametro param in ParametrosEntrada )
                if( param.Definicion.EntradaSalida!=EntradaSalida.S && param.Definicion.TipoDeAcceso==ValorOReferencia.R )
                    param.PedirMemoria( escritor );
        }
        public void LiberarMemoria( EscritorC escritor )
        {
            //Libero la memoria para todos los parametros en los que se pidió memoria anteriormente.
            foreach( Parametro param in ParametrosEntrada )
                if( param.Definicion.EntradaSalida!=EntradaSalida.S && param.Definicion.TipoDeAcceso==ValorOReferencia.R )
                    param.LiberarMemoria( escritor );

            if( ParametrosSalida[0].Definicion.TipoDeAcceso == ValorOReferencia.R )
                ParametrosSalida[0].LiberarMemoria(escritor);
        }
        #endregion

        #endregion

        public void LeerFinDePrueba( StreamReader lector )
        {
            string[] finPrueba;
            finPrueba = MA.Leer( lector );
            if( finPrueba.Length!=1 )
                throw new Exception( Mensajes.FinDePruebaIncorrecto );
            if( finPrueba[0]!="FinDePrueba" )
                throw new Exception( Mensajes.FinDePruebaIncorrecto );
        }
    }
}