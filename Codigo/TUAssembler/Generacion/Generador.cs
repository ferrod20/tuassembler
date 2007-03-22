using System;
using System.IO;
using TUAssembler.Auxiliares;
using TUAssembler.Definicion;
using TUAssembler.JuegoDePrueba;

namespace TUAssembler.Generacion
{
    internal enum TipoSistema
    {
        DOS = 1,
        LINUX = 2
    }

    internal class Generador
    {
        #region Variables miembro
        private string archivoDefinicion;
        private string archivoPrueba;
        private DefinicionFuncion definicion;
        private Prueba[] pruebas;
        private int cantPruebas;
        private int pruebaActual;
        private TipoSistema tipoSistema;
        private bool contarCantInstrucciones;
        private bool frenarEnElPrimerError;
        private bool esAsm;
        private string archivoCuentaInstrucciones;
        #endregion

        #region Propiedades
        public bool ContarCantInstrucciones
        {
            get
            {
                return contarCantInstrucciones;
            }
            set
            {
                contarCantInstrucciones = value;
            }
        }

        public int CantPruebas
        {
            get
            {
                return cantPruebas;
            }
            set
            {
                cantPruebas = value;
            }
        }

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

        public Prueba[] Pruebas
        {
            get
            {
                if( pruebas==null )
                {
                    pruebas = new Prueba[cantPruebas];
                    for( int i = 0; i < cantPruebas; i++ )
                        pruebas[i] = new Prueba();
                }
                return pruebas;
            }
            set
            {
                pruebas = value;
            }
        }

        public Prueba PruebaActual
        {
            get
            {
                return Pruebas[pruebaActual];
            }
            set
            {
                Pruebas[pruebaActual] = value;
            }
        }

        public TipoSistema SistemaOperativo
        {
            get
            {
                return tipoSistema;
            }
            set
            {
                tipoSistema = value;
            }
        }

        public bool FrenarEnElPrimerError
        {
            get
            {
                return frenarEnElPrimerError;
            }
            set
            {
                frenarEnElPrimerError = value;
            }
        }

        public string ArchivoCuentaInstrucciones
        {
            get
            {
                return archivoCuentaInstrucciones;
            }
            set
            {
                archivoCuentaInstrucciones = value;
            }
        }
        #endregion

        #region Métodos

