﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Author
{
    public class AuthorModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<AuthorInPrintingEditionModel> AuthorInPrintingEditions { get; set; }
        public bool IsRemoved { get; set; }
    }
}
