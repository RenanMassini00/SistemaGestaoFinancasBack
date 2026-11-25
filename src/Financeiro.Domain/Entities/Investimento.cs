using Financeiro.Domain.Common;

namespace Financeiro.Domain.Entities;

public class Investimento : BaseEntity
{
    private Investimento() { }

    public Investimento(
        string nome,
        string tipo,
        string instituicao,
        decimal valorInvestido,
        decimal saldoAtual,
        decimal? metaValor)
    {
        Nome = nome;
        Tipo = tipo;
        Instituicao = instituicao;
        ValorInvestido = valorInvestido;
        SaldoAtual = saldoAtual;
        MetaValor = metaValor;
    }

    public string Nome { get; private set; } = string.Empty;
    public string Tipo { get; private set; } = string.Empty;
    public string Instituicao { get; private set; } = string.Empty;
    public decimal ValorInvestido { get; private set; }
    public decimal SaldoAtual { get; private set; }
    public decimal? MetaValor { get; private set; }
    public List<AtualizacaoInvestimento> Atualizacoes { get; private set; } = new();

    public void Atualizar(
        string nome,
        string tipo,
        string instituicao,
        decimal valorInvestido,
        decimal saldoAtual,
        decimal? metaValor)
    {
        Nome = nome;
        Tipo = tipo;
        Instituicao = instituicao;
        ValorInvestido = valorInvestido;
        SaldoAtual = saldoAtual;
        MetaValor = metaValor;
        Touch();
    }

    public void AtualizarSaldo(decimal saldoAtual, DateTime data, string? observacao)
    {
        SaldoAtual = saldoAtual;
        Atualizacoes.Add(new AtualizacaoInvestimento(Id, saldoAtual, data, observacao));
        Touch();
    }
}