        #region Generación de código para la prueba
        public void GenerarPruebas()
        {
            EscritorC escritor = new EscritorC( "codigoProbador.c" );

            escritor.WriteLine( "#include <stdio.h>" );
            escritor.WriteLine("#include \"mallocfree.h\"");
            escritor.WriteLine("#include \"listas.h\"");            
            escritor.WriteLine( "#define bool int" );
            escritor.WriteLine( "#define true 1" );
            escritor.WriteLine( "#define false 0" );
            escritor.WriteLine( "FILE *fs;" );

            EscribirReferenciaExternaDeLaFuncion( escritor );
            EscribirFuncionesDePrueba( escritor );
            EscribirMain( escritor );

            escritor.Close();
        }
        private void EscribirReferenciaExternaDeLaFuncion( EscritorC escritor )
        {
            escritor.WriteLine( "extern " + Definicion.GenerarPrototipo() + ";" );
            escritor.WriteLine("extern long long timer();");
        }
        private void EscribirFuncionesDePrueba( EscritorC escritor )
        {
            pruebaActual = 0;
            foreach( Prueba prueba in Pruebas )
            {
                Mensajes.NombreDePrueba = prueba.Nombre;
                EscribirFuncionDePrueba( escritor );
                pruebaActual++;
            }
        }
        private void EscribirFuncionDePrueba( EscritorC escritor )
        {
            escritor.WriteLine( PruebaActual.Prototipo );
            escritor.AbrirCorchetes();
            escritor.WriteLine( "//------------Variables comunes------------------" );
            escritor.WriteLine( "int salidaFree2;" );
            escritor.WriteLine( "long long tiempoDeEjecucion=0;" );
            escritor.WriteLine( "long long tiempo = 0;" );
            escritor.WriteLine( "int cantCorridas = 100;" );
            escritor.WriteLine( "//------------Parametros-------------------------" );
            PruebaActual.DeclararParametros( escritor );
            escritor.WriteLine( "int cantErrores = 0;" );
            if( !ContarCantInstrucciones )
            {
                escritor.WriteLine( "//------------Pedir memoria----------------------" );
                PruebaActual.PedirMemoria( escritor );
                escritor.WriteLine( "//------------Instanciacion----------------------" );
                PruebaActual.InstanciarParametros( escritor );
                escritor.WriteLine( "//------------LlamadaFuncion---------------------" );
                LlamarFuncionAProbar( escritor );
                escritor.WriteLine( "//------------Comparacion de valores-------------" );
                PruebaActual.CompararValoresDevueltos( escritor );
                escritor.WriteLine( "//------------Liberar memoria--------------------" );
                PruebaActual.LiberarMemoria( escritor );
                //Libera la memoria que pidió y verifica que no se haya escrito fuera del buffer.
                escritor.WriteLine( "//------------Informar cant. de errores----------" );
            }
            else
            {
                escritor.WriteLine( "//------------Cuento instrucciones--------------------" );
                escritor.While( "tiempoDeEjecucion < 10000" );
                escritor.WriteLine( "tiempoDeEjecucion = 0;" );
                escritor.WriteLine( "int i;" );
                escritor.For( "i =0", "i<cantCorridas", "i++" );
                escritor.WriteLine( "//------------Pedir memoria----------------------" );
                PruebaActual.PedirMemoria( escritor );
                escritor.WriteLine( "//------------Instanciacion----------------------" );
                PruebaActual.InstanciarParametros( escritor );
                escritor.WriteLine( "tiempo = timer();" );
                LlamarFuncionAProbar( escritor );
                escritor.WriteLine( "tiempoDeEjecucion += timer() - tiempo;" );
                escritor.WriteLine( "//------------Liberar memoria--------------------" );
                PruebaActual.LiberarMemoria( escritor );
                escritor.FinWhile();
                escritor.WriteLine( "cantCorridas *=10;" );
                escritor.FinFor();
                escritor.WriteLine( "tiempoDeEjecucion = tiempoDeEjecucion / cantCorridas;" );
                escritor.WriteLine();
                escritor.WriteLine( "//---Escribo en archivo la cant de inst.----------" );
                escritor.If( "fs" );
                escritor.Write( "fprintf( fs, \"" );
                foreach( Parametro param in PruebaActual.ParametrosEntrada )
                {
                    param.TamanioOValorParaMedicion( escritor );
                    escritor.Write( "\\t" );
                }
                escritor.WriteLine( "%d\\n\", tiempoDeEjecucion);" );
                escritor.FinIf();
            }

            escritor.PrintfPruebaConcluida();
            escritor.WriteLine( "return cantErrores;" );
            escritor.CerrarCorchetes();
        }
        private void LlamarFuncionAProbar( EscritorC escritor )
        {
            string llamada = "";
            if (Definicion.DefParametroSalida != null && Definicion.DefParametroSalida.Tipo != Tipo.Void)
                llamada = Definicion.DefParametroSalida.Nombre + " = ";
            llamada += Definicion.Nombre + "( ";
            foreach( Parametro param in PruebaActual.ParametrosEntrada )
                if( param.Definicion.EsLista )
                    llamada += "&" + param.Definicion.Nombre + ", "; //por referencia
                else
                    llamada += param.Definicion.Nombre + ", ";
            if( PruebaActual.ParametrosEntrada.Length > 0 ) //Si hay parametros de entrada
                llamada = llamada.Remove( llamada.Length - 2, 2 ); //Elimino la última coma.)
            llamada += " );";
            escritor.WriteLine( llamada );
        }
        private void EscribirMain( EscritorC escritor )
        {
            escritor.WriteLine( "int main()" );
            escritor.AbrirCorchetes();
            if( ContarCantInstrucciones )
            {
                escritor.WriteLine( "/*-----Archivo para contar instrucciones-------*/" );
                escritor.WriteLine( "fs = fopen(\" " + ArchivoCuentaInstrucciones + "\", \"w\");" );
                escritor.If( "!fs" );
                escritor.PrintfNoSePudoAbrirElArchivo( ArchivoCuentaInstrucciones );
                escritor.FinIf();
            }
            escritor.WriteLine( "/*------------Parametros-------------------------*/" );
            escritor.WriteLine( "int cantErrores = 0;" );
            escritor.WriteLine( "/*------------Llamada a pruebas------------------*/" );
            foreach( Prueba prueba in Pruebas )
            {
                if( FrenarEnElPrimerError )
                    escritor.If( "cantErrores == 0" );
                escritor.WriteLine( "cantErrores = " + prueba.Nombre + "();" );
                if( FrenarEnElPrimerError )
                    escritor.FinIf();
            }
            if( ContarCantInstrucciones )
                escritor.WriteLine( "fclose(fs);" );
            escritor.PrintfPruebasConcluidas();
            escritor.WriteLine( "return 0;" );
            escritor.CerrarCorchetes();
        }
        public void GenerarTimer( string funcionAsm )
        {
            try
            {
                File.Delete( "timer.asm" );
            }
            catch
            {
                //                Console.WriteLine("Warning: El archivo timer.asm");
            }
            StreamWriter timer = new StreamWriter( "timer.asm" );
            switch( SistemaOperativo )
            {
                case TipoSistema.DOS:
                    timer.WriteLine( "global _timer" );
                    break;
                case TipoSistema.LINUX:
                    timer.WriteLine( "global timer" );
                    break;
            }
            if (this.esAsm)
                timer.WriteLine( "%include \"" + funcionAsm + "\"" );
            switch( SistemaOperativo )
            {
                case TipoSistema.DOS:
                    timer.WriteLine( "_timer:" );
                    break;
                case TipoSistema.LINUX:
                    timer.WriteLine( "timer:" );
                    break;
            }
            timer.WriteLine( "rdtsc" );
            timer.WriteLine( "ret" );
            timer.Close();
        }
        #endregion

