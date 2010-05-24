using System;
using System.Collections.Generic;
using System.Linq;
using Facet.Combinatorics;

namespace WindowsFormsApplication1
{
    public class Numero
    {
        public static bool EsValido(string numero, out Numero numer)
        {
            numer = null;
            int num;
            var esValido = !string.IsNullOrEmpty(numero) && numero.Length == 4 && int.TryParse(numero, out num);
            if (esValido)
            {
                var n0 = numero[0];
                var n1 = numero[1];
                var n2 = numero[2];
                var n3 = numero[3];

                esValido &= n0 != n1 && n0 != n2 && n0 != n3 && n1 != n2 && n1 != n3 && n2 != n3;
                numer = new Numero(int.Parse(n0.ToString()), int.Parse(n1.ToString()), int.Parse(n2.ToString()), int.Parse(n3.ToString()));
            }

            return esValido;
        }
        public static bool EsValido(string numero)
        {
            Numero n;
            return EsValido(numero, out n);
        }
        #region Variables de instancia
        public int n0;
        public int n1;
        public int n2;
        public int n3;
        #endregion

        #region Constructores
        public Numero(int n0, int n1, int n2, int n3)
        {
            this.n0 = n0;
            this.n1 = n1;
            this.n2 = n2;
            this.n3 = n3;
        }
        public Numero(Numero n)
        {
            n0 = n.n0;
            n1 = n.n1;
            n2 = n.n2;
            n3 = n.n3;
        }
        public Numero(NumeroGenerado numeroGenerado) : this(numeroGenerado[0].Value, numeroGenerado[1].Value, numeroGenerado[2].Value, numeroGenerado[3].Value)
        {
        }
        protected Numero()
        {
        }
        #endregion

        #region Propiedades
        public NumeroGenerado ConvertirEnNumeroGenerado
        {
            get
            {
                return new NumeroGenerado(n0, n1, n2, n3);
            }
        }
        #endregion

        #region Métodos
        public void Calificar(string numero, out int bien, out int regular)
        {
            var n0 = int.Parse(numero[0].ToString());
            var n1 = int.Parse(numero[1].ToString());
            var n2 = int.Parse(numero[2].ToString());
            var n3 = int.Parse(numero[3].ToString());

            Calificar(n0, n1, n2, n3, out bien, out regular);
        }
        public void Calificar(int n0, int n1, int n2, int n3, out int bien, out int regular)
        {
            bien = 0;
            regular = 0;

            if (n0 == this.n0)
                bien++;
            else if (Contiene(n0))
                regular++;

            if (n1 == this.n1)
                bien++;
            else if (Contiene(n1))
                regular++;

            if (n2 == this.n2)
                bien++;
            else if (Contiene(n2))
                regular++;

            if (n3 == this.n3)
                bien++;
            else if (Contiene(n3))
                regular++;
        }

        public void Calificar(Numero numero, out int bien, out int regular)
        {
            Calificar(numero.n0, numero.n1, numero.n2, numero.n3, out bien, out regular);
        }
        private bool Contiene(int digito)
        {
            return n0 == digito || n1 == digito || n2 == digito || n3 == digito;
        }
        public override string ToString()
        {
            return n0 + " " + n1 + " " + n2 + " " + n3 + " ";
        }
        #endregion
    }

    public class NumeroAdivinado : Numero
    {
        #region Variables de instancia
        public int Bien;
        public int Regular;
        #endregion

        #region Constructores
        public NumeroAdivinado(Numero n, int bien, int regular) : base(n)
        {
            this.Bien = bien;
            this.Regular = regular;
        }
        public NumeroAdivinado(int n0, int n1, int n2, int n3, int bien, int regular) : base(n0, n1, n2, n3)
        {
            this.Bien = bien;
            this.Regular = regular;
        }
        public NumeroAdivinado(string numero, int bien, int regular)
        {
            this.Bien = bien;
            this.Regular = regular;

            n0 = int.Parse(numero[0].ToString());
            n1 = int.Parse(numero[1].ToString());
            n2 = int.Parse(numero[2].ToString());
            n3 = int.Parse(numero[3].ToString());
        }
        protected NumeroAdivinado()
        {
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return base.ToString() + "\t" + Bien + "B " + Regular + "R";
        }
        #endregion
    }

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
        public NumeroGenerado(int? a, int? b, int? c, int? d, IList<int> excluidos) : this(a, b, c, d)
        {
            if (excluidos != null)
                ExcluirDigitos(excluidos);
        }
        public NumeroGenerado(int? a, int? b, int? c, int? d)
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
        public bool Equals(Numero other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.n0 == num[0] && other.n1 == num[1] && other.n2 == num[2] && other.n3 == num[3];
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
            if (obj.GetType() == typeof (NumeroGenerado))
                return Equals((NumeroGenerado) obj);
            if (obj.GetType() == typeof (Numero))
                return Equals((Numero) obj);

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
        public static List<NumeroGenerado> GenerarComplementos(List<int> excluidos)
        {
            IList<int> digitos = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var posibles = digitos.Except(excluidos);

            var variaciones = new Variations<int>(posibles, 4);
            return variaciones.Select(variacion => new NumeroGenerado(variacion[0], variacion[1], variacion[2], variacion[3], excluidos.Union(variacion).ToList())).ToList();
        }
        public static Numero GenerarNumeroAlAzar()
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

        public NumeroGenerado UnificarCon(NumeroGenerado numero)
        {
            var numUnificado = new NumeroGenerado();
            for (var i = 0; i < 4; i++)
                if (numero.num[i] == null)
                    numUnificado.num[i] = num[i];
                else
                    numUnificado.num[i] = numero.num[i];

            numUnificado.ExcluirDigitos(numero.DigitosExcluidos.Union(DigitosExcluidos));
            return numUnificado;
        }
        #endregion
    }
}