using RiskApp.Models;

namespace RiskApp.Forms;

public class FrmProyecto : Form
{
    private readonly Proyecto? _proyecto;
    private readonly TextBox txtNombre = new() { Text = "" };
    private readonly TextBox txtCliente = new() { Text = "" };
    private readonly TextBox txtDescripcion = new() { Text = "" };
    private readonly ComboBox cmbEvaluador = new();
    private readonly Label lblFecha = new();

    public FrmProyecto(Proyecto? proyecto = null)
    {
        _proyecto = proyecto;
        Theme.ApplyForm(this);
        Theme.ApplyIcon(this);
        Text = proyecto == null ? "Nuevo Proyecto" : "Editar Proyecto";
        Size = new Size(500, 320);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        int lblW = 130, txtW = 310, xLbl = 15, xTxt = 150, y = 20, h = 25, gap = 35;

        AddLabel("Nombre del Proyecto:", xLbl, y, lblW, h);
        txtNombre.Location = new Point(xTxt, y); txtNombre.Size = new Size(txtW, h);
        Theme.ApplyTextBox(txtNombre);
        Controls.Add(txtNombre); y += gap;

        AddLabel("Cliente:", xLbl, y, lblW, h);
        txtCliente.Location = new Point(xTxt, y); txtCliente.Size = new Size(txtW, h);
        Theme.ApplyTextBox(txtCliente);
        Controls.Add(txtCliente); y += gap;

        AddLabel("Descripción:", xLbl, y, lblW, h);
        txtDescripcion.Location = new Point(xTxt, y); txtDescripcion.Size = new Size(txtW, 60);
        txtDescripcion.Multiline = true;
        txtDescripcion.ScrollBars = ScrollBars.Vertical;
        Theme.ApplyTextBox(txtDescripcion);
        Controls.Add(txtDescripcion); y += 70;

        AddLabel("Evaluador:", xLbl, y, lblW, h);
        cmbEvaluador.Location = new Point(xTxt, y); cmbEvaluador.Size = new Size(txtW, h);
        cmbEvaluador.DropDownStyle = ComboBoxStyle.DropDown;
        cmbEvaluador.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        cmbEvaluador.AutoCompleteSource = AutoCompleteSource.ListItems;
        cmbEvaluador.BackColor = Theme.Card;
        cmbEvaluador.ForeColor = Theme.TextPrimary;
        Controls.Add(cmbEvaluador); y += gap;

        AddLabel("Fecha Creación:", xLbl, y, lblW, h);
        lblFecha.Location = new Point(xTxt, y); lblFecha.Size = new Size(txtW, h);
        lblFecha.ForeColor = Theme.TextSecondary;
        Controls.Add(lblFecha); y += gap + 10;

        var btnGuardar = Theme.MakeButton("Guardar", true);
        btnGuardar.Location = new Point(290, y);

        var btnCancelar = Theme.MakeButton("Cancelar");
        btnCancelar.Location = new Point(398, y);
        btnCancelar.Click += (_, _) => DialogResult = DialogResult.Cancel;

        btnGuardar.Click += Guardar;
        Controls.AddRange(new Control[] { btnGuardar, btnCancelar });

        CargarEvaluadores();
        if (proyecto != null) CargarDatos();
    }

    private void AddLabel(string text, int x, int y, int w, int h)
    {
        var lbl = new Label { Text = text, Location = new Point(x, y), Size = new Size(w, h), TextAlign = ContentAlignment.MiddleLeft };
        Theme.ApplyLabel(lbl, true);
        Controls.Add(lbl);
    }

    private void CargarEvaluadores()
    {
        var nombres = Database.GetEvaluadores().Select(e => e.Nombre).ToList();
        cmbEvaluador.DataSource = nombres;
    }

    private void CargarDatos()
    {
        txtNombre.Text = _proyecto!.NombreProyecto;
        txtCliente.Text = _proyecto.Cliente;
        txtDescripcion.Text = _proyecto.Descripcion;
        lblFecha.Text = _proyecto.FechaCreacion.ToString("dd/MM/yyyy HH:mm");
        cmbEvaluador.Text = Database.GetEvaluador(_proyecto.EvaluadorId)?.Nombre ?? "";
    }

    private void Guardar(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            MessageBox.Show("El nombre del proyecto es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var nombreEval = cmbEvaluador.Text.Trim();
        if (string.IsNullOrWhiteSpace(nombreEval))
        {
            MessageBox.Show("Seleccione o escriba un evaluador.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var evaluadores = Database.GetEvaluadores();
        var evaluador = evaluadores.FirstOrDefault(ev => ev.Nombre.Equals(nombreEval, StringComparison.OrdinalIgnoreCase));
        if (evaluador == null)
        {
            evaluador = new Evaluador { Nombre = nombreEval };
            Database.InsertEvaluador(evaluador);
        }

        if (_proyecto == null)
        {
            var nuevo = new Proyecto
            {
                NombreProyecto = txtNombre.Text.Trim(),
                Cliente = txtCliente.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                FechaCreacion = DateTime.Now,
                EvaluadorId = evaluador.Id
            };
            Database.InsertProyecto(nuevo);
        }
        else
        {
            _proyecto.NombreProyecto = txtNombre.Text.Trim();
            _proyecto.Cliente = txtCliente.Text.Trim();
            _proyecto.Descripcion = txtDescripcion.Text.Trim();
            _proyecto.EvaluadorId = evaluador.Id;
            Database.UpdateProyecto(_proyecto);
        }

        DialogResult = DialogResult.OK;
    }
}
