using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Shared.Enums;

namespace Store.DataAccess.Entities
{
    public class OrderItem : BaseEntity
    {
        public int Amount { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public Enums.Currency Currency { get; set; }
        public int PrintingEditionId { get; set; }
        public PrintingEdition PrintingEdition { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
