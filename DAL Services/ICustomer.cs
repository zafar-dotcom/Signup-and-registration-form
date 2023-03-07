using Complete.Models;

namespace Complete.DAL_Services
{
    public interface ICustomer
    {
        List<Customer_model> GetCustomer();
        bool Delete(int? id);
    }
}
