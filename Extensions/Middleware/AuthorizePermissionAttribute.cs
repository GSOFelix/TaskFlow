using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskFlow.Domain.Exceptions;

namespace TaskFlow.Extensions.Middleware
{
    public class AuthorizePermissionAttribute : Attribute, IAuthorizationFilter
    {

        private readonly string[] _requiredPermissions;

        public AuthorizePermissionAttribute(string[] requiredPermissions)
        {
            _requiredPermissions = requiredPermissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Recupera todas as permissões do usuário
            var userPermissions = user.Claims
                .Where(c => c.Type == "permission")
                .Select(c => c.Value)
                .ToList();

            // Se o usuário tiver "adm", permite automaticamente
            if (userPermissions.Contains("ADM", StringComparer.OrdinalIgnoreCase))
                return;

            // Verifica se o usuário tem pelo menos uma das permissões exigidas
            var hasPermission = _requiredPermissions.Any(rp =>
                userPermissions.Contains(rp, StringComparer.OrdinalIgnoreCase));


            if (!hasPermission)
            {
                throw new ForbiddenException("Usuário sem permissão");
            }
        }
    }
}
