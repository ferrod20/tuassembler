
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
                numer = new Numero(int.Parse(n0.ToString()), int.Parse(n1.ToString()), int.Parse(n2.ToString()),
                                   int.Parse(n3.ToString()));
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

        public Numero(GeneradorDeNumero generadorDeNumero)
            : this(
                generadorDeNumero[0].Value, generadorDeNumero[1].Value, generadorDeNumero[2].Value,
                generadorDeNumero[3].Value)
        {
        }

        protected Numero()
        {
        }
        #endregion

        #region Propiedades
        public GeneradorDeNumero ConvertirEnGeneradorDeNumero
        {
            get { return new GeneradorDeNumero(n0, n1, n2, n3); }
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
}