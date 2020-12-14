namespace Store.Shared.Constants
{
    public static partial class Constants
    {
        public static class Routes
        {
            public const string ERROR_ROUTE = "[/error]";

            //account controller
            public const string SIGN_UP_ROUTE = "SignUp";
            public const string SIGN_IN_ROUTE = "SignIn";
            public const string SIGN_OUT_ROUTE = "SignOut";
            public const string CONFIRM_EMAIL_ROUTE = "ConfirmEmail";
            public const string RESET_PASSWORD_ROUTE = "ResetPassword";
            public const string TOKENS_REFRESHING_ROUTE = "RefreshingTokens";

            //user controller
            public const string USER_DELETE_ROUTE = "deleteUser";
            public const string USER_BLOCK_ROUTE = "changeUserBlockStatus";
            public const string USER_GET_ROUTE = "getUser";
            public const string USER_UPDATE_ROUTE = "updateUser";
            public const string USERS_GET_ALL_ROUTE = "getUsers";


            //author controller
            public const string AUTHOR_CREATE_ROUTE = "createAuthor";
            public const string AUTHOR_GET_ROUTE = "getAuthor";
            public const string AUTHORS_GET_ALL_ROUTE = "getAuthors";
            public const string AUTHORS_GET_LIST_ROUTE = "getAuthorsList";
            public const string AUTHOR_UPDATE_ROUTE = "updateAuthor";
            public const string AUTHOR_DELETE_ROUTE = "deleteAuthor";
            public const string AUTHOR_REMOVE_ROUTE = "removeAuthor";

            //printingEdition controller
            public const string EDITION_CREATE_ROUTE = "createEdition";
            public const string EDITION_GET_ROUTE = "getEdition";
            public const string EDITIONS_GET_ALL_ROUTE = "getEditions";
            public const string EDITION_UPDATE_ROUTE = "updateEdition";
            public const string EDITION_REMOVE_ROUTE = "removeEdition";
            public const string EDITION_DELETE_ROUTE = "deleteEdition";


            //order controller
            public const string ORDER_CREATE_ROUTE = "createOrder";
            public const string ORDER_GET_ROUTE = "getOrder";
            public const string ORDERS_GET_ALL_ROUTE = "getOrders";
            public const string ORDERS_CLIENT_GET = "getClientOrders";
            public const string ORDER_PAY = "payOrder";
            public const string ORDER_UPDATE_ROUTE = "updateOrder";
            public const string ORDER_REMOVE_ROUTE = "removeOrder";

        }
    }
}
