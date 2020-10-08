namespace Store.Shared.Constants
{
    public static partial class Constants
    {
        public static class Password
        {
            public const string PASSWORD_PATERN = @"(?=.*[0 - 9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{16,}";
            public const int PASSWORD_LENGHT = 16;
            public const int SIMBOLS_RANGE_START = 33;
            public const int SIMBOLS_RANGE_END = 125;
        }
    }
}
