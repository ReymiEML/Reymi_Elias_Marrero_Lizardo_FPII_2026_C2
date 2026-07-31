namespace prueba_en_clase
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtCantidad = new TextBox();
            btnAdd = new Button();
            txtElemento = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtCantidad
            // 
            txtCantidad.BackColor = SystemColors.ButtonHighlight;
            txtCantidad.Enabled = false;
            txtCantidad.Location = new Point(289, 270);
            txtCantidad.Multiline = true;
            txtCantidad.Name = "txtCantidad";
            txtCantidad.ReadOnly = true;
            txtCantidad.Size = new Size(279, 42);
            txtCantidad.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(334, 342);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(173, 49);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Añadir";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtElemento
            // 
            txtElemento.Location = new Point(289, 136);
            txtElemento.Multiline = true;
            txtElemento.Name = "txtElemento";
            txtElemento.Size = new Size(279, 49);
            txtElemento.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(392, 236);
            label1.Name = "label1";
            label1.Size = new Size(69, 20);
            label1.TabIndex = 3;
            label1.Text = "Cantidad";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(392, 104);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 4;
            label2.Text = "Elemento";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtElemento);
            Controls.Add(btnAdd);
            Controls.Add(txtCantidad);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCantidad;
        private Button btnAdd;
        private TextBox txtElemento;
        private Label label1;
        private Label label2;
    }
}
