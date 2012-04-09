//#define HacerLegible
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//-extraer "Datos/Extraccion/Cobuild.original.legible" "Datos/Extraccion/Cobuild.extracted" "Datos/Extraccion/Cobuild.extracted.info"
//-comparar "Datos/Extraccion/Cobuild.tagged" "Datos/Extraccion/Cobuild.extracted" "Datos/Extraccion/Cobuild.mconf"
namespace ConsoleApplication1
{    
    internal class Program
    {
        #region Constantes
        private const string cobuildOriginal = @"Datos\Extraccion\Cobuild.original";
        #endregion

        #region Variables de clase
        private static string cobuildOriginalLegible = @"Datos\Extraccion\Cobuild.original.legible";
        #endregion

        #region Métodos
        private static string ConvertirAAscii(string texto)
        {
            var sb = new StringBuilder();

            foreach (var car in texto)
            {
                if (0 < car && car < 255)
                    sb.Append(car);
                if (0 == car)
                    sb.Append(" ");
            }

            return sb.ToString();
        }

        private static string HacerLegiblElBloque(string bloque)
        {
            var partes = bloque.Split('\n');
            var palabra = partes[1].TrimEnd();
            var salida = palabra;
            KeyValuePair<string, string> tip;

            for (var i = 2; i < partes.Length - 1; i++)
            {
                var parte = partes[i].TrimEnd();
                salida += parte + "\n";

                //if (EsEjemplo(ejemplo, palabra, palabrasAnteriores))
                //else if (EsTipo(ejemplo, out tip))
                //  salida += ejemplo + "\n";                                    
            }
            return salida;
        }

        private static void Main(string[] args)
        {
            if(args.Length == 0)
                Console.Write("-help para obtener información de los comandos disponibles");
            else
            {
                var comando = args[0];
                Console.WriteLine();
                args = args.Select(a => a.Replace("\"", "")).ToArray();
                switch (comando)
                {
                    
                    case "-extraer":
                        Extraer(args);
                        break;
                    case "-2pasada":
                        SegundaPasada(args);
                        break;
                    case "-unir":
                        Unir(args);
                        break;
                    case "-comparar":
                        Comparar(args);
                        break;
                    case "-help":
                        Help();
                        break;
                    default:
                        Console.WriteLine("-help para obtener información de los comandos disponibles");
                        break;
                }
            }
        }

        private static void Help()
        {
            Console.WriteLine("-help: este comando de ayuda");
            Console.WriteLine();
            Console.WriteLine("-extraer <CobuildOriginal> <Salida> <SalidaInfo>");
            Console.WriteLine("\t Extrae la informacion gramatical de CobuildOriginal");
            Console.WriteLine("\t CobuildOriginal: nombre del archivo cobuild original");
            Console.WriteLine("\t Salida: nombre del archivo en donde se extraerá la información de cobuild");
            Console.WriteLine(
                "\t SalidaInfo: nombre del archivo en donde se guardará información del archivo extraido: cantidad de palabras, cantidad de etiquetas, etc.");
            Console.WriteLine();
            Console.WriteLine("-unir <CobuildExtraido> <CobuildEtiquetado> <Salida>");
            Console.WriteLine(
                "\t Une la información de etiquetas de cobuild con cobuild etiquetado. Manteniendo las etiquetas de cobuild.");
            Console.WriteLine("\t CobuildExtraido: nombre del archivo extraido de cobuild");
            Console.WriteLine("\t CobuildEtiquetado: nombre del archivo cobuild etiquetado");
            Console.WriteLine("\t Salida: nombre del archivo cobuild etiquetado");
            Console.WriteLine();
            Console.WriteLine("-comparar <GoldStandard> <ArchivoAComparar> <Salida> [-l]");
            Console.WriteLine("\t Compara el archivo GoldStandard contra el ArchivoAComparar generando una matriz de confusion en Salida.");
            Console.WriteLine("\t -l: genera una matriz de confusión para latex");
        }

        private static void Comparar(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("No se han definido los archivos <GoldStandard> <ArchivoParaComparar> y <Salida>");
                Console.WriteLine("-help para obtener información de los comandos disponibles");
            }
            else
            {
                Console.WriteLine("Comparando: " + Path.GetFileName(args[1]) + "(gold standard) contra " +
                                  Path.GetFileName(args[2]));
                Console.WriteLine("Salida: " + Path.GetFileName(args[3]));
                Console.WriteLine();
                var generarMatrizDeConfParaLatex = args.Length > 4 && args[3] == "-l";

                Comparador.Comparar(args[1], args[2], args[3], generarMatrizDeConfParaLatex);
            }
        }

