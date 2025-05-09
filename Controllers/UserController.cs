using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Aplication.Dtos;
using TaskFlow.Application.UseCases.Interfaces;

namespace TaskFlow.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController(IUserUseCase userUseCase) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult> Select([FromRoute] long id, CancellationToken token)
        {
            return Ok(await userUseCase.GetUserById(id, token));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserRequestDto requestDto, CancellationToken token)
        {
            var newUser = await userUseCase.CreateUser(requestDto, token);
            return CreatedAtAction(nameof(Select), new { id = newUser }, newUser);
        }
    }
}
