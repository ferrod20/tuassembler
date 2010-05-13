using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		Juego j ;
		private NumeroGenerado num;
		public Form1()
		{
			InitializeComponent();
			Test.TestJuego3B_2B();
		}

		private void btnAdivinar_Click(object sender, EventArgs e)
		{
			if( j== null)
			{
				j = new Juego();
				num = j.Adivinar();
				lblNumero.Text = num.ToString();
			}
			else
			{
				var bien =txtBien.Text == string.Empty?0: int.Parse(txtBien.Text);
				var regular = txtRegular.Text == string.Empty ? 0 : int.Parse(txtRegular.Text);
				var reglaAgregada = j.AgregarRegla(num[0].Value, num[1].Value, num[2].Value, num[3].Value,bien,regular);

				txtMensajes.Text += reglaAgregada + Environment.NewLine;
				num = j.Adivinar();
				if (num == null)
				{
					lblNumero.Text = "Ocurrio un problema con el programa o puso algo mal....";
					j = null;
					txtMensajes.Text = string.Empty;
				}
				else
					lblNumero.Text = num.ToString();
			}
				
		}

		
	}
}
