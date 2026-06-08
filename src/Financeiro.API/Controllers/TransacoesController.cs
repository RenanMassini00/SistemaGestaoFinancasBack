using Financeiro.Application.Common;
using Financeiro.Application.DTOs.Transacoes;
using Financeiro.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.API.Controllers;

[ApiController]
[Authorize]
[Route("api/transacoes")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _service;

    public TransacoesController(ITransacaoService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<IReadOnlyList<TransacaoResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<TransacaoResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<TransacaoResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(id, cancellationToken);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<TransacaoResponseDto>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] TransacaoRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Result<TransacaoResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<TransacaoResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid id, [FromBody] TransacaoRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _service.UpdateAsync(id, request, cancellationToken);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.DeleteAsync(id, cancellationToken);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }
}
