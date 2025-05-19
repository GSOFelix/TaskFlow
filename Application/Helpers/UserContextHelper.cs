using System.Security.Claims;
using TaskFlow.Domain.Exceptions;

namespace TaskFlow.Application.Helpers
{
    public static class UserContextHelper
    {
        public static long EffectiveUserId(ClaimsPrincipal? user, long? dtoUserID = null)
        {
            var userClaim = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(string.IsNullOrEmpty(userClaim) )
                throw new UnauthorizeException("Usuário não autenticado");

            if (!long.TryParse(userClaim, out var tokenId))
                throw new UnauthorizeException("Token inválido");

           /* if (dtoUserID.HasValue && dtoUserID.Value != tokenId)
            {
                if (!IsAdmin(user))
                    throw new ForbiddenException("Você não tem permissão para agir em nome de outro usuário.");

                return dtoUserID.Value;
            }*/

            return tokenId;
        }

        public static bool IsAdmin(ClaimsPrincipal? user)
        {
            var permissions = user?.FindAll("permission").Select(p => p.Value);

            return permissions != null && permissions.Contains("ADM", StringComparer.OrdinalIgnoreCase);
        }
    }

}
