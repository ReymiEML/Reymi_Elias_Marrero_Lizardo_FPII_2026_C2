using ClosedXML.Excel;
using RiskApp.Models;

namespace RiskApp;

public static class ExportService
{
    public static void ExportarRiesgo(Riesgo riesgo, string filePath)
    {
        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Riesgo");

        ws.Cell(1, 1).Value = "Campo";
        ws.Cell(1, 2).Value = "Valor";
        ws.Range(1, 1, 1, 2).Style.Font.Bold = true;

        ws.Cell(2, 1).Value = "Nombre del Riesgo";
        ws.Cell(2, 2).Value = riesgo.NombreRiesgo;
        ws.Cell(3, 1).Value = "Función (F)";
        ws.Cell(3, 2).Value = riesgo.Funcion;
        ws.Cell(4, 1).Value = "Sustitución (S)";
        ws.Cell(4, 2).Value = riesgo.Sustitucion;
        ws.Cell(5, 1).Value = "Profundidad (P)";
        ws.Cell(5, 2).Value = riesgo.Profundidad;
        ws.Cell(6, 1).Value = "Extensión (E)";
        ws.Cell(6, 2).Value = riesgo.Extension;
        ws.Cell(7, 1).Value = "Agresión (A)";
        ws.Cell(7, 2).Value = riesgo.Agresion;
        ws.Cell(8, 1).Value = "Vulnerabilidad (V)";
        ws.Cell(8, 2).Value = riesgo.Vulnerabilidad;
        ws.Cell(9, 1).Value = "ER";
        ws.Cell(9, 2).Value = riesgo.ER;
        ws.Cell(10, 1).Value = "Clasificación";
        ws.Cell(10, 2).Value = riesgo.Clasificacion;
        ws.Cell(11, 1).Value = "Solución Propuesta";
        ws.Cell(11, 2).Value = riesgo.SolucionPropuesta;
        ws.Cell(12, 1).Value = "Fecha de Creación";
        ws.Cell(12, 2).Value = riesgo.FechaCreacion.ToString("dd/MM/yyyy HH:mm");

        var color = GetExcelColor(riesgo.Clasificacion);
        ws.Cell(10, 2).Style.Fill.BackgroundColor = color;
        ws.Cell(10, 2).Style.Font.FontColor = XLColor.White;

        // ── Traffic light chart ──
        int chartRow = 15;
        ws.Cell(chartRow, 1).Value = "Gráfico de Clasificación";
        ws.Cell(chartRow, 1).Style.Font.Bold = true;
        ws.Cell(chartRow, 1).Style.Font.FontSize = 11;
        ws.Range(chartRow, 1, chartRow, 5).Merge();

        var bandas = new[]
        {
            (nombre: "Muy\npequeño", rango: "2-250",     clasif: "Muy pequeño"),
            (nombre: "Pequeño",      rango: "251-500",   clasif: "Pequeño"),
            (nombre: "Normal",       rango: "501-750",   clasif: "Normal"),
            (nombre: "Grande",       rango: "751-1000",  clasif: "Grande"),
            (nombre: "Elevado",      rango: "1001-1250", clasif: "Elevado")
        };

        int activeIdx = Array.FindIndex(bandas, b => b.clasif == riesgo.Clasificacion);
        int markerRow = chartRow + 1;

        for (int i = 0; i < bandas.Length; i++)
        {
            int c = i + 1;
            bool active = i == activeIdx;

            // Marker row (arrow + ER)
            if (active)
            {
                ws.Cell(markerRow, c).Value = $"↑ ER = {riesgo.ER}";
                ws.Cell(markerRow, c).Style.Font.Bold = true;
                ws.Cell(markerRow, c).Style.Font.FontSize = 10;
            }
            ws.Cell(markerRow, c).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // Colored bar cell
            var barCell = ws.Cell(markerRow + 1, c);
            barCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            barCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            if (active)
            {
                barCell.Style.Fill.BackgroundColor = GetExcelColor(bandas[i].clasif);
                barCell.Style.Font.FontColor = XLColor.White;
                barCell.Style.Font.Bold = true;
                barCell.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                barCell.Style.Border.OutsideBorderColor = XLColor.Black;
            }
            else
            {
                var light = GetLightColor(GetExcelColor(bandas[i].clasif));
                barCell.Style.Fill.BackgroundColor = light;
                barCell.Style.Font.FontColor = XLColor.Gray;
                barCell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                barCell.Style.Border.OutsideBorderColor = XLColor.Gray;
            }

            // Classification name
            var nomCell = ws.Cell(markerRow + 2, c);
            nomCell.Value = bandas[i].nombre;
            nomCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            nomCell.Style.Alignment.WrapText = true;

            // Range
            var rngCell = ws.Cell(markerRow + 3, c);
            rngCell.Value = bandas[i].rango;
            rngCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngCell.Style.Font.FontSize = 9;
            rngCell.Style.Font.FontColor = XLColor.Gray;
        }

        ws.Range(markerRow, 1, markerRow + 3, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

        for (int i = 1; i <= 5; i++)
            ws.Column(i).Width = 16;

        ws.Rows().AdjustToContents();
        wb.SaveAs(filePath);
    }

    private static XLColor GetLightColor(XLColor color)
    {
        var c = color.Color;
        return XLColor.FromArgb(
            Math.Min(255, c.R + 90),
            Math.Min(255, c.G + 90),
            Math.Min(255, c.B + 90)
        );
    }

    public static void ExportarRiesgosProyecto(List<Riesgo> riesgos, string nombreProyecto, string filePath)
    {
        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Riesgos");

        ws.Cell(1, 1).Value = "Nombre del Riesgo";
        ws.Cell(1, 2).Value = "F";
        ws.Cell(1, 3).Value = "S";
        ws.Cell(1, 4).Value = "P";
        ws.Cell(1, 5).Value = "E";
        ws.Cell(1, 6).Value = "A";
        ws.Cell(1, 7).Value = "V";
        ws.Cell(1, 8).Value = "ER";
        ws.Cell(1, 9).Value = "Clasificación";
        ws.Cell(1, 10).Value = "Solución Propuesta";
        ws.Cell(1, 11).Value = "Fecha";
        ws.Range(1, 1, 1, 11).Style.Font.Bold = true;

        for (int i = 0; i < riesgos.Count; i++)
        {
            var r = riesgos[i];
            int row = i + 2;
            ws.Cell(row, 1).Value = r.NombreRiesgo;
            ws.Cell(row, 2).Value = r.Funcion;
            ws.Cell(row, 3).Value = r.Sustitucion;
            ws.Cell(row, 4).Value = r.Profundidad;
            ws.Cell(row, 5).Value = r.Extension;
            ws.Cell(row, 6).Value = r.Agresion;
            ws.Cell(row, 7).Value = r.Vulnerabilidad;
            ws.Cell(row, 8).Value = r.ER;
            ws.Cell(row, 9).Value = r.Clasificacion;
            ws.Cell(row, 10).Value = r.SolucionPropuesta;
            ws.Cell(row, 11).Value = r.FechaCreacion.ToString("dd/MM/yyyy HH:mm");

            var color = GetExcelColor(r.Clasificacion);
            ws.Cell(row, 9).Style.Fill.BackgroundColor = color;
        }

        ws.Columns().AdjustToContents();
        wb.SaveAs(filePath);
    }

    private static XLColor GetExcelColor(string clasificacion)
    {
        return clasificacion switch
        {
            "Muy pequeño" => XLColor.Green,
            "Pequeño" => XLColor.LightGreen,
            "Normal" => XLColor.Yellow,
            "Grande" => XLColor.Orange,
            "Elevado" => XLColor.Red,
            _ => XLColor.Gray
        };
    }
}
