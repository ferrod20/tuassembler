using System;
using System.IO;
using System.Text;
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
        public void LeerDefinicion()
        {
            Definicion = DefinicionFuncion.Leer( archivoDefinicion );
        }
        public void LeerPrueba()
        {
            StreamReader lector = new StreamReader( archivoPrueba );
            LeerSalida( lector );
            LeerEntrada( lector );
        }
        public string GenerarPrueba()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine( "extern " + Definicion.GenerarPrototipo() + ";" );
            sb.AppendLine();
            ArmarFuncionPrueba( ref sb );
            return sb.ToString();
        }
        private void ArmarFuncionPrueba( ref StringBuilder sb )
        {
            sb.AppendLine( " void main()" );
            sb.AppendLine( "{" );
            sb.AppendLine();
            sb.AppendLine("//------------Parametros------------");
            sb.AppendLine();
            DeclararParametros( ref sb );
            sb.AppendLine();
            sb.AppendLine("//------------Instanciación------------");
            sb.AppendLine();
            InstanciarParametros( ref sb );
            sb.AppendLine();
            sb.AppendLine("//------------LlamadaFuncion------------");
            sb.AppendLine();
            LlamarFuncionAProbar( ref sb );
            sb.AppendLine();
            sb.AppendLine("//------------Comparacion de valores------------");
            sb.AppendLine();
            CompararValoresDevueltos( ref sb );
            sb.AppendLine();
            sb.AppendLine( "}" );
        }
        private void CompararValoresDevueltos( ref StringBuilder sb )
        {
            
        }
        private void InstanciarParametros( ref StringBuilder sb )
        {
            string instanciacion;
            int cuantosParam = Definicion.DefParametrosEntrada.Length;
            
            instanciacion = Definicion.DefParametroSalida.Instanciar(Prueba.Salida);
            
            for(int i=0; i<cuantosParam; i++ )
            {
                instanciacion = Definicion.DefParametrosEntrada[i].Instanciar(Prueba.Entrada[i]);
                sb.AppendLine(instanciacion);
            }            
        }
        private void LlamarFuncionAProbar( ref StringBuilder sb )
        {
            
        }
        
        private void DeclararParametros( ref StringBuilder sb )
        {            
            string declaracion;
            
            declaracion = Definicion.DefParametroSalida.Declarar();
            sb.AppendLine(declaracion);                
            
            foreach( DefParametro defParam in Definicion.DefParametrosEntrada )
            {
                declaracion = defParam.Declarar();                
                sb.AppendLine( declaracion );                
            }                        
        }
        private void LeerEntrada( StreamReader lector )
        {
            DefParametro[] defDefParametros;
            string linea;
            int i = 0;

            defDefParametros = Definicion.DefParametrosEntrada;
            Prueba.Entrada = new Parametro[defDefParametros.Length];

            foreach( DefParametro defParametro in defDefParametros )
            {
                linea = lector.ReadLine();
                if( linea==string.Empty )
                    throw new Exception( Mensajes.CantidadParametrosEntradaNoCoincideConDefinicion );
                Prueba.Entrada[i] = ObtenerParametro( linea, defParametro );
                i++;
            }
        }
        private void LeerSalida( StreamReader lector )
        {
            DefParametro[] defDefParametros, defDefParametrosEntrada;
            string linea;
            int i, cuantos;

            //-----------------------Obtengo parametros de salida y los de ES o S
            cuantos = Definicion.CuantosParametrosESoS();
            defDefParametros = new DefParametro[cuantos + 1];
            defDefParametros[0] = Definicion.DefParametroSalida;
            defDefParametrosEntrada = Definicion.ObtenerParametrosESoS();

            for( i = 1; i < cuantos + 1; i++ )
                defDefParametros[i] = defDefParametrosEntrada[i];
            //-----------------------Obtengo parametros de salida y los de ES o S

            Prueba.Salida = new Parametro[defDefParametros.Length];

            foreach( DefParametro defParametro in defDefParametros )
            {
                linea = lector.ReadLine();
                if( linea==string.Empty )
                    throw new Exception( Mensajes.CantidadParametrosEntradaNoCoincideConDefinicion );
                Prueba.Salida[i] = ObtenerParametro( linea, defParametro );
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
                paramMatriz.Leer( linea, defParam.Tipo ); //Lee la salida y verifica que los parametros sean del tipo valido.
                salida = paramMatriz;
            }
            if( defParam.EsVector )
            {
                paramVector = new ParamVector( defParam.Longitud );
                paramVector.Leer( linea, defParam.Tipo ); //Lee la salida y verifica que los parametros sean del tipo valido.
                salida = paramVector;
            }
            if( defParam.EsElemento )
            {
                parametros = linea.Split( ' ' );
                if( parametros.Length!=1 )
                    throw new Exception( Mensajes.CantidadDeParametrosNoCoincidenConDefinicion );
                salida = new Elem( parametros[0] );
            }
            return salida;
        }
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