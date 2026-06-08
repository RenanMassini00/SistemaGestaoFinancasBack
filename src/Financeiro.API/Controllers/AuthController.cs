using Financeiro.Application.Common;
using Financeiro.Application.DTOs.Auth;
using Financeiro.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(Result<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<AuthResponseDto>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request, cancellationToken);
        if (!result.Success)
            return Unauthorized(result);

        return Ok(result);
    }
}
