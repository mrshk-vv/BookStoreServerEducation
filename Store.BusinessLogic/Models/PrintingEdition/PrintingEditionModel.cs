using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Store.BusinessLogic.Models.Author;
using Store.BusinessLogic.Models.AuthorInPrintingEdition;

namespace Store.BusinessLogic.Models.PrintingEdition
{
    public class PrintingEditionModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Currency { get; set; }
        public IList<AuthorInPEModel> Authors { get; set; }
        public bool IsRemoved { get; set; }

    }
}
