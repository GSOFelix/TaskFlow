using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Dtos.TaskAssigneeDto;
using TaskFlow.Application.UseCases.Interfaces;
using TaskFlow.Extensions.Middleware;

namespace TaskFlow.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class AssigneeController(ITaskAssigneeUseCase useCase) : ControllerBase
    {
        [AuthorizePermission(["search_assignee"])]
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetAll([FromRoute] long userId, CancellationToken token)
        {
            return Ok(await useCase.GetAllDesignationsByUser(userId, token));
        }

        [AuthorizePermission(["create_assignee"])]
        [HttpPost]
        public async Task<ActionResult> CreateNew([FromBody] TaskAssigneeRequestDto request, CancellationToken token)
        {
            await useCase.AssignUserToTask(request, token);

            return Created("", "Vinculado com sucesso");
        }

        [AuthorizePermission(["delete_assignee"])]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] TaskAssigneeRequestDto request, CancellationToken token)
        {
            await useCase.UnlinkUserToTask(request, token);
            return Ok("Desvinculado com sucesso");
        }
    }
}
