using RiskApp.Models;

namespace RiskApp.Forms;

public class FrmAsistenteRiesgo : Form
{
    private readonly int _proyectoId;
    private readonly Riesgo? _riesgo;
    private int _paso = 1;

    private readonly Panel panelPasos = new();
    private readonly Label lblPaso = new();
    private readonly Panel step1 = new(), step2 = new(), step3 = new(), step4 = new();

    // Step 1
    private readonly TextBox txtNombre = new();

    // Step 2 — TrackBars + value labels
    private readonly TrackBar[] tbFactores;
    private readonly Label[] lblFactores, lblValores;

    // Step 3
    private readonly TextBox txtSolucion = new();

    // Step 4
    private readonly Panel panelSemaforo = new();
    private readonly Panel cardER = new(), cardClasif = new();
    private readonly Label lblCardER = new(), lblCardValorER = new() { Text = "—" };
    private readonly Label lblCardClasifTitulo = new(), lblCardClasifValor = new();
    private readonly Label lblEscala = new();

    // Navigation
    private readonly Button btnAtras, btnSiguiente, btnIrResultado, btnGuardar, btnExportar, btnCancelar;

    public FrmAsistenteRiesgo(int proyectoId, Riesgo? riesgo = null)
    {
        _proyectoId = proyectoId;
        _riesgo = riesgo;
        Theme.ApplyForm(this);
        Theme.ApplyIcon(this);
        Text = riesgo == null ? "Nuevo Riesgo" : "Editar Riesgo";
        Size = new Size(750, 540);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        tbFactores = new TrackBar[6];
        lblFactores = new Label[6];
        lblValores = new Label[6];

        btnAtras = Theme.MakeButton("← Atrás");
        btnSiguiente = Theme.MakeButton("Siguiente →", true);
        btnIrResultado = Theme.MakeButton("Resultado →");
        btnGuardar = Theme.MakeButton("Guardar", true);
        btnExportar = Theme.MakeButton("Exportar a Excel");
        btnCancelar = Theme.MakeButton("Cancelar");

        Inicializar();
        MostrarPaso(1);
    }

