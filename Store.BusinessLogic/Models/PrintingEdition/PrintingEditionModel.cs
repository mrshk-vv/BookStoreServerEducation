using System.ComponentModel.DataAnnotations;

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
        public string AuthorName { get; set; }
        public bool IsRemoved { get; set; }

    }
}
