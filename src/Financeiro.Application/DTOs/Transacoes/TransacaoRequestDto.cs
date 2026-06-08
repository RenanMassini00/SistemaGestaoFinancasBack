using Financeiro.Domain.Enums;

namespace Financeiro.Application.DTOs.Transacoes;

public class TransacaoRequestDto
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime DataLancamento { get; set; }
    public TipoTransacao Tipo { get; set; }
    public string Categoria { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public bool Conciliado { get; set; }
}
