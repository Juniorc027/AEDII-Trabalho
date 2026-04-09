using AEDII_Trabalho.Classes;
using System;
using System.IO;

namespace AEDII_Trabalho.LerESalvar
{
    public static class Arquivos
    {
        private static string CaminhoBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dados");
        public static void SalvarDados()
        {

            // Cria a pasta se ela não existir
            if (!Directory.Exists(CaminhoBase))
            {
                Directory.CreateDirectory(CaminhoBase);
            }

            // Salvar Alunos
            using (StreamWriter sw = new StreamWriter(Path.Combine(CaminhoBase, "Alunos.dat")))
            {
                for (int i = 0; i < Program.qtdAlunos; i++)
                {
                    Aluno a = Program.alunos[i];
                    sw.WriteLine(a.Matricula + ";" + a.Nome + ";" + a.Idade);
                }
            }

            // Salvar Disciplinas
            using (StreamWriter sw = new StreamWriter(Path.Combine(CaminhoBase, "Disciplinas.dat")))
            {
                for (int i = 0; i < Program.qtdDisciplinas; i++)
                {
                    Disciplina d = Program.disciplinas[i];
                    sw.WriteLine(d.CodigoDisciplina + ";" + d.NomeDisciplina + ";" + d.NotaMinimaDisciplina);
                }
            }

            // Salvar Matrículas
            using (StreamWriter sw = new StreamWriter(Path.Combine(CaminhoBase, "Matriculas.dat")))
            {
                for (int i = 0; i < Program.qtdMatriculas; i++)
                {
                    Matricula m = Program.matriculas[i];
                    sw.WriteLine(m.MatriculaAluno + ";" + m.CodigoDisciplina + ";" + m.Nota1 + ";" + m.Nota2);
                }
            }

            Console.WriteLine("Dados salvos com sucesso na pasta Arquivos!");
            // Console.ReadKey();
        }

        public static void CarregarDados()
        {
            if (File.Exists(Path.Combine(CaminhoBase, "Alunos.dat")))
            {
                using (StreamReader sr = new StreamReader(Path.Combine(CaminhoBase, "Alunos.dat")))
                {
                    string line;
                    Program.qtdAlunos = 0; // Zera a contagem para evitar sobrescrever dados antigos
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(line)) continue;
                        {
                            string[] parts = line.Split(';');
                            if (parts.Length >= 3)
                            {
                                Program.alunos[Program.qtdAlunos] = new Aluno // Cria um novo objeto Aluno e atribui os valores lidos do arquivo
                                {
                                    Matricula = int.Parse(parts[0]),
                                    Nome = parts[1],
                                    Idade = int.Parse(parts[2])
                                };
                                Program.qtdAlunos++;
                            }
                        }
                    }
                }
            }

            if (File.Exists(Path.Combine(CaminhoBase, "Disciplinas.dat")))
            {
                using (StreamReader sr = new StreamReader(Path.Combine(CaminhoBase, "Disciplinas.dat")))
                {
                    string line;
                    Program.qtdDisciplinas = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split(';');
                        if (parts.Length >= 3)
                        {
                            Program.disciplinas[Program.qtdDisciplinas] = new Disciplina
                            {
                                CodigoDisciplina = int.Parse(parts[0]),
                                NomeDisciplina = parts[1],
                                NotaMinimaDisciplina = double.Parse(parts[2])
                            };
                            Program.qtdDisciplinas++;
                        }
                    }
                }
            }
            if (File.Exists(Path.Combine(CaminhoBase, "Matriculas.dat")))
            {
                using (StreamReader sr = new StreamReader(Path.Combine(CaminhoBase, "Matriculas.dat")))
                {
                    string line;
                    Program.qtdMatriculas = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length >= 4) // Verifica se a linha tem pelo menos 4 partes para evitar erros de formatação
                        {
                            Program.matriculas[Program.qtdMatriculas] = new Matricula
                            {
                                MatriculaAluno = int.Parse(parts[0]),
                                CodigoDisciplina = int.Parse(parts[1]),
                                Nota1 = double.Parse(parts[2]),
                                Nota2 = double.Parse(parts[3])
                            };
                            Program.qtdMatriculas++;
                        }
                    }
                }
            }

            Console.WriteLine("Dados carregados com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            return;
        }
    }
}