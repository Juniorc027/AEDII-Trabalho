using System;
using System.IO;
using AEDII_Trabalho.Classes;

namespace AEDII_Trabalho.Listas
{
    public class ListaDisciplinas
    {
        private No cabeca; // Ponteiro para o primeiro nó da lista
        private int quantidade; // Quantidade de disciplinas na lista

        private class No
        {
            // Campos privados (encapsulamento real)
            private Disciplina disciplina;
            private No proximo;

            // Propriedades públicas controladas
            public Disciplina Disciplina
            {
                get { return disciplina; }
                set { disciplina = value; }
            }

            public No Proximo
            {
                get { return proximo; }
                set { proximo = value; }
            }

            // Construtor
            public No(Disciplina disciplina)
            {
                this.disciplina = disciplina;
                this.proximo = null;
            }
        }

        public ListaDisciplinas()
        {
            cabeca = null;
            quantidade = 0;
        }

        // Insere uma nova disciplina no final da lista
        public void Inserir(Disciplina novaDisciplina)
        {
            No novoNo = new No(novaDisciplina);

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

        // Busca disciplina pelo código; retorna null se não encontrada
        public Disciplina BuscarPorCodigo(int codigo)
        {
            No atual = cabeca;
            while (atual != null)
            {
                if (atual.Disciplina.CodigoDisciplina == codigo)
                {
                    return atual.Disciplina;
                }
                atual = atual.Proximo;
            }
            return null;
        }

        // Busca disciplina pelo nome, ignorando maiúsculas e minúsculas
        public Disciplina BuscarPorNome(string nome)
        {
            No atual = cabeca;
            while (atual != null)
            {
                if (atual.Disciplina.NomeDisciplina.Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    return atual.Disciplina;
                }
                atual = atual.Proximo;
            }
            return null;
        }

        // Percorre a lista e retorna o maior código de disciplina existente
        public int GetMaiorCodigo()
        {
            int maior = 0;
            No atual = cabeca;
            while (atual != null)
            {
                if (atual.Disciplina.CodigoDisciplina > maior)
                    maior = atual.Disciplina.CodigoDisciplina;
                atual = atual.Proximo;
            }
            return maior;
        }

        // Percorre a lista e grava cada disciplina no arquivo (código;nome;notaMínima)
        public void SalvarParaArquivo(StreamWriter sw)
        {
            No atual = cabeca;
            while (atual != null)
            {
                Disciplina d = atual.Disciplina;
                sw.WriteLine(d.CodigoDisciplina + ";" + d.NomeDisciplina + ";" +
                             d.NotaMinimaDisciplina.ToString(System.Globalization.CultureInfo.InvariantCulture));
                atual = atual.Proximo;
            }
        }

        public void ExibirTodas()
        {
            if (EstaVazia())
            {
                Console.WriteLine("Nenhuma disciplina cadastrada.");
                return;
            }

            No atual = cabeca;
            while (atual != null)
            {
                Disciplina d = atual.Disciplina;
                Console.WriteLine($"Código: {d.CodigoDisciplina} | Nome: {d.NomeDisciplina} | Nota Mínima: {d.NotaMinimaDisciplina}");
                atual = atual.Proximo; // Avança para o próximo nó
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
