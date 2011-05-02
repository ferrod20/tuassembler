using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    internal class Tags : IComparer<Tags>
    {
        #region Variables de instancia
        public string TagDePrueba;
        public string TagGoldStandard;
        public Dictionary<string, int> Palabras;
        #endregion

        #region Constructores
        public Tags(string tagGoldStandard, string tagDePrueba, string palabra)
        {
            TagDePrueba = tagDePrueba;
            TagGoldStandard = tagGoldStandard;
            Palabras = new Dictionary<string, int> {{palabra, 1}};
        }

        public int TotalDePalabras
        {
            get
            {
                return Palabras.Sum(p => p.Value);
            }
        }

        #endregion

        #region Metodos
        public bool Equals(Tags obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return Equals(obj.TagDePrueba, TagDePrueba) && Equals(obj.TagGoldStandard, TagGoldStandard);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (Tags))
                return false;
            return Equals((Tags) obj);
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
            return string.Format("{0}-{1}", TagDePrueba, TagGoldStandard);
        }

        public int Compare(Tags x, Tags y)
        {
            return 1;
        }

        public void AgregarPalabra(string palabra)
        {
            if(!Palabras.AddIfNoExists(palabra, 1))
                Palabras[palabra]++;

        }
        #endregion
    }
}