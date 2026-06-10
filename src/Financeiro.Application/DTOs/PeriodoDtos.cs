namespace Financeiro.Application.DTOs;
public class DespesaRequestDto { public string Descricao {get;set;} = string.Empty; public decimal Valor {get;set;} public string Tipo {get;set;} = "Fixa"; }
public class DespesaResponseDto { public Guid Id {get;set;} public string Descricao {get;set;} = string.Empty; public decimal Valor {get;set;} public string Tipo {get;set;} = string.Empty; }
public class PeriodoRequestDto { public string MesReferencia {get;set;} = string.Empty; public int DiaPagamento {get;set;} public string Pessoa {get;set;} = string.Empty; public decimal Salario {get;set;} }
public class PeriodoResponseDto { public Guid Id {get;set;} public string MesReferencia {get;set;} = string.Empty; public int DiaPagamento {get;set;} public string Pessoa {get;set;} = string.Empty; public decimal Salario {get;set;} public decimal TotalDespesas {get;set;} public decimal Saldo {get;set;} public List<DespesaResponseDto> Despesas {get;set;} = new(); }
