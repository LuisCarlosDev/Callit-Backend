using AutoMapper;
using Callit.Communication.Responses;
using Callit.Domain.Repositories.Users;

namespace Callit.Application.UseCases.Users.ListUsers;

public class ListUsersUseCase : IListUsersUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ListUsersUseCase(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<ResponseUsersJson>> Execute()
    {
        var users = await _userRepository.ListUsers();

        return _mapper.Map<List<ResponseUsersJson>>(users);
    }
}
