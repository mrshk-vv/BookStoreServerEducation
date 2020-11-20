using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAccess.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public bool IsRemoved { get; set; }
        public List<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; }
    }
}
