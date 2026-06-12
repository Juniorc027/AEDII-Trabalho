using System;
using AEDII_Trabalho.Classes;

namespace AEDII_Trabalho.Controllers
{
    public static class ControladorMatricula
    {
        // Vincula um aluno a uma disciplina, verificando duplicatas
        public static void CadastroMatriculas()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Cadastro de Matrícula ====");

                // Repete a busca até encontrar um aluno válido
                Aluno alunoEncontrado = null;
                while (alunoEncontrado == null)
                {
                    Console.Write("Informe o aluno (nome ou matrícula): ");
                    string verificacaoAluno = Console.ReadLine()?.Trim();

                    if (int.TryParse(verificacaoAluno, out int matriculaInformada))
                        alunoEncontrado = Dados.listaAlunos.BuscarPorMatricula(matriculaInformada);
                    else
                        alunoEncontrado = Dados.listaAlunos.BuscarPeloNome(verificacaoAluno);

                    if (alunoEncontrado == null)
                        Console.WriteLine("Aluno não encontrado. Tente novamente.");
                }

                Console.WriteLine($"Aluno encontrado: {alunoEncontrado.Nome} | Matrícula: {alunoEncontrado.Matricula}");

                // Repete a busca até encontrar uma disciplina válida
                Disciplina disciplinaEncontrada = null;
                while (disciplinaEncontrada == null)
                {
                    Console.Write("\nInforme a disciplina (nome ou código): ");
                    string verificacaoDisciplina = Console.ReadLine()?.Trim();

                    if (int.TryParse(verificacaoDisciplina, out int codigoInformado))
                        disciplinaEncontrada = Dados.listaDisciplinas.BuscarPorCodigo(codigoInformado);
                    else
                        disciplinaEncontrada = Dados.listaDisciplinas.BuscarPorNome(verificacaoDisciplina);

                    if (disciplinaEncontrada == null)
                        Console.WriteLine("Disciplina não encontrada. Tente novamente.");
                }

                Console.WriteLine($"Disciplina encontrada: {disciplinaEncontrada.NomeDisciplina} | Código: {disciplinaEncontrada.CodigoDisciplina}");

                // Impede que o mesmo aluno seja matriculado duas vezes na mesma disciplina
                if (Dados.listaMatriculas.ExisteMatricula(alunoEncontrado.Matricula, disciplinaEncontrada.CodigoDisciplina))
                {
                    Console.WriteLine("\nEste aluno já está matriculado nesta disciplina!");
                }
                else
                {
                    Matricula novaMatricula = new Matricula();
                    novaMatricula.MatriculaAluno = alunoEncontrado.Matricula;
                    novaMatricula.CodigoDisciplina = disciplinaEncontrada.CodigoDisciplina;

                    Dados.listaMatriculas.Inserir(novaMatricula);

                    Console.WriteLine("\nMatrícula realizada com sucesso!");
                    Console.WriteLine($"Aluno: {alunoEncontrado.Nome} | Disciplina: {disciplinaEncontrada.NomeDisciplina}");
                }

                Console.Write("\nDeseja realizar outra matrícula? (S/N): ");
                string resposta = Console.ReadLine()?.Trim().ToUpper();
                if (resposta != "S") return;
            }
        }

        // Busca a matrícula pelo par aluno+disciplina e atribui as notas informadas
        public static void AtribuirNota()
        {
            if (Dados.listaMatriculas.EstaVazia())
            {
                Console.WriteLine("Não existem matrículas cadastradas ainda.");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Atribuir Nota ao Aluno =====");

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

                // Repete a busca até encontrar uma disciplina válida
                Disciplina disciplinaEncontrada = null;
                while (disciplinaEncontrada == null)
                {
                    Console.Write("Informe a disciplina (nome ou código): ");
                    string entrada = Console.ReadLine()?.Trim();

                    if (int.TryParse(entrada, out int codigoInformado))
                        disciplinaEncontrada = Dados.listaDisciplinas.BuscarPorCodigo(codigoInformado);
                    else
                        disciplinaEncontrada = Dados.listaDisciplinas.BuscarPorNome(entrada);

                    if (disciplinaEncontrada == null)
                        Console.WriteLine("Disciplina não encontrada. Tente novamente.");
                }

                // Localiza a matrícula pelo par aluno + disciplina
                Matricula matriculaEncontrada = Dados.listaMatriculas.BuscarPorAlunoEDisciplina(
                    alunoEncontrado.Matricula, disciplinaEncontrada.CodigoDisciplina);

                if (matriculaEncontrada == null)
                {
                    Console.WriteLine($"\nO aluno '{alunoEncontrado.Nome}' não está matriculado em '{disciplinaEncontrada.NomeDisciplina}'.");
                    Console.Write("\nDeseja atribuir nota a outro aluno? (S/N): ");
                    if (Console.ReadLine()?.Trim().ToUpper() != "S") return;
                    continue;
                }

                Console.Write("Digite a Nota 1 (0 a 10): ");
                // Substitui vírgula por ponto para aceitar ambos os separadores decimais
                string inputNota1 = Console.ReadLine()?.Replace(',', '.');
                if (!double.TryParse(inputNota1, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out double nota1)
                    || nota1 < 0 || nota1 > 10)
                {
                    Console.WriteLine("Nota 1 inválida.");
                    Console.Write("\nDeseja atribuir nota a outro aluno? (S/N): ");
                    if (Console.ReadLine()?.Trim().ToUpper() != "S") return;
                    continue;
                }

                Console.Write("Digite a Nota 2 (0 a 10): ");
                string inputNota2 = Console.ReadLine()?.Replace(',', '.');
                if (!double.TryParse(inputNota2, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out double nota2)
                    || nota2 < 0 || nota2 > 10)
                {
                    Console.WriteLine("Nota 2 inválida.");
                    Console.Write("\nDeseja atribuir nota a outro aluno? (S/N): ");
                    if (Console.ReadLine()?.Trim().ToUpper() != "S") return;
                    continue;
                }

                matriculaEncontrada.Nota1 = nota1;
                matriculaEncontrada.Nota2 = nota2;

                Console.WriteLine("\nNotas atribuídas com sucesso!");
                Console.WriteLine($"Aluno: {alunoEncontrado.Nome} | Disciplina: {disciplinaEncontrada.NomeDisciplina}");
                Console.WriteLine($"Nota 1: {nota1} | Nota 2: {nota2}");

                Console.Write("\nDeseja atribuir nota a outro aluno? (S/N): ");
                if (Console.ReadLine()?.Trim().ToUpper() != "S") return;
            }
        }
    }
}
