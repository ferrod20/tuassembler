using System;
using System.IO;
using System.Xml.Serialization;

namespace TUAssembler
{
    internal class Programa
    {
        private static void Main(string[] args)
        {
            DefinicionFuncion def = DefinicionFuncion.Leer("archivito.xml");
            string s = def.GenerarPrototipo();
            //EscribirXml();
        }

        private static void EscribirXml()
        {
            XmlSerializer xml;
            DefinicionFuncion defFuncion = new DefinicionFuncion();
            defFuncion.CrearInstanciaDePrueba();
            string archivo = "archivito.xml";            

            try
            {                
                TextWriter escritor = new StreamWriter(archivo);
                
                xml = new XmlSerializer(typeof (DefinicionFuncion));
                xml.Serialize(escritor, defFuncion);
            }
            catch (Exception e)
            {
                archivo = e.Message;
            }
        }


        public static void Compilar()
        {
            string salida;
            CompiladorAsm compiladorAsm;
            CompiladorC compiladorC;
            Enlazador enlazador;

            try
            {
                //Genera el .o del assembler
                //nasm -f elf -o tpbmp.o tpbmp.asm -Dsistema=$(miSistema)                                
                compiladorAsm = new CompiladorAsm("C:\\F2\\Orga2", "nasm.exe");
                compiladorAsm.Compilar("-fcoff", "C:\\F2\\Orga2\\funcionAsm.asm");

                //Genera el .o del C
                compiladorC = new CompiladorC("", "gcc.exe");
                compiladorC.Compilar("-c -o C:\\F2\\Orga2\\funcionC.o", "C:\\F2\\Orga2\\funcionC.c");

                //gcc C:/F2/Orga2/main.o C:/F2/Orga2/tpbmp.o -o C:/F2/Orga2/bmpmnsj
                //Genera un .exe resultado de enlazar los 2 anteriores.
                enlazador = new Enlazador("", "gcc.exe");
                string[] archivos = new string[2];
                archivos[1] = "C:\\F2\\Orga2\\funcionC.o";
                archivos[0] = "C:\\F2\\Orga2\\funcionAsm.o";
                enlazador.Enlazar("C:\\F2\\Orga2\\e.exe", archivos);

                Ejecutor.Ejecutar("C:\\F2\\Orga2\\e.exe");
            }
            catch (Exception e)
            {
                salida = e.Message;
            }
        }
    }
}