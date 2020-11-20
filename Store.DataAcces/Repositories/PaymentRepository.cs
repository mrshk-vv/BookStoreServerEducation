using System;
using System.Threading.Tasks;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories
{
    public class PaymentRepository : BaseEfRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<int> CreatePayment()
        {
            Payment payment = new Payment()
            {
                TransactionId = Guid.NewGuid()
            };
            payment = await CreateAsync(payment);

            return payment.Id;
        }
    }
}
