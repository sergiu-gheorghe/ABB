using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Users.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
