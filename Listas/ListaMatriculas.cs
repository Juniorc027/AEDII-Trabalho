using System;
using System.IO;
using AEDII_Trabalho.Classes;

namespace AEDII_Trabalho.Listas
{
    public class ListaMatricula
    {
        private No cabeca;
        private int quantidade;

        private class No
        {
            // Campos privados (encapsulamento real)
            private Matricula matricula;
            private No proximo;

            // Propriedades públicas controladas
            public Matricula Matricula
            {
                get { return matricula; }
                set { matricula = value; }
            }

            public No Proximo
            {
                get { return proximo; }
                set { proximo = value; }
            }

            // Construtor
            public No(Matricula matricula)
            {
                this.matricula = matricula;
                this.proximo = null;
            }
        }

        public ListaMatricula()
        {
            cabeca = null;
            quantidade = 0;
        }

        // Insere uma nova matrícula no final da lista
        public void Inserir(Matricula novaMatricula)
        {
            No novoNo = new No(novaMatricula);

            if (cabeca == null)
            {
                cabeca = novoNo;
            }
            else
            {
                No atual = cabeca;
                while (atual.Proximo != null) // Percorre até o último nó
                {
                    atual = atual.Proximo;
                }
                atual.Proximo = novoNo;
            }
            quantidade++;
        }

        // Busca matrícula específica pelo par aluno+disciplina (usado em AtribuirNota)
        public Matricula BuscarPorAlunoEDisciplina(int matriculaAluno, int codigoDisciplina)
        {
            No atual = cabeca;
            while (atual != null)
            {
                if (atual.Matricula.MatriculaAluno == matriculaAluno &&
                    atual.Matricula.CodigoDisciplina == codigoDisciplina)
                {
                    return atual.Matricula;
                }
                atual = atual.Proximo;
            }
            return null;
        }

        // Verifica se já existe matrícula do aluno na disciplina (evita duplicatas)
        public bool ExisteMatricula(int matriculaAluno, int codigoDisciplina)
        {
            return BuscarPorAlunoEDisciplina(matriculaAluno, codigoDisciplina) != null;
        }

        // Percorre a lista e grava cada matrícula no arquivo (matrícula;código;nota1;nota2)
        public void SalvarParaArquivo(StreamWriter sw)
        {
            No atual = cabeca;
            while (atual != null)
            {
                Matricula m = atual.Matricula;
                sw.WriteLine(m.MatriculaAluno + ";" +
                             m.CodigoDisciplina + ";" +
                             m.Nota1.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                             m.Nota2.ToString(System.Globalization.CultureInfo.InvariantCulture));
                atual = atual.Proximo;
            }
        }

        // Percorre a lista e exibe as disciplinas do aluno informado com notas e status
        public void ExibirDisciplinasDoAluno(int matriculaAluno, ListaDisciplinas listaDisciplinas)
        {
            bool encontrou = false;
            No atual = cabeca;
            while (atual != null)
            {
                if (atual.Matricula.MatriculaAluno == matriculaAluno)
                {
                    encontrou = true;
                    Disciplina disciplina = listaDisciplinas.BuscarPorCodigo(atual.Matricula.CodigoDisciplina);
                    if (disciplina != null)
                    {
                        double media = (atual.Matricula.Nota1 + atual.Matricula.Nota2) / 2.0;
                        string status = media >= disciplina.NotaMinimaDisciplina ? "APROVADO" : "REPROVADO";
                        Console.WriteLine($"Código: {disciplina.CodigoDisciplina} | Disciplina: {disciplina.NomeDisciplina,-30} | " +
                                          $"Nota 1: {atual.Matricula.Nota1} | Nota 2: {atual.Matricula.Nota2} | " +
                                          $"Média: {media:F1} | Status: {status}");
                    }
                }
                atual = atual.Proximo;
            }
            if (!encontrou)
                Console.WriteLine("Este aluno não está matriculado em nenhuma disciplina ainda.");
        }

        // Percorre a lista e exibe os alunos da disciplina informada com notas e status
        public void ExibirAlunosDaDisciplina(int codigoDisciplina, double notaMinima, ListaAlunos listaAlunos)
        {
            bool encontrou = false;
            No atual = cabeca;
            while (atual != null)
            {
                if (atual.Matricula.CodigoDisciplina == codigoDisciplina)
                {
                    encontrou = true;
                    Aluno aluno = listaAlunos.BuscarPorMatricula(atual.Matricula.MatriculaAluno);
                    if (aluno != null)
                    {
                        double media = (atual.Matricula.Nota1 + atual.Matricula.Nota2) / 2.0;
                        string status = media >= notaMinima ? "APROVADO" : "REPROVADO";
                        Console.WriteLine($"Matrícula: {aluno.Matricula} | Nome: {aluno.Nome,-25} | " +
                                          $"Nota 1: {atual.Matricula.Nota1} | Nota 2: {atual.Matricula.Nota2} | " +
                                          $"Média: {media:F1} | Status: {status}");
                    }
                }
                atual = atual.Proximo;
            }
            if (!encontrou)
                Console.WriteLine("Nenhum aluno matriculado nesta disciplina ainda.");
        }

        public void ExibirTodas()
        {
            if (EstaVazia())
            {
                Console.WriteLine("Nenhuma matrícula cadastrada.");
                return;
            }

            No atual = cabeca;
            while (atual != null)
            {
                Matricula m = atual.Matricula;
                Console.WriteLine($"Matrícula Aluno: {m.MatriculaAluno} | Código Disciplina: {m.CodigoDisciplina} | Nota 1: {m.Nota1} | Nota 2: {m.Nota2}");
                atual = atual.Proximo;
            }
        }

        public bool EstaVazia()
        {
            return cabeca == null;
        }

        public int GetQuantidade()
        {
            return quantidade;
        }
    }
}
