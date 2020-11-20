using System;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }
        public int PaymentId { get; set; }
        public string Status { get; set; }
    }
}
