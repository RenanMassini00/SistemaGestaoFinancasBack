using Financeiro.Application.Common;
using Financeiro.Application.DTOs;
using Financeiro.Application.Interfaces;
using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces;
using Financeiro.Domain.Interfaces.Repositories;
using System.Text.Json;

namespace Financeiro.Application.Services;

public class InvestimentoService : IInvestimentoService
{
    private readonly IInvestimentoRepository _investimentos;
    private readonly IUnitOfWork _unitOfWork;

    public InvestimentoService(IInvestimentoRepository investimentos, IUnitOfWork unitOfWork)
    {
        _investimentos = investimentos;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IReadOnlyList<InvestimentoResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var investimentos = await _investimentos.GetAllAsync(cancellationToken);
        var response = investimentos
            .OrderBy(x => x.Nome)
            .Select(Map)
            .ToList()
            .AsReadOnly();
        return Result<IReadOnlyList<InvestimentoResponseDto>>.Ok(response);
    }

    public async Task<Result<InvestimentoResponseDto>> CreateAsync(
        InvestimentoRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validation = Validate(request.Nome, request.Tipo, request.Instituicao,
            request.ValorInvestido, request.SaldoAtual, request.MetaValor);
        if (validation is not null)
            return Result<InvestimentoResponseDto>.Fail(validation);

        var investimento = new Investimento(
            request.Nome.Trim(),
            request.Tipo.Trim(),
            request.Instituicao.Trim(),
            request.ValorInvestido,
            request.SaldoAtual,
            request.MetaValor);

        await _investimentos.AddAsync(investimento, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<InvestimentoResponseDto>.Ok(Map(investimento), "Investimento criado com sucesso.");
    }

    public async Task<Result<InvestimentoResponseDto>> UpdateAsync(
        Guid id,
        InvestimentoPatchDto request,
        CancellationToken cancellationToken = default)
    {
        var investimento = await _investimentos.GetByIdAsync(id, cancellationToken);
        if (investimento is null)
            return Result<InvestimentoResponseDto>.Fail("Investimento não encontrado.");

        var nome = request.Nome?.Trim() ?? investimento.Nome;
        var tipo = request.Tipo?.Trim() ?? investimento.Tipo;
        var instituicao = request.Instituicao?.Trim() ?? investimento.Instituicao;
        var valorInvestido = request.ValorInvestido ?? investimento.ValorInvestido;
        var saldoAtual = request.SaldoAtual ?? investimento.SaldoAtual;
        var metaValor = investimento.MetaValor;
        if (request.MetaValor.ValueKind == JsonValueKind.Null)
        {
            metaValor = null;
        }
        else if (request.MetaValor.ValueKind == JsonValueKind.Number &&
                 request.MetaValor.TryGetDecimal(out var metaInformada))
        {
            metaValor = metaInformada;
        }
        else if (request.MetaValor.ValueKind != JsonValueKind.Undefined)
        {
            return Result<InvestimentoResponseDto>.Fail("O valor da meta deve ser numérico ou nulo.");
        }

        var validation = Validate(nome, tipo, instituicao, valorInvestido, saldoAtual, metaValor);
        if (validation is not null)
            return Result<InvestimentoResponseDto>.Fail(validation);

        investimento.Atualizar(nome, tipo, instituicao, valorInvestido, saldoAtual, metaValor);
        _investimentos.Update(investimento);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<InvestimentoResponseDto>.Ok(Map(investimento), "Investimento atualizado com sucesso.");
    }

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var investimento = await _investimentos.GetByIdAsync(id, cancellationToken);
        if (investimento is null)
            return Result<bool>.Fail("Investimento não encontrado.");

        _investimentos.Remove(investimento);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<bool>.Ok(true, "Investimento removido com sucesso.");
    }

    public async Task<Result<InvestimentoResponseDto>> AddAtualizacaoAsync(
        Guid id,
        AtualizacaoInvestimentoRequestDto request,
        CancellationToken cancellationToken = default)
    {
        if (request.Saldo < 0)
            return Result<InvestimentoResponseDto>.Fail("O saldo não pode ser negativo.");
        if (request.Observacao?.Length > 500)
            return Result<InvestimentoResponseDto>.Fail("A observação deve ter no máximo 500 caracteres.");

        var investimento = await _investimentos.GetByIdAsync(id, cancellationToken);
        if (investimento is null)
            return Result<InvestimentoResponseDto>.Fail("Investimento não encontrado.");

        investimento.AtualizarSaldo(request.Saldo, request.Data ?? DateTime.UtcNow, request.Observacao?.Trim());
        _investimentos.Update(investimento);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<InvestimentoResponseDto>.Ok(Map(investimento), "Saldo atualizado com sucesso.");
    }

    private static string? Validate(
        string nome,
        string tipo,
        string instituicao,
        decimal valorInvestido,
        decimal saldoAtual,
        decimal? metaValor)
    {
        if (string.IsNullOrWhiteSpace(nome) || nome.Trim().Length > 150)
            return "O nome é obrigatório e deve ter no máximo 150 caracteres.";
        if (string.IsNullOrWhiteSpace(tipo) || tipo.Trim().Length > 80)
            return "O tipo é obrigatório e deve ter no máximo 80 caracteres.";
        if (string.IsNullOrWhiteSpace(instituicao) || instituicao.Trim().Length > 150)
            return "A instituição é obrigatória e deve ter no máximo 150 caracteres.";
        if (valorInvestido < 0 || saldoAtual < 0 || metaValor < 0)
            return "Os valores do investimento não podem ser negativos.";
        return null;
    }

    private static InvestimentoResponseDto Map(Investimento investimento) =>
        new()
        {
            Id = investimento.Id,
            Nome = investimento.Nome,
            Tipo = investimento.Tipo,
            Instituicao = investimento.Instituicao,
            ValorInvestido = investimento.ValorInvestido,
            SaldoAtual = investimento.SaldoAtual,
            MetaValor = investimento.MetaValor,
            CreatedAt = investimento.CreatedAt,
            UpdatedAt = investimento.UpdatedAt,
            Atualizacoes = investimento.Atualizacoes
                .OrderByDescending(x => x.Data)
                .Select(x => new AtualizacaoInvestimentoResponseDto
                {
                    Id = x.Id,
                    Saldo = x.Saldo,
                    Data = x.Data,
                    Observacao = x.Observacao
                })
                .ToList()
        };
}
