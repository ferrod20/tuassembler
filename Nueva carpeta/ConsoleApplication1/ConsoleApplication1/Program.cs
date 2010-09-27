//#define HacerLegible
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public static class Metodos
    {
        #region Metodos
        public static void AddIfNoExists<A, B>(this Dictionary<A, B> dic, A key, B value)
        {
            if (!dic.ContainsKey(key))
                dic.Add(key, value);
        }

        public static decimal CantidadDeOcurrencias(this string parte, string palabra)
        {
            var cantOc = 0;
            var ind = parte.IndexOf(palabra);

            while (ind != -1)
            {
                cantOc++;
                if (palabra.Length - 1 < ind + 1)
                    ind = -1;
                else
                    ind = parte.IndexOf(palabra, ind + 1);
            }
            return cantOc;
        }

        public static bool ContainsAny(this string str, params string[] pals)
        {
            return pals.Any(pal => str.ToLower() == pal.ToLower());
        }

        public static bool EsConsonante(this char c)
        {
            return char.IsLetter(c) && !c.EsVocal();
        }

        public static bool EsVocal(this char c)
        {
            return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' || c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U';
        }

        /// <summary>
        ///   Reemplaza el espacio con un \_/, tanto en la palabra como en la parte.
        ///   Solo para no tener problemas
        /// </summary>
        public static string ReemplazarEspacioPorStringMagico(this string parte, string palabra)
        {
            var palabra2 = palabra.ReemplazarEspacioPorStringMagico();
            return parte.Replace(palabra, palabra2);
        }

        public static string ReemplazarEspacioPorStringMagico(this string p)
        {
            return p.Replace(" ", @"\_/");
        }

        public static string ReemplazarStringMagicoPorEspacio(this string p)
        {
            return p.Replace(@"\_/", " ");
        }
        #endregion
    }

    public static class InferirTipos
    {
        #region Metodos
        public static bool EsJJR(this string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("er") || forma.ToLower().StartsWith("more") || forma.ToLower().StartsWith("less");
        }

        public static bool EsJJS(this string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("est") || forma.ToLower().StartsWith("most") || forma.ToLower().StartsWith("least");
        }

        public static bool EsNNS(this string palabra, string forma)
        {
            return palabra.Length < forma.Length && forma.ToLower().EndsWith("s");
        }

        public static bool EsRBR(this string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("er") || forma.ToLower().StartsWith("more") || forma.ToLower().StartsWith("less");
        }

        public static bool EsRBS(this string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("est") || forma.ToLower().StartsWith("most") || forma.ToLower().StartsWith("least");
        }

        public static bool EsVBD(this string palabra, List<string> palabrasAnteriores, string forma)
        {
            var ultima = "";
            var anteUltima = "";

            if (palabrasAnteriores.Count() > 0)
                ultima = palabrasAnteriores.Last();
            if (palabrasAnteriores.Count() > 1)
                anteUltima = palabrasAnteriores[palabrasAnteriores.Count - 2];

            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("ed") && !ultima.ContainsAny("have", "has", "had") && !anteUltima.ContainsAny("have", "has", "had");
        }

        public static bool EsVBDoVBN(this string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("ed");
        }

        public static bool EsVBG(this string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("ing");
        }

        public static bool EsVBN(this string palabra, List<string> palabrasAnteriores, string forma)
        {
            var ultima = "";
            var anteUltima = "";

            if (palabrasAnteriores.Count > 0)
                ultima = palabrasAnteriores.Last();
            if (palabrasAnteriores.Count > 1)
                anteUltima = palabrasAnteriores[palabrasAnteriores.Count - 2];

            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("ed") && (ultima.ContainsAny("have", "has", "had") || anteUltima.ContainsAny("have", "has", "had"));
        }

        public static bool EsVBP(this string palabra, List<string> palabrasAnteriores, string forma)
        {
            var ultima = "";
            if (palabrasAnteriores.Count > 0)
                ultima = palabrasAnteriores.Last();
            return palabra == forma && ultima.ToLower() != "to";
        }

        public static bool EsVBZ(this string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("s");
        }

        /// <summary>
        ///   A partir de una palabra de tipo JJ, inferir el tipo de la forma
        /// </summary>
        public static string InferirTipoJJ(this string palabra, string forma)
        {
            var tipoDeLaForma = string.Empty;
            if (palabra.EsJJR(forma))
                tipoDeLaForma = "JJR";
            else if (palabra.EsJJS(forma))
                tipoDeLaForma = "JJS";

            return tipoDeLaForma;
        }

        public static string InferirTipoNN(this string palabra, string forma)
        {
            var tipoDeLaForma = string.Empty;
            if (palabra.EsNNS(forma))
                tipoDeLaForma = "NNS";
            else
                tipoDeLaForma = "NN";

            return tipoDeLaForma;
        }

        public static string InferirTipoRB(this string palabra, string forma)
        {
            var tipoDeLaForma = string.Empty;
            if (palabra.EsRBR(forma))
                tipoDeLaForma = "RBR";
            else if (palabra.EsRBS(forma))
                tipoDeLaForma = "RBS";

            return tipoDeLaForma;
        }

        public static string InferirTipoVB(this string palabra, string forma)
        {
            var tipoDeLaForma = string.Empty;
            if (palabra.EsVBDoVBN(forma))
                tipoDeLaForma = "VBD|VBN";
            else if (palabra.EsVBG(forma))
                tipoDeLaForma = "VBG";
            else if (palabra.EsVBZ(forma))
                tipoDeLaForma = "VBZ";
            else
                tipoDeLaForma = "VB";

            return tipoDeLaForma;
        }
        #endregion
    }

    internal class Program
    {
        #region Constantes
        private const string textoOriginal1 = @"Datos\COBUILD1.txt";
        #endregion

        #region Variables de clase
        private static string archCobuild = @"Datos\Cobuild2.txt";
        private static string archExtraccion = @"Datos\ExtraccionDeDatos.txt";
        private static string archTaggeado = @"Datos\ArchTaggeado.txt";
        private static string matrizDeConf = @"Datos\MatrizDeConf.txt";
        #endregion

        #region Metodos
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

                //if (EsEjemplo(parte, palabra, palabrasAnteriores))
                //else if (EsTipo(parte, out tip))
                //  salida += parte + "\n";                                    
            }
            return salida;
        }

        private static void Main()
        {
            //PonerSaltosDeLinea();
            Extractor.ExtraerDatos(archCobuild, archExtraccion);
            //Comparador.Comparar(archExtraccion, archTaggeado, matrizDeConf);
        }

        private static void PonerSaltosDeLinea()
        {
            var texto = File.ReadAllText(textoOriginal1);

            TextWriter salida = new StreamWriter(archCobuild, false, Encoding.Default);

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