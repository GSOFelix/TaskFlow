using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Dtos.TaskAssigneeDto;
using TaskFlow.Application.UseCases.Interfaces;

namespace TaskFlow.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssigneeController(ITaskAssigneeUseCase useCase) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAll([FromRoute] long id, CancellationToken token)
        {
            return Ok(await useCase.GetAllDesignationsByUser(id, token));
        }

        [HttpPost]
        public async Task<ActionResult> CreateNew([FromBody] TaskAssigneeRequestDto request, CancellationToken token)
        {
            await useCase.AssignUserToTask(request, token);

            return Created("", "Vinculado com sucesso");
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] TaskAssigneeRequestDto request, CancellationToken token)
        {
            await useCase.UnlinkUserToTask(request, token);
            return Ok("Desvinculado com sucesso");
        }
    }
}
