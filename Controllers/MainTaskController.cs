using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Dtos;
using TaskFlow.Application.UseCases.Interfaces;

namespace TaskFlow.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MainTaskController(IMainTaskUseCase useCase) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MainTaskRequestDto request, CancellationToken token)
        {
            var response = await useCase.CreateMainTask(request, token);

            return Ok(response);
        }

        [HttpGet("{userId}/{detail}")]
        public async Task<ActionResult> SelectAllByUser([FromRoute] long userId, CancellationToken token, [FromRoute] bool detail = false)
        {
            return Ok(await useCase.GetAllMainTasks(userId, detail, token));
        }

        [HttpGet("task/{mainTaskId}/{detail}")]
        public async Task<ActionResult> SelectByMainTask([FromRoute] long mainTaskId, CancellationToken token, [FromRoute] bool detail = false)
        {
            return Ok(await useCase.GetMainTaskById(mainTaskId, detail, token));
        }
    }
}
