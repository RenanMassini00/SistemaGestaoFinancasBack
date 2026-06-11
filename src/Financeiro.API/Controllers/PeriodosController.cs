using Financeiro.Application.DTOs; using Financeiro.Application.Interfaces; using Microsoft.AspNetCore.Authorization; using Microsoft.AspNetCore.Mvc;
namespace Financeiro.API.Controllers; [ApiController][Authorize][Route("api/periodos")] public class PeriodosController : ControllerBase
{
    private readonly IPeriodoService _service;

    public PeriodosController(IPeriodoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var result = await _service.GetAllAsync(ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PeriodoRequestDto request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] PeriodoRequestDto request, CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, request, ct);
        return Ok(result);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] PeriodoRequestDto request, CancellationToken ct)
    {
        var result = await _service.UpdateAsync(id, request, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var result = await _service.DeleteAsync(id, ct);
        return Ok(result);
    }

    [HttpPost("{periodoId:guid}/despesas")]
    public async Task<IActionResult> PostDespesa(Guid periodoId, [FromBody] DespesaRequestDto request, CancellationToken ct)
    {
        var result = await _service.AddDespesaAsync(periodoId, request, ct);
        return Ok(result);
    }


    [HttpPut("{periodoId:guid}/despesas/{despesaId:guid}")]
    public async Task<IActionResult> PutDespesa(
        Guid periodoId,
        Guid despesaId,
        [FromBody] DespesaRequestDto request,
        CancellationToken ct)
    {
        return Ok(await _service.UpdateDespesaAsync(periodoId, despesaId, request, ct));
    }

    [HttpDelete("{periodoId:guid}/despesas/{despesaId:guid}")]
    public async Task<IActionResult> DeleteDespesa(Guid periodoId, Guid despesaId, CancellationToken ct)
    {
        var result = await _service.DeleteDespesaAsync(periodoId, despesaId, ct);
        return Ok(result);
    }
}
