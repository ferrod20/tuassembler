using System;
using System.IO;
using TUAssembler.Definicion;
using TUAssembler.JuegoDePrueba;

namespace TUAssembler.Generacion
{
    internal class Generador
    {
        #region Variables miembro
        private string archivoDefinicion;
        private string archivoPrueba;
        private DefinicionFuncion definicion;
        private Prueba prueba;
        #endregion

        #region Propiedades
        public string ArchivoDefinicion
        {
            get
            {
                return archivoDefinicion;
            }
            set
            {
                archivoDefinicion = value;
            }
        }

        public string ArchivoPrueba
        {
            get
            {
                return archivoPrueba;
            }
            set
            {
                archivoPrueba = value;
            }
        }

        public DefinicionFuncion Definicion
        {
            get
            {
                return definicion;
            }
            set
            {
                definicion = value;
            }
        }

        public Prueba Prueba
        {
            get
            {
                if( prueba==null )
                    prueba = new Prueba();
                return prueba;
            }
            set
            {
                prueba = value;
            }
        }
        #endregion

        #region Métodos

        #region Generacion de codigo para la prueba
        public void GenerarPrueba( ref StreamWriter escritor )
        {
            EscribirReferenciaExternaDeLaFuncion( ref escritor );
            escritor.WriteLine();
            EscribirFuncionPrueba( ref escritor );
        }
        private void EscribirReferenciaExternaDeLaFuncion( ref StreamWriter escritor )
        {
            escritor.WriteLine( "extern " + Definicion.GenerarPrototipo() + ";" );
        }
        private void EscribirFuncionPrueba( ref StreamWriter escritor )
        {
            escritor.WriteLine( " void main()" );
            escritor.WriteLine( "{" );
            escritor.WriteLine();
            escritor.WriteLine( "/*------------Parametros-------------------------*/" );
            escritor.WriteLine();
            DeclararParametros( ref escritor );
            escritor.WriteLine();
            escritor.WriteLine( "/*------------Instanciacion----------------------*/" );
            escritor.WriteLine();
            InstanciarParametros( ref escritor );
            escritor.WriteLine();
            escritor.WriteLine( "/*------------LlamadaFuncion---------------------*/" );
            escritor.WriteLine();
            LlamarFuncionAProbar( ref escritor );
            escritor.WriteLine();
            escritor.WriteLine( "/*------------Comparacion de valores-------------*/" );
            escritor.WriteLine();
            CompararValoresDevueltos( ref escritor );
            escritor.WriteLine();
            escritor.WriteLine( "}" );
        }
        private void CompararValoresDevueltos( ref StreamWriter escritor )
        {           
            //Comparo los valores de los parametros de salida y los de ES
            foreach( Parametro param in Prueba.ParametrosSalida  )
                if( param.EsDeSalidaOEntradaSalida )    
                    param.CompararValor( ref escritor );                                                
        }
        private void InstanciarParametros( ref StreamWriter escritor )
        {
            string instanciacion;

            //Instancio el valor que debe devolver la funcion para compararlo despues
            instanciacion = Prueba.ParametrosSalida[0].Instanciar();
            escritor.WriteLine( instanciacion );

            //Instancio el valor de cada uno de los parametros de ParametrosEntrada que son de salida para pasarselo a la funcion.
            foreach( Parametro param in Prueba.ParametrosEntrada )
                if( param.Definicion.EntradaSalida!=EntradaSalida.S )
                {
                    instanciacion = param.Instanciar();
                    escritor.WriteLine( instanciacion );
                }
        }
        private void LlamarFuncionAProbar( ref StreamWriter escritor )
        {
            string llamada;

            llamada = Definicion.DefParametroSalida.Nombre + " = " +
                Definicion.Nombre + "( ";
            foreach( Parametro param in Prueba.ParametrosEntrada )
                llamada += param.Definicion.Nombre + ", ";

            llamada = llamada.Remove( llamada.Length - 2, 2 ); //Elimino la última coma.)

            llamada += " );";
            escritor.WriteLine( llamada );
        }
        private void DeclararParametros( ref StreamWriter escritor )
        {
            string declaracion;

            declaracion = Prueba.ParametrosSalida[0].Declarar();
            escritor.WriteLine( declaracion );

            foreach( Parametro param in Prueba.ParametrosEntrada )
            {
                declaracion = param.Declarar();
                escritor.WriteLine( declaracion );
            }
        }
        #endregion