        #region Lectura de parámetros y función
        public void LeerDefinicion()
        {
            DefinicionFuncion.VerificarDefinicion( archivoDefinicion );
            Definicion = DefinicionFuncion.Leer( archivoDefinicion );
            Definicion.VerificarUnSoloTipo();
            Definicion.VerificarValorOReferencia(); //Si es matriz, lista o vector, el tipoDeAcceso lo establece a "R"
        }
        public void LeerPrueba( StreamReader lector )
        {
            PruebaActual.LeerNombre( lector );
            PruebaActual.GenerarParametros( Definicion );
            //Hace un new de cada parametro( Matriz, Vector o Elem ) segun lo que indica Definicion.
            PruebaActual.LeerParametros( lector );
            PruebaActual.LeerFinDePrueba( lector );
        }
        public void LeerPruebas()
        {
            StreamReader lector = new StreamReader( archivoPrueba );
            LeerCantidadDePruebas( lector );
            pruebaActual = 0;
            while( pruebaActual < CantPruebas )
            {
                LeerPrueba( lector );
                pruebaActual++;
            }
            lector.Close();
            VerificarDistintosNombresDePruebas();
        }
        private void VerificarDistintosNombresDePruebas()
        {
            int i = 0;
            int n = Pruebas.Length;
            bool distintos = true;
            string nombre = string.Empty;
            string[] nombres = new string[n];

            foreach( Prueba prueba in Pruebas )
            {
                nombres[i] = prueba.Nombre;
                i++;
            }

            for( i = 0; i < n && distintos; i++ )
            {
                nombre = nombres[i];
                for( int j = i + 1; j < n && distintos; j++ )
                    distintos &= nombre!=nombres[j];
            }
            if( !distintos )
                throw new Exception( Mensajes.PruebasIguales( nombre ) );
        }
        private void LeerCantidadDePruebas( StreamReader lector )
        {
            string[] cantPruebas = MA.Leer( lector );
            if( cantPruebas.Length!=1 )
                throw new Exception( Mensajes.ParametroCantidadDePruebasIncorrecto );
            try
            {
                CantPruebas = int.Parse( cantPruebas[0] );
            }
            catch( Exception )
            {
                throw new Exception( Mensajes.ParametroCantidadDePruebasIncorrecto );
            }
        }
        #endregion

        #endregion

        #region Constructor
        public Generador(string archivoDefinicion, string archivoPrueba, bool ModoLinux, bool esAsm)
        {
            this.archivoDefinicion = archivoDefinicion;
            this.archivoPrueba = archivoPrueba;
            pruebaActual = 0;
            if( ModoLinux )
                SistemaOperativo = TipoSistema.LINUX;
            else
                SistemaOperativo = TipoSistema.DOS;
            this.esAsm = esAsm;
        }
        #endregion
    }
}