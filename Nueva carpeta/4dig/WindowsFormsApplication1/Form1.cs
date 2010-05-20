﻿using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        #region Variables de instancia
        private Juego j;
        private int partidosJugados;
        #endregion

        #region Constructores
        public Form1()
        {
            InitializeComponent();
        	Regla.GuardarTodasLasOpciones("opciones.txt");
            Test.TestJuego5();
        }
        #endregion

        #region Métodos
        private void Empezar()
        {
            j = new Juego();
            j.GenerarNumeroAAdivinar();
            j.Adivinar();
            partidosJugados++;
            UnJuegoMas();            
            label4.Text = j.NumeroAdivinadoPorLaCompu + " (" + j.CantidadDeOpciones + " opciones)";

            ProximoTurno(true);
            MA.LimpiarTextBoxs(txtHistoriaCompu, txtHistoriaJugador);
        }
        private int UnJuegoMas()
        {
            int cuantos = 0;
            if (File.Exists(@"C:\p.inx"))
            {
                TextReader arch = new StreamReader(@"C:\p.inx");
                var s = arch.ReadLine();
               cuantos = int.Parse(s.Trim());
                arch.Close();
            }

            TextWriter archivo= new StreamWriter(@"C:\p.inx",false);
            archivo.WriteLine(cuantos+1);
            archivo.Close();
            return cuantos + 1;
        }

        private void PantallaGano(string mensaje)
        {
            MessageBox.Show(mensaje);
            Empezar();
        }

        private void ProximoTurno(bool paraLaCompu)
        {
            MA.LimpiarTextBoxs(txtBien, txtRegular, txtNumero);
            MA.MostrarControles(paraLaCompu, txtBien, txtRegular, btnSeguir, lblBien, lblRegular, label4);
            MA.MostrarControles(!paraLaCompu, txtNumero, btnProbar, lblNUm);

            AcceptButton = paraLaCompu? btnSeguir:btnProbar;
            if (paraLaCompu)
                txtBien.Focus();
            else
                txtNumero.Focus();
            
        }
        #endregion

        #region Manejadores de eventos
        private void btnProbar_Click(object sender, EventArgs e)
        {
            int bien, regular;
            if (NumeroGenerado.EsValido(txtNumero.Text))
            {
                j.Calificar(txtNumero.Text, out bien, out regular);
                txtHistoriaJugador.Text += txtNumero.Text + " " + bien + "B " + regular + "R" + Environment.NewLine;
                if (bien < 4)
                    if (j.GanoLaCompu)
                        PantallaGano("Perdiste, el numero era: " + j.NumeroAAdivinarPorElJugador);
                    else
                    {
                        ProximoTurno(true);
                        label4.Text = j.NumeroAdivinadoPorLaCompu + " (" + j.CantidadDeOpciones + " opciones)";
                    }
                else
                    PantallaGano(j.GanoLaCompu ? "Empate" : "Ganaste!");
            }
            else
            {
                MessageBox.Show("Ingresá un número de 4 cifras distintas.");
                txtNumero.Text = string.Empty;
                txtNumero.Focus();
            }
        }
        private void btnSeguir_Click(object sender, EventArgs e)
        {
            if (label4.Text == "Ocurrio un problema con el programa o pusiste algo mal....")
                Empezar();
            else
            {
				if( !j.NumeroAAdivinarPorLaCompuIngresado)
				{
					if (!NumeroGenerado.EsValido(txtNumero.Text))
					{
						var n0 = int.Parse(txtNumero.Text[0].ToString());
						var n1 = int.Parse(txtNumero.Text[1].ToString());
						var n2 = int.Parse(txtNumero.Text[2].ToString());
						var n3 = int.Parse(txtNumero.Text[3].ToString());

						j.NumeroAAdivinarPorLaCompu = new NumeroGenerado(n0, n1, n2, n3);
					}
					else                    
					{
						MessageBox.Show("Ingresá un número de 4 cifras distintas.");
						txtNumero.Text = string.Empty;
						txtNumero.Focus();
					}
				}
				
				
					var reglaAgregada = j.AgregarReglaAlNumeroAdivinado(bien, regular);
						txtHistoriaCompu.Text += reglaAgregada + Environment.NewLine;

						if (bien != 4)
						{
							j.Adivinar();
							if (j.NumeroAdivinadoPorLaCompu == null)
								label4.Text = "Ocurrio un problema con el programa o pusiste algo mal....";
							else
								ProximoTurno(false);
						}
						else
						{
							j.GanoLaCompu = true;
							ProximoTurno(false);
						}					
				}					
				

					
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Empezar();

            partidosJugados = UnJuegoMas();
            if (partidosJugados > 150)
            {
                MessageBox.Show("Esta es una versión gratis y ya ha jugado muchos partidos.");
                Close();
            }
        }
        #endregion
    }
}