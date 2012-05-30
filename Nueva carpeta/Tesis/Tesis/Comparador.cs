using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
    internal class Comparador
    {
        #region Variables de clase
        private static MatrizDeConfusión matrizDeConfusión;
        #endregion
        #region Métodos
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
        private static void Comparar(string palabra, string lineaGoldStandard, string lineaPrueba)
        {
            var partesLineaDePrueba = lineaPrueba.TrimEnd().Split();
            var tagDePrueba = partesLineaDePrueba.Length > 1 ? partesLineaDePrueba.LastOrDefault() : "";
            
            if( !string.IsNullOrEmpty(tagDePrueba) )
            {
                matrizDeConfusión.CantidadDeEtiquetas++;
                var tagGoldStandard = lineaGoldStandard.TrimEnd().Split().LastOrDefault();
                var acierto = tagDePrueba == tagGoldStandard;
                
                if (!acierto)
                    matrizDeConfusión.AgregarError(tagGoldStandard, tagDePrueba, palabra);
            }
        }
        public static void Comparar(string archGoldStandard, string archParaComparar, string salidaMatrizDeConf, bool generarMatrizDeConfusionParaLatex, string titulo = "", string tituloFila = "", string tituloColumna = "")
        {
            matrizDeConfusión = new MatrizDeConfusión();
            GenerarMatrizDeConfusión(archGoldStandard, archParaComparar);

            if(generarMatrizDeConfusionParaLatex)
                matrizDeConfusión.EscribirMatrizDeConfParaLatex(salidaMatrizDeConf, titulo, tituloFila, tituloColumna);
            else
                matrizDeConfusión.EscribirMatrizDeConfusión(salidaMatrizDeConf, titulo, tituloFila, tituloColumna);
        }
        /// <summary>
        ///   Lee los archivos y va generando la matriz de confusión.
        /// </summary>
        private static void GenerarMatrizDeConfusión(string archivoGoldStandard, string archivoParaComparar)
        {
            int i,  j;
            var punto = 1;
            i = j = 0;
            matrizDeConfusión.CantidadDeEtiquetas = 0;
            var aGoldStandard = File.ReadAllText(archivoGoldStandard).Split('\n');
            var aParaComparar = File.ReadAllText(archivoParaComparar).Split('\n');
            var tamGoldStandard = aGoldStandard.Length;
            var tamParaComparar = aParaComparar.Length;
            var parte = tamGoldStandard/20;

            while (i < tamGoldStandard && j < aParaComparar.Length)
            {
                var palabraGoldStandard = aGoldStandard[i].Split('\t')[0].TrimEnd();
                var palabraAComparar = aParaComparar[j].Split('\t')[0].TrimEnd();
                if ( palabraGoldStandard == palabraAComparar )
                    Comparar(palabraGoldStandard, aGoldStandard[i], aParaComparar[j]);

                BuscarNuevasPosicionesSiCorresponde(aGoldStandard, aParaComparar, tamGoldStandard, tamParaComparar, ref i, ref j);

                if (i > parte*punto)
                {
                    Console.Write(".");
                    punto++;
                }
            }
        }
        private static void BuscarNuevasPosicionesSiCorresponde(string[] aGoldStandard, string[] aParaComparar, int tamGoldStandard, int tamParaComparar, ref int i, ref int j)
        {
            int ii, iii, jj, jjj;
            i++;
            j++;
            if (i < tamGoldStandard && j < tamParaComparar && aGoldStandard[i].Split('\t')[0].TrimEnd() != aParaComparar[j].Split('\t')[0].TrimEnd())
            {
                jj = jjj = j;
                ii = iii = i;
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
            }
        }
        #endregion
    }
}