namespace WindowsFormsApplication1
{
    public class NumeroAdivinado : Numero
    {
        #region Variables de instancia
        public int Bien;
        public int Regular;
        #endregion

        #region Constructores
        public NumeroAdivinado(Numero n, int bien, int regular) : base(n)
        {
            Bien = bien;
            Regular = regular;
        }

        public NumeroAdivinado(int n0, int n1, int n2, int n3, int bien, int regular) : base(n0, n1, n2, n3)
        {
            Bien = bien;
            Regular = regular;
        }

        public NumeroAdivinado(string numero, int bien, int regular)
        {
            Bien = bien;
            Regular = regular;

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
}