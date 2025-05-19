using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Dtos.CommentsDto;
using TaskFlow.Application.UseCases.Interfaces;
using TaskFlow.Extensions.Middleware;

namespace TaskFlow.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController(ICommentsUseCase useCase) : ControllerBase
    {
        [AuthorizePermission(["search_comment"])]
        [HttpGet("{commentId}")]
        public async Task<ActionResult> GetCommentById([FromRoute] long commentId, CancellationToken token)
        {
            var comment = await useCase.GetCommentById(commentId, token);
            return Ok(comment);
        }

        [AuthorizePermission(["search_comment"])]
        [HttpGet("task/{maintaskId}")]
        public async Task<ActionResult> GetCommentsByMainTaskId([FromRoute] long maintaskId, CancellationToken token)
        {
            var comments = await useCase.GetCommentsByMainTaskId(maintaskId, token);
            return Ok(comments);
        }

        [AuthorizePermission(["create_comment"])]
        [HttpPost]
        public async Task<ActionResult> CreateNewComment([FromBody] CommentsRequestDto request, CancellationToken token)
        {
            var comment = await useCase.PostComment(request, token);

            return CreatedAtAction(nameof(GetCommentById), new { commentId = comment }, "Comentário criado comsucesso");
        }

        [AuthorizePermission(["update_comment"])]
        [HttpPut]
        public async Task<ActionResult> UpdateComment([FromBody] UpdateCommentRequestDto request, CancellationToken token)
        {
            var comment = await useCase.UpdateComment(request, token);
            return Ok(comment);
        }

        [AuthorizePermission(["delete_assignee"])]
        [HttpDelete("{commentId}/user/{userId}")]
        public async Task<ActionResult> DeleteComment([FromRoute] long commentId, CancellationToken token)
        {
            await useCase.DeleteComment(commentId, token);
            return Ok("Deletado com sucesso");
        }
    }
}
