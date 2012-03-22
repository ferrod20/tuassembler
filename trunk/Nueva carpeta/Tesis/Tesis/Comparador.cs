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
        private static List<Tags> matrizDeConfusión;
        #endregion
        #region StringHelper
        private static void BuscarProximaLinea(string[] uno, string[] otro, ref int i, ref int j)
        {
            var iii = 0;
            var dist = 100;
            for (var ii = 0; ii < 5 && i + ii + 1 < uno.Length; ii++)
            {
                var distancia = BuscarProximaLinea2(uno[i + ii].Split('\t')[0], uno[i + ii + 1].Split('\t')[0], otro, j);

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
            while (lineaI + 2 < lineas.Length && lineas[lineaI].Split('\t')[0] != palabra && lineas[lineaI + 1].Split('\t')[0] != proxPalabra && distancia < 16)
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
            var tagDePrueba =partesLineaDePrueba.Count() >1? partesLineaDePrueba.LastOrDefault():"";
             
            if( !string.IsNullOrEmpty(tagDePrueba) )
            {
                var hasta = partesGoldStandard.Length;
                var tagGoldStandard = "";
                while (string.IsNullOrWhiteSpace(tagGoldStandard) && hasta > 1)
                {
                    tagGoldStandard = partesGoldStandard[hasta - 1];
                    hasta--;
                }

                var acierto = tagDePrueba == tagGoldStandard || tagDePrueba == "VBD|VBN";
                cantTags++;
                if (!acierto)
                {
                    var palabra = string.Empty;
                    if( partesGoldStandard.Length > 0)
                        palabra = partesGoldStandard[0];
                    var tags = new Tags(tagGoldStandard, tagDePrueba, palabra);
                    if (matrizDeConfusión.Contains(tags))
                    {                        
                        var i = matrizDeConfusión.IndexOf(tags);
                        matrizDeConfusión[i].AgregarPalabra(palabra);
                    }
                    else
                        matrizDeConfusión.Add(tags);
                }    
            }
        }
        public static void Comparar(string archGoldStandard, string archParaComparar, string salidaMatrizDeConf, bool generarMatrizDeConfusionParaLatex)
        {
            //matrizDeConfusión = new Dictionary<Tags, int>(EqualityComparer<Tags>.Default);
            matrizDeConfusión = new List<Tags>();

            LeerArchivosParaComparar(archGoldStandard, archParaComparar);
            if(generarMatrizDeConfusionParaLatex)
                GenerarMatrizDeConfParaLatex(salidaMatrizDeConf);
            else
                GenerarMatrizDeConfusión(salidaMatrizDeConf);

            Console.WriteLine();
            Console.WriteLine();
        }
        private static void GenerarMatrizDeConfParaLatex(string archivoDeSalida)
        {
            TextWriter salida = new StreamWriter(archivoDeSalida);

            var cantidadDeErrores = matrizDeConfusión.Sum(s => s.TotalDePalabras);

            var porcentajeDeAciertos = (cantTags - cantidadDeErrores) / (double)cantTags * 100;
            salida.WriteLine("Porcentaje de aciertos: " + porcentajeDeAciertos);

            salida.WriteLine();
            salida.WriteLine("Errores");
            salida.WriteLine("TagWSJ\tTagAsignado\tPorcentajeDeError");

            var tags = matrizDeConfusión.OrderByDescending(s => s.TotalDePalabras).ObtenerTags().Take(10);

            salida.Write(
@"\begin{center}
\begin{longtable}{| l | ");
            for(var i=0;i < tags.Count(); i++)
                salida.Write("c | ");

salida.Write(
@"}
\caption{Ejemplo de matriz de confusión}\\	
\hline
 \backslashbox{\scriptsize{COBUILD}\kern-1em}{\kern-1em \scriptsize{WSJ}}  &	");

            foreach (var tag in tags)
                salida.Write("\\textbf{" + tag + "}	&   ");
salida.Write(@"\hline
\endhead
\hline
\endfoot
\endlastfoot
	\hline
");
            foreach (var tagCol in tags)
            {
                salida.Write(@"\textbf{" + tagCol + "}");
                foreach (var tagFila in tags)
                {
                    salida.Write(" & ");
                    var error = matrizDeConfusión.ObtenerCantidadDeErrores(tagCol, tagFila);//tag columna es tag de prueba, tagFila es tag gold standard
                    if (error == 0)
                        salida.Write("-");
                    else
                        salida.Write(error);
                    //{
                    //    var porcentaje = (error / (double)cantidadDeErrores);
                    //    salida.Write(porcentaje.ToString("0.0000").Replace("0,", "."));                        
                    //}
                }
                salida.WriteLine(@"\\");
            }
                

salida.Write(@"\hline
\end{longtable}
\end{center}");

            salida.Close();
        }
        private static void GenerarMatrizDeConfusión(string archivoDeSalida)
        {
            TextWriter salida = new StreamWriter(archivoDeSalida);

            var cantidadDeErrores = matrizDeConfusión.Sum(s => s.TotalDePalabras);
            var aciertos = cantTags - cantidadDeErrores;
            var porcentajeDeAciertos = aciertos / (double)cantTags * 100;
            salida.WriteLine("Aciertos: " + aciertos + " ( " + porcentajeDeAciertos  + "% )");
            salida.WriteLine("Errores: " + cantidadDeErrores);
            salida.WriteLine("Cantidad de tags: " + cantTags);

            salida.WriteLine();
            salida.WriteLine("Errores");
            salida.WriteLine("TagWSJ\tTagAsignado\tPorcentajeDeError");

            var matrizOrdenada = matrizDeConfusión.OrderByDescending(s => s.TotalDePalabras);

            foreach (var tags in matrizOrdenada)
            {
                var porcentaje = (tags.TotalDePalabras/(double) cantidadDeErrores)*100 + "%";
                salida.WriteLine(tags.TagGoldStandard + " " + tags.TagDePrueba + " " + tags.TotalDePalabras);
                foreach (var palabra in tags.Palabras.OrderByDescending(s=>s.Value).Take(40))
                    salida.WriteLine("\t" + palabra.Key + " " + palabra.Value);
            }            

            salida.Close();
        }
        /// <summary>
        ///   Lee los archivos y va generando la matriz de confusión.
        /// </summary>
        private static void LeerArchivosParaComparar(string archivoGoldStandard, string archivoParaComparar)
        {
            int i = 0, j = 0;
            cantTags = 0;
            
            var aGoldStandard = File.ReadAllText(archivoGoldStandard).Split('\n');
            var aParaComparar = File.ReadAllText(archivoParaComparar).Split('\n');
            
            var w = 0;
            var punto = 1;
            var tamGoldStandard = aGoldStandard.Length;
            var parte = tamGoldStandard/20;
            while (i < tamGoldStandard && j < aParaComparar.Length)
            {
                if (aGoldStandard[i].Split('\t')[0] == aParaComparar[j].Split('\t')[0])
                {
                    Comparar(aGoldStandard[i], aParaComparar[j]);
                    
                    w = 0;
                }
                i++;
                j++;
                if (i < tamGoldStandard && j < aParaComparar.Length && aGoldStandard[i].Split('\t')[0] != aParaComparar[j].Split('\t')[0])                    
                    {
                        var jj = j;
                        var ii = i;
                        var jjj = j;
                        var iii = i;
                        BuscarProximaLinea(aGoldStandard, aParaComparar, ref ii, ref jj);
                        BuscarProximaLinea(aParaComparar, aGoldStandard, ref jjj, ref iii);

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

                if (i > parte*punto)
                {
                    Console.Write(".");
                    punto++;
                }
            }
        }
        #endregion
    }
}