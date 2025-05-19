using System.Security.Claims;
using System.Threading.Tasks;
using TaskFlow.Application.Helpers;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Exceptions;

namespace TaskFlow.Application.Auth
{
    public static class PermissionValidator
    {
        public static void Validate(ClaimsPrincipal? user, MainTask mainTask, long userId)
        {
            bool isCreator = mainTask.UserId == userId;
            bool isAdmin = UserContextHelper.IsAdmin(user);
            bool isAssignee = mainTask.TaskAssignees.Any(a => a.UserId == userId);

            if (!(isAdmin || isCreator || isAssignee))
            {
                throw new ForbiddenException("Você não tem permissão para isso");
            }
        }

        public static void ValidateStatusTask(ClaimsPrincipal? user, MainTask mainTask, long userId, ETaskStatus status)
        {
            var isCreator = mainTask.UserId == userId;
            var isAdmin = UserContextHelper.IsAdmin(user);

            if (status == ETaskStatus.Done)
            {
                if (!(isCreator || isAdmin))
                    throw new ForbiddenException("Apenas o criador da tarefa pode marcá-la como concluída.");
            }
        }
    }
}
