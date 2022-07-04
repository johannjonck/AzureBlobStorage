using Application.Dtos;
using Application.Logic.User.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public UsersController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new ListUserRequest());

                return new JsonResult(result.Entity.UserList)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }

            catch (Exception e)
            {
                return BadRequest("Exception when calling User: " + e.Message);
            }
        }
    }
}
