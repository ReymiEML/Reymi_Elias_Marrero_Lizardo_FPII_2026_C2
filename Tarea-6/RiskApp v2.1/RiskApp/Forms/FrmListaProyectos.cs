using RiskApp.Models;

namespace RiskApp.Forms;

public class FrmListaProyectos : Form
{
    private readonly DataGridView dgv = new();
    private readonly Button btnNuevo;
    private readonly Button btnEditar;
    private readonly Button btnEliminar;
    private readonly Button btnExportarPdf;
    private readonly Button btnEntrar;
    private readonly PictureBox pbLogo = new();

    public FrmListaProyectos()
    {
        Theme.ApplyForm(this);
        Theme.ApplyIcon(this);
        Text = "Calculadora de Riesgos — Proyectos";
        Size = new Size(800, 450);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.png");
        if (File.Exists(logoPath))
        {
            pbLogo.Image = Image.FromFile(logoPath);
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        pbLogo.Location = new Point(12, 12);
        pbLogo.Size = new Size(40, 40);

        var lblTitulo = new Label
        {
            Text = "Calculadora de Riesgos",
            Location = new Point(60, 18),
            Size = new Size(250, 28),
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            ForeColor = Theme.Accent
        };

        dgv.Location = new Point(12, 65);
        dgv.Size = new Size(760, 282);
        dgv.AllowUserToAddRows = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.AllowUserToResizeColumns = false;
        dgv.AllowUserToResizeRows = false;
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgv.ReadOnly = true;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.MultiSelect = false;
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgv.DoubleClick += Entrar;
        Theme.StyleDataGridView(dgv);

        int y = 362;
        btnNuevo = Theme.MakeButton("Nuevo Proyecto", true);
        btnNuevo.Location = new Point(12, y);
        btnNuevo.Click += (_, _) => { using var f = new FrmProyecto(); f.ShowDialog(); Recargar(); };

        btnEditar = Theme.MakeButton("Editar");
        btnEditar.Location = new Point(150, y);
        btnEditar.Click += Editar;

        btnEliminar = Theme.MakeButton("Eliminar");
        btnEliminar.Location = new Point(258, y);
        btnEliminar.Click += Eliminar;

        btnExportarPdf = Theme.MakeButton("Exportar PDF");
        btnExportarPdf.Location = new Point(527, y);
        btnExportarPdf.Size = new Size(105, 32);
        btnExportarPdf.Click += ExportarPdf;

        btnEntrar = Theme.MakeButton("Entrar", true);
        btnEntrar.Location = new Point(642, y);
        btnEntrar.Click += Entrar;

        Controls.AddRange(new Control[] { dgv, btnNuevo, btnEditar, btnEliminar, btnExportarPdf, btnEntrar, pbLogo, lblTitulo });

        Recargar();
    }

    private void Recargar()
    {
        dgv.AutoGenerateColumns = false;
        dgv.Columns.Clear();

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "NombreProyecto",
            HeaderText = "Nombre del Proyecto",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgv.DataSource = Database.GetProyectos();
    }

    private Proyecto? Sel() =>
        dgv.SelectedRows.Count > 0 ? dgv.SelectedRows[0].DataBoundItem as Proyecto : null;

    private void Editar(object? sender, EventArgs e)
    {
        var p = Sel();
        if (p == null) return;
        using var f = new FrmProyecto(p);
        f.ShowDialog();
        Recargar();
    }

    private void Eliminar(object? sender, EventArgs e)
    {
        var p = Sel();
        if (p == null) return;
        if (MessageBox.Show($"¿Eliminar \"{p.NombreProyecto}\"?", "Confirmar",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            Database.DeleteProyecto(p.Id);
            Recargar();
        }
    }

    private void Entrar(object? sender, EventArgs e)
    {
        var p = Sel();
        if (p == null) return;
        Hide();
        using var f = new FrmListaRiesgos(p.Id, p.NombreProyecto);
        f.ShowDialog();
        Show();
        Recargar();
    }

    private void ExportarPdf(object? sender, EventArgs e)
    {
        var p = Sel();
        if (p == null) return;

        using var sfd = new SaveFileDialog
        {
            Filter = "PDF Files|*.pdf",
            FileName = $"{p.NombreProyecto}_Informe.pdf"
        };

        if (sfd.ShowDialog() != DialogResult.OK) return;

        try
        {
            var proyecto = Database.GetProyecto(p.Id);
            var evaluador = Database.GetEvaluador(proyecto.EvaluadorId);
            var riesgos = Database.GetRiesgos(p.Id);

            PdfExportService.ExportarProyecto(proyecto, evaluador.Nombre, riesgos, sfd.FileName);
            MessageBox.Show("PDF exportado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al exportar PDF:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
