using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Dtos.AuthDto;
using TaskFlow.Application.UseCases.Interfaces;

namespace TaskFlow.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthorizeController(IAuthorizationUser authorization) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Login(AuthRequestDto authRequest,CancellationToken token)
        {
            var response = await authorization.AuthenticateUser(authRequest, token);

            return Ok(response);
        }
    }
}
