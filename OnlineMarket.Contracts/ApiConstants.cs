namespace OnlineMarket.Contracts
{
    public class ApiConstants
    {
        public const string BaseUrl = "api/";
        public const string Version = "v1/";

        public static class AuthRoutes
        {
            public const string Base = BaseUrl + Version + "auth/";
            public const string SignIn = Base + "signin";
            public const string SignUp = Base + "signup";
            public const string SellerSignUp = Base + "signup/seller";
        }

        public static class ProductRoutes
        {
            public const string Base = BaseUrl + Version + "product";
            public const string GetAllProducts = Base;
            public const string GetUserProducts = Base + "/user";
            public const string UnapprovedProducts = Base + "/unapproved";
            public const string GetProductById = Base + "/{id}";
            public const string CreateProduct = Base;
            public const string UpdateProduct = Base + "/{id}";
            public const string ApproveProduct = Base + "/{id}/approve";
            public const string DeleteProduct = Base + "/{id}";

        }
        public static class TransactionRoutes
        {
            public const string Base = BaseUrl + Version + "transaction";
            public const string GetAllTransactions = Base;
            public const string GetTransactionById = Base + "/{id}";
            public const string CreateTransaction = Base;
            public const string UpdateTransaction = Base + "/{id}";
            public const string DeleteTransaction = Base + "/{id}";
        }
    }
}