using Store.BusinessLogic.Models.PrintingEdition;
using Store.Shared.Enums;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public Enums.Currency Currency { get; set; }
        public int PrintingEditionId { get; set; }
        public PrintingEditionModel PrintingEdition { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
        public OrderModel Order { get; set; }

    }
}
