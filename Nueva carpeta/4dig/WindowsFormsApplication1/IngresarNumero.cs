using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class IngresarNumero : Form
    {
        #region Variables de instancia
        public Numero NumeroIngresado;
        #endregion

        #region Constructores
        public IngresarNumero()
        {
            InitializeComponent();
        }
        #endregion

        #region Manejadores de eventos
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!Numero.EsValido(txtNumero.Text.Replace(" ", ""), out NumeroIngresado))
            {
                MessageBox.Show("Ingresá un número de 4 cifras distintas.");
                txtNumero.Text = string.Empty;
                txtNumero.Focus();
            }
            else
                Close();
        }

        private void IngresarNumero_Load(object sender, EventArgs e)
        {
            txtNumero.Focus();
        }
        #endregion

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void tecladoDeDigitos1_Oprimir(int nroOprimido)
        {
            if (nroOprimido == -1)
            {
                if (txtNumero.Text.Length > 0)
                    txtNumero.Text = txtNumero.Text.Substring(0, txtNumero.Text.Length - 2);
            }
            else if (txtNumero.Text.Replace(" ", "").Length < 4)
                txtNumero.Text += nroOprimido.ToString() + " ";            
        }
    }
}