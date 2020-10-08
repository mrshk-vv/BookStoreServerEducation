using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAccess.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        public bool IsCanceled { get; set; }
    }
}
