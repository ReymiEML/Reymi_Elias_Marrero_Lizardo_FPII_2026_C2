using LiteDB;
using RiskApp.Models;

namespace RiskApp;

public static class Database
{
    private static LiteDatabase _db = null!;

    private static LiteDatabase DB
    {
        get
        {
            if (_db == null)
            {
                var path = Path.Combine(Application.StartupPath, "RiskApp.db");
                _db = new LiteDatabase($"Filename={path};Connection=direct");
            }
            return _db;
        }
    }

    // Evaluador
    public static List<Evaluador> GetEvaluadores() =>
        DB.GetCollection<Evaluador>("Evaluador").FindAll().ToList();

    public static Evaluador GetEvaluador(int id) =>
        DB.GetCollection<Evaluador>("Evaluador").FindById(id);

    public static void InsertEvaluador(Evaluador e) =>
        DB.GetCollection<Evaluador>("Evaluador").Insert(e);

    public static void UpdateEvaluador(Evaluador e) =>
        DB.GetCollection<Evaluador>("Evaluador").Update(e);

    // Proyecto
    public static List<Proyecto> GetProyectos() =>
        DB.GetCollection<Proyecto>("Proyecto").FindAll().ToList();

    public static Proyecto GetProyecto(int id) =>
        DB.GetCollection<Proyecto>("Proyecto").FindById(id);

    public static void InsertProyecto(Proyecto p) =>
        DB.GetCollection<Proyecto>("Proyecto").Insert(p);

    public static void UpdateProyecto(Proyecto p) =>
        DB.GetCollection<Proyecto>("Proyecto").Update(p);

    public static void DeleteProyecto(int id)
    {
        DB.GetCollection<Riesgo>("Riesgo").DeleteMany(r => r.ProyectoId == id);
        DB.GetCollection<Proyecto>("Proyecto").Delete(id);
    }

    // Riesgo
    public static List<Riesgo> GetRiesgos(int proyectoId) =>
        DB.GetCollection<Riesgo>("Riesgo").Find(r => r.ProyectoId == proyectoId).ToList();

    public static Riesgo GetRiesgo(int id) =>
        DB.GetCollection<Riesgo>("Riesgo").FindById(id);

    public static void InsertRiesgo(Riesgo r) =>
        DB.GetCollection<Riesgo>("Riesgo").Insert(r);

    public static void UpdateRiesgo(Riesgo r) =>
        DB.GetCollection<Riesgo>("Riesgo").Update(r);

    public static void DeleteRiesgo(int id) =>
        DB.GetCollection<Riesgo>("Riesgo").Delete(id);
}
