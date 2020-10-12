using System;

namespace Store.BusinessLogic.Models.AuthorInPrintingEdition
{
    public class AuthorInPEModel
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int PrintingEditionId { get; set; }
        public string PrintingEditionTitle { get; set; }
        public DateTime Date { get; set; }
    }
}
