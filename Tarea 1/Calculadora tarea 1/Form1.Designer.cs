namespace Calculadora_tarea_1
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
            tbdisplay = new TextBox();
            btnsuma = new Button();
            btnresta = new Button();
            btnmultiplicacion = new Button();
            btndivision = new Button();
            btn7 = new Button();
            btn8 = new Button();
            btn9 = new Button();
            btn4 = new Button();
            btn5 = new Button();
            btn6 = new Button();
            btn1 = new Button();
            btn2 = new Button();
            btn3 = new Button();
            btnpunto = new Button();
            btn0 = new Button();
            btnigual = new Button();
            btnclear = new Button();
            SuspendLayout();
            // 
            // tbdisplay
            // 
            tbdisplay.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            tbdisplay.Location = new Point(8, 22);
            tbdisplay.Multiline = true;
            tbdisplay.Name = "tbdisplay";
            tbdisplay.RightToLeft = RightToLeft.Yes;
            tbdisplay.Size = new Size(296, 53);
            tbdisplay.TabIndex = 0;
            tbdisplay.TextChanged += TextBox1_TextChanged;
            // 
            // btnsuma
            // 
            btnsuma.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnsuma.Location = new Point(10, 93);
            btnsuma.Name = "btnsuma";
            btnsuma.Size = new Size(85, 39);
            btnsuma.TabIndex = 1;
            btnsuma.Text = "+";
            btnsuma.UseVisualStyleBackColor = true;
            btnsuma.Click += btnsuma_Click;
            // 
            // btnresta
            // 
            btnresta.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnresta.Location = new Point(100, 93);
            btnresta.Name = "btnresta";
            btnresta.Size = new Size(85, 39);
            btnresta.TabIndex = 2;
            btnresta.Text = "-";
            btnresta.UseVisualStyleBackColor = true;
            btnresta.Click += btnresta_Click;
            // 
            // btnmultiplicacion
            // 
            btnmultiplicacion.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnmultiplicacion.Location = new Point(192, 93);
            btnmultiplicacion.Name = "btnmultiplicacion";
            btnmultiplicacion.Size = new Size(85, 39);
            btnmultiplicacion.TabIndex = 3;
            btnmultiplicacion.Text = "x";
            btnmultiplicacion.UseVisualStyleBackColor = true;
            btnmultiplicacion.Click += btnmultiplicacion_Click;
            // 
            // btndivision
            // 
            btndivision.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btndivision.Location = new Point(282, 93);
            btndivision.Name = "btndivision";
            btndivision.Size = new Size(85, 39);
            btndivision.TabIndex = 4;
            btndivision.Text = "/";
            btndivision.UseVisualStyleBackColor = true;
            btndivision.Click += Btndivision_Click;
            // 
            // btn7
            // 
            btn7.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn7.Location = new Point(8, 153);
            btn7.Name = "btn7";
            btn7.Size = new Size(98, 40);
            btn7.TabIndex = 5;
            btn7.Text = "7";
            btn7.UseVisualStyleBackColor = true;
            btn7.Click += Btn7_Click;
            // 
            // btn8
            // 
            btn8.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn8.Location = new Point(132, 153);
            btn8.Name = "btn8";
            btn8.Size = new Size(98, 40);
            btn8.TabIndex = 6;
            btn8.Text = "8";
            btn8.UseVisualStyleBackColor = true;
            btn8.Click += Btn8_Click;
            // 
            // btn9
            // 
            btn9.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn9.Location = new Point(258, 153);
            btn9.Name = "btn9";
            btn9.Size = new Size(98, 40);
            btn9.TabIndex = 7;
            btn9.Text = "9";
            btn9.UseVisualStyleBackColor = true;
            btn9.Click += Btn9_Click;
            // 
            // btn4
            // 
            btn4.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn4.Location = new Point(10, 218);
            btn4.Name = "btn4";
            btn4.Size = new Size(98, 40);
            btn4.TabIndex = 8;
            btn4.Text = "4";
            btn4.UseVisualStyleBackColor = true;
            btn4.Click += Button8_Click;
            // 
            // btn5
            // 
            btn5.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn5.Location = new Point(132, 218);
            btn5.Name = "btn5";
            btn5.Size = new Size(98, 40);
            btn5.TabIndex = 9;
            btn5.Text = "5";
            btn5.UseVisualStyleBackColor = true;
            btn5.Click += Btn5_Click;
            // 
            // btn6
            // 
            btn6.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn6.Location = new Point(258, 218);
            btn6.Name = "btn6";
            btn6.Size = new Size(98, 40);
            btn6.TabIndex = 10;
            btn6.Text = "6";
            btn6.UseVisualStyleBackColor = true;
            btn6.Click += Btn6_Click;
            // 
            // btn1
            // 
            btn1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn1.Location = new Point(8, 283);
            btn1.Name = "btn1";
            btn1.Size = new Size(98, 40);
            btn1.TabIndex = 11;
            btn1.Text = "1";
            btn1.UseVisualStyleBackColor = true;
            btn1.Click += Btn1_Click;
            // 
            // btn2
            // 
            btn2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn2.Location = new Point(132, 283);
            btn2.Name = "btn2";
            btn2.Size = new Size(98, 40);
            btn2.TabIndex = 12;
            btn2.Text = "2";
            btn2.UseVisualStyleBackColor = true;
            btn2.Click += Btn2_Click;
            // 
            // btn3
            // 
            btn3.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn3.Location = new Point(258, 283);
            btn3.Name = "btn3";
            btn3.Size = new Size(98, 40);
            btn3.TabIndex = 13;
            btn3.Text = "3";
            btn3.UseVisualStyleBackColor = true;
            btn3.Click += Btn3_Click;
            // 
            // btnpunto
            // 
            btnpunto.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnpunto.Location = new Point(8, 354);
            btnpunto.Name = "btnpunto";
            btnpunto.Size = new Size(98, 40);
            btnpunto.TabIndex = 14;
            btnpunto.Text = ".";
            btnpunto.UseVisualStyleBackColor = true;
            btnpunto.Click += btnpunto_Click;
            // 
            // btn0
            // 
            btn0.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn0.Location = new Point(132, 354);
            btn0.Name = "btn0";
            btn0.Size = new Size(98, 40);
            btn0.TabIndex = 15;
            btn0.Text = "0";
            btn0.UseVisualStyleBackColor = true;
            btn0.Click += Btn0_Click;
            // 
            // btnigual
            // 
            btnigual.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnigual.Location = new Point(258, 354);
            btnigual.Name = "btnigual";
            btnigual.Size = new Size(98, 40);
            btnigual.TabIndex = 16;
            btnigual.Text = "=";
            btnigual.UseVisualStyleBackColor = true;
            btnigual.Click += Btnigual_Click;
            // 
            // btnclear
            // 
            btnclear.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnclear.Location = new Point(310, 22);
            btnclear.Name = "btnclear";
            btnclear.Size = new Size(59, 53);
            btnclear.TabIndex = 17;
            btnclear.Text = "C";
            btnclear.UseVisualStyleBackColor = true;
            btnclear.Click += Btnclear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(381, 450);
            Controls.Add(btnclear);
            Controls.Add(btnigual);
            Controls.Add(btn0);
            Controls.Add(btnpunto);
            Controls.Add(btn3);
            Controls.Add(btn2);
            Controls.Add(btn1);
            Controls.Add(btn6);
            Controls.Add(btn5);
            Controls.Add(btn4);
            Controls.Add(btn9);
            Controls.Add(btn8);
            Controls.Add(btn7);
            Controls.Add(btndivision);
            Controls.Add(btnmultiplicacion);
            Controls.Add(btnresta);
            Controls.Add(btnsuma);
            Controls.Add(tbdisplay);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbdisplay;
        private Button btnsuma;
        private Button btnresta;
        private Button btnmultiplicacion;
        private Button btndivision;
        private Button btn7;
        private Button btn8;
        private Button btn9;
        private Button btn4;
        private Button btn5;
        private Button btn6;
        private Button btn1;
        private Button btn2;
        private Button btn3;
        private Button btnpunto;
        private Button btn0;
        private Button btnigual;
        private Button btnclear;
    }
}
