using AEDII_Trabalho.Classes;
using AEDII_Trabalho.LerESalvar;
using System;

namespace AEDII_Trabalho
{
    public class Program
    {
        public static Aluno[] alunos; //Cria um array de alunos para armazenar os objetos do tipo Aluno
        public static Disciplina[] disciplinas;
        public static Matricula[] matriculas;
        public static int qtdAlunos = 0; //Contador para controlar a quantidade de alunos registrados
        public static int qtdDisciplinas = 0;
        public static int qtdMatriculas = 0;


        static void Main(string[] args)
        {
            alunos = new Aluno[200]; //Inicia o array de alunos com o limite de 200 alunos
            disciplinas = new Disciplina[50];
            matriculas = new Matricula[200];

            AEDII_Trabalho.LerESalvar.Arquivos.CarregarDados(); //Carrega os dados 
            MenuPrincipal(); //Chamada do menu principal
        }

        static void MenuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Menu Escolar ====");
                Console.WriteLine("1. Consultas");
                Console.WriteLine("2. Cadastros");
                Console.WriteLine("3. Salvar");
                Console.WriteLine("4. Sair");
                Console.Write("Escolha uma das opções: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        MenuConsultas();
                        break;

                    case "2":
                        MenuCadastro();
                        break;

                    case "3":
                        Arquivos.SalvarDados();
                        break;

                    case "4":
                        Arquivos.SalvarDados();
                        Console.WriteLine("\nPrograma finalizando...");
                        return;

                    default:
                        Console.WriteLine("Opção inválida! Pressione qualquer tecla...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void MenuConsultas()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Menu de Consultas ====");
                Console.WriteLine("1. Alunos");
                Console.WriteLine("2. Disciplinas");
                Console.WriteLine("3. Alunos das Disciplina");
                Console.WriteLine("4. Disciplinas do Aluno");
                Console.WriteLine("5. Voltar Menu Principal");
                Console.Write("Escolha uma das opções: ");
                string opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        ListarAlunos();
                        break;
                    case "2":
                        ListarDisciplinas();
                        break;
                    case "3":
                        AlunosDisciplinas();
                        break;
                    case "4":
                        DisciplinasAlunos();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opção Invalida! Pressione qualquer tecla...");
                        Console.ReadKey();
                        break;

                }
            }
        }

        static void MenuCadastro()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Menu de Cadastros ====");
                Console.WriteLine("1. Cadastrar Alunos");
                Console.WriteLine("2. Cadastrar Disciplinas");
                Console.WriteLine("3. Matrículas");
                Console.WriteLine("4. Atribuir Nota a Aluno");
                Console.WriteLine("5. Voltar Menu Principal");
                Console.Write("Escolha uma das opções: ");

                string opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        CadastroAlunos();
                        break;
                    case "2":
                        CadastroDisciplina();
                        break;
                    case "3":
                        CadastroMatriculas();
                        break;
                    case "4":
                        AtribuirNota();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opção Invalida! Pressione qualquer tecla...");
                        Console.ReadKey();
                        break;


                }


            }
        }

        static void CadastroAlunos()
        {
            //Verificação se tem espaço para registrar um novo aluno
            if (qtdAlunos >= alunos.Length)
            {
                Console.WriteLine("O limite de matriculas foi atingido. Não é possível cadastrar mais nenhuma matricula.");
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Cadastro de Aluno ===");

            Console.WriteLine("Digite o nome do aluno: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite a idade do aluno: ");
            if (!int.TryParse(Console.ReadLine(), out int idade) || idade <= 0)
            {
                Console.WriteLine("Idade inválida. Cadastro cancelado.");
                return;
            }

            //Cria um novo objeto Aluno e atribuindo os valores
            Aluno novoAluno = new Aluno();
            novoAluno.Nome = nome;
            novoAluno.Idade = idade;
            int maior = 0;
            for (int i = 0; i < qtdAlunos; i++) //percorre o array de alunos ate encontrar a maior matricula existente.
            {
                if (alunos[i].Matricula > maior) //verifica se a matricula do aluno atual é maior que a matricula encontrada.
                {
                    maior = alunos[i].Matricula; //se for maior, atualiza a variável "maior" com o valor da matricula do aluno atual.
                }
            }
            novoAluno.Matricula = maior + 1; //Gera uma matricula unica pegando o maior valor de matricula existente e somando 1 para criar a nova matricula.

            // Armazenando o aluno no array e incrementando no contador
            alunos[qtdAlunos] = novoAluno;
            qtdAlunos++;

            Console.WriteLine($"Aluno '{novoAluno.Nome}' cadastrado com sucesso!");
            Console.WriteLine($"Matricula gerada: {novoAluno.Matricula}");
            Console.WriteLine($"Posição no sistema {qtdAlunos}");



        }
        static void ListarAlunos()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Alunos ===");
            Console.WriteLine($"quantidade de alunos cadastrados: \n{qtdAlunos}");

            if (qtdAlunos == 0)
            {
                Console.WriteLine("Nenhum aluno cadastrado.");
                Console.WriteLine("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < qtdAlunos; i++)
            {
                Aluno a = alunos[i];
                Console.WriteLine($"Matrícula: {a.Matricula} | Nome: {a.Nome} | Idade: {a.Idade} anos");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();


        }
        static void CadastroDisciplina()
        {
            if (qtdDisciplinas >= disciplinas.Length)
            {
                Console.WriteLine("O limite de disciplinas foi atingido. Não é possível cadastrar mais disciplinas.");
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Cadastro de Disciplinas ===");

            Console.Write("Digite o nome da disciplina: ");
            string nomeDisciplina = Console.ReadLine()?.Trim();

            Console.Write("Informe a nota mínima (0 a 10): ");
            if (!int.TryParse(Console.ReadLine(), out int notaMinima) || notaMinima < 0 || notaMinima > 10)
            {
                Console.WriteLine("Nota mínima inválida. Cadastro cancelado.");
                return;
            }

            // Cria o objeto Disciplina
            Disciplina novaDisciplina = new Disciplina();
            novaDisciplina.NomeDisciplina = nomeDisciplina;
            novaDisciplina.NotaMinimaDisciplina = notaMinima;

            // Gera código único da disciplina
            int maior = 0;
            for (int i = 0; i < qtdDisciplinas; i++)
            {
                if (disciplinas[i].CodigoDisciplina > maior)
                {
                    maior = disciplinas[i].CodigoDisciplina;
                }
            }
            novaDisciplina.CodigoDisciplina = maior + 1;

            // Armazena a disciplina no array
            disciplinas[qtdDisciplinas] = novaDisciplina;
            qtdDisciplinas++;

            Console.WriteLine($"\nDisciplina '{novaDisciplina.NomeDisciplina}' cadastrada com sucesso!");
            Console.WriteLine($"Código gerado: {novaDisciplina.CodigoDisciplina}");
        }

        static void ListarDisciplinas()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Disciplinas ===");
            Console.WriteLine($"Quantidade de disciplinas cadastradas: {qtdDisciplinas}");
            if (qtdDisciplinas == 0)
            {
                Console.WriteLine("Nenhuma disciplina cadastrada.");
                Console.WriteLine("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < qtdDisciplinas; i++)
            {
                Disciplina d = disciplinas[i];
                Console.WriteLine($"Código: {d.CodigoDisciplina} | Nome: {d.NomeDisciplina} | Nota Mínima: {d.NotaMinimaDisciplina}");
            }
            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        static void CadastroMatriculas()
        {
            if (qtdMatriculas >= matriculas.Length)
            {
                Console.WriteLine("O limite de matrículas foi atingido. Não é possível cadastrar mais nenhuma matrícula.");
                return;
            }

            Console.Clear();
            Console.WriteLine("==== Cadastro de Matrícula ====");

            // BUSCA DO ALUNO 
            Console.Write("Informe o aluno (nome ou matrícula): ");
            string verificacaoAluno = Console.ReadLine();

            Aluno alunoEncontrado = null;

            if (int.TryParse(verificacaoAluno, out int matriculaInformada))
            {
                // Busca por matrícula
                for (int i = 0; i < qtdAlunos; i++)
                {
                    if (alunos[i].Matricula == matriculaInformada)
                    {
                        alunoEncontrado = alunos[i];
                        break;
                    }
                }
            }
            else
            {
                // Busca por nome (ignorando maiúsculas/minúsculas)
                for (int i = 0; i < qtdAlunos; i++)
                {
                    if (alunos[i].Nome.Equals(verificacaoAluno, StringComparison.OrdinalIgnoreCase))
                    {
                        alunoEncontrado = alunos[i];
                        break;
                    }
                }
            }

            if (alunoEncontrado == null)
            {
                Console.WriteLine("Aluno não encontrado!");
                Console.WriteLine("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Aluno encontrado: {alunoEncontrado.Nome} | Matrícula: {alunoEncontrado.Matricula}");

            // BUSCA DA DISCIPLINA


            Console.Write("\nInforme a disciplina (nome ou código): ");
            string verificacaoDisciplina = Console.ReadLine()?.Trim();

            Disciplina disciplinaEncontrada = null;

            if (int.TryParse(verificacaoDisciplina, out int codigoInformado))
            {
                // Busca por código
                for (int i = 0; i < qtdDisciplinas; i++)
                {
                    if (disciplinas[i].CodigoDisciplina == codigoInformado)
                    {
                        disciplinaEncontrada = disciplinas[i];
                        break;
                    }
                }
            }
            else
            {
                // Busca por nome (ignorando maiúsculas/minúsculas)
                for (int i = 0; i < qtdDisciplinas; i++)
                {
                    if (disciplinas[i].NomeDisciplina.Equals(verificacaoDisciplina, StringComparison.OrdinalIgnoreCase))
                    {
                        disciplinaEncontrada = disciplinas[i];
                        break;
                    }
                }
            }

            if (disciplinaEncontrada == null)
            {
                Console.WriteLine("Disciplina não encontrada!");
                Console.WriteLine("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Disciplina encontrada: {disciplinaEncontrada.NomeDisciplina} | Código: {disciplinaEncontrada.CodigoDisciplina}");

            // VERIFICAÇÃO DE DUPLICATA
            for (int i = 0; i < qtdMatriculas; i++)
            {
                if (matriculas[i].MatriculaAluno == alunoEncontrado.Matricula &&
                    matriculas[i].CodigoDisciplina == disciplinaEncontrada.CodigoDisciplina)
                {
                    Console.WriteLine("Este aluno já está matriculado nesta disciplina!");
                    Console.ReadKey();
                    return;
                }
            }

            // CRIAÇÃO DA MATRÍCULA 
            Matricula novaMatricula = new Matricula();
            novaMatricula.MatriculaAluno = alunoEncontrado.Matricula;
            novaMatricula.CodigoDisciplina = disciplinaEncontrada.CodigoDisciplina;

            matriculas[qtdMatriculas] = novaMatricula;
            qtdMatriculas++;

            Console.WriteLine("\nMatrícula realizada com sucesso!");
            Console.WriteLine($"Aluno: {alunoEncontrado.Nome}");
            Console.WriteLine($"Disciplina: {disciplinaEncontrada.NomeDisciplina}");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        static void AtribuirNota()
        {
            if (qtdMatriculas == 0)
            {
                Console.WriteLine("Não existem matrículas cadastradas ainda.");
                Console.ReadKey();
                return;
            }

            Console.Clear();

            Console.WriteLine("===== Atribuir Nota ao Aluno =====");

            Console.WriteLine("Informe o aluno (nome ou matrícula):");
            string entrada = Console.ReadLine();

            Matricula matriculaEncontrada = null;
            if (int.TryParse(entrada, out int MatriculaInformada))
            {
                for (int i = 0; i < qtdMatriculas; i++)
                {
                    if (matriculas[i].MatriculaAluno == MatriculaInformada)
                    {
                        matriculaEncontrada = matriculas[i];
                        break;
                    }
                }
            }

            else
            {
                for (int i = 0; i < qtdAlunos; i++) // Busca pelo nome do aluno (busca no array de alunos primeiro)
                {
                    if (alunos[i].Nome.Equals(entrada, StringComparison.OrdinalIgnoreCase))
                    {
                        for (int j = 0; j < qtdMatriculas; j++) //Bsuca a matricula desse aluno
                        {
                            if (matriculas[j].MatriculaAluno == alunos[i].Matricula)
                            {
                                matriculaEncontrada = matriculas[j];
                                break;
                            }
                        }
                        break;
                    }

                }
            }

            if (matriculaEncontrada == null)
            {
                Console.WriteLine("Matricula não encontrada para este aluno!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Digite a Nota 1 (0 a 10): ");
            if (!double.TryParse(Console.ReadLine(), out double nota1) || nota1 < 0 || nota1 > 10)
            {
                Console.WriteLine("Nota 1 inválida.");
                Console.ReadKey();
                return;
            }

            Console.Write("Digite a Nota 2 (0 a 10): ");
            if (!double.TryParse(Console.ReadLine(), out double nota2) || nota2 < 0 || nota2 > 10)
            {
                Console.WriteLine("Nota 2 inválida.");
                Console.ReadKey();
                return;
            }

            // 3. Atribuir as notas
            matriculaEncontrada.Nota1 = nota1;
            matriculaEncontrada.Nota2 = nota2;

            Console.WriteLine("\nNotas atribuídas com sucesso!");
            Console.WriteLine($"Nota 1: {nota1} | Nota 2: {nota2}");
            Console.WriteLine("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        static void AlunosDisciplinas()
        {
            if (qtdDisciplinas == 0)
            {
                Console.WriteLine("Não há nenhuma disciplina registrada ainda.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Alunos da Disciplina ===");

            // Pedir a disciplina (nome ou código)
            Console.Write("Informe o nome ou código da disciplina: ");
            string entrada = Console.ReadLine()?.Trim();

            Disciplina disciplinaEncontrada = null;

            // Tenta buscar por código ou por nome
            if (int.TryParse(entrada, out int codigoInformado))
            {
                // Busca pelo código
                for (int i = 0; i < qtdDisciplinas; i++)
                {
                    if (disciplinas[i].CodigoDisciplina == codigoInformado)
                    {
                        disciplinaEncontrada = disciplinas[i];
                        break;
                    }
                }
            }
            else
            {
                // Busca por nome (ignorando maiúsculas/minúsculas)
                for (int i = 0; i < qtdDisciplinas; i++)
                {
                    if (disciplinas[i].NomeDisciplina.Equals(entrada, StringComparison.OrdinalIgnoreCase))
                    {
                        disciplinaEncontrada = disciplinas[i];
                        break;
                    }
                }
            }

            // Se não encontrou a disciplina
            if (disciplinaEncontrada == null)
            {
                Console.WriteLine("Disciplina não encontrada!");
                Console.WriteLine("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nDisciplina encontrada: {disciplinaEncontrada.NomeDisciplina} (Código: {disciplinaEncontrada.CodigoDisciplina})");
            Console.WriteLine("Nota mínima para aprovação: " + disciplinaEncontrada.NotaMinimaDisciplina);
            Console.WriteLine("\nAlunos matriculados:");

            bool encontrouAluno = false;

            // Percorre todas as matrículas para encontrar alunos dessa disciplina
            for (int i = 0; i < qtdMatriculas; i++)
            {
                if (matriculas[i].CodigoDisciplina == disciplinaEncontrada.CodigoDisciplina)
                {
                    encontrouAluno = true;

                    // Busca o aluno correspondente
                    Aluno aluno = null;
                    for (int j = 0; j < qtdAlunos; j++)
                    {
                        if (alunos[j].Matricula == matriculas[i].MatriculaAluno)
                        {
                            aluno = alunos[j];
                            break;
                        }
                    }

                    if (aluno != null)
                    {
                        double media = 0;
                        if (matriculas[i].Nota1 > 0 || matriculas[i].Nota2 > 0)
                        {
                            media = (matriculas[i].Nota1 + matriculas[i].Nota2) / 2.0;
                        }

                        string status = media >= disciplinaEncontrada.NotaMinimaDisciplina ? "APROVADO" : "REPROVADO";

                        Console.WriteLine($"Matrícula: {aluno.Matricula} | Nome: {aluno.Nome,-25} | " +
                                          $"Notas: {matriculas[i].Nota1} | {matriculas[i].Nota2} | " +
                                          $"Média: {media:F1} | Status: {status}");
                    }
                }
            }

            if (!encontrouAluno)
            {
                Console.WriteLine("Nenhum aluno matriculado nesta disciplina ainda.");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        static void DisciplinasAlunos()
        {
            if (qtdAlunos == 0)
            {
                Console.WriteLine("Não há nenhum aluno cadastrado ainda.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Disciplinas do Aluno ===");

            // Pede o aluno (nome ou matrícula)
            Console.Write("Informe o aluno (nome ou matrícula): ");
            string entrada = Console.ReadLine()?.Trim();

            Aluno alunoEncontrado = null;

            // Tenta buscar por matrícula ou por nome
            if (int.TryParse(entrada, out int matriculaInformada))
            {
                // Busca pela matrícula
                for (int i = 0; i < qtdAlunos; i++)
                {
                    if (alunos[i].Matricula == matriculaInformada)
                    {
                        alunoEncontrado = alunos[i];
                        break;
                    }
                }
            }
            else
            {
                // Busca pela nome
                for (int i = 0; i < qtdAlunos; i++)
                {
                    if (alunos[i].Nome.Equals(entrada, StringComparison.OrdinalIgnoreCase))
                    {
                        alunoEncontrado = alunos[i];
                        break;
                    }
                }
            }

            if (alunoEncontrado == null)
            {
                Console.WriteLine("Aluno não encontrado!");
                Console.WriteLine("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nAluno encontrado: {alunoEncontrado.Nome} (Matrícula: {alunoEncontrado.Matricula})");
            Console.WriteLine("\nDisciplinas matriculadas:");

            bool encontrouDisciplina = false;

            // Percorre todas as matrículas procurando as do aluno
            for (int i = 0; i < qtdMatriculas; i++)
            {
                if (matriculas[i].MatriculaAluno == alunoEncontrado.Matricula)
                {
                    encontrouDisciplina = true;

                    // Busca a disciplina correspondente
                    Disciplina disciplina = null;
                    for (int j = 0; j < qtdDisciplinas; j++)
                    {
                        if (disciplinas[j].CodigoDisciplina == matriculas[i].CodigoDisciplina)
                        {
                            disciplina = disciplinas[j];
                            break;
                        }
                    }

                    if (disciplina != null)
                    {
                        double media = 0;
                        if (matriculas[i].Nota1 > 0 || matriculas[i].Nota2 > 0)
                        {
                            media = (matriculas[i].Nota1 + matriculas[i].Nota2) / 2.0;
                        }

                        string status = media >= disciplina.NotaMinimaDisciplina ? "APROVADO" : "REPROVADO";

                        Console.WriteLine($"Código: {disciplina.CodigoDisciplina} | Disciplina: {disciplina.NomeDisciplina,-30} | " +
                                          $"Notas: {matriculas[i].Nota1} | {matriculas[i].Nota2} | " +
                                          $"Média: {media:F1} | Status: {status}");
                    }
                }
            }

            if (!encontrouDisciplina)
            {
                Console.WriteLine("Este aluno não está matriculado em nenhuma disciplina ainda.");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}
