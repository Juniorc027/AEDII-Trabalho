# Sistema Escolar — AED II Trabalho N2

Sistema de gerenciamento escolar desenvolvido em C# como trabalho da disciplina de **Algoritmos e Estruturas de Dados II**. Gerencia alunos, disciplinas e matrículas utilizando **Listas Simplesmente Encadeadas** implementadas manualmente, sem uso de estruturas nativas da linguagem.

---

## Estrutura de Arquivos

```
AEII Trabalho/
│
├── Program.cs                        # Ponto de entrada do programa
├── Dados.cs                          # Repositório central das três listas
│
├── Classes/                          # Modelos de dados (objetos)
│   ├── Aluno.cs
│   ├── Disciplina.cs
│   └── Matricula.cs
│
├── Listas/                           # Implementação das listas encadeadas
│   ├── ListaAlunos.cs
│   ├── ListaDisciplinas.cs
│   └── ListaMatriculas.cs
│
├── Controllers/                      # Lógica de negócio e interação com o usuário
│   ├── ControladorAluno.cs
│   ├── ControladorDisciplina.cs
│   └── ControladorMatricula.cs
│
├── Menus/                            # Exibição dos menus de navegação
│   └── Menu.cs
│
└── Services/                         # Serviços de persistência (leitura/escrita)
    └── LerSalvar.cs
```

---

## Por Que Está Separado Desta Forma?

O projeto segue o padrão **MVC (Model — View — Controller)** adaptado para console:

| Camada | Pasta | Responsabilidade |
|---|---|---|
| **Model** | `Classes/` + `Listas/` | Define os dados e como eles são armazenados |
| **View** | `Menus/` | Exibe opções ao usuário e captura a escolha |
| **Controller** | `Controllers/` | Recebe a escolha, executa a lógica, exibe resultados |
| **Serviço** | `Services/` | Leitura e escrita nos arquivos `.dat` |
| **Repositório** | `Dados.cs` | Ponto central que mantém as listas vivas na memória |

Separar dessa forma garante que cada arquivo tenha **uma única responsabilidade**. Se o `Menu.cs` precisar mudar a aparência das opções, os dados não são afetados. Se a regra de aprovação mudar, só o Controller e a Lista são alterados.

---

## Função de Cada Arquivo

### `Program.cs` — Ponto de Entrada
```csharp
Arquivos.CarregarDados();   // carrega os .dat para as listas na memória
MenuPrincipal.Principal();  // inicia o loop do menu principal
```
Duas linhas que definem o ciclo de vida do programa: carrega → exibe menu → salva ao sair.

---

### `Dados.cs` — Repositório Central
```csharp
public static class Dados
{
    public static ListaAlunos listaAlunos = new ListaAlunos();
    public static ListaDisciplinas listaDisciplinas = new ListaDisciplinas();
    public static ListaMatricula listaMatriculas = new ListaMatricula();
}
```
Cria as três listas **uma única vez** em memória. Por serem `static`, qualquer arquivo do projeto que escrever `Dados.listaAlunos` estará acessando **exatamente a mesma lista** — não cópias diferentes. É o "depósito central" que mantém os dados compartilhados durante toda a execução.

---

### `Classes/` — Os Objetos de Dados

Cada classe representa uma entidade do sistema com **encapsulamento real**: campos `private` acessados apenas por propriedades públicas com `get` e `set`.

**`Aluno.cs`**
```csharp
private int matricula;   // ninguém acessa diretamente
public int Matricula     // acesso controlado pela propriedade
{
    get => matricula;
    set => matricula = value;
}
```
Armazena: `Matricula` (int), `Nome` (string), `Idade` (int).

**`Disciplina.cs`**
Armazena: `CodigoDisciplina` (int), `NomeDisciplina` (string), `NotaMinimaDisciplina` (double).

**`Matricula.cs`**
Armazena: `MatriculaAluno` (int), `CodigoDisciplina` (int), `Nota1` (double), `Nota2` (double).
> Não guarda os objetos Aluno ou Disciplina — guarda apenas os **códigos de referência**, como chaves estrangeiras em um banco de dados.

---

### `Listas/` — As Listas Simplesmente Encadeadas

Implementação manual sem uso de `List<T>`, `ArrayList` ou qualquer estrutura nativa. Cada lista tem uma classe `Node`/`No` interna que representa um nó.

#### Como funciona um nó:
```csharp
private class Node
{
    public Aluno Aluno { get; set; }   // dado armazenado
    public Node Proximo { get; set; }  // ponteiro para o próximo nó
}
```

#### Representação visual:
```
cabeca
  │
  ▼
┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│ Aluno: Maria │───▶│ Aluno: João  │───▶│ Aluno: Ana   │───▶ null
│ Matrícula: 1 │    │ Matrícula: 2 │    │ Matrícula: 3 │
└──────────────┘    └──────────────┘    └──────────────┘
```
O campo `cabeca` aponta para o primeiro nó. O último nó aponta para `null`, indicando o fim da lista.

