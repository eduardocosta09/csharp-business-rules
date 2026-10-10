using System.Text.Json;

string caminhoArquivo = "vendas.json";

if (!File.Exists(caminhoArquivo))
{
    Console.WriteLine("Arquivo de vendas não encontrado.");
    return;
}

string json = File.ReadAllText(caminhoArquivo);

DadosVendas? dados = JsonSerializer.Deserialize<DadosVendas>(
    json,
    new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    });

if (dados == null || dados.Vendas.Count == 0)
{
    Console.WriteLine("Nenhuma venda encontrada.");
    return;
}

Dictionary<string, decimal> comissoesPorVendedor = new();

foreach (Venda venda in dados.Vendas)
{
    decimal comissao = CalcularComissao(venda.Valor);

    if (comissoesPorVendedor.ContainsKey(venda.Vendedor))
    {
        comissoesPorVendedor[venda.Vendedor] += comissao;
    }
    else
    {
        comissoesPorVendedor[venda.Vendedor] = comissao;
    }
}

Console.WriteLine("=== COMISSÕES DOS VENDEDORES ===");

foreach (var vendedor in comissoesPorVendedor)
{
    Console.WriteLine($"{vendedor.Key}: R$ {vendedor.Value:F2}");
}


static decimal CalcularComissao(decimal valorVenda)
{
    if (valorVenda < 100)
    {
        return 0;
    }
    else if (valorVenda < 500)
    {
        return valorVenda * 0.01m;
    }
    else
    {
        return valorVenda * 0.05m;
    }
}


class DadosVendas
{
    public List<Venda> Vendas { get; set; } = new();
}


class Venda
{
    public string Vendedor { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}