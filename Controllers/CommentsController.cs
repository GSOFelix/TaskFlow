using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Dtos.CommentsDto;
using TaskFlow.Application.UseCases.Interfaces;

namespace TaskFlow.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommentsController(ICommentsUseCase useCase) : ControllerBase
    {
        [HttpGet("{commentId}")]
        public async Task<ActionResult> GetCommentById([FromRoute] long commentId, CancellationToken token)
        {
            var comment = await useCase.GetCommentById(commentId, token);
            return Ok(comment);
        }

        [HttpGet("task/{maintaskId}")]
        public async Task<ActionResult> GetCommentsByMainTaskId([FromRoute] long maintaskId, CancellationToken token)
        {
            var comments = await useCase.GetCommentsByMainTaskId(maintaskId, token);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewComment([FromBody] CommentsRequestDto request, CancellationToken token)
        {
            var comment = await useCase.PostComment(request, token);

            return CreatedAtAction(nameof(GetCommentById), new { commentId = comment }, "Criado comsucesso");
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateComment([FromBody] UpdateCommentRequestDto request, CancellationToken token)
        {
            var comment = await useCase.UpdateComment(request, token);
            return Ok(comment);
        }

        [HttpDelete("{commentId}/user/{userId}")]
        public async Task<ActionResult> DeleteComment([FromRoute] long commentId, [FromRoute] long userId, CancellationToken token)
        {
            await useCase.DeleteComment(commentId, userId, token);
            return Ok("Deletado com sucesso");
        }
    }
}
