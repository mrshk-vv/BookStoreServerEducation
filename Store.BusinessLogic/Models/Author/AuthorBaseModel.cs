using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Author
{
    public class AuthorBaseModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsRemoved { get; set; }
    }
}