#### Método `Inserir` — percorre até o final e encadeia o novo nó:
```csharp
public void Inserir(Aluno novoAluno)
{
    Node novoNode = new Node(novoAluno);
    if (cabeca == null)           // lista vazia: novo nó vira a cabeça
    {
        cabeca = novoNode;
    }
    else
    {
        Node atual = cabeca;
        while (atual.Proximo != null)   // percorre até o último nó
            atual = atual.Proximo;
        atual.Proximo = novoNode;       // encadeia o novo ao final
    }
    quantidade++;
}
```

#### Método `BuscarPorMatricula` — anda nó a nó comparando:
```csharp
public Aluno BuscarPorMatricula(int matricula)
{
    Node atual = cabeca;
    while (atual != null)
    {
        if (atual.Aluno.Matricula == matricula)
            return atual.Aluno;   // achou
        atual = atual.Proximo;    // avança para o próximo
    }
    return null;                  // não encontrado
}
```

#### Método `SalvarParaArquivo` — percorre e grava cada nó:
```csharp
public void SalvarParaArquivo(StreamWriter sw)
{
    Node atual = cabeca;
    while (atual != null)
    {
        Aluno a = atual.Aluno;
        sw.WriteLine(a.Matricula + ";" + a.Nome + ";" + a.Idade);
        atual = atual.Proximo;
    }
}
```

**`ListaMatriculas.cs`** possui dois métodos extras importantes:
- `ExibirDisciplinasDoAluno`: percorre as matrículas filtrando pelo código do aluno, busca a disciplina correspondente em `listaDisciplinas` e calcula média e status.
- `ExibirAlunosDaDisciplina`: percorre as matrículas filtrando pelo código da disciplina, busca o aluno correspondente em `listaAlunos`.

---

### `Services/LerSalvar.cs` — Persistência de Dados

Responsável por ler e escrever os arquivos `.dat` na pasta `Dados/` dentro do diretório de execução.

#### `SalvarDados()` — lista encadeada → arquivo:
```csharp
using (StreamWriter sw = new StreamWriter(Path.Combine(CaminhoBase, "Alunos.dat")))
{
    Dados.listaAlunos.SalvarParaArquivo(sw); // percorre os nós e grava linha a linha
}
```

#### `CarregarDados()` — arquivo → lista encadeada:
```csharp
string[] partes = linha.Split(';');  // quebra "1;Maria;20" em ["1","Maria","20"]
if (partes.Length >= 3)
{
    Classes.Aluno a = new Classes.Aluno();
    a.Matricula = int.Parse(partes[0]);
    a.Nome = partes[1];
    a.Idade = int.Parse(partes[2]);
    Dados.listaAlunos.Inserir(a);    // insere na lista encadeada
}
```
> O `string[]` aqui é apenas uma ferramenta temporária para quebrar a linha de texto. Ele existe por 4 linhas e some. **Não é a estrutura de dados do trabalho** — os dados vão para os objetos dentro da lista encadeada.

O `double.Parse` usa `CultureInfo.InvariantCulture` para garantir que o ponto decimal seja lido corretamente independente do idioma configurado no Windows do computador.

---

### `Controllers/` — Lógica de Negócio

Cada controller recebe a ação do usuário vinda do Menu, executa a lógica e exibe o resultado. **Nunca armazena dados** — apenas manipula as listas via `Dados`.

#### `ControladorAluno.cs`
- `CadastroAlunos()`: solicita nome e idade. Gera matrícula automaticamente com `GetMaiorMatricula() + 1`, garantindo unicidade. Em caso de dado inválido, pergunta se deseja tentar novamente — sem voltar ao menu.
- `ListarAlunos()`: exibe todos os alunos da lista.
- `DisciplinasDoAluno()`: aceita nome **ou** matrícula. Loop de busca até achar o aluno. Exibe disciplinas com notas e status. Ao final, pergunta se deseja consultar outro aluno.

#### `ControladorDisciplina.cs`
- `CadastroDisciplina()`: solicita nome e nota mínima. Gera código com `GetMaiorCodigo() + 1`. Bloqueia nomes duplicados e valores fora do intervalo 0–10. Em caso de erro, pergunta se deseja tentar novamente.
- `ListarDisciplinas()`: exibe todas as disciplinas.
- `AlunosDaDisciplina()`: aceita nome **ou** código. Exibe alunos com notas e status. Ao final, pergunta se deseja consultar outra disciplina.

#### `ControladorMatricula.cs`
- `CadastroMatriculas()`: aceita nome ou matrícula para o aluno e nome ou código para a disciplina. Bloqueia matrícula duplicada (mesmo aluno na mesma disciplina). Ao final, pergunta se deseja realizar outra matrícula.
- `AtribuirNota()`: busca a matrícula pelo par aluno+disciplina. Aceita vírgula ou ponto como separador decimal. Em caso de erro ou nota inválida, pergunta se deseja tentar com outro aluno.

```csharp
// Aceita tanto "7.5" quanto "7,5" como entrada válida
string inputNota1 = Console.ReadLine()?.Replace(',', '.');
double.TryParse(inputNota1, NumberStyles.Any, CultureInfo.InvariantCulture, out double nota1)
```

---

### `Menus/Menu.cs` — Navegação

