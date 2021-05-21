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

        public static class UserRoutes
        {
            public const string Base = BaseUrl + Version + "user/{userId}";
            public const string GetUserByID = Base;
            public const string UpdateUser = Base;
            public const string DeleteUser = Base;
            public const string GetWishList = Base + "/wishlist";
            public const string AddToWishList = Base + "/wishlist";
            public const string RemoveFromWishList = Base + "/wishlist";
            public const string GetTransactionHistory = Base + "/transactions";

        }

        public static class ProductRoutes
        {
            public const string Base = BaseUrl + Version + "product";
            public const string GetAllProducts = Base;
            public const string GetUserProducts = Base + "/user";
            public const string UnapprovedProducts = Base + "/unapproved";
            public const string RejectedProducts = Base + "/rejected";
            public const string GetProductById = Base + "/{id}";
            public const string CreateProduct = Base;
            public const string UpdateProduct = Base + "/{id}";
            public const string ApproveProduct = Base + "/{id}/approve";
            public const string DeleteProduct = Base + "/{id}";
            public const string MakeMain = Base + "/{id}/media/{imageId}";
            public const string DeleteImage = Base + "/{id}/media/{imageId}";
            public const string GetCategories = Base + "categories";
            public const string AddCategory = Base + "category";
            public const string UpdateCategory = Base + "category/{id}";
            public const string DeleteCategory = Base + "category/{id}";

        }

        public static class ProductReviewRoutes
        {
            public const string Base = BaseUrl + Version + "{id}/reviews" ;
            public const string GetReviewsByProductId = Base ;
            public const string GetReviewsByUserId = BaseUrl + Version + "{userId}/reviews" ;
            public const string CreateProductReview = Base ;
            public const string UpdateProductReview = BaseUrl + Version + "reviews/{id}" ;
            public const string DeleteProductReview = BaseUrl + Version + "reviews/{id}" ;

        }
        public static class TransactionRoutes
        {
            public const string Base = BaseUrl + Version + "transactions";
            public const string GetAllTransactions = Base;
            public const string GetTransactionById = Base + "/{id}";
            public const string CreateTransaction = Base;
            public const string UpdateTransaction = Base + "/{id}";
            public const string DeleteTransaction = Base + "/{id}";
        }
    }
}