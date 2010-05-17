using System;
using System.Collections.Generic;
using System.Linq;
using Facet.Combinatorics;

namespace WindowsFormsApplication1
{
    public class NumeroGenerado
    {
        #region Variables de instancia
        public List<int> DigitosExcluidos = new List<int>();
        private int?[] num = new int?[4];
        #endregion

        #region Constructores
        public NumeroGenerado()
        {
        }
        public NumeroGenerado(int? a, int? b, int? c, int? d, List<int> excluidos) : this(a, b, c, d)
        {
            if (excluidos != null)
                DigitosExcluidos.AddRange(excluidos);
        }
        public NumeroGenerado(int? a, int? b, int? c, int? d)
        {
            num[0] = a;
            num[1] = b;
            num[2] = c;
            num[3] = d;

            if (a != null && !DigitosExcluidos.Contains(a.Value))
                DigitosExcluidos.Add(a.Value);
            if (b != null && !DigitosExcluidos.Contains(b.Value))
                DigitosExcluidos.Add(b.Value);
            if (c != null && !DigitosExcluidos.Contains(c.Value))
                DigitosExcluidos.Add(c.Value);
            if (d != null && !DigitosExcluidos.Contains(d.Value))
                DigitosExcluidos.Add(d.Value);
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

        protected int CuantosFaltanCompletar
        {
            get
            {
                var cuantos = 0;
                for (var i = 0; i < 4; i++)
                    if (num[i] == null)
                        cuantos++;
                return cuantos;
            }
        }
        #endregion

        #region Métodos
        public void Calificar(string numero, out int bien, out int regular)
        {
            bien = 0;
            regular = 0;
            var n0 = int.Parse(numero[0].ToString());
            var n1 = int.Parse(numero[1].ToString());
            var n2 = int.Parse(numero[2].ToString());
            var n3 = int.Parse(numero[3].ToString());

            if (n0 == num[0])
                bien++;
            else if (Contiene(n0))
                regular++;

            if (n1 == num[1])
                bien++;
            else if (Contiene(n1))
                regular++;

            if (n2 == num[2])
                bien++;
            else if (Contiene(n2))
                regular++;

            if (n3 == num[3])
                bien++;
            else if (Contiene(n3))
                regular++;
        }
        public bool Completar()
        {
            IList<int> digitos = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var posiblesValores = digitos.Except(DigitosExcluidos).ToList();

            var todoBien = posiblesValores.Count >= CuantosFaltanCompletar;

            if (todoBien)
                for (var i = 0; i < 4; i++)
                    if (num[i] == null)
                    {
                        num[i] = posiblesValores.First();
                        posiblesValores.Remove(posiblesValores.First());
                    }
            return todoBien;
        }
        private bool Contiene(int digito)
        {
            return num.Any(n => n == digito);
        }
        public bool Equals(NumeroGenerado other)
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
            if (obj.GetType() != typeof (NumeroGenerado))
                return false;
            return Equals((NumeroGenerado) obj);
        }
        private bool EstaElDigito(int? digito)
        {
            var esta = false;

            if (digito != null)
                for (var i = 0; i < 4 && !esta; i++)
                    esta = digito == num[i];

            return esta;
        }
        public bool EsUnificableCon(NumeroGenerado numero)
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
        public static bool EsValido(string numero)
        {
            int num;
            var esValido = !string.IsNullOrEmpty(numero) && numero.Length == 4 && int.TryParse(numero, out num);
            if (esValido)
            {
                var n0 = numero[0];
                var n1 = numero[1];
                var n2 = numero[2];
                var n3 = numero[3];

                esValido &= n0 != n1 && n0 != n2 && n0 != n3 && n1 != n2 && n1 != n3 && n2 != n3;
            }


            return esValido;
        }
        public static List<NumeroGenerado> GenerarComplementos(List<int> excluidos)
        {
            IList<int> digitos = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var posibles = digitos.Except(excluidos);

            var variaciones = new Variations<int>(posibles, 4);
            return variaciones.Select(variacion => new NumeroGenerado(variacion[0], variacion[1], variacion[2], variacion[3], excluidos.Union(variacion).ToList())).ToList();
        }
        public static NumeroGenerado GenerarNumeroAlAzar()
        {
            IList<int> digitos = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var r = new Random((int) DateTime.Now.Ticks);

            var indice = r.Next(0, 9);
            var a = digitos[indice];
            digitos.Remove(a);

            indice = r.Next(0, 8);
            var b = digitos[indice];
            digitos.Remove(b);

            indice = r.Next(0, 7);
            var c = digitos[indice];
            digitos.Remove(c);

            indice = r.Next(0, 6);
            var d = digitos[indice];
            digitos.Remove(d);

            return new NumeroGenerado(a, b, c, d);
        }
        public override int GetHashCode()
        {
            return (num != null ? num.GetHashCode() : 0);
        }
        public override string ToString()
        {
            var numero = string.Empty;
            numero += num[0] == null ? "_" : num[0].ToString();
            numero += num[1] == null ? "_" : num[1].ToString();
            numero += num[2] == null ? "_" : num[2].ToString();
            numero += num[3] == null ? "_" : num[3].ToString();

            return numero;
        }
        public NumeroGenerado UnificarCon(NumeroGenerado numero)
        {
            var numUnificado = new NumeroGenerado();
            for (var i = 0; i < 4; i++)
                if (numero.num[i] == null)
                    numUnificado.num[i] = num[i];
                else
                    numUnificado.num[i] = numero.num[i];

            numUnificado.DigitosExcluidos.AddRange(numero.DigitosExcluidos.Union(DigitosExcluidos));
            return numUnificado;
        }
        #endregion
    }
}