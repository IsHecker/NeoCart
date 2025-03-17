namespace NeoCart.Application.Common;

public static class Roles
{
    public const string User = "User";
    public const string Seller = "Seller";
    public const string Admin = "Admin";

    public static bool IsValidRole(string role)
    {
        return role.Equals(User, StringComparison.OrdinalIgnoreCase) ||
               role.Equals(Seller, StringComparison.OrdinalIgnoreCase) ||
               role.Equals(Admin, StringComparison.OrdinalIgnoreCase);
    }
}