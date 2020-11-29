using System;
using System.Collections.Generic;
using System.Text;
using Store.BusinessLogic.Models.Author;
using Store.Shared.Enums;

namespace Store.BusinessLogic.Models.PrintingEdition
{
    public class PrintingEditionModel: PrintingEditionBaseModel
    {
        public List<AuthorInPrintingEditionModel> AuthorInPrintingEditions { get; set; }
    }
}
