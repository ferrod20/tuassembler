using System;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{           
    public class Comparador
    {
        #region Variables
        private ITablaDeTraducción tablaDeTraducción;
        private MatrizEnProceso matrizEnProceso;
        private string archGoldStandard, archParaComparar, salidaMatrizDeConf, titulo, tituloFila, tituloColumna, archivoTabla;
        private bool generarMatrizDeConfusionParaLatex, hacerTabla;        
        #endregion

        public Comparador(string archGoldStandard, string archParaComparar, string salidaMatrizDeConf)
        {
            this.archGoldStandard = archGoldStandard;
            this.archParaComparar = archParaComparar;
            this.salidaMatrizDeConf = salidaMatrizDeConf;
        }
        public void EstablecerOpciones(bool generarMatrizDeConfusionParaLatex, string titulo = "", string tituloFila = "", string tituloColumna = "", string archivoTabla = "", ITablaDeTraducción tablaDeTraducción = null, bool hacerTabla = false)
        {
            this.titulo = titulo;
            this.tituloFila = tituloFila;
            this.tituloColumna = tituloColumna;
            this.archivoTabla = archivoTabla;
            this.generarMatrizDeConfusionParaLatex = generarMatrizDeConfusionParaLatex;
            this.tablaDeTraducción = tablaDeTraducción ?? new TablaDeTraducciónVacía();
            this.hacerTabla = hacerTabla;
        }
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
                
            if (tablaDeTraducción.GetType() != typeof (TablaDeTraducciónVacía))
            {
                if (tablaDeTraducción.ContieneTraduccionPara(tag2))
                {
                    var tagsBNC = tag1.Split('-');
                    var tagsTraducidosABNC = tablaDeTraducción.ObtenerTraduccionPara(tag2);
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
                escritor.EscribirMatrizDeConfParaLatex(titulo, tituloFila, tituloColumna, tablaDeTraducción);
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