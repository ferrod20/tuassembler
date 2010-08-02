using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        #region Variables de instancia
        private Juego juego;
        private int partidosJugados;
        #endregion

        #region Constructores
        public Form1()
        {
            InitializeComponent();
            //Regla.GuardarTodasLasOpciones("opciones.txt");
            //Test.TestJuego5();
        }
        #endregion

        #region Métodos
        private void Empezar()
        {
            partidosJugados = UnJuegoMas();
            juego = new Juego();
            juego.GenerarNumeroAAdivinar();
            var ing = new IngresarNumero();
            //TODO!
            ing.ShowDialog();

            juego.NumeroAAdivinarPorLaCompu = ing.NumeroIngresado;
            MA.LimpiarTextBoxs(txtHistoriaCompu, txtHistoriaJugador);
            ProximoTurno();
            lblNumeroAAdivinarXLaCompu.Text = juego.NumeroAAdivinarPorLaCompu.ToString();                
        }
        private void JuegaElJugador()
        {
            int bien, regular;
            var num = juego.JuegaElJugador(txtNumero.Text.Replace(" ", ""), out bien, out regular);
            //var viejo = txtHistoriaJugador.Text;
            //var nuevo = viejo + num + Environment.NewLine;
            txtHistoriaJugador.Text += num + Environment.NewLine; ;
            //var i = 3;
            //while(i>0)
            //{
            //    txtHistoriaJugador.Text = viejo;
            //    txtHistoriaJugador.Invalidate();
            //    //Application.DoEvents();
            //    //Thread.Sleep(200);
            //    txtHistoriaJugador.Text = nuevo;
            //    txtHistoriaJugador.Invalidate();
            //    //Application.DoEvents();
            //    i--;
            //}                

            VerificarSiAlguienGano();
        }
        private void VerificarSiAlguienGano()
        {
            switch (juego.Estado)
            {
                case EstadoDelJuego.GanoLaCompu:
                    PantallaGano("Perdiste, el numero era: " + juego.NumeroAAdivinarPorElJugador);
                    break;
                case EstadoDelJuego.GanoElJugador:
                    PantallaGano("Ganaste!");
                    break;
                case EstadoDelJuego.Empate:
                    PantallaGano("Empate");
                    break;
                case EstadoDelJuego.Jugando:
                    ProximoTurno();                   
                    break;
            }
        }
        private void JuegaLaCompu()
        {
            if (label4.Text == "Ocurrio un problema con el programa o pusiste algo mal....")
                Empezar();
            else
            {
                var reglaAgregada = juego.JuegaLaCompu();

                txtHistoriaCompu.Text += reglaAgregada + Environment.NewLine;
                if (juego.NumeroAdivinadoPorLaCompu == null)
                    label4.Text = "Ocurrio un problema con el programa o pusiste algo mal....";
                else
                {
                    label4.Text = juego.NumeroAdivinadoPorLaCompu + " (" + juego.CantidadDeOpciones + " opciones)";
                    VerificarSiAlguienGano();                    
                }
            }
            
        }

        private void PantallaGano(string mensaje)
        {
            MessageBox.Show(mensaje,"",MessageBoxButtons.OK);   
            Empezar();
        }

        private void ProximoTurno()
        {
            MA.LimpiarTextBoxs(txtNumero);            
            txtNumero.Focus();
        }
        private int UnJuegoMas()
        {
            var cuantos = 0;
            try
            {
                if (File.Exists(@"C:\p.inx"))
                {
                    TextReader arch = new StreamReader(@"C:\p.inx");
                    var s = arch.ReadLine();
                    cuantos = int.Parse(s.Trim());
                    arch.Close();
                }

                TextWriter archivo = new StreamWriter(@"C:\p.inx", false);
                archivo.WriteLine(cuantos + 1);
                archivo.Close();
            }
            catch (Exception)
            {
                
                
            }
            
            return cuantos + 1;
        }
        #endregion

        #region Manejadores de eventos
        private void btnProbar_Click(object sender, EventArgs e)
        {
            if (Numero.EsValido(txtNumero.Text.Replace(" ", "")))
            {
                JuegaElJugador();                
                JuegaLaCompu();
            }
            else
            {
                MessageBox.Show("Ingresá un número de 4 cifras distintas.");
                txtNumero.Text = string.Empty;
                txtNumero.Focus();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Empezar();
            if (partidosJugados > 150)
            {
                MessageBox.Show("Esta es una versión gratis y ya ha jugado muchos partidos.");
                Close();
            }
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
            if( nroOprimido == -1)
            {
                if (txtNumero.Text.Length > 0)
                    txtNumero.Text = txtNumero.Text.Substring(0, txtNumero.Text.Length - 2);
            }
            else if( txtNumero.Text.Replace(" ", "").Length<4)
                txtNumero.Text += nroOprimido.ToString() + " ";
        }
    }
}