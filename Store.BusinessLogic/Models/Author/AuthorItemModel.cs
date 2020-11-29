using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Store.BusinessLogic.Models.PrintingEdition;

namespace Store.BusinessLogic.Models.Author
{
    public class AuthorItemModel: AuthorBaseModel
    {
        public List<int> PrintingEditions { get; set; }
    }
}
