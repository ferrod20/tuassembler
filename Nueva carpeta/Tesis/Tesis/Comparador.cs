using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{    
    public interface ITablaDeTraduccion
    {
        List<string> ObtenerTraduccionPara(string tag2);
        bool ContieneTraduccionPara(string tag2);
        string ObtenerTraduccionInversaPara(string tagFila);
    }

    public class TablaDeTraducciónVacía: ITablaDeTraduccion
    {
        public List<string> ObtenerTraduccionPara(string tag)
        {
            return new List<string>{tag};
        }

        public bool ContieneTraduccionPara(string tag)
        {
            return true;
        }

        public string ObtenerTraduccionInversaPara(string tagFila)
        {
            return tagFila;
        }
    }

    public class TraducciónTreebankABNC: ITablaDeTraduccion
    {
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

        public List<string> ObtenerTraduccionPara(string tag)
        {
            return traducciónTreebankABNC[tag];
        }

        public bool ContieneTraduccionPara(string tag)
        {
            return traducciónTreebankABNC.ContainsKey(tag);
        }

        public string ObtenerTraduccionInversaPara(string tag)
        {
            var resultado = tag;
            var traducción = traducciónTreebankABNC.FirstOrDefault(trad => trad.Value.Contains(tag));

            if( !string.IsNullOrWhiteSpace(traducción.Key))
                resultado = traducción.Key;
            
            return resultado;
        }
    }
        

    public class Comparador
    {
        #region Variables        

        private ITablaDeTraduccion tablaDeTraduccion;
        private MatrizEnProceso matrizEnProceso;
        private string archGoldStandard, archParaComparar, salidaMatrizDeConf, titulo, tituloFila, tituloColumna, archivoTabla;
        private bool generarMatrizDeConfusionParaLatex, hacerTabla;        
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

        private bool CompararEtiquetas(string tag1 /*BNC*/, string tag2 /*Treebank*/)
        {
            var resultado = false;
                
            if (tablaDeTraduccion.GetType() != typeof(TablaDeTraducciónVacía))
            {
                if (tablaDeTraduccion.ContieneTraduccionPara(tag2))
                {
                    var tagsBNC = tag1.Split('-');
                    var tagsTraducidosABNC = tablaDeTraduccion.ObtenerTraduccionPara(tag2);
                    resultado = tagsTraducidosABNC.Intersect(tagsBNC).Any();
                }
            }
            else
                resultado = tag1 == tag2;
                
            return resultado;
        }
        private void Comparar(string palabra, string lineaGoldStandard, string lineaPrueba)
        {
            var partesLineaDePrueba = lineaPrueba.TrimEnd().Split();
            var tagDePrueba = partesLineaDePrueba.Length > 1 ? partesLineaDePrueba.LastOrDefault() : "";

            var partesGoldStandard = lineaGoldStandard.TrimEnd().Split();
            var tagGoldStandard = partesGoldStandard.Length > 1 ? partesGoldStandard.LastOrDefault() : "";

            if (!string.IsNullOrEmpty(tagDePrueba) && !string.IsNullOrEmpty(tagGoldStandard))
            {
                matrizEnProceso.SumarEtiqueta();
                var acierto = CompararEtiquetas(tagGoldStandard, tagDePrueba);
                
                if (!acierto)
                    matrizEnProceso.AgregarError(tagGoldStandard, tagDePrueba, palabra);
            }
        }
        
        /// <summary>
        ///   Lee los archivos y va generando la matriz de confusión.
        /// </summary>
        private MatrizDeConfusión GenerarMatrizDeConfusión(string archivoGoldStandard, string archivoParaComparar)
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
                    Comparar(palabraGoldStandard, aGoldStandard[i], aParaComparar[j]);                    
                                    
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

        public void EstablecerOpciones(bool generarMatrizDeConfusionParaLatex, string titulo = "", string tituloFila = "", string tituloColumna = "", string archivoTabla = "", ITablaDeTraduccion tablaDeTraduccion = null, bool hacerTabla = false)
        {
            this.titulo = titulo;
            this.tituloFila = tituloFila;
            this.tituloColumna = tituloColumna;
            this.archivoTabla = archivoTabla;
            this.generarMatrizDeConfusionParaLatex = generarMatrizDeConfusionParaLatex;
            this.tablaDeTraduccion = tablaDeTraduccion ?? new TablaDeTraducciónVacía();
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
            var matrizDeConfusión = GenerarMatrizDeConfusión(archGoldStandard, archParaComparar);
            var escritor = new EscritorDeMatrizDeConfusión(salidaMatrizDeConf, matrizDeConfusión);
            
            if (generarMatrizDeConfusionParaLatex)
                escritor.EscribirMatrizDeConfParaLatex(titulo, tituloFila, tituloColumna, tablaDeTraduccion);
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