using System;

namespace TUAssembler
{
    internal class Programa
    {
        private static void Main( string[] args )
        {
            Compilar();
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
                compiladorC = new CompiladorC( "", "gcc.exe" );
                compiladorC.Compilar("-c -o C:\\F2\\Orga2\\funcionC.o", "C:\\F2\\Orga2\\funcionC.c");

                //gcc C:/F2/Orga2/main.o C:/F2/Orga2/tpbmp.o -o C:/F2/Orga2/bmpmnsj
                //Genera un .exe resultado de enlazar los 2 anteriores.
                enlazador = new Enlazador( "", "gcc.exe" );
                string[] archivos = new string[2];
                archivos[1] = "C:\\F2\\Orga2\\funcionC.o";
                archivos[0] = "C:\\F2\\Orga2\\funcionAsm.o";
                enlazador.Enlazar( "C:\\F2\\Orga2\\e.exe", archivos );

                Ejecutor.Ejecutar("C:\\F2\\Orga2\\e.exe");
            }
            catch( Exception e )
            {
                salida = e.Message;
            }
        }
    }
}