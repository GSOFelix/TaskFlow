using TaskFlow.Application.Auth;
using TaskFlow.Application.Dtos;
using TaskFlow.Application.Dtos.CommentsDto;
using TaskFlow.Application.Helpers;
using TaskFlow.Application.Mappers;
using TaskFlow.Application.UseCases.Interfaces;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Infra.Repositories;

namespace TaskFlow.Application.UseCases.Implementations
{
    public class CommentsUseCase : ICommentsUseCase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserUseCase _userUseCase;
        private readonly IMainTaskRepository _mainTaskRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public CommentsUseCase(ICommentRepository commentRepository, IUserUseCase userUseCase,
            IMainTaskRepository mainTaskRepository, IHttpContextAccessor contextAccessor)
        {
            _commentRepository = commentRepository;
            _userUseCase = userUseCase;
            _mainTaskRepository = mainTaskRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task DeleteComment(long commentId, CancellationToken token)
        {
            // Selecionar o userId da requisição
            var user = _contextAccessor.HttpContext?.User;
            var userId = UserContextHelper.EffectiveUserId(user);

            var comment = await _commentRepository.GetByIdAsync(commentId, token)
                ?? throw new Exception("Comentário nao encontrado");

            if (comment.UserId != userId)
                throw new ForbiddenException("Você não tem permissão para deletar este comentário.");

            await _commentRepository.DeleteAsync(comment, token);
        }

        public async Task<CommentsResponseDto> GetCommentById(long commentId, CancellationToken token)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId, token)
                ?? throw new Exception("Comentário nao encontrado");

            return comment.ToDto();
        }

        public async Task<IEnumerable<CommentsResponseDto>> GetCommentsByMainTaskId(long mainTaskId, CancellationToken token)
        {
            // Selecionar o userId da requisição
            var user = _contextAccessor.HttpContext?.User;
            var userId = UserContextHelper.EffectiveUserId(user);

            // Seleciona a tarefa
            var task = await _mainTaskRepository.GetByIdAsync(mainTaskId, true, token)
                ?? throw new NotFoundException("Tarefa não encontrada");

            PermissionValidator.Validate(user, task, userId);

            var comments = await _commentRepository.GetAllbyTaskAsync(mainTaskId, token);

            return comments.ToListDto() ?? throw new NotFoundException("Nemhum comentário encontrado");
        }

        public async Task<long> PostComment(CommentsRequestDto request, CancellationToken token)
        {
            // Selecionar o userId da requisição
            var user = _contextAccessor.HttpContext?.User;
            var userId = UserContextHelper.EffectiveUserId(user);

            // Verificar existencia da tarefa;
            var task = await _mainTaskRepository.GetByIdAsync(request.MainTaskId, true, token)
                ?? throw new NotFoundException("Tarefa não encontrada");

            // Verificar se o usuario Existe;
            await _userUseCase.GetUserById(userId, token);

            PermissionValidator.Validate(user, task, userId);

            // Registra Comentario;
            return await _commentRepository.InsertAsync(request.ToEntity(userId), token);
        }

        public async Task<CommentsResponseDto> UpdateComment(UpdateCommentRequestDto request, CancellationToken token)
        {
            // Selecionar o userId da requisição
            var user = _contextAccessor.HttpContext?.User;
            var userId = UserContextHelper.EffectiveUserId(user);

            // Selecionar o comentário 
            var comment = await _commentRepository.GetByIdAsync(request.Id, token)
                 ?? throw new NotFoundException("Comentario não encontrado");

            // Verifica se o usuário é o autor do comentário
            if (comment.UserId != userId)
                throw new ForbiddenException("Você não tem permissão para editar este comentário.");

            // Atualiza o texto
            comment.UpdateText(request.Comment);

            // Salva as alterações
            await _commentRepository.UpdateAsync(comment, token);

            return comment.ToDto();
        }
    }
}
