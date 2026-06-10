using Financeiro.Domain.Common;

namespace Financeiro.Domain.Entities;

public class PeriodoPagamento : BaseEntity
{
    private PeriodoPagamento() { }

    public PeriodoPagamento(string mesReferencia, int diaPagamento, string pessoa, decimal salario)
    {
        MesReferencia = mesReferencia;
        DiaPagamento = diaPagamento;
        Pessoa = pessoa;
        Salario = salario;
    }

    public string MesReferencia { get; private set; } = string.Empty;
    public int DiaPagamento { get; private set; }
    public string Pessoa { get; private set; } = string.Empty;
    public decimal Salario { get; private set; }

    public List<DespesaPeriodo> Despesas { get; private set; } = new();

    public void Atualizar(string mesReferencia, int diaPagamento, string pessoa, decimal salario)
    {
        MesReferencia = mesReferencia;
        DiaPagamento = diaPagamento;
        Pessoa = pessoa;
        Salario = salario;
        Touch();
    }
}