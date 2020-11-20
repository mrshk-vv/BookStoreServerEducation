using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Shared.Enums;

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
        [Column(TypeName = "nvarchar(24)")]
        public Enums.Status Status { get; set; }
    }
}
