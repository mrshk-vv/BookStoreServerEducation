using System;
using System.Collections.Generic;
using System.Text;

namespace Store.BusinessLogic.Models.Tokens
{
    public class TokenResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
