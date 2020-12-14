using Store.BusinessLogic.Models.Orders;

namespace Store.BusinessLogic.Models.Payment
{
    public class PaymentModel
    {
        public OrderModel Order { get; set; }
        public string Token { get; set; }
    }
}
