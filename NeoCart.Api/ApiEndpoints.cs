namespace NeoCart.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";
    private const string ProductsBase = $"{ApiBase}/products";
    private const string ReviewsBase = $"{ApiBase}/reviews";
    private const string CartsBase = $"{ApiBase}/cart/me";
    private const string OrdersBase = $"{ApiBase}/orders";
    private const string AuthBase = $"{ApiBase}/auth";
    private const string UsersBase = $"{ApiBase}/users/me";

    public static class Products
    {
        public const string GetAll = ProductsBase;
        public const string GetById = $"{ProductsBase}/{{id:guid}}";
        public const string Create = ProductsBase;
        public const string Update = $"{ProductsBase}/{{id:guid}}";
        public const string Delete = $"{ProductsBase}/{{id:guid}}";
    }

    public static class Reviews
    {
        public const string GetByProduct = $"{ProductsBase}/{{productId:guid}}/reviews";
        public const string Add = $"{ProductsBase}/{{productId:guid}}/reviews";
        public const string Update = $"{ReviewsBase}/{{id:guid}}";
        public const string Delete = $"{ReviewsBase}/{{id:guid}}";
    }

    public static class Carts
    {
        public const string GetUserCart = CartsBase;
        public const string AddItem = $"{CartsBase}/items";
        public const string UpdateItemQuantity = $"{CartsBase}/items/{{itemId:guid}}";
        public const string RemoveItem = $"{CartsBase}/items/{{itemId:guid}}";
        public const string Clear = CartsBase;
    }

    public static class Orders
    {
        // Customer-specific endpoints
        public const string GetUserOrders = $"{OrdersBase}/me";
        public const string Checkout = $"{OrdersBase}/me";
        public const string CancelUserOrder = $"{OrdersBase}/me/{{id:guid}}";

        // Seller-specific endpoints
        public const string GetSellerOrders = $"{OrdersBase}/seller";
        public const string UpdateStatus = $"{OrdersBase}/seller/{{id:guid}}";

        // General order endpoints
        public const string GetById = $"{OrdersBase}/{{id:guid}}";
        public const string GetAll = OrdersBase;
    }

    public static class Auth
    {
        public const string Register = $"{AuthBase}/register";
        public const string Login = $"{AuthBase}/login";
        public const string Logout = $"{AuthBase}/logout";

        public const string ConfirmEmail = $"{AuthBase}/email/confirm";

        public const string ForgotPassword = $"{AuthBase}/forgot-password";
        public const string ResetPassword = $"{AuthBase}/reset-password";
    }

    public static class Users
    {
        public const string GetUserProfile = UsersBase;
        public const string UpdateProfile = UsersBase;
        
        public const string GetSellerDashboard = $"{UsersBase}/dashboard";
        
        public const string ChangePassword = $"{UsersBase}/change-password";
    }
}