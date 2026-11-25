using Financeiro.Domain.Common;

namespace Financeiro.Domain.Entities;

public class AtualizacaoInvestimento : BaseEntity
{
    private AtualizacaoInvestimento() { }

    internal AtualizacaoInvestimento(Guid investimentoId, decimal saldo, DateTime data, string? observacao)
    {
        InvestimentoId = investimentoId;
        Saldo = saldo;
        Data = data;
        Observacao = observacao;
    }

    public Guid InvestimentoId { get; private set; }
    public decimal Saldo { get; private set; }
    public DateTime Data { get; private set; }
    public string? Observacao { get; private set; }
}
