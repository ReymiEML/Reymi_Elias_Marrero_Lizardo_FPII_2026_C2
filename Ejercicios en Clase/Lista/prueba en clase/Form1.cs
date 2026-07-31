namespace prueba_en_clase
{
    public partial class Form1 : Form
    {
        List<Elementos> element = new List<Elementos>();


        public class Elementos
        {
            public string NombreElemento { get; set; }
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            string elemento = txtElemento.Text;

            if (!string.IsNullOrWhiteSpace(elemento))
            {
                element.Add(new Elementos { NombreElemento = elemento });

                txtCantidad.Text = element.Count.ToString();
            }


        }
    }
}
