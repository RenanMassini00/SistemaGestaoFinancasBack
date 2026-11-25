using Financeiro.Application.DTOs;
using Financeiro.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.API.Controllers;

[ApiController]
[Authorize]
[Route("api/investimentos")]
public class InvestimentosController : ControllerBase
{
    private readonly IInvestimentoService _service;

    public InvestimentosController(IInvestimentoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] InvestimentoRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(request, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(
        Guid id,
        [FromBody] InvestimentoPatchDto request,
        CancellationToken cancellationToken)
    {
        var result = await _service.UpdateAsync(id, request, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.DeleteAsync(id, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("{id:guid}/atualizacoes")]
    public async Task<IActionResult> PostAtualizacao(
        Guid id,
        [FromBody] AtualizacaoInvestimentoRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _service.AddAtualizacaoAsync(id, request, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
