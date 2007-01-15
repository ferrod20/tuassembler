using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace TUAssembler
{
    internal class Programa
    {
        private static void Main(string[] args)
        {
            CompilarCodigo();
        }

        private static void CompilarCodigo()
        {
            string salida, error;

            salida = string.Empty;
            error = string.Empty;

            DirectoryInfo d = new DirectoryInfo("C:\\Codigo\\nasm");
            string directorio = d.FullName;

            TempFileCollection arch = new TempFileCollection();
            try
            {
                System.CodeDom.Compiler.CodeCompiler asd;                                
                
                Executor.ExecWaitWithCapture("nasmw.exe", directorio, arch, ref salida, ref error);
            }
            catch( Exception e)
            {
                salida = e.Message;
            }

            salida = salida + error;
        }
    }
}