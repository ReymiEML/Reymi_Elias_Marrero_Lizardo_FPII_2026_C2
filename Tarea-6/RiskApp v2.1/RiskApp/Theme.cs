namespace RiskApp;

public static class Theme
{
    public static Color Bg { get; } = Color.FromArgb(18, 18, 18);
    public static Color Card { get; } = Color.FromArgb(30, 30, 30);
    public static Color CardBorder { get; } = Color.FromArgb(50, 50, 50);
    public static Color TextPrimary { get; } = Color.FromArgb(230, 230, 230);
    public static Color TextSecondary { get; } = Color.FromArgb(150, 150, 150);
    public static Color Accent { get; } = Color.FromArgb(198, 241, 53);
    public static Color AccentText { get; } = Color.Black;

    public static void ApplyForm(Form form)
    {
        form.BackColor = Bg;
        form.ForeColor = TextPrimary;
    }

    public static void ApplyLabel(Label lbl, bool secondary = false)
    {
        lbl.ForeColor = secondary ? TextSecondary : TextPrimary;
        lbl.BackColor = Color.Transparent;
    }

    public static void ApplyTextBox(TextBoxBase tb)
    {
        tb.BackColor = Card;
        tb.ForeColor = TextPrimary;
        tb.BorderStyle = BorderStyle.FixedSingle;
    }

    public static Button MakeButton(string text, bool primary = false)
    {
        return new Button
        {
            Text = text,
            FlatStyle = FlatStyle.Flat,
            BackColor = primary ? Accent : Card,
            ForeColor = primary ? AccentText : TextPrimary,
            FlatAppearance = { BorderSize = 0 },
            Font = new Font("Segoe UI", 9, primary ? FontStyle.Bold : FontStyle.Regular),
            Size = new Size(primary ? 130 : 100, 32),
            UseVisualStyleBackColor = false,
            Cursor = Cursors.Hand
        };
    }

    public static void StyleDataGridView(DataGridView dgv)
    {
        dgv.EnableHeadersVisualStyles = false;
        dgv.BackgroundColor = Card;
        dgv.BorderStyle = BorderStyle.Fixed3D;
        dgv.GridColor = Color.FromArgb(60, 60, 60);
        dgv.RowHeadersVisible = false;
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextPrimary;
        dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        dgv.ColumnHeadersHeight = 30;
        dgv.DefaultCellStyle.BackColor = Card;
        dgv.DefaultCellStyle.ForeColor = TextPrimary;
        dgv.DefaultCellStyle.SelectionBackColor = Accent;
        dgv.DefaultCellStyle.SelectionForeColor = AccentText;
        dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);
    }

    public static Color TextColorForBg(Color bg)
    {
        double lum = (0.299 * bg.R + 0.587 * bg.G + 0.114 * bg.B) / 255.0;
        return lum > 0.5 ? Color.Black : Color.White;
    }

    public static void ApplyIcon(Form form)
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.png");
        if (File.Exists(path))
        {
            using var bmp = new Bitmap(path);
            form.Icon = Icon.FromHandle(bmp.GetHicon());
        }
    }
}
