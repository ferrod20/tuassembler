using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
    internal class Extractor
    {
        #region Constantes
        private const string comienzoDeBloque = "DICTIONARY_ENTRY";
        private const string finDeBloque = "\nDI";
        #endregion

        #region Variables de clase
        private static readonly Dictionary<string, string> tiposs = new Dictionary<string, string> {{"coordinating conjunction", "CC"}, {"number", "CD"}, {"determiner", "DT"}, {"determiner + countable noun in singular", "DT"}, {"preposition", "IN"}, {"subordinating conjunction", "IN"}, {"preposition, or adverb after verb", "IN"}, {"preposition after noun", "IN"}, {"adjective", "JJ"}, //ver como clasificar JJR y JJS
                                                                                                    {"classifying adjective", "JJ"}, {"qualitative adjective", "JJ"}, {"adjective colour", "JJ"}, {"ordinal", "JJ"}, {"adjective after noun", "JJ"}, {"modal", "MD"}, {"adverb", "RB"}, {"noun", "NN"}, //ver q hacer con plurales....
                                                                                                    {"countable noun", "NN"}, {"uncountable noun", "NN"}, {"noun singular", "NN"}, {"countable or uncountable noun", "NN"}, {"countable noun with supporter", "NN"}, {"uncountable or countable noun", "NN"}, {"noun singular with determiner", "NN"}, {"mass noun", "NN"}, {"uncountable noun with supporter", "NN"}, {"partitive noun", "NN"}, {"noun singular with determiner with supporter", "NN"}, {"countable noun + of", "NN"}, {"countable noun, or by + noun", "NN"}, {"countable noun or partitive noun", "NN"}, {"count or uncountable noun", "NN"}, {"countable noun or vocative", "NN"}, {"partitive noun + uncountable noun", "NN"}, {"noun singular with determiner + of", "NN"}, {"noun in titles", "NN"}, //Verificar
                                                                                                    {"noun vocative", "NN"}, {"uncountable noun + of", "NN"}, {"indefinite pronoun", "NN"}, {"uncountable noun, or noun singular", "NN"}, {"countable noun, or in + noun", "NN"}, {"partitive noun + noun in plural", "NN"}, //Ver
                                                                                                    {"countable or uncountable noun with supporter", "NN"}, {"uncountable noun, or noun before noun", "NN"}, {"uncountable or countable noun with supporter", "NN"}, {"noun before noun", "NN"}, {"noun plural with supporter", "NNP"}, {"noun in names", "NNP"}, //Verificar
                                                                                                    {"proper noun or vocative", "NNP"}, {"proper noun", "NNP"}, {"noun plural", "NNS"}, {"predeterminer", "PDT"}, //si empieza con predeterminer....

                                                                                                    {"pronoun", "PP"}, //Ver.....me parece q no va
                                                                                                    {"possessive", "PPS"}, //si empieza con possessive....

                                                                                                    {"adverb with verb", "RB"}, {"adverb after verb", "RB"}, {"sentence adverb", "RB"}, {"adverb + adjective or adverb", "RB"}, {"adverb + adjective", "RB"}, {"preposition or adverb", "RB"}, {"adverb after verb, or classifying adjective", "RB"}, {"adverb or sentence adverb", "RB"}, {"adverb with verb, or sentence adverb", "RB"}, {"exclamation", "UH"}, {"exclam", "UH"}, {"verb", "VB"}, {"verb + object", "VB"}, {"verb or verb + object", "VB"}, {"ergative verb", "VB"}, {"verb + adjunct", "VB"}, {"verb + object + adjunct", "VB"}, {"verb + object (noun group or reflexive)", "VB"}, {"verb + object or reporting clause", "VB"}, {"verb + object (reflexive)", "VB"}, {"verb + adjunct (^i{to^i})", "VB"}, {"verb + object, or phrasal verb", "VB"}, {"verb + to-infinitive", "VB"}, {"verb or verb + adjunct (^i{with)", "VB"}, {"verb + object, verb + object + object, or verb + object + adjunct (to)", "VB"}, {"ergative verb + adjunct", "VB"}, {"verb + object + adjunct (to)", "VB"}, {"verb + object, or verb + adjunct", "VB"}, {"verb + object + adjunct (with)", "VB"}, {"verb + adjunct (with)", "VB"}, {"verb + complement", "VB"}, {"verb + object, or verb", "VB"}, {"verb + object + to-infinitive", "VB"}, {"verb + reporting clause", "VB"}, {"verb or ergative verb", "VB"}, {"verb + adjunct (from)", "VB"}, {"verb + object, verb + object + object, or verb + object + adjunct (for)", "VB"}, {"wh: used as determiner", "WDT"}, {"wh: used as relative pronoun", "WP"}, {"wh: used as pronoun", "WP"}, {"wh: used as adverb", "WRB"}, {"phrase + noun group", ""}, {"convention", ""}, {"combining form", ""}, {"prefix", ""}, {"phrasal verb", ""}, {"other", ""}, {"phrase", ""}, {"suffix", ""}, {"wh", ""}, {"phrase after noun", ""}, {"phrase + reporting clause", ""}};
        #endregion

        #region Metodos
        private static string Desambiguar(string forma, string[] palabrasDelEjemplo, int i, string tipo)
        {
            if (tipo.ContainsAny("VBD|VBN", "VB"))
            {
                var palabra = palabrasDelEjemplo[i];
                var palAnteriores = new List<string>();
                for (var j = 0; j < i; j++)
                    palAnteriores.Add(palabrasDelEjemplo[j]);

                if (tipo == "VBD|VBN")
                    tipo = palabra.EsVBN(palAnteriores, forma) ? "VBN" : "VBD";
                if (tipo == "VB" && palabra.EsVBP(palAnteriores, forma))
                    tipo = "VBP";
            }
            return tipo;
        }

        private static bool EsEjemplo(string parte, IEnumerable<string> palabras)
        {
            return palabras.Any(p => EsEjemplo(parte, p));
        }

        private static bool EsEjemplo(string parte, string palabra)
        {
            var cantPalabras = parte.Split().Count();
            return parte.Contains(palabra) && parte.Length > palabra.Length + 2 && cantPalabras > 4 && parte.Sum(letra => letra == ',' ? 1 : 0) <= 3 && cantPalabras > parte.CantidadDeOcurrencias(palabra);
        }

        private static bool EsTipo(string tipo, out KeyValuePair<string, string> parDeTipos)
        {
            parDeTipos = new KeyValuePair<string, string>("", "");
            tipo = tipo.TrimEnd();
            var tipos2 = tiposs.Where(t => t.Key.StartsWith(tipo));
            if (tiposs.ContainsKey(tipo))
                parDeTipos = new KeyValuePair<string, string>(tipo, tiposs[tipo]);
            else if (tipos2.Count() > 0)
                parDeTipos = tipos2.First();

            return tipos2.Count() > 0;
        }

        private static void ExtraerDatos(string texto, TextWriter tw)
        {
            var indice = 0;
            string bloque, datos = string.Empty;

            while (indice != -1)
            {
                bloque = ObtenerBloque(texto, ref indice);
                if (bloque != string.Empty)
#if HacerLegible
                    	datos = HacerLegiblElBloque(bloque);
#else
                    datos = ExtraerDatosDelBloque(bloque);
                if (!string.IsNullOrEmpty(datos))
                    tw.Write(datos);
#endif
            }
        }

        public static void ExtraerDatos(string archCobuild, string archSalida)
        {
            var texto = File.ReadAllText(archCobuild);
            TextWriter tw = new StreamWriter(archSalida);
            ExtraerDatos(texto, tw);
            tw.Close();
        }

        private static string ExtraerDatosDelBloque(string bloque)
        {
            var partes = bloque.Split('\n');
            var palabra = partes[1].TrimEnd();
            if (palabra.Contains(", "))
                palabra = palabra.Substring(0, palabra.IndexOf(", "));
            var salida = "";
            if (partes.Length > 2)
            {
                var formasDeLaPalabra = partes[2].Split(',').Select(forma => forma.Trim()).ToList();
                formasDeLaPalabra.Add(palabra);
                var escribiLaPalabra = false;
                string deDondeSeSacoElTipo;

                for (var i = 2; i < partes.Length - 1; i++)
                {
                    var parte = partes[i].TrimEnd();

                    if (EsEjemplo(parte, formasDeLaPalabra))
                    {
                        var tipo = ObtenerTipo(partes, i + 1, out deDondeSeSacoElTipo).Value;
                        if (tipo != string.Empty)
                        {
                            if (!escribiLaPalabra)
                                //  salida = palabra + " | " + deDondeSeSacoElTipo.TrimEnd() + "-->" + tipo + "\n";
                                escribiLaPalabra = true;

                            var palabrasConTipo = InferirTipoFormasDeLaPalabra(formasDeLaPalabra, palabra, tipo);
                            palabrasConTipo.AddIfNoExists(palabra.ToLower(), tipo);
                            salida += GetSalida(parte, palabrasConTipo);
                        }
                    }
                }
                //salida += "------------------------";
            }

            return salida;
        }

        /// <summary>
        ///   Obtiene una lista con cada palabra del ejemplo. En la palabra del ejemplo que es la entrada del diccionario, le pone el tipo.
        ///   Tiene en cuenta signos de puntuacion al final y al principio.
        /// </summary>
        private static string GetSalida(string ejemplo, Dictionary<string, string> palabrasConTipo)
        {
            var salida = string.Empty;

            foreach (var palConTipo in palabrasConTipo)
                if (palConTipo.Key.Contains(' '))
                {
                    var pal2 = palConTipo.Key;
                    ejemplo = ejemplo.ReemplazarEspacioPorStringMagico(pal2);
                }

            var palabrasDelEjemplo = ejemplo.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < palabrasDelEjemplo.Length; i++)
            {
                var hayPuntuacion = false;
                var palabraDelEjemplo = palabrasDelEjemplo[i].ReemplazarStringMagicoPorEspacio();
                string puntuacionFinal, parteOriginalSinPuntuacion;

                if (palabraDelEjemplo.StartsWith("..."))
                {
                    palabraDelEjemplo = palabraDelEjemplo.Substring(3);
                    salida += "...\n";
                }

                if (palabraDelEjemplo.EndsWith("..."))
                {
                    puntuacionFinal = "...";
                    parteOriginalSinPuntuacion = palabraDelEjemplo.Substring(0, palabraDelEjemplo.Length - 3);
                    hayPuntuacion = true;
                }
                else
                {
                    puntuacionFinal = (palabraDelEjemplo == string.Empty ? ' ' : palabraDelEjemplo.Last()).ToString();
                    if (char.IsPunctuation(puntuacionFinal[0]))
                    {
                        hayPuntuacion = true;
                        parteOriginalSinPuntuacion = palabraDelEjemplo.Substring(0, palabraDelEjemplo.Length - 1);
                    }
                    else
                        parteOriginalSinPuntuacion = palabraDelEjemplo;
                }

                salida += parteOriginalSinPuntuacion;

                var parteSinPunt = parteOriginalSinPuntuacion.ToLower();

                if (palabrasConTipo.ContainsKey(parteSinPunt))
                    salida += "\t" + Desambiguar(parteSinPunt, palabrasDelEjemplo, i, palabrasConTipo[parteSinPunt]);

                salida += "\n";

                if (hayPuntuacion)
                    salida += puntuacionFinal + "\n";
            }
            return salida;
        }

        private static Dictionary<string, string> InferirTipoFormasDeLaPalabra(IEnumerable<string> formasDeLaPalabra, string palabra, string tipo)
        {
            var formasConTipos = new Dictionary<string, string>();
            var tipoDeLaForma = string.Empty;
            foreach (var forma in formasDeLaPalabra)
            {
                switch (tipo)
                {
                    case "JJ":
                        tipoDeLaForma = palabra.InferirTipoJJ(forma);
                        break;
                    case "VB":
                        tipoDeLaForma = palabra.InferirTipoVB(forma);
                        break;
                    case "RB":
                        tipoDeLaForma = palabra.InferirTipoRB(forma);
                        break;
                    case "NN":
                        tipoDeLaForma = palabra.InferirTipoNN(forma);
                        break;
                }
                if (tipoDeLaForma != string.Empty)
                    formasConTipos.AddIfNoExists(forma.ToLower(), tipoDeLaForma);
            }
            return formasConTipos;
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
        #endregion
    }
}