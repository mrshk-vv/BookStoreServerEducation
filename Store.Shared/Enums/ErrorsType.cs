using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Shared.Enums
{
    public partial class Enums
    {
        public enum Errors
        {
            [EnumDescription("None error")]
            None = 0,

            [EnumDescription("Bad Request")]
            BadRequest = 400,

            [EnumDescription("Unauthorized")]
            Unauthorized = 401,

            [EnumDescription("Forbidden")]
            Forbidden = 403,
            [EnumDescription("Not Found")]
            NotFound = 404,

            [EnumDescription("Method not allowed")]
            MethodNotAllowed = 405,

            [EnumDescription("Request TimeOut")]
            RequestTimeOut = 408,

            [EnumDescription("Internal Server Error")]
            InternalServerError = 500


        }
    }

    public class EnumDescription : Attribute
    {
        public string Description { get; set; }
        public EnumDescription(string description = null)
        {
            Description = description is null ? string.Empty : description;
        }
    }
}
