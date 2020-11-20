using System;
using System.Collections.Generic;
using System.Text;
using Store.BusinessLogic.Models.Author;
using Store.Shared.Enums;

namespace Store.BusinessLogic.Models.PrintingEdition
{
    public class PrintingEditionModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Enums.Edition EditionType { get; set; }
        public Enums.Currency EditionCurrency { get; set; }
        public bool IsRemoved { get; set; }
        public List<AuthorInPrintingEditionModel> AuthorInPrintingEditions { get; set; }
    }
}
