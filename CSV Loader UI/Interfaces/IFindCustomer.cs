using System.Threading.Tasks;

namespace CSV_Loader_UI
{
    public interface IFindCustomer
    {
        Task<string> GetCustomerInformationByCustomerReferenceAsync(string customerReference);
        void Run();
    }
}