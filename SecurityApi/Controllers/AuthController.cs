using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using SecurityApi.Models;
using SecurityApi.Services.Interfaces;
using SecurityApi.Utils;
using SecurityApi.VM;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SecurityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SecurityCoreContext _context;
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(SecurityCoreContext context, ILogger<AuthController> logger, IAuthService authService)
        {
            _context = context;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthVM auth)
        {
            _logger.LogInformation("------Start AuthController - Authenticate ------");
            try
            {
                //Valid Model
                if (!ModelState.IsValid) 
                {
                    _logger.LogWarning("The model is invalid");
                    return BadRequest(ErrorHelper.GetErrorsFromModelState(ModelState));
                }

                //Valid User
                _logger.LogInformation("Search User in db");
                var user = await _context.Users.FirstOrDefaultAsync(x=> x.EmailUser == auth.EmailUser);

                if (user is null)
                {
                    _logger.LogWarning("User not found");
                    return NotFound("User not found");
                }

                //Valid User Password
                _logger.LogInformation("Compare Hashes");
                if (SecurityHelper.CheckHashes(auth.PasswordUser, user.PasswordUser, user.Salt))
                {
                    _logger.LogWarning("Invalid Credentials");
                    return Forbid();
                }

                var barerToken = _authService.CreateToken(auth.EmailUser);

                if (string.IsNullOrEmpty(barerToken))
                {
                    _logger.LogWarning("Error with token creation");
                    return Problem("Error with token creation", null, null, "Auth Fail", null);
                }

                _logger.LogInformation("Authenticate Succefull");
                return Ok(barerToken);

            }
            catch (Exception e)
            {
                _logger.LogError("Auth Fail: {0}",e.Message);
                return Problem(e.Message, null, null, "Auth Fail", null);
            }
            finally
            {
                _logger.LogInformation("------Finish AuthController - Authenticate ------");
            }
        }
    }
}
