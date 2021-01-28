using System.Threading.Tasks;
using BusinessLayer.Models;

namespace CSV_Loader_UI
{
    public interface IReadFile
    {
        void Run();
        Task<bool> SendCustomerInformationAsync(CustomerModel customerModel);
    }
}