using System;
using System.IO;
using AEDII_Trabalho.Classes;

namespace AEDII_Trabalho.Listas
{
    public class ListaAlunos
    {
        private Node cabeca; // Ponteiro para o primeiro nó da lista
        private int quantidade; // Quantidade de alunos na lista

        private class Node
        {
            // Campos privados
            private Aluno aluno;
            private Node proximo;

            // Propriedades públicas controladas
            public Aluno Aluno
            {
                get { return aluno; }
                set { aluno = value; }
            }

            public Node Proximo
            {
                get { return proximo; }
                set { proximo = value; }
            }

            // Construtor
            public Node(Aluno aluno)
            {
                this.aluno = aluno;
                this.proximo = null;
            }
        }

        public ListaAlunos()
        {
            cabeca = null;
            quantidade = 0;
        }

        // Insere um novo aluno no final da lista
        public void Inserir(Aluno novoAluno)
        {
            Node novoNode = new Node(novoAluno);
            if (cabeca == null)
            {
                cabeca = novoNode;
            }
            else
            {
                Node atual = cabeca;
                while (atual.Proximo != null) // Percorre até o último nó
                {
                    atual = atual.Proximo;
                }
                atual.Proximo = novoNode;
            }
            quantidade++;
        }

        // Busca o aluno pela matrícula; retorna null se não encontrado
        public Aluno BuscarPorMatricula(int matricula)
        {
            Node atual = cabeca;
            while (atual != null)
            {
                if (atual.Aluno.Matricula == matricula)
                {
                    return atual.Aluno;
                }
                atual = atual.Proximo;
            }
            return null;
        }

        // Busca o aluno pelo nome, ignorando maiúsculas e minúsculas
        public Aluno BuscarPeloNome(string nome)
        {
            Node atual = cabeca;
            while (atual != null)
            {
                if (atual.Aluno.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    return atual.Aluno;
                }
                atual = atual.Proximo;
            }
            return null;
        }

        // Percorre a lista e retorna o maior número de matrícula existente
        public int GetMaiorMatricula()
        {
            int maior = 0;
            Node atual = cabeca;
            while (atual != null)
            {
                if (atual.Aluno.Matricula > maior)
                    maior = atual.Aluno.Matricula;
                atual = atual.Proximo;
            }
            return maior;
        }

        // Percorre a lista e grava cada aluno no arquivo (matrícula;nome;idade)
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

        public void ExibirTodos()
        {
            if (EstaVazia())
            {
                Console.WriteLine("Nenhum aluno cadastrado.");
                return;
            }
            Node atual = cabeca;
            while (atual != null)
            {
                Aluno a = atual.Aluno;
                Console.WriteLine($"Matrícula: {a.Matricula} | Nome: {a.Nome} | Idade: {a.Idade}");
                atual = atual.Proximo; // Avança para o próximo nó
            }
        }

        public int GetQuantidade()
        {
            return quantidade;
        }

        public bool EstaVazia()
        {
            return cabeca == null;
        }
    }
}
