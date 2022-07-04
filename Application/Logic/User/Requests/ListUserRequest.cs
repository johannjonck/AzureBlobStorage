
using Application.Logic.User.Responses;
using MediatR;

namespace Application.Logic.User.Requests
{
    public class ListUserRequest : IRequest<HandlerResult<ListUserResponse>>
    {

    }
}
