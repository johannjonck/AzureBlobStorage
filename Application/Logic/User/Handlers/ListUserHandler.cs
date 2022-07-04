using Application.Dtos;
using Application.Logic.User.Responses;
using Application.Logic.User.Requests;
using AutoMapper;
using Persistance;

namespace Application.Logic.User.Handlers
{
    public class ListUserHandler : HandlerBase<ListUserRequest, HandlerResult<ListUserResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public ListUserHandler(IDbContext context, IMapper mapper)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override HandlerResult<ListUserResponse> HandleRequest(ListUserRequest request, CancellationToken cancellationToken, HandlerResult<ListUserResponse> result)
        {
            var user = _dbContext.User.ToList();

            result.Entity = new ListUserResponse
            {
                UserList = _mapper.Map<List<UserDto>>(user)
            };

            return result;
        }
    }
}
