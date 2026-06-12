# Resumo Técnico — AED II Trabalho N2

---

## 1. Lista Simplesmente Encadeada

É uma estrutura de dados onde cada elemento (chamado **nó**) guarda dois itens: o dado em si e um ponteiro para o próximo nó. Não usa índices — você sempre começa do primeiro e segue os ponteiros.

```
cabeca ──▶ [dado|próximo] ──▶ [dado|próximo] ──▶ [dado|null]
```

O `null` no último nó é o sinal de fim da lista.

**Por que não usar array?** Array tem tamanho fixo definido na criação. Lista encadeada cresce dinamicamente — cada `Inserir` cria um novo nó na memória sem precisar reservar espaço antecipadamente.

**Os três ponteiros que controlam tudo:**
- `cabeca` — aponta para o primeiro nó. Se for `null`, a lista está vazia.
- `atual` — ponteiro auxiliar usado para percorrer a lista durante buscas e exibições.
- `Proximo` — dentro de cada nó, aponta para o nó seguinte.

**Operação de inserção no final — passo a passo:**
```csharp
Node novoNode = new Node(novoAluno);   // 1. cria o nó
if (cabeca == null)                    // 2. lista vazia?
    cabeca = novoNode;                 //    novo nó vira a cabeça
else
{
    Node atual = cabeca;               // 3. começa do início
    while (atual.Proximo != null)      // 4. anda até o último
        atual = atual.Proximo;
    atual.Proximo = novoNode;          // 5. encadeia ao final
}
quantidade++;
```

**Operação de busca — passo a passo:**
```csharp
Node atual = cabeca;           // começa do primeiro nó
while (atual != null)          // enquanto não chegou ao fim
{
    if (atual.Aluno.Matricula == matricula)
        return atual.Aluno;    // achou — retorna o dado
    atual = atual.Proximo;     // não achou — avança
}
return null;                   // percorreu tudo, não existe
```

**Classe interna `Node/No`:** É declarada `private` dentro da lista — isso significa que nenhum código fora da lista sabe que o `Node` existe. Quem usa `ListaAlunos` só vê os métodos públicos (`Inserir`, `Buscar`, etc.), nunca os nós internos. Isso é encapsulamento aplicado à estrutura de dados.

---

## 2. Encapsulamento

Princípio de esconder os dados internos de uma classe e controlar o acesso por métodos ou propriedades.

**Campo privado + propriedade pública:**
```csharp
private int matricula;        // o dado real — protegido
public int Matricula          // a porta de entrada controlada
{
    get => matricula;         // leitura permitida
    set => matricula = value; // escrita permitida
}
```

**Por que importa?** Se `matricula` fosse `public`, qualquer parte do código poderia fazer `aluno.matricula = -999`. Com `private` + propriedade, você poderia adicionar validação no `set`:
```csharp
set
{
    if (value > 0)            // só aceita matrícula positiva
        matricula = value;
}
```

No projeto, as **classes** (`Aluno`, `Disciplina`, `Matricula`) encapsulam os dados dos objetos. As **listas** encapsulam os nós internos (`Node`/`No` são `private`). Duas camadas de encapsulamento.

---

## 3. Classes, Objetos e `new`

**Classe** é o molde. **Objeto** é a instância criada a partir do molde na memória.

```csharp
// Classe = molde (definição)
public class Aluno
{
    private int matricula;
    private string nome;
    private int idade;
}

// Objeto = instância real na memória
Aluno a = new Aluno();   // new executa o construtor e aloca memória
a.Matricula = 1;
a.Nome = "Maria";
a.Idade = 20;
```

Cada vez que você chama `new Aluno()`, um objeto novo e independente é criado na memória. Alterar `a.Nome` não afeta nenhum outro objeto `Aluno`.

---

## 4. Construtor e o `()`

O construtor é um método especial com o mesmo nome da classe, sem tipo de retorno. Executa automaticamente quando `new` é chamado.

```csharp
public ListaAlunos()       // construtor
{
    cabeca = null;         // lista nasce vazia
    quantidade = 0;        // contador nasce em zero
}
```

O `()` em `new ListaAlunos()` é a chamada ao construtor. Sem ele rodar, `cabeca` teria lixo de memória em vez de `null`, e `EstaVazia()` retornaria resultado incorreto.

