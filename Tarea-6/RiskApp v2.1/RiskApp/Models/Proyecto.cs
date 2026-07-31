namespace RiskApp.Models;

public class Proyecto
{
    public int Id { get; set; }
    public string NombreProyecto { get; set; } = string.Empty;
    public string Cliente { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public int EvaluadorId { get; set; }
}
