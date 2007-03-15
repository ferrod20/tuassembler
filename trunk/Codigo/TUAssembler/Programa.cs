using System;
using System.IO;
using TUAssembler.Compilacion;
using TUAssembler.Generacion;

namespace TUAssembler
{
    internal class Programa
    {
        #region Opciones de entrada
        private static string archDefinicion;
        private static string archJuegoDePruebas;
        private static bool esAssembler;
        private static string archFuncion;
        private static bool frenarEnLaPrimerError;
        private static bool salidaPorArchivo;
        private static string archSalida;
        private static bool contarCantInstrucciones;
        private static bool EsModoLinux;
        private static string archCantInst;
        #endregion

        private static void Main( string[] args )
        {
            //Iniciar( "Prueba1/archDef.xml", "Prueba1/archPrueba.jdp", "Prueba1/funcionAsm.asm" ); //Prueba la devolucion de un UInt8
            //Iniciar("Prueba2/archDef.xml", "Prueba1/archPrueba.jdp", "Prueba2/funcionAsm.asm"); //Prueba la devolucion de un UInt16
            //Iniciar("Prueba3/archDef.xml", "Prueba3/archPrueba.jdp", "Prueba3/funcionAsm.asm"); //Prueba la devolucion de un UInt32
            //Iniciar("Prueba4/archDef.xml", "Prueba4/archPrueba.jdp", "Prueba4/funcionAsm.asm"); //Prueba la devolucion de un UInt64
            //Iniciar("Prueba5/archDef.xml", "Prueba5/archPrueba.jdp", "Prueba5/funcionAsm.asm"); //Prueba la funcion  UInt64 funcion1( UInt8 E, UInt16 ES, UInt32 S );
            //Iniciar( "Prueba6/archDef.xml", "Prueba6/archPrueba.jdp", "Prueba6/funcionAsm.asm", "DOS");
            //Iniciar( "Prueba7/archDef.xml", "Prueba7/archPrueba.jdp", "Prueba7/funcionAsm.asm", "DOS" );
            //Prueba la funcion  UInt64 funcion1( Vector ES );            
            //            Iniciar("Prueba8/archDef.xml", "Prueba8/archPrueba.jdp", "Prueba8/funcionAsm.asm");//Prueba pasarle una matriz a una funcion.                                   //Prueba pasarle una matriz a una funcion.
            //Iniciar("Circular/archDef.xml", "Circular/archPrueba.jdp", "Circular/funcionAsm.asm", "DOS");
            string[] argumentos = new string[] { "Circular/archDef.xml", "Circular/archPrueba.jdp", "-asm", "Circular/funcionAsm.asm", "-dos" };
            try{
                LeerOpciones(argumentos);
                //LeerOpciones( args );
                Iniciar();
            }catch( Exception e ){
                Console.Write( e.Message );
            }
        }
        private static void LeerOpciones( string[] args )
        {
            if( args.Length < 4 )
                throw new Exception( Mensajes.CantidadDeParametrosIncorrectos );
            archDefinicion = args[0];
            archJuegoDePruebas = args[1];
            esAssembler = args[2]=="-asm";
            archFuncion = args[3];

            for( int i = 4; i < args.Length; i++ )
                switch( args[i].ToLower() )
                {
                    case "-fpe":
                        frenarEnLaPrimerError = true;
                        break;
                    case "-as":
                        salidaPorArchivo = true;
                        i++;
                        archSalida = args[i];
                        break;
                    case "-cci":
                        contarCantInstrucciones = true;
                        i++;
                        archCantInst = args[i];
                        break;
                    case "-linux":
                        EsModoLinux = true;
                        break;
                    case "-dos":
                        EsModoLinux = false;
                        break;
                    default:
                        throw new Exception( Mensajes.OpcionIncorrecta( args[i] ) );
                }
        }
        public static void Iniciar()
        {
            Generador generador = new Generador( archDefinicion, archJuegoDePruebas, EsModoLinux);
            generador.FrenarEnElPrimerError = frenarEnLaPrimerError;
            generador.ContarCantInstrucciones = contarCantInstrucciones;
            generador.ArchivoCuentaInstrucciones = archCantInst;
            generador.LeerDefinicion(); //Lee y verifica que el archivo definicion sea valido.
            generador.LeerPruebas();
            generador.GenerarPruebas();
            if( esAssembler )
                generador.GenerarTimer( archFuncion );
            if(!EsModoLinux )   //En caso de usarse bajo Linux, debe usarse el MakeFile destinado a tal efecto.
                CompilarYEjecutar();

        }
        public static void CompilarYEjecutar()
        {
            CompiladorAsm compiladorAsm;
            CompiladorC compiladorC;
            Compilador compilador;

            compiladorAsm = new CompiladorAsm("", "nasm.exe");
            compiladorC = new CompiladorC("", "gcc.exe");
            compilador = new Compilador("", "gcc.exe", "salida.txt", "error.txt");

            try
            {
                Ejecutor.ArchivoSalida = salidaPorArchivo ? archSalida : "salida.txt";
                Ejecutor.ArchivoError = "errorEjecucion.txt";

                if (esAssembler)
                    compiladorAsm.Compilar("-fcoff", "timer.asm");

                compiladorC.Compilar("-c -o codigoProbador.o", "codigoProbador.c");

                string[] archivos = new string[2];
                archivos[1] = "codigoProbador.o";
                if (esAssembler)
                    archivos[0] = "timer.o";
                else
                    archivos[0] = archFuncion;

                //Genera un .exe resultado de enlazar los 2 anteriores.                
                compilador.Enlazar("prueba.exe", archivos);

                Ejecutor.Ejecutar("prueba.exe");
                Console.Write(Ejecutor.ObtenerSalida());
            }
            finally
            {
                if (esAssembler)
                    compiladorAsm.BorrarArchivosSalidaYError();
                compiladorC.BorrarArchivosSalidaYError();
                compilador.BorrarArchivosSalidaYError();
                Ejecutor.BorrarArchivosTemporales(!salidaPorArchivo);
                File.Delete("codigoProbador.o");
                File.Delete("timer.o");
            }

        }
    }
}