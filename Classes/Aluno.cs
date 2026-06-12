using System;

namespace AEDII_Trabalho.Classes
{
    public class Aluno
    {
        // Campos privados com encapsulamento 
        private int matricula;
        private string nome;
        private int idade;

        public int Matricula
        {
            get => matricula;
            set => matricula = value;
        }

        public string Nome
        {
            get => nome;
            set => nome = value;
        }

        public int Idade
        {
            get => idade;
            set => idade = value;
        }
    }
}
