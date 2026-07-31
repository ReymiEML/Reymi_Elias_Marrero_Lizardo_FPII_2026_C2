using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RiskApp.Models;

namespace RiskApp;

public static class PdfExportService
{
    public static void ExportarProyecto(Proyecto proyecto, string evaluadorNombre, List<Riesgo> riesgos, string filePath)
    {
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Segoe UI"));

                page.Header().Element(c => ComposeHeader(c, proyecto, evaluadorNombre));
                page.Content().Element(c => ComposeBody(c, riesgos));
                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Generado por Calculadora de Riesgos — ");
                    text.CurrentPageNumber();
                });
            });
        }).GeneratePdf(filePath);
    }

    private static void ComposeHeader(IContainer container, Proyecto proyecto, string evaluadorNombre)
    {
        container.Column(col =>
        {
            col.Item().Text("Informe de Evaluación de Riesgos")
                .FontSize(18).Bold().FontColor(Colors.Black);

            col.Item().PaddingTop(12).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

            col.Item().PaddingTop(10).Table(tbl =>
            {
                tbl.ColumnsDefinition(c =>
                {
                    c.ConstantColumn(120);
                    c.RelativeColumn();
                });

                void Row(string label, string value)
                {
                    tbl.Cell().Text(label).Bold().FontColor(Colors.Grey.Darken2);
                    tbl.Cell().Text(value);
                }

                Row("Proyecto:", proyecto.NombreProyecto);
                Row("Cliente:", proyecto.Cliente);
                Row("Descripción:", proyecto.Descripcion);
                Row("Evaluador:", evaluadorNombre);
                Row("Fecha:", proyecto.FechaCreacion.ToString("dd/MM/yyyy HH:mm"));
            });

            col.Item().PaddingTop(8).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
        });
    }

    private static void ComposeBody(IContainer container, List<Riesgo> riesgos)
    {
        container.PaddingTop(10).Column(col =>
        {
            col.Item().Text($"Riesgos evaluados: {riesgos.Count}")
                .FontSize(14).Bold();

            foreach (var riesgo in riesgos)
            {
                riesgo.Calcular();

                col.Item().PaddingTop(12).Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).Column(riskCol =>
                {
                    riskCol.Item().Text(riesgo.NombreRiesgo)
                        .FontSize(13).Bold().FontColor(Colors.Black);

                    riskCol.Item().PaddingTop(6).Table(tbl =>
                    {
                        tbl.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn();
                            c.RelativeColumn();
                            c.RelativeColumn();
                            c.RelativeColumn();
                            c.RelativeColumn();
                            c.RelativeColumn();
                        });

                        var criterios = new[] { ("F", riesgo.Funcion), ("S", riesgo.Sustitucion),
                            ("P", riesgo.Profundidad), ("E", riesgo.Extension),
                            ("A", riesgo.Agresion), ("V", riesgo.Vulnerabilidad) };

                        foreach (var (nom, val) in criterios)
                        {
                            tbl.Cell().Border(1).BorderColor(Colors.Grey.Lighten3)
                                .Padding(4).AlignCenter().Text($"{nom}={val}").FontSize(10);
                        }
                    });

                    riskCol.Item().PaddingTop(6).Row(row =>
                    {
                        row.RelativeItem().Text($"ER = {riesgo.ER}    Clasificación: {riesgo.Clasificacion}")
                            .FontSize(11).Bold();
                    });

                    riskCol.Item().PaddingTop(6)
                        .Element(c => DrawSemaforoPdf(c, riesgo.ER));

                    if (!string.IsNullOrWhiteSpace(riesgo.SolucionPropuesta))
                    {
                        riskCol.Item().PaddingTop(6).Text("Solución propuesta:").Bold().FontSize(10);
                        riskCol.Item().Text(riesgo.SolucionPropuesta).FontSize(10);
                    }
                });
            }
        });
    }

    private static void DrawSemaforoPdf(IContainer container, int er)
    {
        int activeIdx = SemaforoHelper.GetActiveIndex(er);

        container.Column(col =>
        {
            col.Item().Height(22).Row(row =>
            {
                for (int i = 0; i < SemaforoHelper.Bandas.Length; i++)
                {
                    var b = SemaforoHelper.Bandas[i];
                    int idx = i;
                    string hex = $"#{b.color.R:X2}{b.color.G:X2}{b.color.B:X2}";
                    string bgHex = idx == activeIdx ? hex : "#2A2A2A";
                    var bgColor = idx == activeIdx ? b.color : System.Drawing.Color.FromArgb(42, 42, 42);
                    string textColor = Theme.TextColorForBg(bgColor) == System.Drawing.Color.Black
                        ? Colors.Black : Colors.White;

                    row.RelativeItem().Background(bgHex)
                        .Border(0.5f).BorderColor("#505050")
                        .AlignCenter().AlignMiddle()
                        .Text(b.nom).FontSize(8).FontColor(textColor);
                }
            });

            col.Item().PaddingTop(2).Height(14).Row(row =>
            {
                for (int i = 0; i < SemaforoHelper.Bandas.Length; i++)
                {
                    var b = SemaforoHelper.Bandas[i];
                    string rangoStr = i == 0 ? "2-250" : $"{b.min}-{b.max}";
                    row.RelativeItem().AlignCenter().Text(rangoStr)
                        .FontSize(6).FontColor(Colors.Grey.Darken3);
                }
            });
        });
    }
}
