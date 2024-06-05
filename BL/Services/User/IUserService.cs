namespace BL.Services.User
{
    public interface IUserService
    {
        IEnumerable<Domain.User> GetUsers(CancellationToken cancellationToken = default);

        Domain.User? GetUser(int userId, CancellationToken cancellationToken);

        Task CreateUser(Domain.User user, CancellationToken cancellationToken);

        Task ModifyUser(Domain.User user, CancellationToken cancellationToken);

        Task DeleteUser(int audienceId, CancellationToken cancellationToken);
    }
}
