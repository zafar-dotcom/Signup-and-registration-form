using System.Data;

namespace Complete.Models
{
    public interface IExport_To_Excell
    {
        DataTable Exportcustomer();
        DataSet Exportfromdatabase();
       
    }
}
