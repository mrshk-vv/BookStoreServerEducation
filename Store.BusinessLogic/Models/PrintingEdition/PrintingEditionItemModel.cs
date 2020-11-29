using System.Collections.Generic;
using Store.BusinessLogic.Models.Author;

namespace Store.BusinessLogic.Models.PrintingEdition
{
    public class PrintingEditionItemModel: PrintingEditionBaseModel
    {
        public List<int> Authors { get; set; }
    }
}
