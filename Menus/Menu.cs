using System;
using AEDII_Trabalho.Controllers;
using AEDII_Trabalho.LerESalvar;

namespace AEDII_Trabalho.Menus
{
    public static class MenuPrincipal
    {
        public static void Principal()
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
                        MenuConsultas.Consultas();
                        break;
                    case "2":
                        MenuCadastro.Cadastros();
                        break;
                    case "3":
                        Arquivos.SalvarDados();
                        Console.WriteLine("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case "4":
                        Arquivos.SalvarDados(); // Salva automaticamente ao sair
                        Console.WriteLine("\nPrograma finalizando...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida! Pressione qualquer tecla...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }

    public static class MenuConsultas
    {
        public static void Consultas()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Menu de Consultas ====");
                Console.WriteLine("1. Alunos");
                Console.WriteLine("2. Disciplinas");
                Console.WriteLine("3. Alunos da Disciplina");
                Console.WriteLine("4. Disciplinas do Aluno");
                Console.WriteLine("5. Voltar Menu Principal");
                Console.Write("Escolha uma das opções: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ControladorAluno.ListarAlunos();
                        break;
                    case "2":
                        ControladorDisciplina.ListarDisciplinas();
                        break;
                    case "3":
                        ControladorDisciplina.AlunosDaDisciplina();
                        break;
                    case "4":
                        ControladorAluno.DisciplinasDoAluno();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }

    public static class MenuCadastro
    {
        public static void Cadastros()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Menu de Cadastros ====");
                Console.WriteLine("1. Cadastrar Alunos");
                Console.WriteLine("2. Cadastrar Disciplinas");
                Console.WriteLine("3. Matricular Aluno");
                Console.WriteLine("4. Atribuir Nota a Aluno");
                Console.WriteLine("5. Voltar Menu Principal");
                Console.Write("Escolha uma das opções: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ControladorAluno.CadastroAlunos();
                        break;
                    case "2":
                        ControladorDisciplina.CadastroDisciplina();
                        break;
                    case "3":
                        ControladorMatricula.CadastroMatriculas();
                        break;
                    case "4":
                        ControladorMatricula.AtribuirNota();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
