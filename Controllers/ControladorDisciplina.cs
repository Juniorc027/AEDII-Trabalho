using System;
using AEDII_Trabalho.Classes;

namespace AEDII_Trabalho.Controllers
{
    public static class ControladorDisciplina
    {
        // Solicita nome e nota mínima, gera código único e insere na lista encadeada
        public static void CadastroDisciplina()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Cadastro de Disciplinas ===");

                Console.Write("Digite o nome da disciplina: ");
                string nomeDisciplina = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(nomeDisciplina))
                {
                    Console.WriteLine("Nome inválido.");
                    Console.Write("Deseja tentar novamente? (S/N): ");
                    if (Console.ReadLine()?.Trim().ToUpper() != "S") return;
                    continue;
                }

                // Impede cadastrar disciplina com nome duplicado
                if (Dados.listaDisciplinas.BuscarPorNome(nomeDisciplina) != null)
                {
                    Console.WriteLine("Já existe uma disciplina com esse nome.");
                    Console.Write("Deseja tentar novamente? (S/N): ");
                    if (Console.ReadLine()?.Trim().ToUpper() != "S") return;
                    continue;
                }

                Console.Write("Informe a nota mínima (0 a 10): ");
                // Substitui vírgula por ponto para aceitar ambos os separadores decimais
                string inputNota = Console.ReadLine()?.Replace(',', '.');
                if (!double.TryParse(inputNota, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out double notaMinima)
                    || notaMinima < 0 || notaMinima > 10)
                {
                    Console.WriteLine("Nota mínima inválida.");
                    Console.Write("Deseja tentar novamente? (S/N): ");
                    if (Console.ReadLine()?.Trim().ToUpper() != "S") return;
                    continue;
                }

                // Gera código único: maior valor existente na lista + 1
                int novoCodigo = Dados.listaDisciplinas.GetMaiorCodigo() + 1;

                Disciplina novaDisciplina = new Disciplina();
                novaDisciplina.NomeDisciplina = nomeDisciplina;
                novaDisciplina.NotaMinimaDisciplina = notaMinima;
                novaDisciplina.CodigoDisciplina = novoCodigo;

                Dados.listaDisciplinas.Inserir(novaDisciplina);

                Console.WriteLine($"\nDisciplina '{novaDisciplina.NomeDisciplina}' cadastrada com sucesso!");
                Console.WriteLine($"Código gerado: {novaDisciplina.CodigoDisciplina}");

                Console.Write("\nDeseja cadastrar outra disciplina? (S/N): ");
                string resposta = Console.ReadLine()?.Trim().ToUpper();
                if (resposta != "S") return;
            }
        }

        // Exibe todas as disciplinas cadastradas na lista encadeada
        public static void ListarDisciplinas()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Disciplinas ===");
            Console.WriteLine($"Quantidade de disciplinas cadastradas: {Dados.listaDisciplinas.GetQuantidade()}");

            if (Dados.listaDisciplinas.EstaVazia())
            {
                Console.WriteLine("Nenhuma disciplina cadastrada.");
                Console.WriteLine("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Dados.listaDisciplinas.ExibirTodas();

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        // Lista todos os alunos de uma disciplina com notas e situação (aprovado/reprovado)
        public static void AlunosDaDisciplina()
        {
            if (Dados.listaDisciplinas.EstaVazia())
            {
                Console.WriteLine("Não há nenhuma disciplina registrada ainda.");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Alunos da Disciplina ===");

                // Repete a busca até encontrar uma disciplina válida
                Disciplina disciplinaEncontrada = null;
                while (disciplinaEncontrada == null)
                {
                    Console.Write("Informe o nome ou código da disciplina: ");
                    string entrada = Console.ReadLine()?.Trim();

                    if (int.TryParse(entrada, out int codigoInformado))
                        disciplinaEncontrada = Dados.listaDisciplinas.BuscarPorCodigo(codigoInformado);
                    else
                        disciplinaEncontrada = Dados.listaDisciplinas.BuscarPorNome(entrada);

                    if (disciplinaEncontrada == null)
                        Console.WriteLine("Disciplina não encontrada. Tente novamente.");
                }

                Console.WriteLine($"\nDisciplina: {disciplinaEncontrada.NomeDisciplina} (Código: {disciplinaEncontrada.CodigoDisciplina})");
                Console.WriteLine($"Nota mínima para aprovação: {disciplinaEncontrada.NotaMinimaDisciplina}");
                Console.WriteLine("\nAlunos matriculados:");

                // A lista percorre internamente até encontrar o código da disciplina e exibe os alunos matriculados com suas notas e situação
                Dados.listaMatriculas.ExibirAlunosDaDisciplina(
                    disciplinaEncontrada.CodigoDisciplina,
                    disciplinaEncontrada.NotaMinimaDisciplina,
                    Dados.listaAlunos);

                Console.Write("\nDeseja consultar outra disciplina? (S/N): ");
                if (Console.ReadLine()?.Trim().ToUpper() != "S") return;
            }
        }
    }
}
