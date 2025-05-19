using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Dtos;
using TaskFlow.Application.Dtos.MainTaskDto;
using TaskFlow.Application.UseCases.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Extensions.Middleware;

namespace TaskFlow.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class MainTaskController(IMainTaskUseCase useCase) : ControllerBase
    {
        [AuthorizePermission(["create_task"])]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MainTaskRequestDto request, CancellationToken token)
        {
            var response = await useCase.CreateMainTask(request, token);

            return CreatedAtAction(nameof(SelectByMainTask), new { mainTaskId = response, detail = true},response);
        }

        [AuthorizePermission(["ADM"])] 
        [HttpGet("{userId}/{detail}")]
        public async Task<ActionResult> SelectAllByUser([FromRoute] long userId, CancellationToken token, [FromRoute] bool detail = false)
        {
            return Ok(await useCase.GetAllMainTasks(userId, detail, token));
        }

        [AuthorizePermission(["search_task"])]
        [HttpGet("MyTask/{detail}")]
        public async Task<ActionResult> SelectMyTasks(CancellationToken token, [FromRoute] bool detail = false)
        {
            return Ok(await useCase.GetAllMyTasks(detail, token));
        }

        [AuthorizePermission(["search_task"])]
        [HttpGet("task/{mainTaskId}/{detail}")]
        public async Task<ActionResult> SelectByMainTask([FromRoute] long mainTaskId, CancellationToken token, [FromRoute] bool detail = false)
        {
            return Ok(await useCase.GetMainTaskById(mainTaskId, detail, token));
        }

        [AuthorizePermission(["update_task"])]
        [HttpPatch]
        public async Task<ActionResult> UpdtadeStatusTask([FromBody] MainTaskUpdateStatusDto request,CancellationToken token)
        {
            await useCase.UpdateStatus(request, token);
            return Ok("Status atualizado com sucesso");
        }
    }
}
