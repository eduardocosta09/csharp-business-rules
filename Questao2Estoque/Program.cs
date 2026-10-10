using System.Text.Json;

string caminhoArquivo = "produtos.json";

if (!File.Exists(caminhoArquivo))
{
    Console.WriteLine("Arquivo de produtos não encontrado.");
    return;
}

string json = File.ReadAllText(caminhoArquivo);

DadosEstoque? dados = JsonSerializer.Deserialize<DadosEstoque>(
    json,
    new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    });

if (dados == null || dados.Estoque.Count == 0)
{
    Console.WriteLine("Nenhum produto encontrado.");
    return;
}

List<Movimentacao> movimentacoes = new();

while (true)
{
    Console.WriteLine("\n=== CONTROLE DE ESTOQUE ===");
    Console.WriteLine("1 - Realizar movimentação");
    Console.WriteLine("2 - Consultar estoque");
    Console.WriteLine("3 - Exibir movimentações");
    Console.WriteLine("0 - Encerrar");
    Console.Write("Escolha uma opção: ");

    string opcao = Console.ReadLine() ?? "";

    switch (opcao)
    {
        case "1":
            if (RealizarMovimentacao(dados, movimentacoes))
            {
                SalvarEstoque(caminhoArquivo, dados);
            }
            break;

        case "2":
            ExibirEstoque(dados);
            break;

        case "3":
            ExibirMovimentacoes(movimentacoes);
            break;

        case "0":
            Console.WriteLine("\nPrograma encerrado.");
            return;

        default:
            Console.WriteLine("\nOpção inválida.");
            break;
    }
}


static bool RealizarMovimentacao(
    DadosEstoque dados,
    List<Movimentacao> movimentacoes)
{
    ExibirEstoque(dados);

    Console.Write("\nDigite o código do produto: ");

    if (!int.TryParse(Console.ReadLine(), out int codigo))
    {
        Console.WriteLine("Código inválido.");
        return false;
    }

    Produto? produto = dados.Estoque
        .FirstOrDefault(p => p.CodigoProduto == codigo);

    if (produto == null)
    {
        Console.WriteLine("Produto não encontrado.");
        return false;
    }

    Console.Write("Tipo da movimentação (E = Entrada / S = Saída): ");
    string tipo = Console.ReadLine()?.Trim().ToUpper() ?? "";

    if (tipo != "E" && tipo != "S")
    {
        Console.WriteLine("Tipo de movimentação inválido.");
        return false;
    }

    Console.Write("Quantidade: ");

    if (!int.TryParse(Console.ReadLine(), out int quantidade)
        || quantidade <= 0)
    {
        Console.WriteLine("Quantidade inválida.");
        return false;
    }

    if (tipo == "S" && quantidade > produto.Estoque)
    {
        Console.WriteLine(
            $"Estoque insuficiente. Estoque disponível: {produto.Estoque}"
        );

        return false;
    }

    if (tipo == "E")
    {
        produto.Estoque += quantidade;
    }
    else
    {
        produto.Estoque -= quantidade;
    }

    Movimentacao movimentacao = new()
    {
        Id = Guid.NewGuid(),
        CodigoProduto = produto.CodigoProduto,
        DescricaoProduto = produto.DescricaoProduto,
        DescricaoMovimentacao =
            tipo == "E"
                ? "Entrada de mercadoria"
                : "Saída de mercadoria",
        Quantidade = quantidade,
        EstoqueFinal = produto.Estoque
    };

    movimentacoes.Add(movimentacao);

    Console.WriteLine("\n=== MOVIMENTAÇÃO REALIZADA ===");
    Console.WriteLine($"ID: {movimentacao.Id}");
    Console.WriteLine($"Produto: {movimentacao.DescricaoProduto}");
    Console.WriteLine($"Descrição: {movimentacao.DescricaoMovimentacao}");
    Console.WriteLine($"Quantidade movimentada: {movimentacao.Quantidade}");
    Console.WriteLine($"Estoque final: {movimentacao.EstoqueFinal}");

    return true;
}


static void ExibirEstoque(DadosEstoque dados)
{
    Console.WriteLine("\n=== ESTOQUE ATUAL ===");

    foreach (Produto produto in dados.Estoque)
    {
        Console.WriteLine(
            $"Código: {produto.CodigoProduto} | " +
            $"Produto: {produto.DescricaoProduto} | " +
            $"Estoque: {produto.Estoque}"
        );
    }
}


static void ExibirMovimentacoes(List<Movimentacao> movimentacoes)
{
    Console.WriteLine("\n=== HISTÓRICO DE MOVIMENTAÇÕES ===");

    if (movimentacoes.Count == 0)
    {
        Console.WriteLine("Nenhuma movimentação realizada.");
        return;
    }

    foreach (Movimentacao movimentacao in movimentacoes)
    {
        Console.WriteLine(
            $"ID: {movimentacao.Id} | " +
            $"Produto: {movimentacao.DescricaoProduto} | " +
            $"Tipo: {movimentacao.DescricaoMovimentacao} | " +
            $"Quantidade: {movimentacao.Quantidade} | " +
            $"Estoque final: {movimentacao.EstoqueFinal}"
        );
    }
}


static void SalvarEstoque(
    string caminhoArquivo,
    DadosEstoque dados)
{
    JsonSerializerOptions opcoes = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    string jsonAtualizado =
        JsonSerializer.Serialize(dados, opcoes);

    File.WriteAllText(caminhoArquivo, jsonAtualizado);
}


class DadosEstoque
{
    public List<Produto> Estoque { get; set; } = new();
}


class Produto
{
    public int CodigoProduto { get; set; }

    public string DescricaoProduto { get; set; } = string.Empty;

    public int Estoque { get; set; }
}


class Movimentacao
{
    public Guid Id { get; set; }

    public int CodigoProduto { get; set; }

    public string DescricaoProduto { get; set; } = string.Empty;

    public string DescricaoMovimentacao { get; set; } = string.Empty;

    public int Quantidade { get; set; }

    public int EstoqueFinal { get; set; }
}