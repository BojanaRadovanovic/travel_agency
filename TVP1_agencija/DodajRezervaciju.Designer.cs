namespace TVP1_agencija
{
    partial class DodajRezervaciju
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
            this.btnRezervisi = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.datum = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.brMesta = new System.Windows.Forms.TextBox();
            this.mesto = new System.Windows.Forms.TextBox();
            this.cmdMesto = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMesto = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnRezervisi
            // 
            this.btnRezervisi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRezervisi.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRezervisi.Location = new System.Drawing.Point(302, 397);
            this.btnRezervisi.Name = "btnRezervisi";
            this.btnRezervisi.Size = new System.Drawing.Size(121, 41);
            this.btnRezervisi.TabIndex = 15;
            this.btnRezervisi.Text = "Rezerviši";
            this.btnRezervisi.UseVisualStyleBackColor = false;
            this.btnRezervisi.Click += new System.EventHandler(this.btnRezervisi_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(12, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 25);
            this.label3.TabIndex = 14;
            this.label3.Text = "Za koliko osoba?";
            // 
            // datum
            // 
            this.datum.Location = new System.Drawing.Point(174, 200);
            this.datum.Name = "datum";
            this.datum.Size = new System.Drawing.Size(200, 22);
            this.datum.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(28, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Datum";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(447, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "Izaberite mesto";
            // 
            // brMesta
            // 
            this.brMesta.Location = new System.Drawing.Point(220, 109);
            this.brMesta.Multiline = true;
            this.brMesta.Name = "brMesta";
            this.brMesta.Size = new System.Drawing.Size(154, 32);
            this.brMesta.TabIndex = 10;
            // 
            // mesto
            // 
            this.mesto.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mesto.Location = new System.Drawing.Point(442, 71);
            this.mesto.Multiline = true;
            this.mesto.Name = "mesto";
            this.mesto.Size = new System.Drawing.Size(346, 367);
            this.mesto.TabIndex = 9;
            // 
            // cmdMesto
            // 
            this.cmdMesto.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMesto.FormattingEnabled = true;
            this.cmdMesto.Location = new System.Drawing.Point(613, 28);
            this.cmdMesto.Name = "cmdMesto";
            this.cmdMesto.Size = new System.Drawing.Size(132, 28);
            this.cmdMesto.TabIndex = 20;
            this.cmdMesto.SelectedIndexChanged += new System.EventHandler(this.cmdMesto_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(115, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 25);
            this.label4.TabIndex = 21;
            this.label4.Text = "Mesto";
            // 
            // txtMesto
            // 
            this.txtMesto.Location = new System.Drawing.Point(220, 49);
            this.txtMesto.Multiline = true;
            this.txtMesto.Name = "txtMesto";
            this.txtMesto.Size = new System.Drawing.Size(154, 32);
            this.txtMesto.TabIndex = 22;
            // 
            // DodajRezervaciju
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtMesto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdMesto);
            this.Controls.Add(this.btnRezervisi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.datum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.brMesta);
            this.Controls.Add(this.mesto);
            this.Name = "DodajRezervaciju";
            this.Text = "DodajRezervaciju";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRezervisi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker datum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox brMesta;
        private System.Windows.Forms.TextBox mesto;
        private System.Windows.Forms.ComboBox cmdMesto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMesto;
    }
}