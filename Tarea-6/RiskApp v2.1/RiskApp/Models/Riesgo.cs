namespace RiskApp.Models;

public class Riesgo
{
    public int Id { get; set; }
    public int ProyectoId { get; set; }
    public string NombreRiesgo { get; set; } = string.Empty;
    public int Funcion { get; set; }
    public int Sustitucion { get; set; }
    public int Profundidad { get; set; }
    public int Extension { get; set; }
    public int Agresion { get; set; }
    public int Vulnerabilidad { get; set; }
    public int ER { get; set; }
    public string Clasificacion { get; set; } = string.Empty;
    public string SolucionPropuesta { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }

    public void Calcular()
    {
        int i = Funcion * Sustitucion;
        int d = Profundidad * Extension;
        int c = i + d;
        int pr = Agresion * Vulnerabilidad;
        ER = c * pr;

        Clasificacion = ER switch
        {
            <= 250 => "Muy pequeño",
            <= 500 => "Pequeño",
            <= 750 => "Normal",
            <= 1000 => "Grande",
            <= 1250 => "Elevado",
            _ => "Fuera de rango"
        };
    }
}
