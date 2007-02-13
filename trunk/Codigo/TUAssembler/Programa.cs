using System;
using System.IO;
using TUAssembler.Compilacion;
using TUAssembler.Generacion;

namespace TUAssembler
{
    internal class Programa
    {
        private static void Main( string[] args )
        {
            //Iniciar( "Prueba1/archDef.xml", "Prueba1/archPrueba.jdp", "Prueba1/funcionAsm.asm" ); //Prueba la devolucion de un UInt8
            //Iniciar("Prueba2/archDef.xml", "Prueba1/archPrueba.jdp", "Prueba2/funcionAsm.asm"); //Prueba la devolucion de un UInt16
            //Iniciar("Prueba3/archDef.xml", "Prueba3/archPrueba.jdp", "Prueba3/funcionAsm.asm"); //Prueba la devolucion de un UInt32
            Iniciar("Prueba4/archDef.xml", "Prueba4/archPrueba.jdp", "Prueba4/funcionAsm.asm"); //Prueba la devolucion de un UInt64
            //Iniciar("archDef.xml", "archPrueba.jdp", "funcionAsm.asm");
        }
        public static void Iniciar(string archDef, string archPrueba, string funcionAsm)
        {
            try
            {
                StreamWriter escritor = new StreamWriter( "codigoProbador.c" );
                //Archivo que se generara para probar la funcion
                Generador generador = new Generador( archDef, archPrueba );
                // Toma las definiciones de la funcion y los resultados esperados
                generador.LeerDefinicion();
                generador.LeerPrueba();
                generador.GenerarPrueba( ref escritor );
                escritor.Close();
                CompilarYEjecutar( funcionAsm );
            }
            catch( Exception e )
            {
                Console.Write( e.Message );
            }
        }
        public static void CompilarYEjecutar( string funcionAsm )
        {
            CompiladorAsm compiladorAsm;
            CompiladorC compiladorC;
            Compilador compilador;
            //Genera el .o del assembler
            //nasm -f elf -o tpbmp.o tpbmp.asm -Dsistema=$(miSistema)
            compiladorAsm = new CompiladorAsm( "", "nasm.exe" );
            compiladorAsm.Compilar( "-fcoff", funcionAsm );

            compiladorC = new CompiladorC( "", "gcc.exe" );
            compiladorC.Compilar( "-c -o codigoProbador.o", "codigoProbador.c" );

            //gcc C:/F2/Orga2/main.o C:/F2/Orga2/tpbmp.o -o C:/F2/Orga2/bmpmnsj
            //Genera un .exe resultado de enlazar los 2 anteriores.
            compilador = new Compilador( "", "gcc.exe", "salida.txt", "error.txt" );

            string[] archivos = new string[2];
            archivos[1] = "codigoProbador.o";
            archivos[0] = Path.Combine(  Path.GetDirectoryName( funcionAsm),  "funcionAsm.o" );
            compilador.Enlazar( "prueba.exe", archivos );

            Ejecutor.Ejecutar( "prueba.exe" );
        }
        /*   private static void EscribirPruebaXml()
        {
            XmlSerializer xml;
            Prueba prueba = new Prueba();
            prueba.CrearInstanciaDePrueba();            
            string archivo = "archivoPrueba.xml";
            try{
                TextWriter escritor = new StreamWriter(archivo);

                xml = new XmlSerializer(typeof(Prueba));
                xml.Serialize(escritor, prueba);
            }catch (Exception e){

                archivo = ExcepcionCompleta(e);
            }
        }

 
        private static void EscribirDefinicionFuncionXml()
        {
            XmlSerializer xml;
            DefinicionFuncion defFuncion = new DefinicionFuncion();
            defFuncion.CrearInstanciaDePrueba();
            string archivo = "archivito.xml";

            try{
                TextWriter escritor = new StreamWriter( archivo );
                xml = new XmlSerializer( typeof( DefinicionFuncion ) );
                xml.Serialize( escritor, defFuncion );
            }catch( Exception e ){
                archivo = e.Message;
            }
        }
*/
    }
}