Contém três classes estáticas de menu:

| Classe | Função |
|---|---|
| `MenuPrincipal` | Menu raiz com: Consultas, Cadastros, Salvar, Sair |
| `MenuConsultas` | Submenu: Alunos, Disciplinas, Alunos da Disciplina, Disciplinas do Aluno |
| `MenuCadastro` | Submenu: Alunos, Disciplinas, Matrículas, Atribuir Nota |

O menu **não executa lógica** — apenas chama o método correto do Controller:
```csharp
case "1":
    ControladorAluno.CadastroAlunos();  // delega para o controller
    break;
```

Ao selecionar "Sair", o programa **salva automaticamente** antes de encerrar:
```csharp
case "4":
    Arquivos.SalvarDados();  // salva tudo antes de fechar
    return;
```

---

## Como os Arquivos se Comunicam

```
┌──────────┐   chama    ┌──────────────┐   acessa   ┌────────────┐
│  Menu.cs │──────────▶ │  Controller  │──────────▶ │  Dados.cs  │
└──────────┘            └──────────────┘            └────────────┘
                               │                          │
                               │ exibe resultado          │ contém
                               ▼                          ▼
                          Console              ┌──────────────────┐
                                               │  ListaAlunos     │
┌──────────────┐  carrega/salva               │  ListaDisciplinas│
│  LerSalvar   │◀────────────────────────────▶│  ListaMatriculas │
└──────────────┘                              └──────────────────┘
       │                                               │
       │ lê/grava                                      │ armazena
       ▼                                               ▼
  Arquivos .dat                                 Objetos das Classes
  (Alunos.dat)                                 (Aluno, Disciplina,
  (Disciplinas.dat)                             Matricula)
  (Matriculas.dat)
```

---

## Fluxo Completo de Execução

```
1. Program.cs inicia
       │
       ├─▶ LerSalvar.CarregarDados()
       │       └─ lê Alunos.dat     → Inserir() em Dados.listaAlunos
       │       └─ lê Disciplinas.dat → Inserir() em Dados.listaDisciplinas
       │       └─ lê Matriculas.dat  → Inserir() em Dados.listaMatriculas
       │
       └─▶ MenuPrincipal.Principal()  ← loop infinito até "Sair"
               │
               ├─ "1" Consultas ──▶ MenuConsultas
               │       ├─ "1" ──▶ ControladorAluno.ListarAlunos()
               │       ├─ "2" ──▶ ControladorDisciplina.ListarDisciplinas()
               │       ├─ "3" ──▶ ControladorDisciplina.AlunosDaDisciplina()
               │       └─ "4" ──▶ ControladorAluno.DisciplinasDoAluno()
               │
               ├─ "2" Cadastros ──▶ MenuCadastro
               │       ├─ "1" ──▶ ControladorAluno.CadastroAlunos()
               │       ├─ "2" ──▶ ControladorDisciplina.CadastroDisciplina()
               │       ├─ "3" ──▶ ControladorMatricula.CadastroMatriculas()
               │       └─ "4" ──▶ ControladorMatricula.AtribuirNota()
               │
               ├─ "3" Salvar ──▶ LerSalvar.SalvarDados()
               │       └─ percorre cada lista → grava nos .dat
               │
               └─ "4" Sair ──▶ LerSalvar.SalvarDados() → encerra
```

---

## Regras de Negócio Implementadas

| Regra | Onde está |
|---|---|
| Matrícula do aluno gerada automaticamente e única | `ControladorAluno.CadastroAlunos()` — `GetMaiorMatricula() + 1` |
| Código da disciplina gerado automaticamente e único | `ControladorDisciplina.CadastroDisciplina()` — `GetMaiorCodigo() + 1` |
| Mesmo aluno não pode se matricular duas vezes na mesma disciplina | `ListaMatriculas.ExisteMatricula()` |
| Busca por nome **ou** matrícula/código em todas as consultas | `int.TryParse` decide se é número ou texto |
| Aprovado: média ≥ nota mínima da disciplina | `ListaMatriculas.ExibirDisciplinasDoAluno()` e `ExibirAlunosDaDisciplina()` |
| Reprovado: média < nota mínima da disciplina | mesmos métodos acima |
| Salvar automaticamente ao sair | `Menu.cs` — `case "4"` chama `SalvarDados()` |
| Nota aceita vírgula ou ponto decimal | `Replace(',', '.')` + `CultureInfo.InvariantCulture` |
| Erro de digitação não volta ao menu | `continue` no `while(true)` após "Deseja tentar novamente?" |

---

## Formato dos Arquivos `.dat`

Os arquivos ficam em `bin/Debug/net10.0/Dados/` após a primeira execução.

**Alunos.dat**
```
1;Maria Silva;20
2;João Souza;22
```

**Disciplinas.dat**
```
1;Algoritmos e Estruturas de Dados II;6.0
2;Matemática Discreta;5.0
```

**Matriculas.dat**
```
1;1;8.5;7.0
1;2;6.0;9.0
2;1;5.5;4.0
```
Formato matrícula: `matriculaAluno;codigoDisciplina;nota1;nota2`
