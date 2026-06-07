namespace Bookify.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetUserById(Guid userId,CancellationToken cancellationToken = default);
    
    void Add(User user);
}