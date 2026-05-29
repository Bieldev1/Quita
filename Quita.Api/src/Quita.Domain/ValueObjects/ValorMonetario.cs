namespace Quita.Domain.ValueObjects;

/// <summary>
/// Value Object imutável para valores financeiros em BRL.
/// Garante que nenhum valor negativo ou com precisão incorreta
/// atravesse o domínio.
/// </summary>
public sealed record ValorMonetario
{
    public decimal Valor { get; }

    public ValorMonetario(decimal valor)
    {
        if (valor < 0)
            throw new Exceptions.DomainException("Valor monetário não pode ser negativo.");

        // Arredondamento financeiro padrão (≥ 0,005 → sobe)
        Valor = Math.Round(valor, 2, MidpointRounding.AwayFromZero);
    }

    public static ValorMonetario Zero => new(0m);

    // Operadores para facilitar cálculos sem vazar o decimal para fora
    public static ValorMonetario operator +(ValorMonetario a, ValorMonetario b) => new(a.Valor + b.Valor);
    public static ValorMonetario operator -(ValorMonetario a, ValorMonetario b) => new(Math.Max(0m, a.Valor - b.Valor));
    public static ValorMonetario operator *(ValorMonetario a, decimal fator)    => new(a.Valor * fator);

    // Conversão implícita evita casts desnecessários ao retornar resultados
    public static implicit operator decimal(ValorMonetario v) => v.Valor;

    public override string ToString() => Valor.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
}
