using Financeiro.Domain.Common;
using Financeiro.Domain.Enums;

namespace Financeiro.Domain.Entities;

public class Transacao : BaseEntity
{
    private Transacao() { }

    public Transacao(string descricao, decimal valor, DateTime dataLancamento, TipoTransacao tipo, string categoria, string status, bool conciliado)
    {
        Descricao = descricao;
        Valor = valor;
        DataLancamento = dataLancamento;
        Tipo = tipo;
        Categoria = categoria;
        Status = status;
        Conciliado = conciliado;
    }

    public string Descricao { get; private set; } = string.Empty;
    public decimal Valor { get; private set; }
    public DateTime DataLancamento { get; private set; }
    public TipoTransacao Tipo { get; private set; }
    public string Categoria { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;
    public bool Conciliado { get; private set; }

    public void Atualizar(string descricao, decimal valor, DateTime dataLancamento, TipoTransacao tipo, string categoria, string status, bool conciliado)
    {
        Descricao = descricao;
        Valor = valor;
        DataLancamento = dataLancamento;
        Tipo = tipo;
        Categoria = categoria;
        Status = status;
        Conciliado = conciliado;
        Touch();
    }
}
