namespace turnosTestForm
{
    partial class frmRecursos
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
            this.dgDatos = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnLimpiarCampos = new System.Windows.Forms.Button();
            this.btnLeer = new System.Windows.Forms.Button();
            this.btnListar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbClientes = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tiNombre = new System.Windows.Forms.TextBox();
            this.tiId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_AgregarDisponibilidad = new System.Windows.Forms.Button();
            this.lstDisponibilidades = new System.Windows.Forms.ListBox();
            this.lstDias = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tiHoraFinal = new System.Windows.Forms.TextBox();
            this.tiHoraInicial = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tiAdicionalHF = new System.Windows.Forms.TextBox();
            this.tiAdicionalHI = new System.Windows.Forms.TextBox();
            this.lstDiasAdicionales = new System.Windows.Forms.ListBox();
            this.btn_AgregarDiaAdicional = new System.Windows.Forms.Button();
            this.dtpDisponible = new System.Windows.Forms.DateTimePicker();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tiNoDisponiblelHF = new System.Windows.Forms.TextBox();
            this.tiNoDisponiblelHI = new System.Windows.Forms.TextBox();
            this.lstNoDisponible = new System.Windows.Forms.ListBox();
            this.btn_AgregarNoDisponible = new System.Windows.Forms.Button();
            this.dtpNoDisponible = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tiDuracion = new System.Windows.Forms.TextBox();
            this.cmbClienteBuscarTurno = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCalendarioDisponibilidadTest = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tiHoraFTurnoLibre = new System.Windows.Forms.TextBox();
            this.dpDiaTurnosLibres = new System.Windows.Forms.DateTimePicker();
            this.tiHoraITurnoLibre = new System.Windows.Forms.TextBox();
            this.btnBuscarLibres = new System.Windows.Forms.Button();
            this.lstTurnosLibres = new System.Windows.Forms.ListBox();
            this.tiJSon = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgDatos)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgDatos
            // 
            this.dgDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDatos.Location = new System.Drawing.Point(229, 64);
            this.dgDatos.MultiSelect = false;
            this.dgDatos.Name = "dgDatos";
            this.dgDatos.Size = new System.Drawing.Size(554, 143);
            this.dgDatos.TabIndex = 4;
            this.dgDatos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDatos_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBuscar);
            this.groupBox2.Controls.Add(this.btnLimpiarCampos);
            this.groupBox2.Controls.Add(this.btnLeer);
            this.groupBox2.Controls.Add(this.btnListar);
            this.groupBox2.Controls.Add(this.btnGuardar);
            this.groupBox2.Controls.Add(this.btnBorrar);
            this.groupBox2.Location = new System.Drawing.Point(12, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(771, 57);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Acciones";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(97, 19);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 7;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // btnLimpiarCampos
            // 
            this.btnLimpiarCampos.Location = new System.Drawing.Point(690, 19);
            this.btnLimpiarCampos.Name = "btnLimpiarCampos";
            this.btnLimpiarCampos.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiarCampos.TabIndex = 6;
            this.btnLimpiarCampos.Text = "Limpiar Campos";
            this.btnLimpiarCampos.UseVisualStyleBackColor = true;
            this.btnLimpiarCampos.Click += new System.EventHandler(this.btnLimpiarCampos_Click);
            // 
            // btnLeer
            // 
            this.btnLeer.Location = new System.Drawing.Point(6, 19);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(75, 23);
            this.btnLeer.TabIndex = 2;
            this.btnLeer.Text = "Buscar x Id";
            this.btnLeer.UseVisualStyleBackColor = true;
            // 
            // btnListar
            // 
            this.btnListar.Location = new System.Drawing.Point(352, 19);
            this.btnListar.Name = "btnListar";
            this.btnListar.Size = new System.Drawing.Size(75, 23);
            this.btnListar.TabIndex = 5;
            this.btnListar.Text = "Listar";
            this.btnListar.UseVisualStyleBackColor = true;
            this.btnListar.Click += new System.EventHandler(this.btnListar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(190, 19);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnBorrar
            // 
            this.btnBorrar.Location = new System.Drawing.Point(271, 19);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(75, 23);
            this.btnBorrar.TabIndex = 4;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = true;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbClientes);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tiNombre);
            this.groupBox1.Controls.Add(this.tiId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 105);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Usuario";
            // 
            // cmbClientes
            // 
            this.cmbClientes.FormattingEnabled = true;
            this.cmbClientes.Location = new System.Drawing.Point(77, 47);
            this.cmbClientes.Name = "cmbClientes";
            this.cmbClientes.Size = new System.Drawing.Size(123, 21);
            this.cmbClientes.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Cliente";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // tiNombre
            // 
            this.tiNombre.Location = new System.Drawing.Point(77, 73);
            this.tiNombre.Name = "tiNombre";
            this.tiNombre.Size = new System.Drawing.Size(123, 20);
            this.tiNombre.TabIndex = 7;
            // 
            // tiId
            // 
            this.tiId.Location = new System.Drawing.Point(77, 12);
            this.tiId.Name = "tiId";
            this.tiId.Size = new System.Drawing.Size(123, 20);
            this.tiId.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Id";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_AgregarDisponibilidad);
            this.groupBox3.Controls.Add(this.lstDisponibilidades);
            this.groupBox3.Controls.Add(this.lstDias);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tiHoraFinal);
            this.groupBox3.Controls.Add(this.tiHoraInicial);
            this.groupBox3.Location = new System.Drawing.Point(6, 299);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(265, 200);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Disponibilidad";
            // 
            // btn_AgregarDisponibilidad
            // 
            this.btn_AgregarDisponibilidad.Location = new System.Drawing.Point(6, 166);
            this.btn_AgregarDisponibilidad.Name = "btn_AgregarDisponibilidad";
            this.btn_AgregarDisponibilidad.Size = new System.Drawing.Size(253, 26);
            this.btn_AgregarDisponibilidad.TabIndex = 26;
            this.btn_AgregarDisponibilidad.Text = "Agregar";
            this.btn_AgregarDisponibilidad.UseVisualStyleBackColor = true;
            this.btn_AgregarDisponibilidad.Click += new System.EventHandler(this.btn_AgregarDisponibilidad_Click);
            // 
            // lstDisponibilidades
            // 
            this.lstDisponibilidades.FormattingEnabled = true;
            this.lstDisponibilidades.Location = new System.Drawing.Point(139, 52);
            this.lstDisponibilidades.Name = "lstDisponibilidades";
            this.lstDisponibilidades.Size = new System.Drawing.Size(120, 108);
            this.lstDisponibilidades.TabIndex = 31;
            // 
            // lstDias
            // 
            this.lstDias.FormattingEnabled = true;
            this.lstDias.Items.AddRange(new object[] {
            "Lunes",
            "Martes",
            "Miercoles",
            "Jueves",
            "Viernes",
            "Sabado",
            "Domingo"});
            this.lstDias.Location = new System.Drawing.Point(6, 52);
            this.lstDias.Name = "lstDias";
            this.lstDias.Size = new System.Drawing.Size(120, 108);
            this.lstDias.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "a";
            // 
            // tiHoraFinal
            // 
            this.tiHoraFinal.Location = new System.Drawing.Point(76, 26);
            this.tiHoraFinal.Name = "tiHoraFinal";
            this.tiHoraFinal.Size = new System.Drawing.Size(50, 20);
            this.tiHoraFinal.TabIndex = 28;
            this.tiHoraFinal.Text = "18:00";
            // 
            // tiHoraInicial
            // 
            this.tiHoraInicial.Location = new System.Drawing.Point(11, 26);
            this.tiHoraInicial.Name = "tiHoraInicial";
            this.tiHoraInicial.Size = new System.Drawing.Size(40, 20);
            this.tiHoraInicial.TabIndex = 27;
            this.tiHoraInicial.Text = "9:00";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.tiAdicionalHF);
            this.groupBox4.Controls.Add(this.tiAdicionalHI);
            this.groupBox4.Controls.Add(this.lstDiasAdicionales);
            this.groupBox4.Controls.Add(this.btn_AgregarDiaAdicional);
            this.groupBox4.Controls.Add(this.dtpDisponible);
            this.groupBox4.Location = new System.Drawing.Point(283, 299);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(213, 200);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Dias Adicionales";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "a";
            // 
            // tiAdicionalHF
            // 
            this.tiAdicionalHF.Location = new System.Drawing.Point(76, 28);
            this.tiAdicionalHF.Name = "tiAdicionalHF";
            this.tiAdicionalHF.Size = new System.Drawing.Size(50, 20);
            this.tiAdicionalHF.TabIndex = 34;
            this.tiAdicionalHF.Text = "18:00";
            // 
            // tiAdicionalHI
            // 
            this.tiAdicionalHI.Location = new System.Drawing.Point(11, 28);
            this.tiAdicionalHI.Name = "tiAdicionalHI";
            this.tiAdicionalHI.Size = new System.Drawing.Size(40, 20);
            this.tiAdicionalHI.TabIndex = 33;
            this.tiAdicionalHI.Text = "9:00";
            // 
            // lstDiasAdicionales
            // 
            this.lstDiasAdicionales.FormattingEnabled = true;
            this.lstDiasAdicionales.Location = new System.Drawing.Point(6, 114);
            this.lstDiasAdicionales.Name = "lstDiasAdicionales";
            this.lstDiasAdicionales.Size = new System.Drawing.Size(200, 82);
            this.lstDiasAdicionales.TabIndex = 32;
            // 
            // btn_AgregarDiaAdicional
            // 
            this.btn_AgregarDiaAdicional.Location = new System.Drawing.Point(6, 82);
            this.btn_AgregarDiaAdicional.Name = "btn_AgregarDiaAdicional";
            this.btn_AgregarDiaAdicional.Size = new System.Drawing.Size(201, 26);
            this.btn_AgregarDiaAdicional.TabIndex = 32;
            this.btn_AgregarDiaAdicional.Text = "Agregar";
            this.btn_AgregarDiaAdicional.UseVisualStyleBackColor = true;
            this.btn_AgregarDiaAdicional.Click += new System.EventHandler(this.btn_AgregarDiaAdicional_Click);
            // 
            // dtpDisponible
            // 
            this.dtpDisponible.Location = new System.Drawing.Point(6, 56);
            this.dtpDisponible.Name = "dtpDisponible";
            this.dtpDisponible.Size = new System.Drawing.Size(200, 20);
            this.dtpDisponible.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.tiNoDisponiblelHF);
            this.groupBox5.Controls.Add(this.tiNoDisponiblelHI);
            this.groupBox5.Controls.Add(this.lstNoDisponible);
            this.groupBox5.Controls.Add(this.btn_AgregarNoDisponible);
            this.groupBox5.Controls.Add(this.dtpNoDisponible);
            this.groupBox5.Location = new System.Drawing.Point(502, 301);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(219, 198);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Dias No Disponible";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "a";
            // 
            // tiNoDisponiblelHF
            // 
            this.tiNoDisponiblelHF.Location = new System.Drawing.Point(83, 19);
            this.tiNoDisponiblelHF.Name = "tiNoDisponiblelHF";
            this.tiNoDisponiblelHF.Size = new System.Drawing.Size(50, 20);
            this.tiNoDisponiblelHF.TabIndex = 35;
            this.tiNoDisponiblelHF.Text = "18:00";
            // 
            // tiNoDisponiblelHI
            // 
            this.tiNoDisponiblelHI.Location = new System.Drawing.Point(6, 19);
            this.tiNoDisponiblelHI.Name = "tiNoDisponiblelHI";
            this.tiNoDisponiblelHI.Size = new System.Drawing.Size(40, 20);
            this.tiNoDisponiblelHI.TabIndex = 34;
            this.tiNoDisponiblelHI.Text = "9:00";
            // 
            // lstNoDisponible
            // 
            this.lstNoDisponible.FormattingEnabled = true;
            this.lstNoDisponible.Location = new System.Drawing.Point(6, 106);
            this.lstNoDisponible.Name = "lstNoDisponible";
            this.lstNoDisponible.Size = new System.Drawing.Size(200, 69);
            this.lstNoDisponible.TabIndex = 33;
            // 
            // btn_AgregarNoDisponible
            // 
            this.btn_AgregarNoDisponible.Location = new System.Drawing.Point(4, 68);
            this.btn_AgregarNoDisponible.Name = "btn_AgregarNoDisponible";
            this.btn_AgregarNoDisponible.Size = new System.Drawing.Size(202, 26);
            this.btn_AgregarNoDisponible.TabIndex = 33;
            this.btn_AgregarNoDisponible.Text = "Agregar";
            this.btn_AgregarNoDisponible.UseVisualStyleBackColor = true;
            this.btn_AgregarNoDisponible.Click += new System.EventHandler(this.btn_AgregarNoDisponible_Click);
            // 
            // dtpNoDisponible
            // 
            this.dtpNoDisponible.Location = new System.Drawing.Point(6, 42);
            this.dtpNoDisponible.Name = "dtpNoDisponible";
            this.dtpNoDisponible.Size = new System.Drawing.Size(200, 20);
            this.dtpNoDisponible.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 271);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(763, 25);
            this.label8.TabIndex = 40;
            this.label8.Text = "Datos de los dias y horarios disponibles (solo luego de que se creo el recurso)";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.tiDuracion);
            this.groupBox6.Controls.Add(this.cmbClienteBuscarTurno);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.btnCalendarioDisponibilidadTest);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.tiHoraFTurnoLibre);
            this.groupBox6.Controls.Add(this.dpDiaTurnosLibres);
            this.groupBox6.Controls.Add(this.tiHoraITurnoLibre);
            this.groupBox6.Controls.Add(this.btnBuscarLibres);
            this.groupBox6.Controls.Add(this.lstTurnosLibres);
            this.groupBox6.Location = new System.Drawing.Point(876, 48);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(275, 614);
            this.groupBox6.TabIndex = 41;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Buscar turno disponible";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(127, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 13);
            this.label10.TabIndex = 50;
            this.label10.Text = "Duracion Turno";
            // 
            // tiDuracion
            // 
            this.tiDuracion.Location = new System.Drawing.Point(214, 56);
            this.tiDuracion.Name = "tiDuracion";
            this.tiDuracion.Size = new System.Drawing.Size(50, 20);
            this.tiDuracion.TabIndex = 49;
            this.tiDuracion.Text = "60";
            // 
            // cmbClienteBuscarTurno
            // 
            this.cmbClienteBuscarTurno.FormattingEnabled = true;
            this.cmbClienteBuscarTurno.Location = new System.Drawing.Point(71, 80);
            this.cmbClienteBuscarTurno.Name = "cmbClienteBuscarTurno";
            this.cmbClienteBuscarTurno.Size = new System.Drawing.Size(193, 21);
            this.cmbClienteBuscarTurno.TabIndex = 48;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 47;
            this.label9.Text = "Cliente";
            // 
            // btnCalendarioDisponibilidadTest
            // 
            this.btnCalendarioDisponibilidadTest.Location = new System.Drawing.Point(10, 585);
            this.btnCalendarioDisponibilidadTest.Name = "btnCalendarioDisponibilidadTest";
            this.btnCalendarioDisponibilidadTest.Size = new System.Drawing.Size(254, 23);
            this.btnCalendarioDisponibilidadTest.TabIndex = 46;
            this.btnCalendarioDisponibilidadTest.Text = "Turnos Formato JSon";
            this.btnCalendarioDisponibilidadTest.UseVisualStyleBackColor = true;
            this.btnCalendarioDisponibilidadTest.Click += new System.EventHandler(this.btnCalendarioDisponibilidadTest_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "a";
            // 
            // tiHoraFTurnoLibre
            // 
            this.tiHoraFTurnoLibre.Location = new System.Drawing.Point(71, 54);
            this.tiHoraFTurnoLibre.Name = "tiHoraFTurnoLibre";
            this.tiHoraFTurnoLibre.Size = new System.Drawing.Size(50, 20);
            this.tiHoraFTurnoLibre.TabIndex = 44;
            this.tiHoraFTurnoLibre.Text = "18:00";
            // 
            // dpDiaTurnosLibres
            // 
            this.dpDiaTurnosLibres.Location = new System.Drawing.Point(6, 28);
            this.dpDiaTurnosLibres.Name = "dpDiaTurnosLibres";
            this.dpDiaTurnosLibres.Size = new System.Drawing.Size(258, 20);
            this.dpDiaTurnosLibres.TabIndex = 42;
            // 
            // tiHoraITurnoLibre
            // 
            this.tiHoraITurnoLibre.Location = new System.Drawing.Point(6, 54);
            this.tiHoraITurnoLibre.Name = "tiHoraITurnoLibre";
            this.tiHoraITurnoLibre.Size = new System.Drawing.Size(40, 20);
            this.tiHoraITurnoLibre.TabIndex = 43;
            this.tiHoraITurnoLibre.Text = "9:00";
            // 
            // btnBuscarLibres
            // 
            this.btnBuscarLibres.Location = new System.Drawing.Point(178, 115);
            this.btnBuscarLibres.Name = "btnBuscarLibres";
            this.btnBuscarLibres.Size = new System.Drawing.Size(86, 23);
            this.btnBuscarLibres.TabIndex = 40;
            this.btnBuscarLibres.Text = "Buscar Turnos";
            this.btnBuscarLibres.UseVisualStyleBackColor = true;
            this.btnBuscarLibres.Click += new System.EventHandler(this.btnBuscarLibres_Click);
            // 
            // lstTurnosLibres
            // 
            this.lstTurnosLibres.FormattingEnabled = true;
            this.lstTurnosLibres.Location = new System.Drawing.Point(10, 144);
            this.lstTurnosLibres.Name = "lstTurnosLibres";
            this.lstTurnosLibres.Size = new System.Drawing.Size(254, 433);
            this.lstTurnosLibres.TabIndex = 41;
            // 
            // tiJSon
            // 
            this.tiJSon.Location = new System.Drawing.Point(12, 566);
            this.tiJSon.Multiline = true;
            this.tiJSon.Name = "tiJSon";
            this.tiJSon.Size = new System.Drawing.Size(868, 90);
            this.tiJSon.TabIndex = 42;
            // 
            // frmRecursos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 674);
            this.Controls.Add(this.tiJSon);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgDatos);
            this.Name = "frmRecursos";
            this.Text = "Recursos - BETA !!";
            this.Load += new System.EventHandler(this.frmRecursos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgDatos)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgDatos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLimpiarCampos;
        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.Button btnListar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tiNombre;
        private System.Windows.Forms.TextBox tiId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_AgregarDisponibilidad;
        private System.Windows.Forms.ListBox lstDisponibilidades;
        private System.Windows.Forms.ListBox lstDias;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tiHoraFinal;
        private System.Windows.Forms.TextBox tiHoraInicial;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox lstDiasAdicionales;
        private System.Windows.Forms.Button btn_AgregarDiaAdicional;
        private System.Windows.Forms.DateTimePicker dtpDisponible;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox lstNoDisponible;
        private System.Windows.Forms.Button btn_AgregarNoDisponible;
        private System.Windows.Forms.DateTimePicker dtpNoDisponible;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tiAdicionalHF;
        private System.Windows.Forms.TextBox tiAdicionalHI;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tiNoDisponiblelHF;
        private System.Windows.Forms.TextBox tiNoDisponiblelHI;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tiHoraFTurnoLibre;
        private System.Windows.Forms.DateTimePicker dpDiaTurnosLibres;
        private System.Windows.Forms.TextBox tiHoraITurnoLibre;
        private System.Windows.Forms.Button btnBuscarLibres;
        private System.Windows.Forms.ListBox lstTurnosLibres;
        private System.Windows.Forms.Button btnCalendarioDisponibilidadTest;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tiDuracion;
        private System.Windows.Forms.ComboBox cmbClienteBuscarTurno;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tiJSon;
    }
}