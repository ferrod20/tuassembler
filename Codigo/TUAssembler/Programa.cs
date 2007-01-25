using System;
using System.IO;
using System.Xml.Serialization;
using TUAssembler.Definicion;
using TUAssembler.Compilacion;
using TUAssembler.Generacion;

namespace TUAssembler
{
    internal class Programa
    {
        private static void Main( string[] args )
        {
            try
            {
                Generador generador = new Generador("archDef.xml", "archPrueba.jdp");
                generador.LeerDefinicion();
                generador.LeerPrueba();
            }
            catch( Exception e)
            {
                Console.Write(e.Message);    
            }            
        }

        private static void EscribirPruebaXml()
        {
            /*
            XmlSerializer xml;
            Prueba prueba = new Prueba();
            prueba.CrearInstanciaDePrueba();            
            string archivo = "archivoPrueba.xml";

            try
            {
                TextWriter escritor = new StreamWriter(archivo);

                xml = new XmlSerializer(typeof(Prueba));
                xml.Serialize(escritor, prueba);
            }
            catch (Exception e)
            {

                archivo = ExcepcionCompleta(e);
            }
             * */
        }
        private static string ExcepcionCompleta( Exception e )
        {
            string salida = string.Empty;
            while (e != null)
            {
                salida += e.Message;
                e = e.InnerException;
            }
            return salida;
        }
        private static void EscribirDefinicionFuncionXml()
        {
            XmlSerializer xml;
            DefinicionFuncion defFuncion = new DefinicionFuncion();
            defFuncion.CrearInstanciaDePrueba();
            string archivo = "archivito.xml";

            try
            {
                TextWriter escritor = new StreamWriter( archivo );

                xml = new XmlSerializer( typeof( DefinicionFuncion ) );
                xml.Serialize( escritor, defFuncion );
            }
            catch( Exception e )
            {
                archivo = e.Message;
            }
        }
        public static void Compilar()
        {
            string salida;
            CompiladorAsm compiladorAsm;
            CompiladorC compiladorC;
            Compilador compilador;

            try
            {
                //Genera el .o del assembler
                //nasm -f elf -o tpbmp.o tpbmp.asm -Dsistema=$(miSistema)                                
                compiladorAsm = new CompiladorAsm( "C:\\F2\\Orga2", "nasm.exe" );
                compiladorAsm.Compilar( "-fcoff", "C:\\F2\\Orga2\\funcionAsm.asm" );

                //Genera el .o del C
                compiladorC = new CompiladorC( "", "gcc.exe" );
                compiladorC.Compilar( "-c -o C:\\F2\\Orga2\\funcionC.o", "C:\\F2\\Orga2\\funcionC.c" );

                //gcc C:/F2/Orga2/main.o C:/F2/Orga2/tpbmp.o -o C:/F2/Orga2/bmpmnsj
                //Genera un .exe resultado de enlazar los 2 anteriores.
                compilador = new Compilador( "", "gcc.exe", "salida.txt", "error.txt" );
                
                string[] archivos = new string[2];
                archivos[1] = "C:\\F2\\Orga2\\funcionC.o";
                archivos[0] = "C:\\F2\\Orga2\\funcionAsm.o";
                compilador.Enlazar( "C:\\F2\\Orga2\\e.exe", archivos );

                Ejecutor.Ejecutar( "C:\\F2\\Orga2\\e.exe" );
            }
            catch( Exception e )
            {
                salida = e.Message;
            }
        }
    }
}