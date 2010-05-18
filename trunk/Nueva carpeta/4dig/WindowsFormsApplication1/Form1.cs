using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        #region Variables de instancia
        private Juego j;
        #endregion

        #region Constructores
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region Métodos
        private void Empezar()
        {
            j = new Juego();
            j.GenerarNumeroAAdivinar();
            j.Adivinar();
            label4.Text = j.NumeroAdivinadoPorLaCompu + " (" + j.CantidadDeOpciones + " opciones)";

            ProximoTurno(true);
            MA.LimpiarTextBoxs(txtHistoriaCompu, txtHistoriaJugador);
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
                var mensajeDeError = string.Empty;
                int bien = 0, regular = 0;
                
                if (regular + bien > 4)
                    mensajeDeError = "Estas poniendo cualquier cosa!!";
                if (mensajeDeError == string.Empty)
                {
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
                else
                    MessageBox.Show(mensajeDeError);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Empezar();
        }
        #endregion
    }
}