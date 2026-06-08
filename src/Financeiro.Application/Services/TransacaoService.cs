using Financeiro.Application.Common;
using Financeiro.Application.DTOs.Transacoes;
using Financeiro.Application.Interfaces.Services;
using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces;
using Financeiro.Domain.Interfaces.Repositories;

namespace Financeiro.Application.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public TransacaoService(ITransacaoRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TransacaoResponseDto>> CreateAsync(TransacaoRequestDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Transacao(
            request.Descricao,
            request.Valor,
            request.DataLancamento,
            request.Tipo,
            request.Categoria,
            request.Status,
            request.Conciliado);

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<TransacaoResponseDto>.Ok(Map(entity), "Transação criada com sucesso.");
    }

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return Result<bool>.Fail("Transação não encontrada.");

        _repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<bool>.Ok(true, "Transação removida com sucesso.");
    }

    public async Task<Result<IReadOnlyList<TransacaoResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        var result = items.Select(Map).ToList().AsReadOnly();
        return Result<IReadOnlyList<TransacaoResponseDto>>.Ok(result);
    }

    public async Task<Result<TransacaoResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null
            ? Result<TransacaoResponseDto>.Fail("Transação não encontrada.")
            : Result<TransacaoResponseDto>.Ok(Map(entity));
    }

    public async Task<Result<TransacaoResponseDto>> UpdateAsync(Guid id, TransacaoRequestDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return Result<TransacaoResponseDto>.Fail("Transação não encontrada.");

        entity.Atualizar(
            request.Descricao,
            request.Valor,
            request.DataLancamento,
            request.Tipo,
            request.Categoria,
            request.Status,
            request.Conciliado);

        _repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<TransacaoResponseDto>.Ok(Map(entity), "Transação atualizada com sucesso.");
    }

    private static TransacaoResponseDto Map(Transacao entity) => new()
    {
        Id = entity.Id,
        Descricao = entity.Descricao,
        Valor = entity.Valor,
        DataLancamento = entity.DataLancamento,
        Tipo = entity.Tipo,
        Categoria = entity.Categoria,
        Status = entity.Status,
        Conciliado = entity.Conciliado
    };
}
