using Application.Users.Repositories;
using Domain;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class GetUsersQuery : IRequest<IEnumerable<User>>
    {
        // Could've add more filters here but just to take less time to implement it
        public int? UserId { get; set; }
        public string Name { get; set; }
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        private IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersAsync();

            if(request.UserId != null)
            {
                users = users.Where(u => u.Id == request.UserId);
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
               users = users.Where(u => u.Name.Contains(request.Name));
            }
            
            return users;
        }
    }
}
