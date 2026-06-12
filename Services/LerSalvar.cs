using System;
using System.IO;

namespace AEDII_Trabalho.LerESalvar
{
    public static class Arquivos
    {
        private static string CaminhoBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dados");

        public static void SalvarDados()
        {
            // Cria a pasta de dados se ainda não existir
            if (!Directory.Exists(CaminhoBase))
                Directory.CreateDirectory(CaminhoBase);

            // Salvar Alunos — a lista percorre seus nós e grava cada registro
            using (StreamWriter sw = new StreamWriter(Path.Combine(CaminhoBase, "Alunos.dat")))
            {
                Dados.listaAlunos.SalvarParaArquivo(sw);
            }

            // Salvar Disciplinas — a lista percorre seus nós e grava cada registro
            using (StreamWriter sw = new StreamWriter(Path.Combine(CaminhoBase, "Disciplinas.dat")))
            {
                Dados.listaDisciplinas.SalvarParaArquivo(sw);
            }

            // Salvar Matrículas — a lista percorre seus nós e grava cada registro
            using (StreamWriter sw = new StreamWriter(Path.Combine(CaminhoBase, "Matriculas.dat")))
            {
                Dados.listaMatriculas.SalvarParaArquivo(sw);
            }

            Console.WriteLine("Dados salvos com sucesso!");
        }

        public static void CarregarDados()
        {
            // Carrega Alunos do arquivo para a lista encadeada
            if (File.Exists(Path.Combine(CaminhoBase, "Alunos.dat")))
            {
                using (StreamReader sr = new StreamReader(Path.Combine(CaminhoBase, "Alunos.dat")))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(linha))
                        { 
                            continue;
                        }
                        // Split retorna string[] nativamente — necessário para leitura do arquivo .dat
                        string[] partes = linha.Split(';');
                        if (partes.Length >= 3)
                        {
                            Classes.Aluno a = new Classes.Aluno();
                            a.Matricula = int.Parse(partes[0]);
                            a.Nome = partes[1];
                            a.Idade = int.Parse(partes[2]);
                            Dados.listaAlunos.Inserir(a);
                        }
                    }
                }
            }

            // Carrega Disciplinas do arquivo para a lista encadeada
            if (File.Exists(Path.Combine(CaminhoBase, "Disciplinas.dat")))
            {
                using (StreamReader sr = new StreamReader(Path.Combine(CaminhoBase, "Disciplinas.dat")))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(linha))
                        { 
                            continue;
                        }
                        // Split retorna string[] nativamente necessário para leitura do arquivo .dat
                        string[] partes = linha.Split(';');
                        if (partes.Length >= 3)
                        {
                            Classes.Disciplina d = new Classes.Disciplina();
                            d.CodigoDisciplina = int.Parse(partes[0]);
                            d.NomeDisciplina = partes[1];
                            // InvariantCulture garante leitura correta em qualquer sistema (ponto ou vírgula decimal)
                            d.NotaMinimaDisciplina = double.Parse(partes[2], System.Globalization.CultureInfo.InvariantCulture);
                            Dados.listaDisciplinas.Inserir(d);
                        }
                    }
                }
            }

            // Carrega Matrículas do arquivo para a lista encadeada
            if (File.Exists(Path.Combine(CaminhoBase, "Matriculas.dat")))
            {
                using (StreamReader sr = new StreamReader(Path.Combine(CaminhoBase, "Matriculas.dat")))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(linha))
                        { 
                            continue;
                        }
                        // Split retorna string[] nativamente se necessário para leitura do arquivo .dat
                        string[] partes = linha.Split(';');
                        if (partes.Length >= 4)
                        {
                            Classes.Matricula m = new Classes.Matricula();
                            m.MatriculaAluno = int.Parse(partes[0]);
                            m.CodigoDisciplina = int.Parse(partes[1]);
                            m.Nota1 = double.Parse(partes[2], System.Globalization.CultureInfo.InvariantCulture);
                            m.Nota2 = double.Parse(partes[3], System.Globalization.CultureInfo.InvariantCulture);
                            Dados.listaMatriculas.Inserir(m);
                        }
                    }
                }
            }

            bool algumArquivoExiste =
                File.Exists(Path.Combine(CaminhoBase, "Alunos.dat")) ||
                File.Exists(Path.Combine(CaminhoBase, "Disciplinas.dat")) ||
                File.Exists(Path.Combine(CaminhoBase, "Matriculas.dat"));

            if (algumArquivoExiste)
            {
                Console.WriteLine("Dados carregados com sucesso!");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
