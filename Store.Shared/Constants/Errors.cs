using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Shared.Constants
{
    public partial class Constants
    {
        public static class Errors
        {
            //User errors
            public const string EMPTY_FIELD = "empty field";
            public const string USER_NOT_FOUND = "User not found";
            public const string USER_IS_BLOCKED = "User is blocked";
            public const string INVALID_CREDENTIALS = "Invalid password or email";
            public const string CREATE_USER_FAILED = "Creating user operation is failed";
            public const string REFRESH_TOKEN_NOT_EQUALS = "Refresh token(old) not equals refresh token from HttpClient";

            //Author errors
            public const string AUTHOR_EMPTY = "Data about author is empty";
            public const string AUTHOR_NOT_FOUND = "Author not found";
            public const string AUTHOR_ID_NOT_EXIST = "AuthorId not exist";
            public const string AUTHOR_ALREADY_EXIST = "Author already exist";

            //Edition errors
            public const string EDITION_EMPTY = "Data about Edition is empty";
            public const string EDITION_NOT_FOUND = "Edition not found";
            public const string EDITION_ID_NOT_EXIST = "EditionId not exist";
            public const string EDITION_ALREADY_EXIST = "Edition already exist";

            //OrderItem errors
            public const string ORDER_ITEM_EMPTY = "Data about Edition is empty";
            public const string ORDER_ITEM_NOT_FOUND = "Edition not found";
            public const string ORDER_ITEM_ID_NOT_EXIST = "EditionId not exist";
            public const string ORDER_ITEM_ALREADY_EXIST = "Edition already exist";

        }
    }
}
