using System;
using System.IO;
using TUAssembler.Auxiliares;
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
            escritor.WriteLine( "#include \"libreria.h\"" );
            escritor.WriteLine( "#define bool int" );
            escritor.WriteLine( "#define true 1" );
            escritor.WriteLine( "#define false 0" );

            EscribirReferenciaExternaDeLaFuncion( escritor );
            EscribirMallocFreeFunctions( escritor );
            EscribirFuncionesDePrueba( escritor );
            EscribirMain( escritor );

            escritor.Close();
        }
        private void EscribirReferenciaExternaDeLaFuncion( EscritorC escritor )
        {
            escritor.WriteLine( "extern " + Definicion.GenerarPrototipo() + ";" );
        }
        private void EscribirMallocFreeFunctions( EscritorC escritor )
        {
            //El uso es el siguiente:
            // - La funcion a probar debe llamar a malloc2 con la cantidad de bytes que se quieren pedir.
            // - Cuando ya no necesite la memoria llamara a free2 pasandole de parametro el puntero devuelto por malloc2.
            // - Desde fuera de la funcion se liberara realmente toda la memoria pedida llamando a free2all.
            // Ej:
            // void prueba(){
            //    char* c = malloc2(10);
            //    printf("Hola!");
            //    free2(c);
            // }
            //
            // int main(){
            //    prueba();
            //    free2all();
            //    return 0;
            // }
            escritor.IdentacionActiva = false;
            escritor.WriteLine();
            escritor.WriteLine( "#define todoBien 0" );
            escritor.WriteLine( "#define liberarPosMemNoValida 1" );
            escritor.WriteLine( "#define escrituraFueraDelBuffer 2" );
            escritor.WriteLine();
            escritor.WriteLine( "int cantPedidosMemoria = 0;" );
            escritor.WriteLine( "char* pedidos[sizeof(int)*10000];" );
            escritor.WriteLine( "int tamanioPedidos[sizeof(int)*10000];" );
            escritor.WriteLine( "bool fueLiberado[sizeof(bool)*10000];" );
            escritor.WriteLine();
            escritor.WriteLine( "char* malloc2(int cantBytes){" );
            escritor.WriteLine( "   int i;" );
            escritor.WriteLine( "   char* ret_value;" );
            escritor.WriteLine(
                "   ret_value = malloc(cantBytes + 8 + 8);	//8 bytes antes y 8 bytes despues para controlar que no se pase de la longitud del buffer" );
            escritor.WriteLine( "   pedidos[cantPedidosMemoria] = ret_value;" );
            escritor.WriteLine( "   tamanioPedidos[cantPedidosMemoria] = cantBytes;" );
            escritor.WriteLine( "   fueLiberado[cantPedidosMemoria] = false;" );
            escritor.WriteLine( "   for(i=0; i<8; i++){" );
            escritor.WriteLine( "       ((char*)ret_value)[i] = 'A';" );
            escritor.WriteLine( "       ((char*)ret_value)[cantBytes + 8 + i] = 'A';" );
            escritor.WriteLine( "   }" );
            escritor.WriteLine( "   cantPedidosMemoria++;" );
            escritor.WriteLine( "   return ret_value + 8;" );
            escritor.WriteLine( "}" );
            escritor.WriteLine();
            escritor.WriteLine( "int free2(char* punteroABloque)" );
            escritor.WriteLine( "{" );
            escritor.WriteLine( "   int pos, i;" );
            escritor.WriteLine( "   int salida = todoBien;" );
            escritor.WriteLine( "   for(pos=0; pos<cantPedidosMemoria && pedidos[pos]!=punteroABloque-8; pos++);" );
            escritor.WriteLine( "       if(pedidos[pos] !=punteroABloque-8)" );
            escritor.WriteLine(
                "           salida = liberarPosMemNoValida;//printf(\"Se intento liberar una posicion de memoria no valida\");" );
            escritor.WriteLine( "       else{" );
            escritor.WriteLine( "           fueLiberado[pos] = true;" );
            escritor.WriteLine( "           for (i = 0; i < 8; i++)" );
            escritor.WriteLine(
                "               if(((char*)punteroABloque-8)[i] != 'A'|| ((char*)punteroABloque)[tamanioPedidos[pos] + i] != 'A'){" );
            escritor.WriteLine(
                "                   salida = escrituraFueraDelBuffer;//printf(\"Se ha escrito fuera del buffer\");" );
            escritor.WriteLine( "                   break;" );
            escritor.WriteLine( "               }" );
            escritor.WriteLine( "       }" );
            escritor.WriteLine( "       return salida;" );
            escritor.WriteLine( "}" );
            escritor.WriteLine();
            escritor.WriteLine( "void free2all(){" );
            escritor.WriteLine( "   int i;" );
            escritor.WriteLine( "   int bytesNoLiberados = 0;" );
            escritor.WriteLine( "   for(i=0; i<cantPedidosMemoria; i++){" );
            escritor.WriteLine( "       free(pedidos[i]);" );
            escritor.WriteLine( "       if(fueLiberado[i]== false)" );
            escritor.WriteLine( "           bytesNoLiberados = bytesNoLiberados + tamanioPedidos[i];" );
            escritor.WriteLine( "   }" );
            escritor.WriteLine( "   if(bytesNoLiberados >0)" );
            escritor.WriteLine( "       printf(\"No se han liberado %d bytes de memoria\", bytesNoLiberados);" );
            escritor.WriteLine( "}" );
            escritor.WriteLine();
            escritor.IdentacionActiva = true;
        }
        private void EscribirFuncionesDePrueba( EscritorC escritor )
        {
            pruebaActual = 0;
            foreach( Prueba prueba in Pruebas )
            {
                Mensajes.NombreDePrueba = prueba.Nombre;
                EscribirFuncionPrueba( escritor );
                pruebaActual++;
            }
        }
        private void EscribirFuncionPrueba( EscritorC escritor )
        {
            escritor.WriteLine( PruebaActual.Prototipo );
            escritor.AbrirCorchetes();
            escritor.WriteLine( "//------------Variables comunes------------------" );
            escritor.WriteLine( "int salidaFree2;" );
            escritor.WriteLine( "long long tiempoDeEjecucion=0;" );
            escritor.WriteLine( "//------------Parametros-------------------------" );
            PruebaActual.DeclararParametros( escritor );
            escritor.WriteLine( "int cantErrores = 0;" );
            escritor.WriteLine( "//------------Pedir memoria----------------------" );
            PruebaActual.PedirMemoria( escritor );
            escritor.WriteLine( "//------------Instanciacion----------------------" );
            PruebaActual.InstanciarParametros( escritor );
            escritor.WriteLine( "//------------LlamadaFuncion---------------------" );
            escritor.WriteLine( "tiempoDeEjecucion = timer();" );
            LlamarFuncionAProbar( escritor );
            escritor.WriteLine( "tiempoDeEjecucion = timer() - tiempoDeEjecucion;" );
            /*
             * long long tiempo = 0;
             * long long tiempoDeEjecucion = 0;
             * int cantCorridas = 100;
             * while( tiempoDeEjecucion < 10000000 )
             * {
                    tiempoDeEjecucion = 0;
                    for( int i =0; i<cantCorridas; i++ )
             *      {
             *          //Pido memoria, instancio
             *          tiempo = timer();
             *          LlamadaFuncion; 
             *          tiempoDeEjecucion += timer() - tiempo;                          
             *          //Libero memoria
             *      }             
             *      cantCorridas *=10;
             * }
             * *    tiempoDeEjecucion = tiempoDeEjecucion / hasta;
             *      archivoSalida. cout>> tiempoDeEjecucion, //
             * 
             *      
             */
            //foreach (Parametro param in PruebaActual.ParametrosEntrada)
            //{
            //    param.TamanioOValorParaMedicion( escritor );
            //    escritor.Write( "\t" );
            //}
            //*fs;
            //fs = fopen("nombreArch", "wb");
            //fwrite("buffer", 1, bytesLeidos, fs);
            //fclose(fs);

            escritor.WriteLine( "//------------Comparacion de valores-------------" );
            PruebaActual.CompararValoresDevueltos( escritor );
            escritor.WriteLine( "//------------Liberar memoria--------------------" );
            PruebaActual.LiberarMemoria( escritor );
            //Libera la memoria que pidió y verifica que no se haya escrito fuera del buffer.
            escritor.WriteLine( "//------------Informar cant. de errores----------" );
            escritor.PrintfPruebaConcluida();
            escritor.WriteLine( "return cantErrores;" );
            escritor.CerrarCorchetes();
        }
        private void LlamarFuncionAProbar( EscritorC escritor )
        {
            string llamada = "";
            if( Definicion.DefParametroSalida!=null )
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
        private void EscribirMain(EscritorC escritor)
        {
            escritor.WriteLine("int main()");
            escritor.AbrirCorchetes();
            escritor.WriteLine("/*------------Parametros-------------------------*/");
            escritor.WriteLine("int cantErrores = 0;");
            escritor.WriteLine("/*------------Llamada a pruebas------------------*/");
            foreach (Prueba prueba in Pruebas)
            {
                if (FrenarEnElPrimerError)
                    escritor.If("cantErrores == 0");
                escritor.WriteLine("cantErrores = " + prueba.Nombre + "();");
                if (FrenarEnElPrimerError)
                    escritor.FinIf();
            }
            escritor.WriteLine("return 0;");
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
        public Generador( string archivoDefinicion, string archivoPrueba, string sistema )
        {
            this.archivoDefinicion = archivoDefinicion;
            this.archivoPrueba = archivoPrueba;
            pruebaActual = 0;
            switch( sistema.ToUpper().Trim() )
            {
                case "DOS":
                    SistemaOperativo = TipoSistema.DOS;
                    break;
                case "LINUX":
                    SistemaOperativo = TipoSistema.LINUX;
                    break;
            }
        }
        #endregion
    }
}