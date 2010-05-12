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
			this.lblBien = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtBien = new System.Windows.Forms.TextBox();
			this.txtRegular = new System.Windows.Forms.TextBox();
			this.btnAdivinar = new System.Windows.Forms.Button();
			this.txtMensajes = new System.Windows.Forms.TextBox();
			this.lblNumeroGenerado = new System.Windows.Forms.Label();
			this.lblNumero = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblBien
			// 
			this.lblBien.AutoSize = true;
			this.lblBien.Location = new System.Drawing.Point(17, 59);
			this.lblBien.Name = "lblBien";
			this.lblBien.Size = new System.Drawing.Size(28, 13);
			this.lblBien.TabIndex = 0;
			this.lblBien.Text = "Bien";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Regular";
			// 
			// txtBien
			// 
			this.txtBien.Location = new System.Drawing.Point(67, 56);
			this.txtBien.Name = "txtBien";
			this.txtBien.Size = new System.Drawing.Size(100, 20);
			this.txtBien.TabIndex = 2;
			// 
			// txtRegular
			// 
			this.txtRegular.Location = new System.Drawing.Point(67, 85);
			this.txtRegular.Name = "txtRegular";
			this.txtRegular.Size = new System.Drawing.Size(100, 20);
			this.txtRegular.TabIndex = 3;
			// 
			// btnAdivinar
			// 
			this.btnAdivinar.Location = new System.Drawing.Point(92, 111);
			this.btnAdivinar.Name = "btnAdivinar";
			this.btnAdivinar.Size = new System.Drawing.Size(75, 23);
			this.btnAdivinar.TabIndex = 4;
			this.btnAdivinar.Text = "Adivinar";
			this.btnAdivinar.UseVisualStyleBackColor = true;
			this.btnAdivinar.Click += new System.EventHandler(this.btnAdivinar_Click);
			// 
			// txtMensajes
			// 
			this.txtMensajes.Location = new System.Drawing.Point(173, 12);
			this.txtMensajes.MaximumSize = new System.Drawing.Size(550, 500);
			this.txtMensajes.Multiline = true;
			this.txtMensajes.Name = "txtMensajes";
			this.txtMensajes.Size = new System.Drawing.Size(222, 125);
			this.txtMensajes.TabIndex = 6;
			// 
			// lblNumeroGenerado
			// 
			this.lblNumeroGenerado.AutoSize = true;
			this.lblNumeroGenerado.Location = new System.Drawing.Point(64, 15);
			this.lblNumeroGenerado.Name = "lblNumeroGenerado";
			this.lblNumeroGenerado.Size = new System.Drawing.Size(0, 13);
			this.lblNumeroGenerado.TabIndex = 7;
			// 
			// lblNumero
			// 
			this.lblNumero.AutoSize = true;
			this.lblNumero.Location = new System.Drawing.Point(70, 27);
			this.lblNumero.Name = "lblNumero";
			this.lblNumero.Size = new System.Drawing.Size(0, 13);
			this.lblNumero.TabIndex = 8;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(403, 152);
			this.Controls.Add(this.lblNumero);
			this.Controls.Add(this.lblNumeroGenerado);
			this.Controls.Add(this.txtMensajes);
			this.Controls.Add(this.btnAdivinar);
			this.Controls.Add(this.txtRegular);
			this.Controls.Add(this.txtBien);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblBien);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblBien;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtBien;
		private System.Windows.Forms.TextBox txtRegular;
		private System.Windows.Forms.Button btnAdivinar;
		private System.Windows.Forms.TextBox txtMensajes;
		private System.Windows.Forms.Label lblNumeroGenerado;
		private System.Windows.Forms.Label lblNumero;
	}
}

