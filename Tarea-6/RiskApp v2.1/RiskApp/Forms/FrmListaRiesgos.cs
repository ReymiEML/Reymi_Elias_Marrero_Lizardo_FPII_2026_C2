using RiskApp.Models;

namespace RiskApp.Forms;

public class FrmListaRiesgos : Form
{
    private readonly int _proyectoId;
    private readonly string _nombreProyecto;
    private readonly DataGridView dgv = new();
    private readonly Button btnAgregar;
    private readonly Button btnEditar;
    private readonly Button btnEliminar;
    private readonly Button btnExportar;
    private readonly Button btnVolver;

    public FrmListaRiesgos(int proyectoId, string nombreProyecto)
    {
        _proyectoId = proyectoId;
        _nombreProyecto = nombreProyecto;
        Theme.ApplyForm(this);
        Theme.ApplyIcon(this);
        Text = $"Riesgos — {nombreProyecto}";
        Size = new Size(800, 450);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        dgv.Location = new Point(12, 12);
        dgv.Size = new Size(760, 328);
        dgv.AllowUserToAddRows = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.AllowUserToResizeColumns = false;
        dgv.AllowUserToResizeRows = false;
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgv.ReadOnly = true;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.MultiSelect = false;
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgv.DoubleClick += Editar;
        Theme.StyleDataGridView(dgv);

        int y = 350;
        btnAgregar = Theme.MakeButton("Agregar Riesgo", true);
        btnAgregar.Location = new Point(12, y);
        btnAgregar.Click += (_, _) => { Hide(); using var f = new FrmAsistenteRiesgo(_proyectoId); f.ShowDialog(); Show(); Recargar(); };

        btnEditar = Theme.MakeButton("Editar");
        btnEditar.Location = new Point(150, y);
        btnEditar.Click += Editar;

        btnEliminar = Theme.MakeButton("Eliminar");
        btnEliminar.Location = new Point(258, y);
        btnEliminar.Click += Eliminar;

        btnExportar = Theme.MakeButton("Exportar a Excel");
        btnExportar.Location = new Point(568, y);
        btnExportar.Size = new Size(120, 32);
        btnExportar.Click += Exportar;

        btnVolver = Theme.MakeButton("Volver");
        btnVolver.Location = new Point(698, y);
        btnVolver.Size = new Size(74, 32);
        btnVolver.Click += (_, _) => Close();

        Controls.AddRange(new Control[] { dgv, btnAgregar, btnEditar, btnEliminar, btnExportar, btnVolver });

        Recargar();
    }

    private void Recargar()
    {
        dgv.AutoGenerateColumns = false;
        dgv.Columns.Clear();

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "NombreRiesgo",
            HeaderText = "Nombre del Riesgo",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "FechaCreacion",
            HeaderText = "Fecha de Creación",
            Width = 150,
            DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
        });

        dgv.DataSource = Database.GetRiesgos(_proyectoId);
    }

    private Riesgo? Sel() =>
        dgv.SelectedRows.Count > 0 ? dgv.SelectedRows[0].DataBoundItem as Riesgo : null;

    private void Editar(object? sender, EventArgs e)
    {
        var r = Sel();
        if (r == null) return;
        Hide();
        using var f = new FrmAsistenteRiesgo(_proyectoId, r);
        f.ShowDialog();
        Show();
        Recargar();
    }

    private void Eliminar(object? sender, EventArgs e)
    {
        var r = Sel();
        if (r == null) return;
        if (MessageBox.Show($"¿Eliminar \"{r.NombreRiesgo}\"?", "Confirmar",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            Database.DeleteRiesgo(r.Id);
            Recargar();
        }
    }

    private void Exportar(object? sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog
        {
            Filter = "Excel Files|*.xlsx",
            FileName = $"{_nombreProyecto}_Riesgos.xlsx"
        };

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            var riesgos = Database.GetRiesgos(_proyectoId);
            ExportService.ExportarRiesgosProyecto(riesgos, _nombreProyecto, sfd.FileName);
            MessageBox.Show("Exportación completada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
