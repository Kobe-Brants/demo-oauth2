using Core.Interfaces.Repositories;

namespace BL.Services.User;

public class UserService : IUserService
{
    private readonly IGenericRepository<Domain.User> _userRepository;

    public UserService(IGenericRepository<Domain.User> userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<Domain.User> GetUsers(CancellationToken cancellationToken = default)
    {
        return _userRepository.GetAll();
    }

    public Domain.User? GetUser(int userId, CancellationToken cancellationToken)
    {
        return _userRepository.Find(x => x.Id == userId).FirstOrDefault();
    }

    public async Task CreateUser(Domain.User user, CancellationToken cancellationToken)
    {
        await _userRepository.Add(user, cancellationToken);
        await _userRepository.Save(cancellationToken);
    }

    public Task ModifyUser(Domain.User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUser(int userId, CancellationToken cancellationToken)
    {
        var user = _userRepository.Find(x => x.Id == userId).FirstOrDefault();

        if (user is not null)
            _userRepository.Delete(user);
        
        await _userRepository.Save(cancellationToken);
    }
}