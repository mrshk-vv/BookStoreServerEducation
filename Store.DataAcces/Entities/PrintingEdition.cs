using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Shared.Enums;

namespace Store.DataAccess.Entities
{
    public class PrintingEdition : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public Enums.Edition EditionType { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public Enums.Currency EditionCurrency { get; set; }
        public bool IsRemoved { get; set; }
        public List<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; }
    }
}
