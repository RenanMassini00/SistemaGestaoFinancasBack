using System.Text.Json;

namespace Financeiro.Application.DTOs;

public class InvestimentoRequestDto
{
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Instituicao { get; set; } = string.Empty;
    public decimal ValorInvestido { get; set; }
    public decimal SaldoAtual { get; set; }
    public decimal? MetaValor { get; set; }
}

public class InvestimentoPatchDto
{
    public string? Nome { get; set; }
    public string? Tipo { get; set; }
    public string? Instituicao { get; set; }
    public decimal? ValorInvestido { get; set; }
    public decimal? SaldoAtual { get; set; }
    public JsonElement MetaValor { get; set; }
}

public class AtualizacaoInvestimentoRequestDto
{
    public decimal Saldo { get; set; }
    public DateTime? Data { get; set; }
    public string? Observacao { get; set; }
}

public class InvestimentoResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Instituicao { get; set; } = string.Empty;
    public decimal ValorInvestido { get; set; }
    public decimal SaldoAtual { get; set; }
    public decimal? MetaValor { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public IReadOnlyList<AtualizacaoInvestimentoResponseDto> Atualizacoes { get; set; } =
        Array.Empty<AtualizacaoInvestimentoResponseDto>();
}

public class AtualizacaoInvestimentoResponseDto
{
    public Guid Id { get; set; }
    public decimal Saldo { get; set; }
    public DateTime Data { get; set; }
    public string? Observacao { get; set; }
}
