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
		private Juego j;
		private NumeroGenerado num;
		private NumeroGenerado numeroAAdivinar;
		private bool ganoLaCompu;
		public Form1()
		{
			InitializeComponent();
			
		}
		private void Empezar()
		{
			j = new Juego();
			numeroAAdivinar = NumeroGenerado.GenerarNumeroAlAzar();
			num = j.Adivinar();
			label4.Text = num.ToString();

			ProximoTurno(true);
			MA.LimpiarTextBoxs(txtHistoriaCompu,txtHistoriaJugador);			
		}

		private void btnSeguir_Click(object sender, EventArgs e)
		{
			if (label4.Text == "Ocurrio un problema con el programa o pusiste algo mal....")
				Empezar();
			else
			{
				var mensajeDeError = string.Empty;
				int bien = 0,regular =0;
				if (txtBien.Text != string.Empty)
					if (!int.TryParse(txtBien.Text, out bien))
						mensajeDeError = "Escribir un numero en bien!";

				if (txtRegular.Text != string.Empty)
					if (!int.TryParse(txtRegular.Text, out regular))
						mensajeDeError = "Escribir un numero en regular!";
						
				if( regular+bien > 4)
					mensajeDeError = "Estas poniendo cualquier cosa putoo/aa!";
				if( mensajeDeError == string.Empty)
				{
					var reglaAgregada = j.AgregarRegla(num[0].Value, num[1].Value, num[2].Value, num[3].Value, bien, regular);
					txtHistoriaCompu.Text += reglaAgregada + Environment.NewLine;

					if (bien != 4)
					{
						
						num = j.Adivinar();
						if (num == null)
							label4.Text = "Ocurrio un problema con el programa o pusiste algo mal....";
						else
							ProximoTurno(false);
					}
					else
					{
						ganoLaCompu = true;
						ProximoTurno(false);
					}						
				}
				else
					MessageBox.Show(mensajeDeError);
			}
		}

		private void btnProbar_Click(object sender, EventArgs e)
		{
			int bien, regular;
			if (NumeroGenerado.EsValido(txtNumero.Text))
			{
				numeroAAdivinar.Calificar(txtNumero.Text, out bien, out regular);
				txtHistoriaJugador.Text += txtNumero.Text + " " + bien + " BIEN " + regular + " REGULAR" + Environment.NewLine;
				if (bien < 4)
				{
					if( ganoLaCompu )						
						PantallaGano("Perdiste puto/a!!!!");
					else
					{
						ProximoTurno(true);
						label4.Text = num.ToString();	
					}
					
				}
				else
					PantallaGano(ganoLaCompu ? "Empate" : "Ganaste!");
					
			}
			else
			{
				MessageBox.Show("Debe ingresar un número de 4 cifras distintas.");
				txtNumero.Text = string.Empty;
				txtNumero.Focus();
			}	
		}
		private void PantallaGano(string mensaje)
		{
			MessageBox.Show( mensaje );
			Empezar();
		}
		
		private void ProximoTurno(bool paraLaCompu)
		{
			MA.LimpiarTextBoxs(txtBien, txtRegular,txtNumero);
			MA.MostrarControles(paraLaCompu, txtBien, txtRegular, btnSeguir, lblBien, lblRegular);
			MA.MostrarControles(!paraLaCompu, label5, txtNumero, btnProbar, lblNUm);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Empezar();
		}
	}
}
