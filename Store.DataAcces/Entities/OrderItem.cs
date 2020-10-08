using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAccess.Entities
{
    public class OrderItem : BaseEntity
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string PrintingEditionId { get; set; }
        public PrintingEdition PrintingEdition { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
