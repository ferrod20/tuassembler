using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
    public static class StringHelper
    {
        #region Métodos
        //public static string SubstringEntre(this string str, char a, char b)
        //{
        //    var inicio = str.IndexOf(a);
        //    var fin = str.IndexOf(b, inicio + 1);
        //    var hasta = fin - (inicio + 1);
        //    return str.Substring(inicio + 1, hasta);
        //}
        //public static string SubstringEntre(this string str, string a, string b)
        //{
        //    var inicio = str.IndexOf(a);
        //    var fin = str.IndexOf(b, inicio + 1);
        //    var hasta = fin - (inicio + a.Length);
        //    return str.Substring(inicio + a.Length, hasta);
        //}
        //public static bool EsConsonante(this char c)
        //{
        //    return char.IsLetter(c) && !c.EsVocal();
        //}

        //public static bool EsVocal(this char c)
        //{
        //    return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' || c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U';
        //}

        public static bool AgregarSiNoExiste<A, B>(this IDictionary<A, B> dic, A key, B value)
        {
            var exists = dic.ContainsKey(key);
            if (!exists)
                dic.Add(key, value);

            return exists;
        }

        public static IEnumerable<T> Unir<T>(this IEnumerable<T> lista, T elemento)
        {
            return lista.Concat(new List<T> { elemento });
        }

        public static decimal CantidadDeOcurrencias(this string texto, string palabra)
        {
            var cantidadDeOcurrencias = 0;
            var índice = texto.IndexOf(palabra);

            while (índice != -1)
            {
                cantidadDeOcurrencias++;
                if (palabra.Length - 1 < índice + 1)
                    índice = -1;
                else
                    índice = texto.IndexOf(palabra, índice + 1);
            }
            return cantidadDeOcurrencias;
        }

        public static bool EsAlgunaDeEstas(this string str, params string[] pals)
        {
            return pals.Any(pal => str.ToLower() == pal.ToLower());
        }

        public static bool ContieneAlgunaDeEstas(this string str, params string[] pals)
        {
            return pals.Any(str.Contains);
        }

        public static bool EmpiezaConAlgunaDeEstas(this string str, params string[] pals)
        {
            return pals.Any(str.StartsWith);
        }

        /// <summary>
        /// Reconoce strings como: "tramo." y "tramo..."
        /// Devuelve la palabra sin puntuación ("tramo" en este caso) y la puntuación final "." o "..."
        /// </summary>
        public static string HayPuntuaciónFinal(this string palabra, out string puntuaciónFinal)
        {
            var palabraSinPuntuación = palabra;
            puntuaciónFinal = string.Empty;

            if (palabra != string.Empty)
            {
                if (palabra.EndsWith("..."))
                {
                    palabraSinPuntuación = palabra.Substring(0, palabra.Length - 3);
                    puntuaciónFinal = "...";
                }
                else if (char.IsPunctuation(palabra.Last()))
                {
                    palabraSinPuntuación = palabra.Substring(0, palabra.Length - 1);
                    puntuaciónFinal = palabra.Last().ToString();
                }
            }

            return palabraSinPuntuación;
        }        
        #endregion
    }

    public static class InferirTipos
    {
        #region Métodos
        public static bool EsVBP(this string palabra, List<string> palabrasAnteriores)
        {
            string ultima = "", anteUltima = "";
            if (palabrasAnteriores.Count > 0)
                ultima = palabrasAnteriores.Last();
            if (palabrasAnteriores.Count > 1)
                anteUltima = palabrasAnteriores[palabrasAnteriores.Count - 2];

            if (palabrasAnteriores.Count > 0)
                ultima = palabrasAnteriores.Last();

            return ultima.ToLower() != "to" && anteUltima.ToLower() != "to";
        }

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

        public static bool EsVBDoVBN(this string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("ed");
        }

        public static bool EsVBG(this string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("ing");
        }

        /// <summary>
        /// Dada una palabra etiquetada como VBD|VBN devuelve true si es VBN; esto es si alguna de las 2 palabras anteriores son have has o had
        /// </summary>
        public static bool EsVBN(this string palabra, List<string> palabrasAnteriores)
        {
            var ultima = "";
            var anteUltima = "";

            if (palabrasAnteriores.Count > 0)
                ultima = palabrasAnteriores.Last();
            if (palabrasAnteriores.Count > 1)
                anteUltima = palabrasAnteriores[palabrasAnteriores.Count - 2];

            return ultima.EsAlgunaDeEstas("have", "has", "had") || anteUltima.EsAlgunaDeEstas("have", "has", "had");
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
            var etiquetaInferida = string.Empty;
            if (palabra.EsJJR(forma))
                etiquetaInferida = "JJR";
            else if (palabra.EsJJS(forma))
                etiquetaInferida = "JJS";

            return etiquetaInferida;
        }

        public static string InferirTipoNN(this string palabra, string forma)
        {
            return palabra.EsNNS(forma) ? "NNS" : "NN";
        }

        public static string InferirTipoRB(this string palabra, string forma)
        {
            var etiquetaInferida = string.Empty;
            if (palabra.EsRBR(forma))
                etiquetaInferida = "RBR";
            else if (palabra.EsRBS(forma))
                etiquetaInferida = "RBS";

            return etiquetaInferida;
        }

        public static string InferirTipoVB(this string palabra, string forma)
        {
            var etiquetaInferida = string.Empty;
            if (palabra.EsVBDoVBN(forma))
                etiquetaInferida = "VBD|VBN";
            else if (palabra.EsVBG(forma))
                etiquetaInferida = "VBG";
            else if (palabra.EsVBZ(forma))
                etiquetaInferida = "VBZ";
            else
                etiquetaInferida = "VB";

            return etiquetaInferida;
        }
        #endregion
    }

    internal class Extractor
    {
        #region Constantes
        private const string comienzoDeBloque = "DICTIONARY_ENTRY";
        private const string finDeBloque = "\nDI";
        #endregion

        #region Variables de clase
        private static int CantidadDeSignosDePuntuación = 0;
        private static int CantidadDePalabras = 0;
        private static int CantidadDePalabrasEtiquetadas = 0;
        
        /// <summary>
        /// Tabla para traducir etiquetas COBUILD en etiquetas Penn TreeBank
        /// </summary>
        private static readonly Dictionary<string, string> tablaDeTraducciónDeEtiquetas = new Dictionary<string, string> {
        {"coordinating conjunction", "CC"}, 
        {"number", "CD"}, 
        {"determiner", "DT"}, 
        {"determiner + countable noun in singular", "DT"}, 
        {"preposition", "IN"}, 
        {"subordinating conjunction", "IN"}, 
        {"preposition, or adverb after verb", "IN"}, 
        {"preposition after noun", "IN"}, 
        {"adjective", "JJ"}, //ver como clasificar JJR y JJS
        {"classifying adjective", "JJ"}, 
        {"qualitative adjective", "JJ"}, 
        {"adjective colour", "JJ"}, 
        {"ordinal", "JJ"}, 
        {"adjective after noun", "JJ"}, 
        {"modal", "MD"}, 
        {"adverb", "RB"}, 
        {"noun", "NN"}, //ver q hacer con plurales....
        {"countable noun", "NN"}, 
        {"uncountable noun", "NN"}, 
        {"noun singular", "NN"}, 
        {"countable or uncountable noun", "NN"}, 
        {"countable noun with supporter", "NN"}, 
        {"uncountable or countable noun", "NN"}, 
        {"noun singular with determiner", "NN"}, 
        {"mass noun", "NN"}, 
        {"uncountable noun with supporter", "NN"}, 
        {"partitive noun", "NN"}, 
        {"noun singular with determiner with supporter", "NN"}, 
        {"countable noun + of", "NN"}, 
        {"countable noun, or by + noun", "NN"}, 
        {"countable noun or partitive noun", "NN"}, 
        {"count or uncountable noun", "NN"}, 
        {"countable noun or vocative", "NN"}, 
        {"partitive noun + uncountable noun", "NN"}, 
        {"noun singular with determiner + of", "NN"}, 
        {"noun in titles", "NN"}, //Verificar
        {"noun vocative", "NN"}, 
        {"uncountable noun + of", "NN"}, 
        {"indefinite pronoun", "NN"}, 
        {"uncountable noun, or noun singular", "NN"}, 
        {"countable noun, or in + noun", "NN"}, 
        {"partitive noun + noun in plural", "NN"}, //Ver
        {"countable or uncountable noun with supporter", "NN"}, 
        {"uncountable noun, or noun before noun", "NN"}, 
        {"uncountable or countable noun with supporter", "NN"}, 
        {"noun before noun", "NN"}, 
        {"noun plural with supporter", "NNS"}, 
        {"noun in names", "NNP"}, //Verificar
        {"proper noun or vocative", "NNP"}, 
        {"proper noun", "NNP"}, 
        {"noun plural", "NNS"}, 
        {"predeterminer", "PDT"}, //si empieza con predeterminer....
        {"pronoun", "PP"}, //Ver.....me parece q no va
        {"possessive", "PPS"}, //si empieza con possessive....
        {"adverb with verb", "RB"}, 
        {"adverb after verb", "RB"}, 
        {"sentence adverb", "RB"}, 
        {"adverb + adjective or adverb", "RB"}, 
        {"adverb + adjective", "RB"}, 
        {"preposition or adverb", "RB"}, 
        {"adverb after verb, or classifying adjective", "RB"}, 
        {"adverb or sentence adverb", "RB"}, 
        {"adverb with verb, or sentence adverb", "RB"}, 
        {"exclamation", "UH"}, 
        {"exclam", "UH"}, 
        {"verb", "VB"}, 
        {"verb + object", "VB"}, 
        {"verb or verb + object", "VB"}, 
        {"ergative verb", "VB"}, 
        {"verb + adjunct", "VB"}, 
        {"verb + object + adjunct", "VB"}, 
        {"verb + object (noun group or reflexive)", "VB"}, 
        {"verb + object or reporting clause", "VB"}, 
        {"verb + object (reflexive)", "VB"}, 
        {"verb + adjunct (^i{to^i})", "VB"}, 
        {"verb + object, or phrasal verb", "VB"}, 
        {"verb + to-infinitive", "VB"}, 
        {"verb or verb + adjunct (^i{with)", "VB"}, 
        {"verb + object, verb + object + object, or verb + object + adjunct (to)", "VB"}, 
        {"ergative verb + adjunct", "VB"}, 
        {"verb + object + adjunct (to)", "VB"}, 
        {"verb + object, or verb + adjunct", "VB"}, 
        {"verb + object + adjunct (with)", "VB"}, 
        {"verb + adjunct (with)", "VB"}, 
        {"verb + complement", "VB"}, 
        {"verb + object, or verb", "VB"}, 
        {"verb + object + to-infinitive", "VB"}, 
        {"verb + reporting clause", "VB"}, 
        {"verb or ergative verb", "VB"}, 
        {"verb + adjunct (from)", "VB"}, 
        {"verb + object, verb + object + object, or verb + object + adjunct (for)", "VB"}, 
        {"wh: used as determiner", "WDT"}, 
        {"wh: used as relative pronoun", "WP"}, 
        {"wh: used as pronoun", "WP"}, 
        {"wh: used as adverb", "WRB"}, 
        {"phrase + noun group", ""}, 
        {"convention", ""}, 
        {"combining form", ""}, 
        {"prefix", ""}, 
        {"phrasal verb", ""}, 
        {"other", ""}, 
        {"phrase", ""}, 
        {"suffix", ""}, 
        {"wh", ""}, 
        {"phrase after noun", ""}, 
        {"phrase + reporting clause", ""}};
        #endregion
        
        #region Métodos
        public static void ExtraerLaInformaciónDeCobuild(string archCobuild, string archSalida, string archDeInformación)
        {
            var texto = File.ReadAllText(archCobuild);
            TextWriter tw = new StreamWriter(archSalida);
        
            ExtraerLaInformaciónDeCobuild(texto, tw);
            tw.Close();
            
            var total = CantidadDePalabras + CantidadDeSignosDePuntuación;
            
            TextWriter info = new StreamWriter(archDeInformación);    
            info.WriteLine("Cantidad de palabras y signos puntuación: " + total);
            info.WriteLine("Cantidad de signos de puntuación: " + CantidadDeSignosDePuntuación);
            info.WriteLine("Cantidad de palabras: " + CantidadDePalabras);
            info.WriteLine("Cantidad de palabras etiquetadas: " + CantidadDePalabrasEtiquetadas + "\t( " + ((CantidadDePalabrasEtiquetadas * 100) / (double)CantidadDePalabras) + "% )");

            info.WriteLine();
            info.WriteLine();
            info.Close();
        }
        private static void ExtraerLaInformaciónDeCobuild(string texto, TextWriter tw)
        {
            var indice = 0;
            string entradaCobuild, informaciónExtraída = string.Empty;
            
            while (indice != -1)
            {
                entradaCobuild = ObtenerEntrada(texto, ref indice);
                if (entradaCobuild != string.Empty)
                    informaciónExtraída = ExtraerInformaciónDeLaEntrada(entradaCobuild);
                if (!string.IsNullOrEmpty(informaciónExtraída))
                    tw.Write(informaciónExtraída);
            }
        }
        /// <summary>
        /// Obtiene la entrada de COBUILD a partir del indice pasado por parametro
        /// Actualiza el indice apuntando a la próxima entrada
        /// </summary>
        private static string ObtenerEntrada(string texto, ref int indice)
        {
            var salida = string.Empty;
            var inicio = texto.IndexOf(comienzoDeBloque, indice);
            if (inicio != -1)
            {
                var fin = texto.IndexOf(finDeBloque, inicio);
                indice = fin;
                if (inicio != -1 && fin != -1)
                    salida = texto.Substring(inicio, fin - inicio + 1);
            }
            else
                indice = -1;

            return salida;
        }

        /// <summary>
        /// Dada una entrada de Cobuild, extrae la información almacenada (palabras y tags)
        /// </summary>
        public static string ExtraerInformaciónDeLaEntrada(string entrada)
        {
            var líneas = entrada.Split('\n');
            var salida = "";

            if (líneas.Length > 2)
            {
                var i = 2;

                var palabra = líneas[1].Split(',')[0].TrimEnd();//Pueden venir cosas del estilo: A, a
                var formasDeLaPalabra = líneas[2].Split(',', ';').Select(forma => forma.Trim()).ToList();//A veces no hay formas de la palabra en la entrada (ver entrada ABC)
                if (formasDeLaPalabra.Any(f => f.Split().Length > 2 || f.Contains('*') || f.Contains('!')))
                    formasDeLaPalabra.Clear();
                    
                var palabras = formasDeLaPalabra.Unir(palabra);

                for (; i < líneas.Length - 1; i++)
                {
                    var línea = líneas[i].Trim();

                    if (EsEjemploODefinición(línea, palabras))
                    {
                        var etiquetaPennTreebank = ObtenerEtiquetaPennTreebank(líneas, i + 1);//Busca en las próximas líneas la etiqueta Cobuild para la entrada. Es decir, una ejemplo que esté en la tabla de traducción de etiquetas. Luego traduce esa etiqueta Cobuild en la etiqueta Penn Treebak correspondiente.
                        if (!string.IsNullOrEmpty(etiquetaPennTreebank))
                        {
                            var etiquetasObtenidas = InferirEtiquetasParaLasFormasDeLaPalabra(formasDeLaPalabra, palabra, etiquetaPennTreebank);
                            etiquetasObtenidas.AgregarSiNoExiste(palabra.ToLower(), etiquetaPennTreebank);
                            salida += GenerarSalidaEtiquetada(línea, etiquetasObtenidas);
                        }
                    }
                }
            }

            return salida;
        }

        /// <summary>
        /// Indica si la ejemplo es un ejemplo o definición de Cobuild;
        /// "used for the", "is used in the present tense", "Someone or something that is", "If something is", "If you are" son parte de la expliación del uso de la palabras
        /// '/' es utilizado en las pronunciaciones
        /// </summary>
        private static bool EsEjemploODefinición(string línea, IEnumerable<string> formasDeLaPalabra)
        {
            var esEjemploODefinición =
                !línea.EmpiezaConAlgunaDeEstas("Someone or something that is","If something is","If you are") &&
                !línea.ContieneAlgunaDeEstas("is used in the present tense", "used for the" , "/", "*") &&                 
                línea.Sum(letra => letra == ',' || letra == ';' ? 1 : 0) <= 3;
            return esEjemploODefinición && formasDeLaPalabra.Any(p => EsEjemploODefinición(línea, p));
        }

        /// <summary>
        /// Indica si la ejemplo es un ejemplo o definición de Cobuild;
        /// si la linea contiene a la palabra 
        /// y no contiene al caracter '/' o '*' (los caracteres '/' y '*' se encuentran en la explicación de la pronunciación)
        /// y tiene al menos 2 letras más que la palabra
        /// y tiene al menos 4 palabras
        /// y tiene como máximo 3 comas
        /// y tiene al menos una palabra que no es la 'palabra'
        /// y no tiene un espacio ' ' (no es una palabra compuesta)
        /// </summary>
        private static bool EsEjemploODefinición(string línea, string palabra)
        {
            var cantPalabras = línea.Split().Count();
            return !palabra.Contains(' ') && línea.Contains(palabra) && línea.Length > palabra.Length + 2 && cantPalabras > 4 && cantPalabras > línea.CantidadDeOcurrencias(palabra);
        }
        
        /// <summary>
        ///   Obtiene una lista con cada palabra del ejemplo. En la palabra o formas de palabra para las que se obtuvieron etiquetas, le asigna la etiqueta correspondiente.
        ///   Tiene en cuenta signos de puntuacion al final y al principio.
        /// </summary>
        private static string GenerarSalidaEtiquetada(string ejemplo, Dictionary<string, string> etiquetas)
        {
            var salida = string.Empty;
            var palabrasDelEjemplo = ejemplo.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var palabraDelEjemplo in palabrasDelEjemplo)
            {
                var palabra = palabraDelEjemplo;

                if (palabra.StartsWith("..."))
                {
                    palabra = palabra.Substring(3);
                    salida += "...\n";
                    CantidadDeSignosDePuntuación++;
                }

                string puntuaciónFinal;
                var palabraSinPuntuación = palabra.HayPuntuaciónFinal(out puntuaciónFinal);

                if (palabraSinPuntuación != string.Empty )
                {
                    salida += palabraSinPuntuación;
                    CantidadDePalabras++;        

                    var palabraEnMinúscula = palabraSinPuntuación.ToLower();

                    if (etiquetas.ContainsKey(palabraEnMinúscula))
                    {
                        var etiquetaPennTreebank = etiquetas[palabraEnMinúscula];
                        //var etiquetaInferida = InferirEtiqueta(palabraSinPuntuación, etiquetaPennTreebank, palabrasDelEjemplo, i);
                        
                        salida += "\t" + etiquetaPennTreebank;
                        CantidadDePalabrasEtiquetadas++;
                    }
                    salida += "\n";
                }

                if (puntuaciónFinal != string.Empty)
                {
                    salida += puntuaciónFinal + "\n";
                    CantidadDeSignosDePuntuación++;
                }
            }
            return salida;
        }

        /// <summary>
        /// Si la etiqueta es 
        ///     VBD|VBN: desambigua a VBN si alguna de las 2 palabras anteriores es "has" "have" o "had"
        ///     VB:      devuelve VBP si alguna de las 2 palabras anteriores "to"
        /// si no, devuelve la etiquetaPennTreebank
        /// </summary>
        /// <param name="posición">posición de la palabra en el ejemplo</param>
        private static string InferirEtiqueta(string palabra, string etiquetaPennTreebank, string[] palabrasDelEjemplo, int posición)
        {
            if (etiquetaPennTreebank.EsAlgunaDeEstas("VBD|VBN", "VB"))
            {
                var palAnteriores = new List<string>();
                for (var j = 0; j < posición; j++)
                    palAnteriores.Add(palabrasDelEjemplo[j]);

                switch (etiquetaPennTreebank)
                {
                    case "VBD|VBN":
                        etiquetaPennTreebank = palabra.EsVBN(palAnteriores) ? "VBN" : "VBD";
                        break;
                    case "VB":
                        if (palabra.EsVBP(palAnteriores))
                            etiquetaPennTreebank = "VBP";
                        break;
                }
            }
            return etiquetaPennTreebank;
        }

        /// <summary>
        /// Para cada una de las formas de la palabra infiere su etiquetaPennTreebank basado en la palabra y su etiqueta PennTreebank
        /// </summary>
        private static Dictionary<string, string> InferirEtiquetasParaLasFormasDeLaPalabra(IEnumerable<string> formasDeLaPalabra, string palabra, string etiquetaPennTreebank)
        {
            var etiquetasInferidas = new Dictionary<string, string>();
            var etiquetaInferida = string.Empty;
            foreach (var forma in formasDeLaPalabra)
            {
                switch (etiquetaPennTreebank)
                {
                    case "JJ":
                        etiquetaInferida = palabra.InferirTipoJJ(forma);
                        break;
                    case "VB":
                        etiquetaInferida = palabra.InferirTipoVB(forma);
                        break;
                    case "RB":
                        etiquetaInferida = palabra.InferirTipoRB(forma);
                        break;
                    case "NN":
                        etiquetaInferida = palabra.InferirTipoNN(forma);
                        break;
                }
                if (etiquetaInferida != string.Empty && forma.ToLower() != palabra.ToLower())
                    etiquetasInferidas.AgregarSiNoExiste(forma.ToLower(), etiquetaInferida);
            }
            return etiquetasInferidas;
        }

        /// <summary>
        /// Busca en la entrada, (en cada renglón) si hay alguna palabra que sea un tag COBUILD. Es decir; 
        /// si está en la tabla de traducción de etiquetas, y si es así lo traduce con la etiqueta Penn Treebank correspondiente.
        /// </summary>
        private static string ObtenerEtiquetaPennTreebank(string[] líneas, int i)
        {
            string etiquetaPennTreebankObtenida = null;

            for (; i < líneas.Length && etiquetaPennTreebankObtenida == null; i++)
            {
                var línea = líneas[i];
                if(línea!= string.Empty)
                    etiquetaPennTreebankObtenida = ObtenerEtiquetaPennTreebank(línea);
            }

            return etiquetaPennTreebankObtenida;
        }
        /// <summary>
        /// Busca en la tabla de traducción de etiquetas si existe alguna etiqueta Penn Treebank para la etiqueta Cobuild posibleEtiquetaCobuild
        /// </summary>
        private static string ObtenerEtiquetaPennTreebank(string posibleEtiquetaCobuild)
        {
            string etiquetaPennTreebank = null;
            posibleEtiquetaCobuild = posibleEtiquetaCobuild.TrimEnd();
            var posiblesEtiquetasPennTreebank = tablaDeTraducciónDeEtiquetas.Where(t => t.Key.StartsWith(posibleEtiquetaCobuild));

            if (tablaDeTraducciónDeEtiquetas.ContainsKey(posibleEtiquetaCobuild))
                etiquetaPennTreebank = tablaDeTraducciónDeEtiquetas[posibleEtiquetaCobuild];
            else if (posiblesEtiquetasPennTreebank.Any())
                etiquetaPennTreebank = posiblesEtiquetasPennTreebank.First().Value;

            return etiquetaPennTreebank;
        }
        #endregion
    }
}