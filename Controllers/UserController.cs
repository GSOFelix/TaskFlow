using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Dtos.UserDto;
using TaskFlow.Application.UseCases.Interfaces;
using TaskFlow.Extensions.Middleware;

namespace TaskFlow.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController(IUserUseCase userUseCase) : ControllerBase
    {
        [AuthorizePermission(["search_user"])]
        [HttpGet("{id}")]
        public async Task<ActionResult> Select([FromRoute] long id, CancellationToken token)
        {
            return Ok(await userUseCase.GetUserById(id, token));
        }

        [AuthorizePermission(["create_user"])]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserRequestDto requestDto, CancellationToken token)
        {
            var newUser = await userUseCase.CreateUser(requestDto, token);
            return CreatedAtAction(nameof(Select), new { id = newUser }, "Usuário criado com sucesso");
        }
    }
}