        private static void SegundaPasada(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("No se han definido los archivos <CobuildExtraido> <CobuildEtiquetado> y <Salida>");
                Console.WriteLine("-help para obtener información de los comandos disponibles");
            }
            else
            {

                Console.WriteLine("Generando la 2 pasada: " + Path.GetFileName(args[1]) + " con " + Path.GetFileName(args[2]));
                Console.WriteLine("Salida: " + Path.GetFileName(args[3]));
                Console.WriteLine();

                UnirCobuildExtraidoConCobuildTaggeado(args[1], args[2], args[3], SegundaPasada);
            }
        }

        private static void Unir(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("No se han definido los archivos <CobuildExtraido> <CobuildEtiquetado> y <Salida>");
                Console.WriteLine("-help para obtener información de los comandos disponibles");
            }
            else
            {

                Console.WriteLine("Uniendo: " + Path.GetFileName(args[1]) + " con " + Path.GetFileName(args[2]));
                Console.WriteLine("Salida: " + Path.GetFileName(args[3]));
                Console.WriteLine();

                UnirCobuildExtraidoConCobuildTaggeado(args[1], args[2], args[3], Unir);
            }
        }

        private static void Extraer(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("No se han definido los archivos <CobuildOriginal> <CobuildExtraido> y <CobuildInfoExtraido>");
                Console.WriteLine("-help para obtener información de los comandos disponibles");
            }
            else
            {
                Console.WriteLine("Extrayendo: " + Path.GetFileName(args[1]));
                Console.WriteLine("Salida: " + Path.GetFileName(args[2]));
                Console.WriteLine("Info: " + Path.GetFileName(args[3]));
                Console.WriteLine();

                Extractor.ExtraerLaInformaciónDeCobuild(args[1], args[2], args[3]);
            }
        }

        private static void UnirCobuildExtraidoConCobuildTaggeado(string cobuildExtraido, string cobuildEtiquetado, string cobuildFinal, Func<TextWriter, string, string, string[], bool> Hacer)
        {
            var textoExtraido = new StreamReader(cobuildExtraido);
            var textoEtiquetado = new StreamReader(cobuildEtiquetado);

            TextWriter salida = new StreamWriter(cobuildFinal, false, Encoding.Default);

            var líneaExtraída = textoExtraido.ReadLine();
            var etiquetaEtiquetada = textoEtiquetado.ReadLine();

            while (líneaExtraída != null && etiquetaEtiquetada != null)
            {
                if( líneaExtraída != string.Empty )
                {
                    var partesExtraídas = líneaExtraída.Split();
                    salida.Write(partesExtraídas[0]);                    
                    var etiquetaExtraída = partesExtraídas.Last();
                    Hacer(salida, etiquetaExtraída, etiquetaEtiquetada, partesExtraídas);

                    salida.WriteLine();
                }

                líneaExtraída = textoExtraido.ReadLine();
                etiquetaEtiquetada = textoEtiquetado.ReadLine();    
            }

            salida.Close();
            textoExtraido.Close();
            textoEtiquetado.Close();
        }

        private static bool Unir(TextWriter salida, string etiquetaExtraída, string etiquetaEtiquetada, string[] partesExtraídas)
        {
            salida.Write("\t");
            if (partesExtraídas.Count() > 1 && !string.IsNullOrEmpty(etiquetaExtraída) && etiquetaExtraída != "VBD|VBN")
                salida.Write(etiquetaExtraída);
            else
                salida.Write(etiquetaEtiquetada.Split().Last());

            return true;
        }

        /// <summary>
        /// Si se obtuvo de Cobuild NN 	pero TnT asigno NNS, NNP o NNPS,  asignar tag TnT
        /// Si se obtuvo de Cobuild NNS pero TnT asigno NNPS, asignar NNPS
        /// Si se obtuvo de Cobuild VB  pero TnT asigno VBP,  asignar VBP 
        /// Si se obtuvo de Cobuild VB  pero TnT asigno VBD,  asignar VBD   
        /// </summary>
        private static bool SegundaPasada(TextWriter salida, string etiquetaExtraída, string etiquetaEtiquetada, string[] partesExtraídas)
        {
            var etiquetado = etiquetaEtiquetada.Split().Last();
            if (partesExtraídas.Count() > 1 && !string.IsNullOrEmpty(etiquetaExtraída) )
                switch (etiquetaExtraída)
                {
                    case "NN":
                        salida.Write("\t");
                        salida.Write(etiquetado.EsAlgunaDeEstas("NNS", "NNP", "NNPS") ? etiquetado : etiquetaExtraída);
                        break;
                    case "NNS":
                        salida.Write("\t");
                        salida.Write(etiquetado == "NNPS" ? etiquetado : etiquetaExtraída);
                        break;
                    case "VB":
                        salida.Write("\t"); 
                        salida.Write(etiquetado.EsAlgunaDeEstas("VBP", "VBD") ? etiquetado : etiquetaExtraída);                    
                        break;
                }

            return true;
        }

        private static void HacerLegibleCobuild()
        {
            var texto = File.ReadAllText(cobuildOriginal);

            TextWriter salida = new StreamWriter(cobuildOriginalLegible, false, Encoding.Default);

            PonerSaltosDeLinea(texto, salida);
            salida.Close();
        }

        private static void PonerSaltosDeLinea(string texto, TextWriter salida)
        {
            //An ostrich cannot fly...
            var texto2 = ConvertirAAscii(texto);
            salida.Write(texto2);
        }
        #endregion
    }
}