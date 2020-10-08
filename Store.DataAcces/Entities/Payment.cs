using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAccess.Entities
{
    public class Payment : BaseEntity
    {
        public Guid TransactionId { get; set; }
    }
}
