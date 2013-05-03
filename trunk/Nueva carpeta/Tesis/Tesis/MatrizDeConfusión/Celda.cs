using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class Celda : IComparer<Celda>
    {
        #region Variables de instancia
        public string TagDePrueba { get; private set; }
        public string TagGoldStandard {get; private set;}
        private Dictionary<string, int> palabras;
        #endregion

        #region Constructores
        public Celda(string tagGoldStandard, string tagDePrueba, string palabra)
        {
            TagDePrueba = tagDePrueba;
            TagGoldStandard = tagGoldStandard;
            palabras = new Dictionary<string, int> {{palabra, 1}};
        }
        #endregion

        #region Métodos
        public bool Equals(Celda otraCelda)
        {
            if (ReferenceEquals(null, otraCelda))
                return false;
            if (ReferenceEquals(this, otraCelda))
                return true;
            return Equals(otraCelda.TagDePrueba, TagDePrueba) && Equals(otraCelda.TagGoldStandard, TagGoldStandard);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (Celda))
                return false;
            return Equals((Celda) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((TagDePrueba != null ? TagDePrueba.GetHashCode() : 0)*397) ^ (TagGoldStandard != null ? TagGoldStandard.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}(gold)-{1}(prueba): {2}", TagGoldStandard, TagDePrueba, TotalDePalabras);
        }

        public int Compare(Celda x, Celda y)
        {
            return 1;
        }
        #endregion

        public IEnumerable<KeyValuePair<string, int>> TomarPalabrasDeMayorError(int cuantas)
        {
            return palabras.OrderByDescending(s => s.Value).Take(cuantas);
        }
        public int TotalDePalabras
        {
            get
            {
                return palabras.Sum(p => p.Value);
            }
        }
        public void AgregarPalabra(string palabra)
        {
            if (palabras.AgregarSiNoExiste(palabra, 1))
                palabras[palabra]++;

        }
    }
}