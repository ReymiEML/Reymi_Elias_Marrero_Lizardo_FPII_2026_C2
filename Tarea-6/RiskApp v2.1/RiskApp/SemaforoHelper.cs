namespace RiskApp;

public static class SemaforoHelper
{
    public static (int min, int max, string nom, Color color)[] Bandas { get; } =
    {
        (2, 250, "Muy pequeño", Color.FromArgb(76, 175, 80)),
        (251, 500, "Pequeño",   Color.FromArgb(139, 195, 74)),
        (501, 750, "Normal",    Color.FromArgb(255, 235, 59)),
        (751, 1000, "Grande",   Color.FromArgb(255, 152, 0)),
        (1001, 1250, "Elevado", Color.FromArgb(244, 67, 54))
    };

    public static int GetActiveIndex(int er)
    {
        for (int i = 0; i < Bandas.Length; i++)
            if (er >= Bandas[i].min && er <= Bandas[i].max)
                return i;
        return -1;
    }

    public static Color GetClassificationColor(string clasificacion)
    {
        foreach (var b in Bandas)
            if (b.nom == clasificacion)
                return b.color;
        return Color.Gray;
    }

    public static float GetMarkerX(float barWidth, int er)
    {
        float x = barWidth * (er - 2) / 1248f;
        return Math.Clamp(x, 6, barWidth - 6);
    }
}
