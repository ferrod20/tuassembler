﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
    internal class Comparador
    {
        #region Variables
        private static Dictionary<string, List<string>> traducciónTreebankABNC = new Dictionary<string, List<string>>
                                                                                     {
                                                                                          {"JJ", new List<string>{"AJ0", "ORD"} },
{"JJR", new List<string>{"AJC"} },
{"JJS", new List<string>{"AJS"} },
{"DT", new List<string>{"AT0", "DT0"} },
{"RB", new List<string>{"AV0", "XX0"} }, 
{"RP", new List<string>{"AVP"} }, 
{"WRB", new List<string>{"AVQ"} }, 
{"CC", new List<string>{"CJC"} }, 
{"IN", new List<string>{"AV0", "AVP", "CJS", "CJT", "PRF", "PRP"} }, 
{"CD", new List<string>{"CRD"} }, 
{"PRP$", new List<string>{"DPS", "PNI"} }, 
{"WDT", new List<string>{"DTQ"} },   
{"EX", new List<string>{"EX0"} },   
{"UH", new List<string>{"ITJ"} },     
{"NN", new List<string>{"NN0", "NN1", "PNI"} },     
{"NNS", new List<string>{"NN2"} },      
{"NNP", new List<string>{"NP0"} },        
{"NNPS", new List<string>{"NP0"} },        
{"PRP", new List<string>{"PNX", "PNP"} },        
{"WP", new List<string>{"PNQ", "DTQ"} },        
{"POS", new List<string>{"POS"} },        
{"TO", new List<string>{"TO0", "PRP"} },        
{"VBP", new List<string>{"VVB","VBB", "VDB", "VHB"} },        
{"VBD", new List<string>{"VBD", "VDD", "VHD", "VVD"} },        
{"VBG", new List<string>{"VBG", "VDG", "VHG", "VVG"} },        
{"VB", new List<string>{"VBI", "VDI", "VHI", "VVB", "VVI"} },        
{"VBN", new List<string>{"VBN", "VHN", "VDN", "VVN"} },        
{"VBZ", new List<string>{"VBZ", "VDZ", "VHZ", "VVZ"} },        
{"MD", new List<string>{"VM0"} },        
{"FW", new List<string>{"UNC"} },        
{"LS", new List<string>{"ORD"} },        
{"PDT", new List<string>{"DT0"} },        
{"RBR", new List<string>{"AV0"} },        
{"RBS", new List<string>{"AV0"} },        
{"SYM", new List<string>{"ZZ0"} },        
{"WP$", new List<string>{"PNQ"} },        
 

{"'", new List<string>{"PUQ"} },        
{"''", new List<string>{"PUQ"} },        
{"\"", new List<string>{"PUQ"} },        
{"(", new List<string>{"PUL"} },        
{"[", new List<string>{"PUL"} },        
{")", new List<string>{"PUR"} },        
{"]", new List<string>{"PUR"} },        
{",", new List<string>{"PUN"} },        
{".", new List<string>{"PUN"} },        
{"!", new List<string>{"PUN"} },        
{"?", new List<string>{"PUN"} },        
{":", new List<string>{"PUN"} },        
{";", new List<string>{"PUN"} },        
{"-", new List<string>{"PUN"} },
{"``", new List<string>{"PUQ"} },
{"`", new List<string>{"PUQ"} }
};
        private MatrizEnProceso matrizEnProceso;
        private string archGoldStandard, archParaComparar, salidaMatrizDeConf, titulo, tituloFila, tituloColumna, archivoTabla;
        private bool generarMatrizDeConfusionParaLatex, compararContraBNC, hacerTabla;
                //compararContraBNC = false
                //hacerTabla = false
                //string titulo = ""
                //tituloFila = "", tituloColumna = "", archivoTabla = "", 
        #endregion

        #region Métodos
        private void BuscarProximaLinea(string[] uno, string[] otro, ref int i, ref int j)
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
        private int BuscarProximaLinea2(string palabra, string proxPalabra, string[] lineas, int lineaI)
        {
            var distancia = 0;
            while (lineaI + 2 < lineas.Length && lineas[lineaI].Split('\t')[0] != palabra && lineas[lineaI + 1].Split('\t')[0] != proxPalabra && distancia < 16)
            {
                lineaI++;
                distancia++;
            }
            return distancia;
        }

        private bool ComparaciónSimple(string tag1, string tag2)
        {
            return tag1 == tag2;
        }

        private bool CompararBNC_Treebank(string tagBNC, string tagTreebank)
        {
            var resultado = false;
            if (traducciónTreebankABNC.ContainsKey(tagTreebank))
            {
                var tagsBNC = tagBNC.Split('-');
                var tagsTraducidosABNC = traducciónTreebankABNC[tagTreebank];
                resultado = tagsTraducidosABNC.Intersect(tagsBNC).Any();    
            }
            return resultado;
        }

        private void Comparar(string palabra, string lineaGoldStandard, string lineaPrueba, Func<string, string, bool> compare)
        {
            var partesLineaDePrueba = lineaPrueba.TrimEnd().Split();
            var tagDePrueba = partesLineaDePrueba.Length > 1 ? partesLineaDePrueba.LastOrDefault() : "";

            var partesGoldStandard = lineaGoldStandard.TrimEnd().Split();
            var tagGoldStandard = partesGoldStandard.Length > 1 ? partesGoldStandard.LastOrDefault() : "";

            if (!string.IsNullOrEmpty(tagDePrueba) && !string.IsNullOrEmpty(tagGoldStandard))
            {
                matrizEnProceso.SumarEtiqueta();

                var acierto = compare(tagGoldStandard, tagDePrueba);
                
                if (!acierto)
                    matrizEnProceso.AgregarError(tagGoldStandard, tagDePrueba, palabra);
            }
        }
                
        
        /// <summary>
        ///   Lee los archivos y va generando la matriz de confusión.
        /// </summary>
        private MatrizDeConfusión GenerarMatrizDeConfusión(string archivoGoldStandard, string archivoParaComparar, bool compararContraBNC = false)
        {
            matrizEnProceso = new MatrizEnProceso();
            int i = 0, j = 0, punto = 1;
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
                {
                    if(compararContraBNC)
                        Comparar(palabraGoldStandard, aGoldStandard[i], aParaComparar[j], CompararBNC_Treebank);
                    else
                        Comparar(palabraGoldStandard, aGoldStandard[i], aParaComparar[j], ComparaciónSimple);
                }                    

                BuscarNuevasPosicionesSiCorresponde(aGoldStandard, aParaComparar, tamGoldStandard, tamParaComparar, ref i, ref j);

                if (i > parte*punto)
                {
                    Console.Write(".");
                    punto++;
                }
            }

            return matrizEnProceso.FinalizarProceso();
        }

        private void BuscarNuevasPosicionesSiCorresponde(string[] aGoldStandard, string[] aParaComparar, int tamGoldStandard, int tamParaComparar, ref int i, ref int j)
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

        public void EstablecerOpciones(bool generarMatrizDeConfusionParaLatex, string titulo = "", string tituloFila = "", string tituloColumna = "", string archivoTabla = "", bool compararContraBnc = false, bool hacerTabla = false)
        {
            this.titulo = titulo;
            this.tituloFila = tituloFila;
            this.tituloColumna = tituloColumna;
            this.archivoTabla = archivoTabla;
            this.generarMatrizDeConfusionParaLatex = generarMatrizDeConfusionParaLatex;
            compararContraBNC = compararContraBnc;
            this.hacerTabla = hacerTabla;
        }
        public Comparador(string archGoldStandard, string archParaComparar, string salidaMatrizDeConf)
        {
            this.archGoldStandard = archGoldStandard;
            this.archParaComparar = archParaComparar;
            this.salidaMatrizDeConf = salidaMatrizDeConf;
        }
        public void GenerarTablaDeEtiquetas()
        {
            var matrizDeConfusión = GenerarMatrizDeConfusión(archGoldStandard, archParaComparar);
            var escritor = new EscritorDeMatrizDeConfusión(salidaMatrizDeConf, matrizDeConfusión);
            escritor.EscribirTablaDeTraducciónDeEtiquetas();           
        }
        public void Comparar()
        {
            var matrizDeConfusión = GenerarMatrizDeConfusión(archGoldStandard, archParaComparar, compararContraBNC);
            var escritor = new EscritorDeMatrizDeConfusión(salidaMatrizDeConf, matrizDeConfusión);

            if (generarMatrizDeConfusionParaLatex)
                escritor.EscribirMatrizDeConfParaLatex(titulo, tituloFila, tituloColumna);
            else
                escritor.EscribirMatrizDeConfusión(titulo, tituloFila, tituloColumna);

            if (hacerTabla && !string.IsNullOrEmpty(archivoTabla))
            {
                TextWriter salida = new StreamWriter(archivoTabla, true);
                salida.WriteLine(titulo + "\t\t&" + Math.Round(matrizDeConfusión.PorcentajeDeAciertos, 2));

                salida.Close();
            }
        }
    }
}