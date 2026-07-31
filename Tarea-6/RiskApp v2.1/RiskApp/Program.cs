using QuestPDF.Infrastructure;
using RiskApp.Forms;

namespace RiskApp;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmListaProyectos());
    }
}
