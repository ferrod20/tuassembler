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
        private const string CompilerNotFound = "Compiler '{0}' was not found";

        private static void Main(string[] args)
        {
            CompilarCodigo();
        }
        /// <summary>
        ///		Call the compiler to compile generated source file
        /// </summary>
        /// <param name="options">Parameters to the CodeCompiler call. The TempFileCollection is used.</param>
        /// <param name="compilerDirectory">Where to find the compiler</param>
        /// <param name="compilerExe">The .exe name of the compiler to use</param>
        /// <param name="arguments">String of arguments to pass to compiler</param>
        /// <param name="outputFile">Where to put the compiler output, if null a file name is returned</param>
        /// <param name="nativeReturnValue">Return from calling the compiler</param>
        /// <param name="useResponse">Use a compiler response file</param>
        public static void Compile(CompilerParameters options, string compilerDirectory, string compilerExe,
                                   string arguments, ref string outputFile, ref int nativeReturnValue, bool useResponse)
        {
            TempFileCollection tfc = options.TempFiles;

            if (useResponse) arguments = GetResponseFileCmdArgs(tfc, arguments);

            string errorFile = null;
            if (outputFile == null) outputFile = tfc.AddExtension("out");

            // Try to execute the compiler with a full path name.
            string fullname = compilerDirectory + compilerExe;
            if (File.Exists(fullname))
            {
                nativeReturnValue =
                    Executor.ExecWaitWithCapture(options.UserToken, "\"" + fullname + "\" " + arguments, tfc,
                                                 ref outputFile, ref errorFile);
            }
            else
            {
                throw new InvalidOperationException(string.Format(CompilerNotFound, fullname));
            }
        }

        /// <summary>
        ///		Write compiler arguments to a response file and return new argument string for compiler
        /// </summary>
        /// <param name="tfc">The response file will be created in here</param>
        /// <param name="cmdArgs">Original compiler argument string</param>
        /// <returns>Replacement compiler argument string referencing the response file</returns>
        private static string GetResponseFileCmdArgs(TempFileCollection tfc, string cmdArgs)
        {
            string responseFileName = tfc.AddExtension("cmdline");

            Stream temp = new FileStream(responseFileName, FileMode.Create, FileAccess.Write, FileShare.Read);
            try
            {
                StreamWriter sw = new StreamWriter(temp, Encoding.UTF8);
                sw.Write(cmdArgs);
                sw.Flush();
                sw.Close();
            }
            finally
            {
                temp.Close();
            }

            return "/noconfig " + "@\"" + responseFileName + "\"";
        }

        private static void CompilarCodigo()
        {
            string salida, error;
            int sali = 0;
            salida = string.Empty;
            error = string.Empty;

            DirectoryInfo d = new DirectoryInfo("C:\\Codigo\\nasm");
            string directorio = d.FullName;

            TempFileCollection arch = new TempFileCollection();
            salida = Path.Combine(d.FullName , "salida.txt");
            CompilerParameters params2 = new CompilerParameters();
            try
            {
                Compile( params2, directorio, "\\nasmw.exe", "nasmw -felf funcionAsm.asm", ref salida, ref sali, true );
                
                //Executor.ExecWaitWithCapture("nasmw.exe", directorio, arch, ref salida, ref error);
            }
            catch( Exception e)
            {
                salida = e.Message;
            }

            salida = salida + error;
        }
    }
}