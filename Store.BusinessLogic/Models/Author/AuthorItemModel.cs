using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Store.BusinessLogic.Models.PrintingEdition;

namespace Store.BusinessLogic.Models.Author
{
    public class AuthorItemModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<PrintingEditionModel> PrintingEditions { get; set; }
        public bool IsRemoved { get; set; }
    }
}