Se você não escrever um construtor, o C# cria um vazio automaticamente — mas nesse caso o construtor existe intencionalmente para inicializar o estado da lista.

---

## 5. `static` — Classes e Membros Estáticos

Um membro `static` pertence à **classe**, não a um objeto. Existe uma única cópia durante toda a execução do programa.

```csharp
public static class Dados
{
    public static ListaAlunos listaAlunos = new ListaAlunos();
}
```

- **`static class`**: não pode ser instanciada com `new`. Acessa pelo nome: `Dados.listaAlunos`.
- **`static` membro**: existe uma única vez na memória, compartilhado por todo o programa.

**Comparação:**
```csharp
// SEM static — cada acesso seria um objeto diferente
Dados d1 = new Dados();
Dados d2 = new Dados();
d1.listaAlunos != d2.listaAlunos  // listas diferentes!

// COM static — uma única instância
Dados.listaAlunos  // sempre a mesma lista, de qualquer lugar
```

Os Controllers também são `static` pelo mesmo motivo: não faz sentido criar objetos `ControladorAluno` — eles são apenas agrupamentos de métodos.

---

## 6. Namespaces

Namespace é um agrupamento lógico de classes, evitando conflitos de nome.

```csharp
namespace AEDII_Trabalho.Controllers
{
    public static class ControladorAluno { ... }
}
```

Para usar `ControladorAluno` de outro namespace:
```csharp
using AEDII_Trabalho.Controllers;  // importa o namespace
```

No projeto, cada pasta tem seu próprio namespace:
- `AEDII_Trabalho.Classes` — os objetos de dados
- `AEDII_Trabalho.Listas` — as listas encadeadas
- `AEDII_Trabalho.Controllers` — a lógica de negócio
- `AEDII_Trabalho.LerESalvar` — persistência

---

## 7. Padrão MVC Aplicado ao Projeto

| Camada | Arquivo(s) | Faz o quê |
|---|---|---|
| **Model** | `Classes/` + `Listas/` | Define estrutura dos dados e como armazená-los |
| **View** | `Menus/Menu.cs` | Exibe opções, lê escolha do usuário |
| **Controller** | `Controllers/` | Recebe a escolha, executa lógica, mostra resultado |
| **Service** | `Services/LerSalvar.cs` | Lê e grava nos arquivos `.dat` |
| **Repository** | `Dados.cs` | Mantém as listas vivas e acessíveis globalmente |

**Fluxo de uma ação:**
```
Usuário digita "1" no menu
    └─▶ Menu.cs chama ControladorAluno.ListarAlunos()
            └─▶ acessa Dados.listaAlunos
                    └─▶ chama ExibirTodos()
                            └─▶ percorre nós e imprime no console
```

Cada camada só faz sua parte. O Menu não sabe como a lista funciona. O Controller não sabe como os dados são salvos. A Lista não sabe que existe um menu.

---

## 8. Persistência em Arquivo — `StreamWriter` e `StreamReader`

**Salvar — lista → arquivo:**
```csharp
using (StreamWriter sw = new StreamWriter("Alunos.dat"))
{
    Dados.listaAlunos.SalvarParaArquivo(sw);
}
// o "using" garante que o arquivo é fechado mesmo se der erro
```

Dentro de `SalvarParaArquivo`, cada nó gera uma linha:
```csharp
sw.WriteLine(a.Matricula + ";" + a.Nome + ";" + a.Idade);
// resultado no arquivo: "1;Maria;20"
```

**Carregar — arquivo → lista:**
```csharp
string linha;
while ((linha = sr.ReadLine()) != null)   // lê até o fim do arquivo
{
    string[] partes = linha.Split(';');   // quebra "1;Maria;20"
    Classes.Aluno a = new Classes.Aluno();
    a.Matricula = int.Parse(partes[0]);   // "1" vira int 1
    a.Nome = partes[1];                   // "Maria"
    a.Idade = int.Parse(partes[2]);       // "20" vira int 20
    Dados.listaAlunos.Inserir(a);         // vai para a lista encadeada
}
```

O `string[]` de `Split` é temporário — existe só para quebrar o texto. Os dados reais vão para o objeto `Aluno` dentro da lista.

---

## 9. `CultureInfo.InvariantCulture` — Por Que Existe

