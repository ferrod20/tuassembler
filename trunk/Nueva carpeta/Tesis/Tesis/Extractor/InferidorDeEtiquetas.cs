using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class InferiridorDeEtiquetas
    {
        public string Palabra { get; set; }    

        #region Métodos
        /// <summary>
        /// Si la etiqueta es 
        ///     VBD|VBN: desambigua a VBN si alguna de las 2 palabras anteriores es "has" "have" o "had"
        ///     VB:      devuelve VBP si alguna de las 2 palabras anteriores "to"
        /// si no, devuelve la etiquetaPennTreebank
        /// </summary>
        /// <param name="posición">posición de la Palabra en el ejemplo</param>
        private string InferirEtiqueta(string etiquetaPennTreebank, string[] palabrasDelEjemplo, int posición)
        {
            if (etiquetaPennTreebank.EsAlgunaDeEstas("VBD|VBN", "VB"))
            {
                var palAnteriores = new List<string>();
                for (var j = 0; j < posición; j++)
                    palAnteriores.Add(palabrasDelEjemplo[j]);

                switch (etiquetaPennTreebank)
                {
                    case "VBD|VBN":
                        etiquetaPennTreebank = EsVBN(palAnteriores) ? "VBN" : "VBD";
                        break;
                    case "VB":
                        if (EsVBP(palAnteriores))
                            etiquetaPennTreebank = "VBP";
                        break;
                }
            }
            return etiquetaPennTreebank;
        }

        /// <summary>
        /// Para cada una de las formas de la Palabra infiere su etiquetaPennTreebank basado en la Palabra y su etiqueta PennTreebank
        /// </summary>
        public Dictionary<string, string> InferirEtiquetasParaLasFormasDeLaPalabra(IEnumerable<string> formasDeLaPalabra, string etiquetaPennTreebank)
        {
            var etiquetasInferidas = new Dictionary<string, string>();
            var etiquetaInferida = string.Empty;
            foreach (var forma in formasDeLaPalabra)
            {
                switch (etiquetaPennTreebank)
                {
                    case "JJ":
                        etiquetaInferida = InferirTipoJJ(forma);
                        break;
                    case "VB":
                        etiquetaInferida = InferirTipoVB(forma);
                        break;
                    case "RB":
                        etiquetaInferida = InferirTipoRB(forma);
                        break;
                    case "NN":
                        etiquetaInferida = InferirTipoNN(forma);
                        break;
                }
                if (etiquetaInferida != string.Empty && forma.ToLower() != Palabra.ToLower())
                    etiquetasInferidas.AgregarSiNoExiste(forma.ToLower(), etiquetaInferida);
            }
            return etiquetasInferidas;
        }

        private bool EsVBP(List<string> palabrasAnteriores)
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

        private bool EsJJR(string forma)
        {
            return Palabra.Length <= forma.Length && forma.ToLower().EndsWith("er") || forma.ToLower().StartsWith("more") || forma.ToLower().StartsWith("less");
        }

        private bool EsJJS(string forma)
        {
            return Palabra.Length <= forma.Length && forma.ToLower().EndsWith("est") || forma.ToLower().StartsWith("most") || forma.ToLower().StartsWith("least");
        }

        private bool EsNNS(string forma)
        {
            return Palabra.Length < forma.Length && forma.ToLower().EndsWith("s");
        }

        private bool EsRBR(string forma)
        {
            return Palabra.Length <= forma.Length && forma.ToLower().EndsWith("er") || forma.ToLower().StartsWith("more") || forma.ToLower().StartsWith("less");
        }

        private bool EsRBS(string forma)
        {
            return Palabra.Length <= forma.Length && forma.ToLower().EndsWith("est") || forma.ToLower().StartsWith("most") || forma.ToLower().StartsWith("least");
        }

        private bool EsVBDoVBN(string forma)
        {
            return Palabra.Length <= forma.Length && forma.ToLower().EndsWith("ed");
        }

        private bool EsVBG(string forma)
        {
            return Palabra.Length <= forma.Length && forma.ToLower().EndsWith("ing");
        }

        /// <summary>
        /// Dada una Palabra etiquetada como VBD|VBN devuelve true si es VBN; esto es si alguna de las 2 palabras anteriores son have has o had
        /// </summary>
        private bool EsVBN(List<string> palabrasAnteriores)
        {
            var ultima = "";
            var anteUltima = "";

            if (palabrasAnteriores.Count > 0)
                ultima = palabrasAnteriores.Last();
            if (palabrasAnteriores.Count > 1)
                anteUltima = palabrasAnteriores[palabrasAnteriores.Count - 2];

            return ultima.EsAlgunaDeEstas("have", "has", "had") || anteUltima.EsAlgunaDeEstas("have", "has", "had");
        }


        private bool EsVBZ(string forma)
        {
            return Palabra.Length <= forma.Length && forma.ToLower().EndsWith("s");
        }

        /// <summary>
        ///   A partir de una Palabra de tipo JJ, inferir el tipo de la forma
        /// </summary>
        private string InferirTipoJJ(string forma)
        {
            var etiquetaInferida = string.Empty;
            if (EsJJR(forma))
                etiquetaInferida = "JJR";
            else if (EsJJS(forma))
                etiquetaInferida = "JJS";

            return etiquetaInferida;
        }

        private string InferirTipoNN(string forma)
        {
            return EsNNS(forma) ? "NNS" : "NN";
        }

        private string InferirTipoRB(string forma)
        {
            var etiquetaInferida = string.Empty;
            if (EsRBR(forma))
                etiquetaInferida = "RBR";
            else if (EsRBS(forma))
                etiquetaInferida = "RBS";

            return etiquetaInferida;
        }

        private string InferirTipoVB(string forma)
        {
            var etiquetaInferida = string.Empty;
            if (EsVBDoVBN(forma))
                etiquetaInferida = "VBD|VBN";
            else if (EsVBG(forma))
                etiquetaInferida = "VBG";
            else if (EsVBZ(forma))
                etiquetaInferida = "VBZ";
            else
                etiquetaInferida = "VB";

            return etiquetaInferida;
        }
        #endregion
    }
}