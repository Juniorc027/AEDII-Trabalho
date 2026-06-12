using System;
using AEDII_Trabalho.Classes;

namespace AEDII_Trabalho.Controllers
{
    public static class ControladorAluno
    {
        // Solicita nome e idade, gera matrícula única e insere na lista encadeada
        public static void CadastroAlunos()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Cadastro de Aluno ===");

                Console.Write("Digite o nome do aluno: ");
                string nome = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(nome))
                {
                    Console.WriteLine("Nome inválido.");
                    Console.Write("Deseja tentar novamente? (S/N): ");
                    if (Console.ReadLine()?.Trim().ToUpper() != "S") return;
                    continue;
                }

                Console.Write("Digite a idade do aluno: ");
                if (!int.TryParse(Console.ReadLine(), out int idade) || idade <= 0)
                {
                    Console.WriteLine("Idade inválida.");
                    Console.Write("Deseja tentar novamente? (S/N): ");
                    if (Console.ReadLine()?.Trim().ToUpper() != "S") return;
                    continue;
                }

                // Gera matrícula única: maior valor existente na lista + 1
                int novaMatricula = Dados.listaAlunos.GetMaiorMatricula() + 1;

                Aluno novoAluno = new Aluno();
                novoAluno.Nome = nome;
                novoAluno.Idade = idade;
                novoAluno.Matricula = novaMatricula;

                Dados.listaAlunos.Inserir(novoAluno);

                Console.WriteLine($"\nAluno '{novoAluno.Nome}' cadastrado com sucesso!");
                Console.WriteLine($"Matrícula gerada: {novoAluno.Matricula}");

                Console.Write("\nDeseja cadastrar outro aluno? (S/N): ");
                string resposta = Console.ReadLine()?.Trim().ToUpper();
                if (resposta != "S") return;
            }
        }

        // Exibe todos os alunos cadastrados na lista encadeada
        public static void ListarAlunos()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Alunos ===");
            Console.WriteLine($"Quantidade de alunos cadastrados: {Dados.listaAlunos.GetQuantidade()}");

            if (Dados.listaAlunos.EstaVazia())
            {
                Console.WriteLine("Nenhum aluno cadastrado.");
                Console.WriteLine("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Dados.listaAlunos.ExibirTodos();

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        // Lista todas as disciplinas de um aluno com notas e situação (aprovado/reprovado)
        public static void DisciplinasDoAluno()
        {
            if (Dados.listaAlunos.EstaVazia())
            {
                Console.WriteLine("Não há nenhum aluno cadastrado ainda.");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Disciplinas do Aluno ===");

                // Repete a busca até encontrar um aluno válido
                Aluno alunoEncontrado = null;
                while (alunoEncontrado == null)
                {
                    Console.Write("Informe o aluno (nome ou matrícula): ");
                    string entrada = Console.ReadLine()?.Trim();

                    if (int.TryParse(entrada, out int matriculaInformada))
                        alunoEncontrado = Dados.listaAlunos.BuscarPorMatricula(matriculaInformada);
                    else
                        alunoEncontrado = Dados.listaAlunos.BuscarPeloNome(entrada);

                    if (alunoEncontrado == null)
                        Console.WriteLine("Aluno não encontrado. Tente novamente.");
                }

                Console.WriteLine($"\nAluno: {alunoEncontrado.Nome} (Matrícula: {alunoEncontrado.Matricula})");
                Console.WriteLine("\nDisciplinas matriculadas:");

                // A lista percorre internamente
                Dados.listaMatriculas.ExibirDisciplinasDoAluno(alunoEncontrado.Matricula, Dados.listaDisciplinas);

                Console.Write("\nDeseja consultar as disciplinas de outro aluno? (S/N): ");
                if (Console.ReadLine()?.Trim().ToUpper() != "S") return;
            }
        }
    }
}
