//#define HacerLegible
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    internal class Program
    {
        #region Variables de clase
        private static string comienzoDeBloque = "DICTIONARY_ENTRY";

        private static string finDeBloque = "\nDI";

        private static string textoOriginal1 = @"Datos\COBUILD.DAt";

        private static string textoOriginal2 = @"Datos\PruebaCobuild.3.txt";

        private static string textoOriginal3 = @"Datos\ExtraccionDeDatos.txt";

        private static List<string> tipos = new List<string> {"phrasal verb", "adverb", "other", "phrase", "verb", "adjective", "noun"};

        private static Dictionary<string, string> tiposs = new Dictionary<string, string>
                                                               {
                                                                   {"coordinating conjunction", "CC"}, 
{"number", "CD"}, 
{"determiner", "DT"}, 
{"determiner + countable noun in singular", "DT"}, 

{"preposition", "IN"},
{"subordinating conjunction", "IN"},
{"preposition, or adverb after verb", "IN"},
{"preposition after noun", "IN"},

{"adjective", "JJ"},//ver como clasificar JJR y JJS
{"classifying adjective", "JJ"},
{"qualitative adjective", "JJ"},
{"adjective colour", "JJ"},
{"ordinal", "JJ"},
{"adjective after noun", "JJ"},

{"modal", "MD"}, 

{"adverb", "RB"},

{"noun", "NN"},//ver q hacer con plurales....
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
{"partitive noun + noun in plural", "NN"},//Ver
{"countable or uncountable noun with supporter", "NN"},
{"uncountable noun, or noun before noun", "NN"},
{"uncountable or countable noun with supporter", "NN"},
{"noun before noun", "NN"},
{"noun plural with supporter", "NNP"},
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
{"phrase + reporting clause", ""}
                                                               };
        #endregion

        #region Métodos
        private static decimal CantidadDeOcurrencias(string parte, string palabra)
        {
            var cantOc = 0;
            var ind = parte.IndexOf(palabra);

            while (ind != -1)
            {
                cantOc++;
                ind = parte.IndexOf(palabra, ind + 1);
            }
            return cantOc;
        }

        private static string ConvertirAAscii(string texto)
        {
            var sb = new StringBuilder();

            foreach (var car in texto)
                if (1 < car && car < 127)
                    sb.Append(car);

            return sb.ToString();

            //var unicode = Encoding.BigEndianUnicode;            
            //var ascii = Encoding.ASCII;
            //var bytes = unicode.GetBytes(texto3);
            //var asciiBytes = Encoding.Convert(unicode, ascii, bytes);

            //var asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            //ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            //return new string(asciiChars);
        }

        private static bool EsEjemplo(string parte, List<string> palabras)
        {
            return palabras.Any(palabra => EsEjemplo(parte, palabra));
        }

        private static bool EsEjemplo(string parte, string palabra)
        {
            var cantPalabras = parte.Split().Count();
            return parte.Contains(palabra) && parte.Length > palabra.Length + 2 && cantPalabras > 4 && parte.Sum(letra => letra == ',' ? 1 : 0) <= 3 && cantPalabras > CantidadDeOcurrencias(parte, palabra);
        }

        private static bool EsTipo(string tipo, out KeyValuePair<string, string> parDeTipos)
        {
            parDeTipos = new KeyValuePair<string, string>("", "");
            var tipos2 = tiposs.Where(t => t.Key.StartsWith(tipo.TrimEnd()));
            if (tipos2.Count() > 0)
                parDeTipos = tipos2.First();

            return tipos.Any(tipo.StartsWith);
        }

        private static void ExtraerDatos(string texto, TextWriter salida)
        {
            var indice = 0;
            var bloque = string.Empty;
            string datos;
            while (indice != -1)
            {
                bloque = ObtenerBloque(texto, ref indice);
                if (bloque != string.Empty)
                {
#if HacerLegible
                    	datos = HacerLegiblElBloque(bloque);
#else
                    datos = ExtraerDatosDelBloque(bloque);
#endif
                    salida.WriteLine(datos);
                }
            }
        }

        private static void ExtraerDatos()
        {
            TextReader archivo = new StreamReader(textoOriginal2, Encoding.Default);
            TextWriter salida = new StreamWriter(textoOriginal3);

            var texto = archivo.ReadToEnd();
            archivo.Close();
            ExtraerDatos(texto, salida);
            salida.Close();
        }

        private static string ExtraerDatosDelBloque(string bloque)
        {
            var partes = bloque.Split('\n');
            var palabra = partes[1].TrimEnd();            
            var formasDeLaPalabra = partes[2].Split(',').Select(forma=>forma.TrimEnd());
            var palabras = new List<string>(formasDeLaPalabra);
            palabras.Add(palabra);
            var salida = "";
            string deDondeSeSacoElTipo;
            
            for (var i = 2; i < partes.Length - 1; i++)
            {
                var parte = partes[i].TrimEnd();
                if (EsEjemplo(parte, palabras))
                {
                    var tipo = ObtenerTipo(partes, i + 1, out deDondeSeSacoElTipo).Value;                    
                    if (tipo != string.Empty)
                    {
                        salida = palabra + " | " + deDondeSeSacoElTipo.TrimEnd() + "-->" + tipo + "\n";
                        Dictionary<string, string> palabrasConTipo = InferirTipoFormasDeLaPalabra(formasDeLaPalabra,palabra, tipo);
                        palabrasConTipo.Add(palabra, tipo);
                        salida += GetSalida(parte, palabra, tipo);
                    }                        
                }
            }

            return  salida + "------------------------";
        }
        private static Dictionary<string, string> InferirTipoFormasDeLaPalabra(IEnumerable<string> formasDeLaPalabra, string palabra, string tipo)
        {
            var formasConTipos = new Dictionary<string, string>();
            string tipoDeLaForma = string.Empty;
            foreach (var forma in formasDeLaPalabra)
            {
                switch (tipo)
                {
                    case "JJ":
                        tipoDeLaForma = InferirTipoJJ(palabra, forma);
                        break;
                    case "VB":
                        tipoDeLaForma = InferirTipoVB(palabra, forma);
                        break;
                    case "RB":
                        tipoDeLaForma = InferirTipoRB(palabra, forma);
                        break;

                }
                if (tipoDeLaForma != string.Empty)
                    formasConTipos.Add(forma, tipoDeLaForma);
            }                
            return formasConTipos;
        }
        private static string InferirTipoVB(string palabra, string forma)
        {
            var tipoDeLaForma = string.Empty;
            if (EsVBD(palabra, forma))
                tipoDeLaForma = "VBD|VBN";
            else if (EsVBG(palabra, forma))
                tipoDeLaForma = "VBG";
            else if (EsVBZ(palabra, forma))
                tipoDeLaForma = "VBZ";

            return tipoDeLaForma;
        }
        private static string InferirTipoRB(string palabra, string forma)
        {
            var tipoDeLaForma = string.Empty;
            if (EsRBR(palabra, forma))
                tipoDeLaForma = "RBR";
            else if (EsRBS(palabra, forma))
                tipoDeLaForma = "RBS";

            return tipoDeLaForma;
        }
        /// <summary>
        /// A partir de una palabra de tipo JJ, inferir el tipo de la forma
        /// </summary>
        private static string InferirTipoJJ(string palabra, string forma)
        {
            var tipoDeLaForma = string.Empty;
            if (EsJJR(palabra, forma))
                tipoDeLaForma = "JJR";
            else if (EsJJS(palabra, forma))
                tipoDeLaForma = "JJS";    

            return tipoDeLaForma;
        }
        private static bool EsJJS(string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("est") || forma.ToLower().StartsWith("most") || forma.ToLower().StartsWith("least");
        }

        private static bool EsRBS(string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("est") || forma.ToLower().StartsWith("most") || forma.ToLower().StartsWith("least");
        }

        private static bool EsJJR(string palabra, string forma)
        {                        
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("er") || forma.ToLower().StartsWith("more") || forma.ToLower().StartsWith("less");
        }

        private static bool EsRBR(string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("er") || forma.ToLower().StartsWith("more") || forma.ToLower().StartsWith("less");
        }

        private static bool EsVBD(string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("ed");            
        }

        private static bool EsVBG(string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("ing");
        }

        private static bool EsVBZ(string palabra, string forma)
        {
            return palabra.Length <= forma.Length && forma.ToLower().EndsWith("s");
        }

        private static bool EsVocal(char c)
        {
            return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' || c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U';
        }
        private static bool EsConsonante(char c)
        {
            return char.IsLetter(c) && !EsVocal(c);
        }
        /// <summary>
        /// Obtiene una lista con cada palabra del ejemplo. En la palabra del ejemplo que es la entrada del diccionario, le pone el tipo.
        /// Tiene en cuenta signos de puntuacion al final y al principio.
        /// </summary>
        private static string GetSalida(string ejemplo, string palabra, string tipo)
        {
            var salida = string.Empty;
            
            if (palabra.Contains(' '))
                ejemplo = ReemplazarEspacioPorStringMagico(ref palabra, ejemplo);

            var palabrasDelEjemplo = ejemplo.Split(' ');

            foreach (var palabraDelEjemplo in palabrasDelEjemplo)
            {
                string puntuacionFinal;
                var parteOrginial = ReemplazarStringMagicoPorEspacio(palabraDelEjemplo);
                
                string parteOriginalSinPuntuacion;
                var hayPuntuacion = false;
                if (parteOrginial.StartsWith("..."))
                {
                    parteOrginial = parteOrginial.Substring(3);
                    salida += "...\n";
                }

                if (parteOrginial.EndsWith("..."))
                {
                    puntuacionFinal = "...";
                    parteOriginalSinPuntuacion = parteOrginial.Substring(0, parteOrginial.Length - 3);
                    hayPuntuacion = true;
                }
                else
                {
                    puntuacionFinal = (parteOrginial == string.Empty ? ' ' : parteOrginial.Last()).ToString();
                    if (char.IsPunctuation(puntuacionFinal[0]))
                    {
                        hayPuntuacion = true;
                        parteOriginalSinPuntuacion = parteOrginial.Substring(0, parteOrginial.Length - 1);
                    }
                    else
                        parteOriginalSinPuntuacion = parteOrginial;
                }

                salida += parteOriginalSinPuntuacion;

                if (parteOriginalSinPuntuacion.ToLower() == palabra.ToLower())
                    salida += "\t" + tipo;

                salida += "\n";

                if (hayPuntuacion)
                    salida += puntuacionFinal + "\n";
            }
            return salida;
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

                if (EsEjemplo(parte, palabra))
                    salida += parte + "\n";
                else if (EsTipo(parte, out tip))
                    salida += parte + "\n";
            }
            return salida;
        }

        private static void Main(string[] args)
        {
            //PonerSaltosDeLinea();
            ExtraerDatos();
        }

        private static string ObtenerBloque(string texto, ref int indice)
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

        private static KeyValuePair<string, string> ObtenerTipo(string[] partes, int i, out string obtenido)
        {
            obtenido = string.Empty;
            var tipo = new KeyValuePair<string, string>();

            for (; i < partes.Length; i++)
            {
                obtenido = partes[i];
                if (EsTipo(obtenido, out tipo))
                    break;
            }

            return tipo;
        }

        private static void PonerSaltosDeLinea()
        {
            TextReader archivo = new StreamReader(textoOriginal1, Encoding.Default);
            var texto = archivo.ReadToEnd();
            archivo.Close();

            TextWriter salida = new StreamWriter(@"Datos\cosa.txt", false, Encoding.Default);

            PonerSaltosDeLinea(texto, salida);
            salida.Close();
        }

        private static void PonerSaltosDeLinea(string texto, TextWriter salida)
        {
            //var texto2 = texto.Replace("\0\0", Environment.NewLine).Replace("\0\0", Environment.NewLine);
            //var texto2 = texto.Replace("^b{", "").Replace("^b}", "").Replace("^i{", "").Replace("^i}", "");
            var texto2 = ConvertirAAscii(texto);
            salida.Write(texto2);
        }

        private static string ReemplazarStringMagicoPorEspacio(string p)
        {
            return p.Replace(@"\_/", " ");
        }

        /// <summary>
        /// Reemplaza el espacio con un \_/, tanto en la palabra como en la parte.
        /// Solo para no tener problemas
        /// </summary>
        private static string ReemplazarEspacioPorStringMagico(ref string palabra, string parte)
        {
            var palabra2 = palabra.Replace(" ", @"\_/");
            parte = parte.Replace(palabra, palabra2);
            palabra = palabra2;
            return parte;
        }
        #endregion
    }
}