O Windows pode estar configurado em idiomas diferentes. Em português brasileiro, o separador decimal é vírgula (`7,5`). Em inglês americano, é ponto (`7.5`).

O `double.Parse` sem especificar cultura usa a configuração do sistema. Se o arquivo foi salvo com ponto e o sistema está em pt-BR, a leitura falha.

**Solução:** `InvariantCulture` — uma cultura neutra que sempre usa ponto:
```csharp
// Salvar — sempre grava com ponto
d.NotaMinimaDisciplina.ToString(CultureInfo.InvariantCulture) // "6.5"

// Carregar — sempre lê com ponto
double.Parse(partes[2], CultureInfo.InvariantCulture) // "6.5" → 6.5
```

**Para entrada do usuário**, onde o usuário pode digitar vírgula ou ponto:
```csharp
string input = Console.ReadLine()?.Replace(',', '.');  // normaliza para ponto
double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double nota)
```

---

## 10. Controle de Fluxo — `while(true)`, `continue`, `return`

Os loops de navegação do projeto usam três palavras-chave em conjunto:

```csharp
while (true)               // loop infinito — só sai por return
{
    // pede dados ao usuário

    if (dado inválido)
    {
        Console.Write("Deseja tentar novamente? (S/N): ");
        if (Console.ReadLine()?.ToUpper() != "S") return;  // sai do método → menu
        continue;          // volta para o topo do while(true) → tenta de novo
    }

    // processa dado válido

    Console.Write("Deseja fazer outro? (S/N): ");
    if (Console.ReadLine()?.ToUpper() != "S") return;      // sai → menu
}
// o continue pula para cá → repete o loop
```

- **`while(true)`**: loop que nunca para sozinho — é a "tela" que fica aberta.
- **`continue`**: abandona o restante da iteração atual e volta ao topo do loop.
- **`return`**: sai do método completamente — volta para quem chamou (o Menu).

---

## 11. `int.TryParse` para Aceitar Nome ou Código

O requisito diz: aceitar tanto nome quanto matrícula/código. A distinção é feita com `TryParse`:

```csharp
string entrada = Console.ReadLine()?.Trim();

if (int.TryParse(entrada, out int matricula))
    // entrada é um número → busca por matrícula
    alunoEncontrado = Dados.listaAlunos.BuscarPorMatricula(matricula);
else
    // entrada não é número → busca por nome
    alunoEncontrado = Dados.listaAlunos.BuscarPeloNome(entrada);
```

`TryParse` retorna `true` se conseguiu converter para `int`, `false` caso contrário — sem lançar exceção. O valor convertido vai para a variável `out int matricula` automaticamente.

---

## 12. Member Access Chain — `Dados.listaAlunos.BuscarPorMatricula()`

Cada `.` é o **operador de acesso a membro** — significa "dentro de" ou "pertencente a":

```
Dados         .listaAlunos        .BuscarPorMatricula(1)
  │                 │                      │
classe estática   campo do tipo        método da classe
(acessada pelo    ListaAlunos          ListaAlunos
 nome, sem new)   (objeto real
                  na memória)
```

A expressão completa chama o método `BuscarPorMatricula` no objeto `listaAlunos` que está guardado dentro da classe `Dados`. O nome técnico da expressão inteira é **member access expression**.

---

## Resumo Visual — O que cada conceito resolve no projeto

| Conceito | Problema que resolve |
|---|---|
| Lista encadeada | Armazenar quantidade variável de alunos/disciplinas sem tamanho fixo |
| Encapsulamento | Proteger os dados internos dos nós e dos objetos |
| `static` em Dados | Uma única lista compartilhada por todo o programa |
| Construtor | Garantir que a lista nasce com `cabeca = null` e `quantidade = 0` |
| Namespace | Organizar as classes por responsabilidade sem conflito de nomes |
| MVC | Cada arquivo tem uma única função — fácil de manter e explicar |
| `StreamWriter/Reader` | Persistir os dados em disco entre execuções do programa |
| `InvariantCulture` | Ler e gravar decimais corretamente em qualquer idioma do Windows |
| `while(true)` + `continue` | Manter o usuário na tela ao errar sem voltar ao menu |
| `TryParse` | Decidir se o usuário digitou um número ou um nome sem travar o programa |
