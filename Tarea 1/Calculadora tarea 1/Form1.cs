namespace Calculadora_tarea_1
{
    public partial class Form1 : Form
    {
        private double valor1;
        private double valor2;

        private double resultado;

        private int operacion; // 1 = suma, 2 = resta, 3 = multiplicacion, 4 = division

        public Form1()
        {
            InitializeComponent();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            //numero 4
            tbdisplay.Text += "4";
        }

        private void Btn0_Click(object sender, EventArgs e)
        {
            //numero 0
            tbdisplay.Text += "0";
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            //numero 1
            tbdisplay.Text += "1";
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            //numero 2
            tbdisplay.Text += "2";

        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            //numero 3    
            tbdisplay.Text += "3";
        }

        private void Btn5_Click(object sender, EventArgs e)
        {
            //numero 5
            tbdisplay.Text += "5";
        }

        private void Btn6_Click(object sender, EventArgs e)
        {
            //numero 6
            tbdisplay.Text += "6";
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            //numero 7
            tbdisplay.Text += "7";
        }

        private void Btn8_Click(object sender, EventArgs e)
        {
            //numero 8
            tbdisplay.Text += "8";
        }

        private void Btn9_Click(object sender, EventArgs e)
        {
            //numero 9
            tbdisplay.Text += "9";
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Btnclear_Click(object sender, EventArgs e)
        {
            //boton limpiar
            tbdisplay.Text = "";
        }

        private void Btnigual_Click(object sender, EventArgs e)
        {
            //boton igual
            valor2 = Convert.ToDouble(tbdisplay.Text);
            switch (operacion)
            {
                case 1:
                    resultado = valor1 + valor2;
                    break;
                case 2:
                    resultado = valor1 - valor2;
                    break;
                case 3:
                    resultado = valor1 * valor2;
                    break;
                case 4:
                    resultado = valor1 / valor2;
                    break;
            }
            tbdisplay.Text = resultado.ToString();
        }

        private void btnsuma_Click(object sender, EventArgs e)
        {
            //boton suma
            operacion = 1;
            valor1 = Convert.ToDouble(tbdisplay.Text);
            tbdisplay.Text = "";
        }

        private void btnresta_Click(object sender, EventArgs e)
        {
            //boto resta
            operacion = 2;
            valor1 = Convert.ToDouble(tbdisplay.Text);
            tbdisplay.Text = "";
        }

        private void btnmultiplicacion_Click(object sender, EventArgs e)
        {
            //boton multiplicacion
            operacion = 3;
            valor1 = Convert.ToDouble(tbdisplay.Text);
            tbdisplay.Text = "";
        }

        private void Btndivision_Click(object sender, EventArgs e)
        {
            //boton division
            operacion = 4;
            valor1 = Convert.ToDouble(tbdisplay.Text);
            tbdisplay.Text = "";
        }

        private void btnpunto_Click(object sender, EventArgs e)
        {
            //boton punto

            tbdisplay.Text += ".";
            
        }
    }
}