        #region Lectura de parámetros y funcion
        public void LeerDefinicion()
        {
            Definicion = DefinicionFuncion.Leer( archivoDefinicion );
        }
        public void LeerPrueba()
        {
            StreamReader lector = new StreamReader( archivoPrueba );
            LeerParametrosSalida( lector );
            LeerParametrosEntrada( lector );
        }
        private void LeerParametrosEntrada( StreamReader lector )
        {
            DefParametro[] defParametros;
            string linea;
            int i = 0;

            defParametros = Definicion.DefParametrosEntrada;
            Prueba.ParametrosEntrada = new Parametro[defParametros.Length];

            foreach( DefParametro defParametro in defParametros )
            {
                linea = lector.ReadLine();
                if( linea==string.Empty )
                    throw new Exception( Mensajes.CantidadParametrosEntradaNoCoincideConDefinicion );

                Prueba.ParametrosEntrada[i] = ObtenerParametro( linea, defParametro );
                i++;
            }
        }
        private void LeerParametrosSalida( StreamReader lector )
        {
            DefParametro[] defParametros, defParametrosSoES;
            string linea;
            int i, cuantos;

            //Obtengo la definicion de los parametros de salida y los de ES o S
            //defParametrosSoES queda como defParametrosSoES[0] la definicion del parametro que devuelve la funcion
            //defParametrosSoES queda como defParametrosSoES[i] i>0, la definicion del i-esimo parametro de Salida o ES que toma la funcion.
            cuantos = Definicion.CuantosParametrosESoS();
            defParametros = new DefParametro[cuantos + 1];
            defParametros[0] = Definicion.DefParametroSalida;
            defParametrosSoES = Definicion.ObtenerDefParametrosESoS();

            for( i = 1; i < cuantos + 1; i++ )
                defParametros[i] = defParametrosSoES[i - 1];

            //Obtengo los parametros de salida y los de ES o S
            //Prueba.ParametrosSalida queda como Prueba.ParametrosSalida[0] el parametro que tiene que devolver la funcion
            //Prueba.ParametrosSalida queda como Prueba.ParametrosSalida[i] i>0, el parametro i-esimo de Salida o ES que toma la funcion.
            Prueba.ParametrosSalida = new Parametro[defParametros.Length];

            i = 0;
            foreach( DefParametro defParametro in defParametros )
            {
                linea = lector.ReadLine();
                if( linea==string.Empty )
                    throw new Exception( Mensajes.CantidadParametrosEntradaNoCoincideConDefinicion );

                Prueba.ParametrosSalida[i] = ObtenerParametro( linea, defParametro );
                i++;
            }
        }
        private Parametro ObtenerParametro( string linea, DefParametro defParam )
        {
            ParamMatriz paramMatriz;
            ParamVector paramVector;
            string[] parametros;
            Parametro salida = null;
            if( defParam.EsMatriz )
            {
                paramMatriz = new ParamMatriz( defParam.CantFilas, defParam.CantColumnas );
                paramMatriz.Leer( linea, defParam.Tipo );
                //Lee la salida y verifica que los parametros sean del tipo valido.
                salida = paramMatriz;
            }
            if( defParam.EsVector )
            {
                paramVector = new ParamVector( defParam.Longitud );
                paramVector.Leer( linea, defParam.Tipo );
                //Lee la salida y verifica que los parametros sean del tipo valido.
                salida = paramVector;
            }
            if( defParam.EsElemento )
            {
                parametros = linea.Split( ' ' );
                if( parametros.Length!=1 )
                    throw new Exception( Mensajes.CantidadDeParametrosNoCoincidenConDefinicion );
                salida = new Elem( parametros[0] );
            }
            salida.Definicion = defParam;
            return salida;
        }
        #endregion

        #endregion

        #region Constructor
        public Generador( string archivoDefinicion, string archivoPrueba )
        {
            this.archivoDefinicion = archivoDefinicion;
            this.archivoPrueba = archivoPrueba;
        }
        #endregion
    }
}