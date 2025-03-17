namespace NeoCart.Application.Abstractions;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}