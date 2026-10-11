# Calculadora de Descontos com xUnit

Atividade da disciplina **Garantia da Qualidade de Software** (Gestão e Qualidade de Software) — Prof. Daniel Henrique Matos de Paiva.

O projeto é uma solução **.NET 10** que implementa regras simples de desconto e as valida com **testes parametrizados** usando xUnit, demonstrando a diferença entre `[Fact]` e `[Theory]`.

---

## Estrutura da solução

```
calculadora-descontos-xunit/
├── CalculadoraDescontos.sln
├── CalculadoraDescontos.App/          # Código de produção (console)
│   └── DescontoService.cs
└── CalculadoraDescontos.Tests/        # Testes unitários (xUnit)
    └── DescontoServiceTests.cs
```

## Regras implementadas (`DescontoService`)

| Método | Retorno | Regra |
|--------|---------|-------|
| `ObterCategoriaCliente(int totalCompras)` | `string` | `"BRONZE"` para menos de 5 compras; `"PRATA"` de 5 a 10 (inclusive); `"OURO"` para mais de 10 |
| `CalcularDescontoPorPercentual(int valorOriginal, int percentualDesconto)` | `int` | Retorna o valor final com o desconto aplicado (ex.: 100 com 10% → 90) |
| `EValidoParaCupom(int idade, bool primeiraCompra)` | `bool` | `true` se a idade for 18 ou mais **ou** se for a primeira compra; caso contrário, `false` |

---

## `[Fact]` vs `[Theory]`

### `[Fact]`

Marca um teste **único e sem parâmetros**. Ele valida um cenário fixo e roda **uma vez**.

```csharp
[Fact]
public void ObterCategoriaCliente_DeveRetornarBronze_ParaDuasCompras()
{
    var servico = new DescontoService();

    var resultado = servico.ObterCategoriaCliente(2);

    Assert.Equal("BRONZE", resultado);
}
```

Para testar outros cenários com `[Fact]`, é preciso **duplicar o método** para cada caso.

### `[Theory]`

Marca um teste **parametrizado**: o mesmo método roda **várias vezes**, uma para cada conjunto de dados fornecido por atributos como `[InlineData]`. Cada `[InlineData]` aparece como um **teste individual** no resultado.

```csharp
[Theory]
[InlineData(2, "BRONZE")]
[InlineData(7, "PRATA")]
[InlineData(15, "OURO")]
public void ObterCategoriaClienteTest(int totalCompras, string esperado)
{
    var servico = new DescontoService();

    string resultado = servico.ObterCategoriaCliente(totalCompras);

    Assert.Equal(esperado, resultado);
}
```

### Comparativo

| | `[Fact]` | `[Theory]` |
|---|---|---|
| Parâmetros no método | Não | Sim |
| Fonte de dados | Valores fixos no corpo do teste | `[InlineData]`, `[MemberData]`, `[ClassData]` |
| Execuções | 1 | 1 por conjunto de dados |
| Quando usar | Cenário único e invariável | Mesma lógica com várias entradas e saídas |
| Duplicação de código | Alta, se houver muitos cenários | Baixa |

---

## Cenários testados

**Categoria do cliente (`string`)**

| Compras | Esperado |
|---------|----------|
| 2 | `BRONZE` |
| 7 | `PRATA` |
| 15 | `OURO` |

**Desconto por percentual (`int`)**

| Valor original | Desconto (%) | Esperado |
|----------------|--------------|----------|
| 100 | 10 | 90 |
| 200 | 20 | 160 |
| 50 | 0 | 50 |

**Elegibilidade ao cupom (`bool`)**

| Idade | Primeira compra | Esperado |
|-------|-----------------|----------|
| 20 | `false` | `true` |
| 16 | `true` | `true` |
| 17 | `false` | `false` |

---

## Como executar

### Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- Git

### Clonar o repositório

```bash
git clone https://github.com/marlon-II/calculadora-descontos-xunit.git
cd calculadora-descontos-xunit
```

### Executar os testes

Na raiz da solução, rode:

```bash
dotnet test
```

Para ver cada `[InlineData]` listado como um teste individual:

```bash
dotnet test --logger "console;verbosity=detailed"
```

Se tudo estiver correto, serão executados **9 testes** (3 métodos `[Theory]` × 3 `[InlineData]`), todos aprovados.

---

## Como a solução foi criada (.NET CLI)

```bash
# 1. Cria a solução
dotnet new sln -n CalculadoraDescontos

# 2. Cria o projeto da aplicação
dotnet new console -n CalculadoraDescontos.App -f net10.0

# 3. Cria o projeto de testes com xUnit
dotnet new xunit -n CalculadoraDescontos.Tests -f net10.0

# 4. Adiciona os projetos à solução
dotnet sln add CalculadoraDescontos.App/CalculadoraDescontos.App.csproj
dotnet sln add CalculadoraDescontos.Tests/CalculadoraDescontos.Tests.csproj

# 5. Referencia o projeto de produção no projeto de testes
dotnet add CalculadoraDescontos.Tests/CalculadoraDescontos.Tests.csproj reference CalculadoraDescontos.App/CalculadoraDescontos.App.csproj
```

---

## Tecnologias

- C# / .NET 10
- xUnit

## Autor

**Marlon Andrade Bartoli** — Ciência da Computação, UNA.

