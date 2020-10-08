using System;
using System.Collections.Generic;
using System.Text;

namespace Store.BusinessLogic.Models.Tokens
{
    public class TokenRequestModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
