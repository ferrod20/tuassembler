using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class TecladoDeDigitos : UserControl
    {
        public TecladoDeDigitos()
        {
            InitializeComponent();
        }

        public event Oprimir Oprimir;


        private void btn1_Click(object sender, EventArgs e)
        {
            if (Oprimir != null)
                Oprimir(1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (Oprimir != null)
                Oprimir(2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (Oprimir != null)
                Oprimir(3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (Oprimir != null)
                Oprimir(4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (Oprimir != null)
                Oprimir(5);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (Oprimir != null)
                Oprimir(6);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (Oprimir != null)
                Oprimir(7);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (Oprimir != null)
                Oprimir(8);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (Oprimir != null)
                Oprimir(9);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (Oprimir != null)
                Oprimir(0);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (Oprimir != null)
                Oprimir(-1);
        }
    }

    public delegate void Oprimir(int nroOprimido);
}