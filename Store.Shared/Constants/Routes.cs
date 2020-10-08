namespace Store.Shared.Constants
{
    public static partial class Constants
    {
        public static class Routes
        {
            public const string ERROR_ROUTE = "[/error]";

            //account controller
            public const string SIGN_UP_ROUTE = "singUp";
            public const string SIGN_IN_ROUTE = "singIn";
            public const string SING_OUT_ROUTE = "singOut";
            public const string CONFIRM_EMAIL_ROUTE = "confirmEmail";
            public const string RESET_PASSWORD = "resetPassword";
            public const string TOKENS_REFRESHING_ROUTE = "refreshingTokens";

            //user controller
            public const string USER_DELETE_ROUTE = "deleteUser";
            public const string USER_BLOCK_ROUTE = "blockUser";
            public const string USER_GET_ROUTE = "getUser";
            public const string USERS_GET_ALL_ROUTE = "getUsers";

            //author controller
            public const string AUTHOR_CREATE_ROUTE = "createAuthor";
            public const string AUTHOR_GET_ROUTE = "getAuthor";
            public const string AUTHORS_GET_ALL_ROUTE = "getAuthors";
            public const string AUTHOR_UPDATE_ROUTE = "updateAuthor";
            public const string AUTHOR_REMOVE_ROUTE = "removeAuthor";

            //printingEdition controller
            public const string EDITION_CREATE_ROUTE = "createEdition";
            public const string EDITION_GET_ROUTE = "getEdition";
            public const string EDITIONS_GET_ALL_ROUTE = "getEditions";
            public const string EDITION_UPDATE_ROUTE = "updateEdition";
            public const string EDITION_REMOVE_ROUTE = "removeEdition";
        }
    }
}
