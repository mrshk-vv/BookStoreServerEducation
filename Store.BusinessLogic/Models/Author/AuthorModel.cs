using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Store.BusinessLogic.Models.AuthorInPrintingEdition;
using Store.BusinessLogic.Models.PrintingEdition;

namespace Store.BusinessLogic.Models.Author
{
    public class AuthorModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IList<AuthorInPEModel> PrintingEditions { get; set; }
        public bool IsRemoved { get; set; }
    }
}
