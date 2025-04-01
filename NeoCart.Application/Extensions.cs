using System.Security.Claims;
using NeoCart.Application.DTOs;

namespace NeoCart.Application;

public static class Extensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationParams pagination)
    {
        return query.Skip(pagination.PageSize * (pagination.PageNumber - 1))
            .Take(pagination.PageSize);
    }
}