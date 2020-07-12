namespace PepegaFoodServer.Contracts
{
    public static class ApiRoutes
    {
        public static class Identity
        {
            public const string Login = "api/identity/login";
            public const string Register = "api/identity/regist";
            public const string GetUserInfo = "api/identity/get_user_info/{username}";
            public const string CheckJWT = "api/identity/check_jwt";
        }


        public static class ClientData
        {
            public const string ChangePassword = "api/ClientData/change_password/{username}";
            public const string ChangeEmail = "api/ClientData/change_email/{username}";
            public const string ChangeFirstName = "api/ClientData/change_first_name/{username}";
            public const string ChangeLastName = "api/ClientData/change_last_name/{username}";
            public const string ChangePhone = "api/ClientData/change_phone/{username}";
            public const string ChangeAddress = "api/ClientData/change_address/{username}";
            public const string DeleteUser = "api/ClientData/delete_user/{username}";
        }


        public static class Products
        {
            public const string AddProducts = "api/Products/add_products";
            public const string GetNumOfAllPages = "api/Products/num_all_pages";
            public const string GetProducts = "api/Products/products";
            public const string GetNumOfPagesForCategory = "api/Products/num_all_pages_for_categories";
            public const string GetProductsByCategory = "api/Products/products_by_categories";
            public const string RemoveProducts = "api/Products/remove_products";
            public const string UpdateProduct = "api/Products/update_product";
            public const string GetProductByName = "api/Products/product_by_name";
            public const string GetProductById = "api/Products/product_by_id/{id}";
            public const string GetCategories = "api/Products/categories";
        }

    }
}
