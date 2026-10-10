using System.Globalization;

const decimal taxaDiaria = 0.025m;

Console.WriteLine("=== CÁLCULO DE JUROS ===");

Console.Write("Digite o valor: R$ ");

if (!decimal.TryParse(
        Console.ReadLine(),
        NumberStyles.Number,
        CultureInfo.GetCultureInfo("pt-BR"),
        out decimal valor) || valor <= 0)
{
    Console.WriteLine("Valor inválido.");
    return;
}

Console.Write("Digite a data de vencimento (dd/MM/yyyy): ");

if (!DateTime.TryParseExact(
        Console.ReadLine(),
        "dd/MM/yyyy",
        CultureInfo.InvariantCulture,
        DateTimeStyles.None,
        out DateTime dataVencimento))
{
    Console.WriteLine("Data inválida.");
    return;
}

DateTime hoje = DateTime.Today;

if (dataVencimento >= hoje)
{
    Console.WriteLine("\nO pagamento ainda não está em atraso.");
    Console.WriteLine("Dias de atraso: 0");
    Console.WriteLine("Juros: R$ 0,00");
    Console.WriteLine($"Valor total: R$ {valor:F2}");
    return;
}

int diasAtraso = (hoje - dataVencimento.Date).Days;

decimal juros = valor * taxaDiaria * diasAtraso;
decimal valorTotal = valor + juros;

Console.WriteLine("\n=== RESULTADO ===");
Console.WriteLine($"Valor original: R$ {valor:F2}");
Console.WriteLine($"Data de vencimento: {dataVencimento:dd/MM/yyyy}");
Console.WriteLine($"Data do cálculo: {hoje:dd/MM/yyyy}");
Console.WriteLine($"Dias de atraso: {diasAtraso}");
Console.WriteLine($"Taxa diária: {taxaDiaria:P1}");
Console.WriteLine($"Juros: R$ {juros:F2}");
Console.WriteLine($"Valor total: R$ {valorTotal:F2}");