# Aplicações de regras de negócio em C#

Três aplicações de console desenvolvidas em C# com .NET 10: cálculo de comissões, controle de estoque e cálculo de juros.

## Questão 1 - Comissão

Lê as vendas do arquivo `vendas.json`, calcula a comissão de cada venda e acumula os valores por vendedor.

- Abaixo de R$ 100,00: sem comissão.
- A partir de R$ 100,00 e abaixo de R$ 500,00: 1%.
- A partir de R$ 500,00: 5%.

## Questão 2 - Estoque

Permite realizar entradas e saídas, consultar o estoque e visualizar as movimentações da execução atual. Cada movimentação recebe um identificador único e registra o produto, a descrição, a quantidade e o estoque final.

As quantidades devem ser inteiras e positivas. Saídas superiores ao estoque disponível são rejeitadas.

Os saldos são atualizados no arquivo `produtos.json`. O histórico de movimentações fica apenas na memória e é perdido ao encerrar o programa. Executar movimentações altera o JSON usado pelo programa.

## Questão 3 - Juros

Recebe um valor positivo e uma data de vencimento no formato `dd/MM/yyyy`. O cálculo usa juros simples de 2,5% ao dia, considerando os dias corridos de atraso até a data local da máquina.

```text
Juros = Valor × 0,025 × Dias de atraso
Valor total = Valor + Juros
```

Para vencimentos na data atual ou no futuro, não há juros. Digite valores no formato brasileiro, como `100,50`, sem o símbolo `R$`.

## Como executar

É necessário ter o SDK do .NET 10 instalado.

A partir da pasta raiz, entre na pasta da questão desejada e execute um dos exemplos abaixo:

```sh
cd Questao1Comissao
dotnet run
```

```sh
cd Questao2Estoque
dotnet run
```

```sh
cd Questao3Juros
dotnet run
```

Execute dentro da pasta da questão para que os arquivos JSON sejam encontrados.

Para compilar os três projetos a partir da raiz:

```sh
dotnet build Questao1Comissao
dotnet build Questao2Estoque
dotnet build Questao3Juros
```
