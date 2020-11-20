using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public class AuthorInPrintingEdition
    {
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        public int PrintingEditionId { get; set; }
        [ForeignKey("PrintingEditionId")]
        public PrintingEdition PrintingEdition { get; set; }

        public DateTime Date { get; set; }

    }
}