    private void Inicializar()
    {
        lblPaso.Location = new Point(20, 15);
        lblPaso.Size = new Size(600, 30);
        lblPaso.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        Theme.ApplyLabel(lblPaso);
        Controls.Add(lblPaso);

        panelPasos.Location = new Point(15, 50);
        panelPasos.Size = new Size(705, 380);
        panelPasos.BackColor = Theme.Bg;
        panelPasos.BorderStyle = BorderStyle.FixedSingle;
        Controls.Add(panelPasos);

        // ───────── Step 1 ─────────
        step1.Location = new Point(0, 0);
        step1.Size = panelPasos.Size;
        step1.BackColor = Theme.Bg;

        var lblNombre = new Label { Text = "Nombre del Riesgo:", Location = new Point(20, 35), Size = new Size(200, 25) };
        Theme.ApplyLabel(lblNombre, true);
        txtNombre.Location = new Point(20, 65);
        txtNombre.Size = new Size(650, 30);
        txtNombre.Font = new Font("Segoe UI", 12);
        Theme.ApplyTextBox(txtNombre);
        step1.Controls.AddRange(new Control[] { lblNombre, txtNombre });
        panelPasos.Controls.Add(step1);

        // ───────── Step 2 ─────────
        step2.Location = new Point(0, 0);
        step2.Size = panelPasos.Size;
        step2.BackColor = Theme.Bg;

        string[] nomFactores = { "Función (F)", "Sustitución (S)", "Profundidad (P)", "Extensión (E)", "Agresión (A)", "Vulnerabilidad (V)" };
        for (int i = 0; i < 6; i++)
        {
            int col = i % 2, row = i / 2;
            int x = 30 + col * 310;
            int y = 30 + row * 95;

            lblFactores[i] = new Label
            {
                Text = nomFactores[i],
                Location = new Point(x, y),
                Size = new Size(120, 22),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            Theme.ApplyLabel(lblFactores[i]);

            tbFactores[i] = new TrackBar
            {
                Location = new Point(x + 5, y + 34),
                Size = new Size(170, 46),
                Minimum = 1,
                Maximum = 5,
                Value = 1,
                TickStyle = TickStyle.None,
                LargeChange = 1,
                SmallChange = 1
            };

            int idx = i;
            tbFactores[i].Scroll += (_, _) => lblValores[idx].Text = tbFactores[idx].Value.ToString();

            lblValores[i] = new Label
            {
                Text = "1",
                Location = new Point(x + 190, y + 35),
                Size = new Size(38, 28),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Theme.Accent,
                ForeColor = Theme.AccentText
            };

            step2.Controls.AddRange(new Control[] { lblFactores[i], tbFactores[i], lblValores[i] });
        }
        panelPasos.Controls.Add(step2);

        // ───────── Step 3 ─────────
        step3.Location = new Point(0, 0);
        step3.Size = panelPasos.Size;
        step3.BackColor = Theme.Bg;

        var lblSol = new Label { Text = "Solución Propuesta:", Location = new Point(20, 20), Size = new Size(200, 25) };
        Theme.ApplyLabel(lblSol, true);
        txtSolucion.Location = new Point(20, 50);
        txtSolucion.Size = new Size(630, 290);
        txtSolucion.Multiline = true;
        txtSolucion.ScrollBars = ScrollBars.Vertical;
        txtSolucion.Font = new Font("Segoe UI", 11);
        Theme.ApplyTextBox(txtSolucion);
        step3.Controls.AddRange(new Control[] { lblSol, txtSolucion });
        panelPasos.Controls.Add(step3);

        // ───────── Step 4 ─────────
        step4.Location = new Point(0, 0);
        step4.Size = panelPasos.Size;
        step4.BackColor = Theme.Bg;

        // Semáforo
        panelSemaforo.Location = new Point(20, 75);
        panelSemaforo.Size = new Size(665, 75);
        panelSemaforo.BackColor = Theme.Card;
        panelSemaforo.Paint += PanelSemaforo_Paint;
        step4.Controls.Add(panelSemaforo);

        // ── Dos tarjetas lado a lado ──
        int cardY = 205;
        int cardW = 320, cardH = 90;

        // Tarjeta ER
        cardER.Location = new Point(20, cardY);
        cardER.Size = new Size(cardW, cardH);
        cardER.BackColor = Theme.Card;
        cardER.Paint += (_, e) =>
        {
            using var p = new Pen(Theme.CardBorder);
            e.Graphics.DrawRectangle(p, 0, 0, cardER.Width - 1, cardER.Height - 1);
        };

        lblCardER.Location = new Point(10, 8);
        lblCardER.Size = new Size(280, 22);
        lblCardER.Text = "ER (Evaluación del Riesgo)";
        lblCardER.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        lblCardER.ForeColor = Theme.Accent;
        lblCardER.TextAlign = ContentAlignment.MiddleLeft;

        lblCardValorER.Location = new Point(0, 28);
        lblCardValorER.Size = new Size(320, 58);
        lblCardValorER.Font = new Font("Segoe UI", 28, FontStyle.Bold);
        lblCardValorER.ForeColor = Theme.TextPrimary;
        lblCardValorER.TextAlign = ContentAlignment.MiddleCenter;

        cardER.Controls.AddRange(new Control[] { lblCardER, lblCardValorER });
        step4.Controls.Add(cardER);

        // Tarjeta Clasificación
        cardClasif.Location = new Point(355, cardY);
        cardClasif.Size = new Size(cardW, cardH);
        cardClasif.BackColor = Theme.Card;

        lblCardClasifTitulo.Location = new Point(10, 8);
        lblCardClasifTitulo.Size = new Size(280, 22);
        lblCardClasifTitulo.Text = "Clasificación";
        lblCardClasifTitulo.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        lblCardClasifTitulo.ForeColor = Theme.Accent;
        lblCardClasifTitulo.TextAlign = ContentAlignment.MiddleLeft;

        lblCardClasifValor.Location = new Point(5, 32);
        lblCardClasifValor.Size = new Size(310, 50);
        lblCardClasifValor.Font = new Font("Segoe UI", 18, FontStyle.Bold);
        lblCardClasifValor.TextAlign = ContentAlignment.MiddleCenter;
        lblCardClasifValor.ForeColor = Color.White;

        cardClasif.Controls.AddRange(new Control[] { lblCardClasifTitulo, lblCardClasifValor });
        step4.Controls.Add(cardClasif);

        // Escala de referencia
        lblEscala.Location = new Point(20, 310);
        lblEscala.Size = new Size(665, 20);
        lblEscala.Font = new Font("Segoe UI", 7.5f);
        lblEscala.ForeColor = Theme.TextSecondary;
        lblEscala.TextAlign = ContentAlignment.MiddleCenter;
        Theme.ApplyLabel(lblEscala);

        step4.Controls.AddRange(new Control[] { lblEscala });
        panelPasos.Controls.Add(step4);

        // ───────── Navigation buttons ─────────
        int btnY = 440;
        btnAtras.Location = new Point(15, btnY);
        btnAtras.Click += (_, _) => MostrarPaso(_paso - 1);

        btnSiguiente.Location = new Point(125, btnY);
        btnSiguiente.Click += (_, _) => MostrarPaso(_paso + 1);

        btnIrResultado.Location = new Point(265, btnY);
        btnIrResultado.Click += (_, _) =>
        {
            if (_paso == 1 && string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del riesgo es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }
            MostrarPaso(4);
        };

        btnGuardar.Location = new Point(365, btnY);
        btnGuardar.Click += Guardar;

        btnExportar.Location = new Point(495, btnY);
        btnExportar.Size = new Size(120, 32);
        btnExportar.Click += Exportar;

        btnCancelar.Location = new Point(620, btnY);
        btnCancelar.Click += (_, _) => DialogResult = DialogResult.Cancel;

        Controls.AddRange(new Control[] { btnAtras, btnSiguiente, btnIrResultado, btnGuardar, btnExportar, btnCancelar });

        if (_riesgo != null)
        {
            txtNombre.Text = _riesgo.NombreRiesgo;
            for (int i = 0; i < 6; i++)
            {
                int val = i switch
                {
                    0 => _riesgo.Funcion,
                    1 => _riesgo.Sustitucion,
                    2 => _riesgo.Profundidad,
                    3 => _riesgo.Extension,
                    4 => _riesgo.Agresion,
                    _ => _riesgo.Vulnerabilidad
                };
                tbFactores[i].Value = val;
                lblValores[i].Text = val.ToString();
            }
            txtSolucion.Text = _riesgo.SolucionPropuesta;
        }
    }

    private void MostrarPaso(int paso)
    {
        if (paso < 1 || paso > 4) return;

        if (paso > _paso && !ValidarPasoActual()) return;

        _paso = paso;
        step1.Visible = paso == 1;
        step2.Visible = paso == 2;
        step3.Visible = paso == 3;
        step4.Visible = paso == 4;

        btnAtras.Visible = paso > 1;
        btnSiguiente.Visible = paso < 4;
        btnIrResultado.Visible = paso < 4 && _riesgo != null;
        btnGuardar.Visible = paso == 4;
        btnExportar.Visible = paso == 4;

        int btnY;
        if (paso == 1)
        {
            btnY = 440;
            btnSiguiente.Location = new Point(15, btnY);
            btnIrResultado.Location = new Point(155, btnY);
            btnAtras.Location = new Point(15, btnY);
            btnGuardar.Location = new Point(365, btnY);
            btnExportar.Location = new Point(495, btnY);
            btnCancelar.Location = new Point(620, btnY);
        }
        else
        {
            btnY = 440;
            btnSiguiente.Location = new Point(125, btnY);
            btnIrResultado.Location = new Point(265, btnY);
            btnAtras.Location = new Point(15, btnY);
            btnGuardar.Location = new Point(365, btnY);
            btnExportar.Location = new Point(495, btnY);
            btnCancelar.Location = new Point(620, btnY);
        }

        lblPaso.Text = $"Paso {paso} de 4";

        if (paso == 4) CalcularYMostrar();
    }

    private bool ValidarPasoActual()
    {
        if (_paso == 1 && string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            MessageBox.Show("El nombre del riesgo es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtNombre.Focus();
            return false;
        }
        return true;
    }

    private void CalcularYMostrar()
    {
        var temp = NewRiesgoFromControls();
        temp.Calcular();

        lblCardValorER.Text = $"{temp.ER}";

        var clr = SemaforoHelper.GetClassificationColor(temp.Clasificacion);
        cardClasif.BackColor = clr;
        lblCardClasifTitulo.ForeColor = Theme.TextColorForBg(clr);
        lblCardClasifValor.Text = temp.Clasificacion;
        lblCardClasifValor.ForeColor = temp.Clasificacion == "Normal" ? Color.Black : Color.White;

        lblEscala.Text = "Escala: Muy pequeño 2–250  · Pequeño 251–500  · Normal 501–750  · Grande 751–1000  · Elevado 1001–1250";

        panelSemaforo.Invalidate();
    }

    private Riesgo NewRiesgoFromControls()
    {
        return new Riesgo
        {
            NombreRiesgo = txtNombre.Text.Trim(),
            Funcion = tbFactores[0].Value,
            Sustitucion = tbFactores[1].Value,
            Profundidad = tbFactores[2].Value,
            Extension = tbFactores[3].Value,
            Agresion = tbFactores[4].Value,
            Vulnerabilidad = tbFactores[5].Value
        };
    }

    private void PanelSemaforo_Paint(object? sender, PaintEventArgs e)
    {
        var temp = NewRiesgoFromControls();
        temp.Calcular();
        int er = temp.ER;

        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        int barY = 15, barH = 28, barX = 8, barW = panelSemaforo.ClientSize.Width - 16;
        int activeIdx = SemaforoHelper.GetActiveIndex(er);
        float segW = barW / (float)SemaforoHelper.Bandas.Length;

        for (int i = 0; i < SemaforoHelper.Bandas.Length; i++)
        {
            float x = barX + i * segW;
            var banda = SemaforoHelper.Bandas[i];
            int alpha = i == activeIdx ? 255 : 60;
            using var brush = new SolidBrush(Color.FromArgb(alpha, banda.color));
            g.FillRectangle(brush, x, barY, segW, barH);
            using var pen = new Pen(Color.FromArgb(80, 80, 80));
            g.DrawRectangle(pen, x, barY, segW, barH);

            using var fNom = new Font("Segoe UI", 7);
            using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
            g.DrawString(banda.nom, fNom, Brushes.Gray, new RectangleF(x, barY + barH + 2, segW, 20), sf);

            string rangoStr = i == 0 ? "2-250" : $"{banda.min}-{banda.max}";
            using var fRng = new Font("Segoe UI", 6);
            g.DrawString(rangoStr, fRng, Brushes.DimGray, new RectangleF(x, barY + barH + 16, segW, 12), sf);
        }

        float markerX = SemaforoHelper.GetMarkerX(barW, er);
        markerX = barX + Math.Clamp(markerX, 0, barW);

        using var mb = new SolidBrush(Color.White);
        g.FillPolygon(mb, new Point[]
        {
            new((int)markerX, barY),
            new((int)markerX - 5, barY - 7),
            new((int)markerX + 5, barY - 7)
        });
    }

    private void Guardar(object? sender, EventArgs e)
    {
        var riesgo = _riesgo ?? new Riesgo { ProyectoId = _proyectoId };

        riesgo.NombreRiesgo = txtNombre.Text.Trim();
        riesgo.Funcion = tbFactores[0].Value;
        riesgo.Sustitucion = tbFactores[1].Value;
        riesgo.Profundidad = tbFactores[2].Value;
        riesgo.Extension = tbFactores[3].Value;
        riesgo.Agresion = tbFactores[4].Value;
        riesgo.Vulnerabilidad = tbFactores[5].Value;
        riesgo.SolucionPropuesta = txtSolucion.Text.Trim();
        riesgo.Calcular();

        if (_riesgo == null)
        {
            riesgo.FechaCreacion = DateTime.Now;
            Database.InsertRiesgo(riesgo);
        }
        else
        {
            Database.UpdateRiesgo(riesgo);
        }

        DialogResult = DialogResult.OK;
    }

    private void Exportar(object? sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog
        {
            Filter = "Excel Files|*.xlsx",
            FileName = $"{txtNombre.Text.Trim()}.xlsx"
        };

        if (sfd.ShowDialog() != DialogResult.OK) return;

        var riesgo = new Riesgo
        {
            NombreRiesgo = txtNombre.Text.Trim(),
            Funcion = tbFactores[0].Value,
            Sustitucion = tbFactores[1].Value,
            Profundidad = tbFactores[2].Value,
            Extension = tbFactores[3].Value,
            Agresion = tbFactores[4].Value,
            Vulnerabilidad = tbFactores[5].Value,
            SolucionPropuesta = txtSolucion.Text.Trim(),
            FechaCreacion = _riesgo?.FechaCreacion ?? DateTime.Now
        };
        riesgo.Calcular();

        ExportService.ExportarRiesgo(riesgo, sfd.FileName);
        MessageBox.Show("Exportación completada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
