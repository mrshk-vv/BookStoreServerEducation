using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        public Task<int> CreatePayment();
    }
}
