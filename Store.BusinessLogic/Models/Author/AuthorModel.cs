using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Author
{
    public class AuthorModel: AuthorBaseModel
    {
        public List<AuthorInPrintingEditionModel> AuthorInPrintingEditions { get; set; }
    }
}
