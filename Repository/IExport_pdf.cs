using Complete.Models;

namespace Complete.Repository
{
    public interface IExport_pdf
    {
        List<ExportPdfEmployeemodel> Get_Employee_Export_Pdfs();
    }
}
