namespace WindowsFormsApplication1
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtHistoriaCompu = new System.Windows.Forms.TextBox();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.txtHistoriaJugador = new System.Windows.Forms.TextBox();
            this.btnProbar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNumero = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblNumeroAAdivinarXLaCompu = new System.Windows.Forms.Label();
            this.tecladoDeDigitos1 = new WindowsFormsApplication1.TecladoDeDigitos();
            this.SuspendLayout();
            // 
            // txtHistoriaCompu
            // 
            this.txtHistoriaCompu.BackColor = System.Drawing.Color.LightSteelBlue;
            this.txtHistoriaCompu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHistoriaCompu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHistoriaCompu.Location = new System.Drawing.Point(137, 128);
            this.txtHistoriaCompu.MaximumSize = new System.Drawing.Size(550, 500);
            this.txtHistoriaCompu.Multiline = true;
            this.txtHistoriaCompu.Name = "txtHistoriaCompu";
            this.txtHistoriaCompu.ReadOnly = true;
            this.txtHistoriaCompu.Size = new System.Drawing.Size(144, 147);
            this.txtHistoriaCompu.TabIndex = 6;
            // 
            // txtNumero
            // 
            this.txtNumero.BackColor = System.Drawing.Color.LightSteelBlue;
            this.txtNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumero.Location = new System.Drawing.Point(400, 102);
            this.txtNumero.MaxLength = 4;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(144, 20);
            this.txtNumero.TabIndex = 0;
            this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumero_KeyPress);
            // 
            // txtHistoriaJugador
            // 
            this.txtHistoriaJugador.BackColor = System.Drawing.Color.LightSteelBlue;
            this.txtHistoriaJugador.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHistoriaJugador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHistoriaJugador.Location = new System.Drawing.Point(400, 128);
            this.txtHistoriaJugador.MaximumSize = new System.Drawing.Size(550, 500);
            this.txtHistoriaJugador.Multiline = true;
            this.txtHistoriaJugador.Name = "txtHistoriaJugador";
            this.txtHistoriaJugador.ReadOnly = true;
            this.txtHistoriaJugador.Size = new System.Drawing.Size(144, 147);
            this.txtHistoriaJugador.TabIndex = 12;
            // 
            // btnProbar
            // 
            this.btnProbar.BackColor = System.Drawing.Color.Transparent;
            this.btnProbar.Location = new System.Drawing.Point(296, 252);
            this.btnProbar.Name = "btnProbar";
            this.btnProbar.Size = new System.Drawing.Size(90, 23);
            this.btnProbar.TabIndex = 13;
            this.btnProbar.Text = "Probar";
            this.btnProbar.UseVisualStyleBackColor = false;
            this.btnProbar.Click += new System.EventHandler(this.btnProbar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(397, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Vos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(134, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "La compu";
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(197, 76);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(0, 13);
            this.lblNumero.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(134, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "1234 (12341 opciones)";
            // 
            // lblNumeroAAdivinarXLaCompu
            // 
            this.lblNumeroAAdivinarXLaCompu.AutoSize = true;
            this.lblNumeroAAdivinarXLaCompu.BackColor = System.Drawing.Color.Transparent;
            this.lblNumeroAAdivinarXLaCompu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblNumeroAAdivinarXLaCompu.Location = new System.Drawing.Point(134, 111);
            this.lblNumeroAAdivinarXLaCompu.Name = "lblNumeroAAdivinarXLaCompu";
            this.lblNumeroAAdivinarXLaCompu.Size = new System.Drawing.Size(35, 13);
            this.lblNumeroAAdivinarXLaCompu.TabIndex = 18;
            this.lblNumeroAAdivinarXLaCompu.Text = "1234";
            // 
            // tecladoDeDigitos1
            // 
            this.tecladoDeDigitos1.BackColor = System.Drawing.Color.Transparent;
            this.tecladoDeDigitos1.Location = new System.Drawing.Point(296, 128);
            this.tecladoDeDigitos1.Name = "tecladoDeDigitos1";
            this.tecladoDeDigitos1.Size = new System.Drawing.Size(98, 118);
            this.tecladoDeDigitos1.TabIndex = 19;
            this.tecladoDeDigitos1.Oprimir += new WindowsFormsApplication1.Oprimir(this.tecladoDeDigitos1_Oprimir);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnProbar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(705, 365);
            this.Controls.Add(this.tecladoDeDigitos1);
            this.Controls.Add(this.lblNumeroAAdivinarXLaCompu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblNumero);
            this.Controls.Add(this.btnProbar);
            this.Controls.Add(this.txtHistoriaJugador);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.txtHistoriaCompu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "La adivinanza de Juan Cruz";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.TextBox txtHistoriaCompu;
		private System.Windows.Forms.TextBox txtNumero;
		private System.Windows.Forms.TextBox txtHistoriaJugador;
		private System.Windows.Forms.Button btnProbar;
        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNumeroAAdivinarXLaCompu;
        private TecladoDeDigitos tecladoDeDigitos1;
	}
}

