using Financeiro.Domain.Common;
namespace Financeiro.Domain.Entities;
public class DespesaPeriodo : BaseEntity {
    private DespesaPeriodo(){}
    public DespesaPeriodo(Guid periodoPagamentoId,string descricao,decimal valor,string tipo){PeriodoPagamentoId=periodoPagamentoId;Descricao=descricao;Valor=valor;Tipo=tipo;}
    public Guid PeriodoPagamentoId {get; private set;}
    public string Descricao {get; private set;} = string.Empty;
    public decimal Valor {get; private set;}
    public string Tipo {get; private set;} = "Fixa";
}
