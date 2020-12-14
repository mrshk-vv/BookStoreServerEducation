using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Store.BusinessLogic.Models.Payment;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.Entities;
using Store.Shared.Enums;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public UserModel User { get; set; }
        public DateTime Date { get; set; }
        public int PaymentId { get; set; }
        public PaymentModel Payment { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public Enums.Status Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
