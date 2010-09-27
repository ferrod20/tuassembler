using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
    internal class Comparador
    {
        #region Variables de clase
        private static int cantTags;
        private static IDictionary<Tags, int> matrizDeConfusión;
        #endregion

        //private static readonly TextWriter textW = new StreamWriter(@"D:\Fer\Facultad\PLN\Tp final\Freeling\bin\compa.txt");

        #region Metodos
        private static void BuscarProximaLinea(string[] uno, string[] otro, ref int i, ref int j)
        {
            var iii = 0;
            var dist = 100;
            for (var ii = 0; ii < 5 && i + ii + 1 < uno.Length; ii++)
            {
                var distancia = BuscarProximaLinea2(uno[i + ii].Split(' ')[0], uno[i + ii + 1].Split(' ')[0], otro, j);

                if (distancia + ii < dist + iii)
                {
                    iii = ii;
                    dist = distancia;
                }
            }
            i = i + iii;
            j = j + dist;
        }

        private static int BuscarProximaLinea2(string palabra, string proxPalabra, string[] lineas, int lineaI)
        {
            var distancia = 0;
            while (lineaI + 2 < lineas.Length && lineas[lineaI].Split(' ')[0] != palabra && lineas[lineaI + 1].Split(' ')[0] != proxPalabra && distancia < 16)
            {
                lineaI++;
                distancia++;
            }
            return distancia;
        }

        private static void Comparar(string lineaGoldStandard, string lineaPrueba)
        {
            var partesGoldStandard = lineaGoldStandard.Split();
            var partesLineaDePrueba = lineaPrueba.Split();
            var tagDePrueba = partesLineaDePrueba.Length > 2 ? partesLineaDePrueba[2] : "";
            tagDePrueba = tagDePrueba.Substring(0, Math.Min(2, tagDePrueba.Length)).ToUpper();
            var tagGoldStandard = partesGoldStandard.Length > 1 ? partesGoldStandard[1] : "";
            tagGoldStandard = tagGoldStandard.Substring(0, Math.Min(2, tagGoldStandard.Length)).ToUpper();
            var acierto = tagDePrueba.StartsWith(tagGoldStandard);

            if (!acierto)
            {
                //textW.WriteLine(lineaGoldStandard);
                //textW.WriteLine(lineaPrueba);
                //textW.WriteLine();
                var tags = new Tags(tagGoldStandard, tagDePrueba);
                if (matrizDeConfusión.ContainsKey(tags))
                    matrizDeConfusión[tags]++;
                else
                    matrizDeConfusión.Add(tags, 1);
            }
        }

        public static void Comparar(string arch1, string arch2, string salidaMatrizDeConf)
        {
            matrizDeConfusión = new Dictionary<Tags, int>(EqualityComparer<Tags>.Default);

            LeerArchivosParaComparar(arch1, arch2);
            GrabarComparación(salidaMatrizDeConf);
        }

        private static void GrabarComparación(string archivoDeSalida)
        {
            TextWriter salida = new StreamWriter(archivoDeSalida);

            var cantidadDeErrores = matrizDeConfusión.Sum(s => s.Value);

            var porcentajeDeAciertos = (cantTags - cantidadDeErrores)/(double) cantTags*100;
            salida.WriteLine("Porcentaje de aciertos: " + porcentajeDeAciertos);

            var matrizOrdenada = matrizDeConfusión.OrderByDescending(s => s.Value);

            for (var i = 0; i < 5; i++)
            {
                var tags = matrizOrdenada.ElementAt(i);
                salida.WriteLine(tags.Key.TagGoldStandard + " " + tags.Key.TagDePrueba + " " + (tags.Value/(double) cantidadDeErrores)*100 + "%");
            }

            salida.Close();
        }

        /// <summary>
        ///   Lee los archivos y va generando la matriz de confusión.
        /// </summary>
        private static void LeerArchivosParaComparar(string archivoGoldStandard, string archivoDePrueba)
        {
            int i = 0, j = 0;
            cantTags = 0;
            TextReader goldStandard = new StreamReader(archivoGoldStandard);
            TextReader prueba = new StreamReader(archivoDePrueba);

            var aGoldStandard = goldStandard.ReadToEnd().Split('\n');
            var aPrueba = prueba.ReadToEnd().Split('\n');
            goldStandard.Close();
            prueba.Close();

            var w = 0;
            while (i < aGoldStandard.Length && j < aPrueba.Length)
            {
                if (aGoldStandard[i].Split(' ')[0] == aPrueba[j].Split(' ')[0])
                {
                    Comparar(aGoldStandard[i], aPrueba[j]);
                    cantTags++;
                    w = 0;
                }

                i++;
                j++;
                if (i < aGoldStandard.Length && j < aPrueba.Length && aGoldStandard[i].Split(' ')[0] != aPrueba[j].Split(' ')[0])
                    if (aGoldStandard[i].Split(' ')[0] == "del" && aPrueba[j].Split(' ')[0] == "de")
                    {
                        i++;
                        j += 2;
                    }
                    else
                    {
                        var jj = j;
                        var ii = i;
                        var jjj = j;
                        var iii = i;

                        BuscarProximaLinea(aGoldStandard, aPrueba, ref ii, ref jj);
                        BuscarProximaLinea(aPrueba, aGoldStandard, ref jjj, ref iii);

                        if (iii + jjj < ii + jj)
                        {
                            i = iii;
                            j = jjj;
                        }
                        else
                        {
                            i = ii;
                            j = jj;
                        }

                        w++;
                        if (w > 4)
                            w = w;
                    }
            }
        }
        #endregion
    }
}