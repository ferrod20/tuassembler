using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public static class StringExtensions
    {
        #region Métodos
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
        /// Devuelve la Palabra sin puntuación ("tramo" en este caso) y la puntuación final "." o "..."
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
                else
                {                    
                    var últimoChar = palabra.Last();
                    if (char.IsPunctuation(últimoChar))
                    {
                        var posiblePalabraSinPuntuación = palabra.Substring(0, palabra.Length - 1);
                        if (!posiblePalabraSinPuntuación.EsAlgunaDeEstas("Mr", "Mrs", "St", "Eds") && !posiblePalabraSinPuntuación.Contains("."))
                        {
                            palabraSinPuntuación = posiblePalabraSinPuntuación;
                            puntuaciónFinal = últimoChar.ToString();    
                        }
                    }
                }
            }

            return palabraSinPuntuación;
        }        
        #endregion
    }
}