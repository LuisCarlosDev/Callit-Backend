using Callit.Communication.Responses;

namespace Callit.Application.UseCases.Users.ListUsers;

public interface IListUsersUseCase
{
    Task<List<ResponseUsersJson>> Execute();
}
