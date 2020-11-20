namespace Store.BusinessLogic.Models.Orders
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public int PrintingEditionId { get; set; }
    }
}
