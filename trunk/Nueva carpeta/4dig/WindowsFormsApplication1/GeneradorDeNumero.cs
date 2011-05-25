using System;
using System.Collections.Generic;
using System.Linq;
using Facet.Combinatorics;

namespace WindowsFormsApplication1
{
    public class GeneradorDeNumero
    {
        #region Variables de instancia
        public List<int> DigitosExcluidos = new List<int>();
        private int?[] num = new int?[4];
        #endregion

        #region Constructores
        public GeneradorDeNumero()
        {
        }
        public GeneradorDeNumero(int? a, int? b, int? c, int? d, IList<int> excluidos)
            : this(a, b, c, d)
        {
            if (excluidos != null)
                ExcluirDigitos(excluidos);
        }
        public GeneradorDeNumero(int? a, int? b, int? c, int? d)
        {
            num[0] = a;
            num[1] = b;
            num[2] = c;
            num[3] = d;

            ExcluirDigito(a);
            ExcluirDigito(b);
            ExcluirDigito(c);
            ExcluirDigito(d);
        }
        #endregion

        #region Propiedades
        public int? this[int i]
        {
            get
            {
                return num[i];
            }
        }

        public int CantidadDeNulls
        {
            get
            {
                return num.Count(n => n == null);
            }
        }
        #endregion

        #region Métodos
        public bool Completar()
        {
            var posiblesValores = digitos.Except(DigitosExcluidos).ToList();
            var todoBien = posiblesValores.Count >= CantidadDeNulls;

            if (todoBien)
                for (var i = 0; i < 4; i++)
                    if (num[i] == null)
                    {
                        num[i] = posiblesValores.First();
                        posiblesValores.Remove(posiblesValores.First());
                    }
            return todoBien;
        }
        public bool Equals(Numero other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.n0 == num[0] && other.n1 == num[1] && other.n2 == num[2] && other.n3 == num[3];
        }
        public bool Equals(GeneradorDeNumero other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.num[0] == num[0] && other.num[1] == num[1] && other.num[2] == num[2] && other.num[3] == num[3];
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() == typeof(GeneradorDeNumero))
                return Equals((GeneradorDeNumero)obj);
            if (obj.GetType() == typeof(Numero))
                return Equals((Numero)obj);

            return false;
        }
        private bool EstaElDigito(int? digito)
        {
            var esta = false;

            if (digito != null)
                for (var i = 0; i < 4 && !esta; i++)
                    esta = digito == num[i];

            return esta;
        }
        public bool EsUnificableCon(GeneradorDeNumero numero)
        {
            var esUnificable = true;
            for (var i = 0; i < 4; i++)
            {
                var n1 = numero.num[i];
                var n2 = num[i];
                var esUnif = true;

                if (n1.HasValue)
                    if (n2.HasValue)
                        esUnif = n1.Value == n2.Value;
                    else
                        esUnif = !DigitosExcluidos.Contains(n1.Value) && !EstaElDigito(n1.Value);
                else if (n2.HasValue)
                    esUnif = !numero.DigitosExcluidos.Contains(n2.Value) && !numero.EstaElDigito(n2.Value);

                esUnificable &= esUnif;
            }
            return esUnificable;
        }
        public void ExcluirDigito(int? digito)
        {
            if (digito != null && !DigitosExcluidos.Contains(digito.Value))
                DigitosExcluidos.Add(digito.Value);
        }
        public void ExcluirDigitos(IEnumerable<int?> digitos)
        {
            foreach (var digito in digitos)
                ExcluirDigito(digito);
        }
        public void ExcluirDigitos(IEnumerable<int> digitos)
        {
            foreach (var digito in digitos)
                ExcluirDigito(digito);
        }

        public static List<GeneradorDeNumero> GenerarComplementos(List<int> excluidos)
        {
            var posibles = digitos.Except(excluidos);

            var variaciones = new Variations<int>(posibles, 4);
            return variaciones.Select(variacion => new GeneradorDeNumero(variacion[0], variacion[1], variacion[2], variacion[3], excluidos.Union(variacion).ToList())).ToList();
        }
        public static Numero GenerarNumeroAlAzar()
        {
            var digs = new List<int>(digitos);
            var r = new Random((int)DateTime.Now.Ticks);

            var indice = r.Next(0, 9);
            var a = digs[indice];
            digs.Remove(a);

            indice = r.Next(0, 8);
            var b = digs[indice];
            digs.Remove(b);

            indice = r.Next(0, 7);
            var c = digs[indice];
            digs.Remove(c);

            indice = r.Next(0, 6);
            var d = digs[indice];
            digs.Remove(d);

            return new Numero(a, b, c, d);
        }
        public override int GetHashCode()
        {
            return (num != null ? num.GetHashCode() : 0);
        }
        public string MostrarDigitosExcluidos()
        {
            return DigitosExcluidos.Aggregate(string.Empty, (s, dig) => s + dig + ", ");
        }
        public override string ToString()
        {
            var numero = string.Empty;
            numero += num[0] == null ? "_" : num[0].ToString();
            numero += " ";
            numero += num[1] == null ? "_" : num[1].ToString();
            numero += " ";
            numero += num[2] == null ? "_" : num[2].ToString();
            numero += " ";
            numero += num[3] == null ? "_" : num[3].ToString();

            return numero;
        }

        public GeneradorDeNumero UnificarCon(GeneradorDeNumero numero)
        {
            var numUnificado = new GeneradorDeNumero();
            for (var i = 0; i < 4; i++)
                if (numero.num[i] == null)
                    numUnificado.num[i] = num[i];
                else
                    numUnificado.num[i] = numero.num[i];

            numUnificado.ExcluirDigitos(numero.DigitosExcluidos.Union(DigitosExcluidos));
            return numUnificado;
        }
        #endregion

        static IList<int> digitos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public IList<GeneradorDeNumero> ObtenerPosibles(IEnumerable<GeneradorDeNumero> noPermitidos)
        {
            var posiblesValores = digitos.Except(DigitosExcluidos).ToList();
            var todoBien = posiblesValores.Count >= CantidadDeNulls;
            IList<GeneradorDeNumero> posibles = new List<GeneradorDeNumero>();
            if (todoBien)
            {
                var variaciones = new Variations<int>(posiblesValores, CantidadDeNulls);
                var todos = variaciones.Select(GenerarNumero).ToList();
                posibles = todos.Except(noPermitidos).ToList();
            }

            return posibles;
        }
        private GeneradorDeNumero GenerarNumero(IList<int> variacion)
        {
            var j = 0;
            var resultado = new GeneradorDeNumero();

            for (var i = 0; i < 4; i++)
            {
                if (num[i] == null)
                {
                    resultado.num[i] = variacion[j];
                    j++;
                }
                else
                    resultado.num[i] = num[i];
            }
            return resultado;
        }
    }
}