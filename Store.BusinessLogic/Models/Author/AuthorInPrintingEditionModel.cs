using System;
using Store.BusinessLogic.Models.PrintingEdition;

namespace Store.BusinessLogic.Models.Author
{
    public class AuthorInPrintingEditionModel
    {
        public int AuthorId { get; set; }
        public AuthorModel Author { get; set; }
        public int PrintingEditionId { get; set; }
        public PrintingEditionModel PrintingEdition { get; set; }
        public DateTime Date { get; set; }
    }
}
