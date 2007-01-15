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
                compiladorAsm = new CompiladorAsm( "C:\\Dev-Cpp\\bin", "nasmw.exe" );
                compiladorAsm.Compilar( "-felf", "C:\\F2\\Orga2\\funcionAsm.asm" );

                //Genera el .o del C
                compiladorC = new CompiladorC( "C:\\Dev-Cpp\\bin", "gcc.exe" );
                compiladorC.Compilar( "-c -o C:\\F2\\Orga2\\funcionC.o", "C:\\F2\\Orga2\\funcionC.c" );

                //Genera un .exe resultado de enlazar los 2 anteriores.
                enlazador = new Enlazador( "C:\\Dev-Cpp\\bin", "gcc.exe" );
                string[] archivos = new string[2];
                archivos[1] = "C:\\F2\\Orga2\\funcionC.o";
                //  archivos[0] = "C:\\F2\\Orga2\\funcionAsm.o";
                enlazador.Enlazar( "C:\\F2\\Orga2\\e.exe", archivos );
            }
            catch( Exception e )
            {
                salida = e.Message;
            }
        }
    }
}