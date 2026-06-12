using System;

namespace AEDII_Trabalho.Classes
{
    // Representa uma disciplina com código único, nome e nota mínima para aprovação
    public class Disciplina
    {
        // Encapsulamento dos campos privados.
        private int codigoDisciplina;
        private string nomeDisciplina;
        private double notaMinimaDisciplina;

        public int CodigoDisciplina
        {
            get => codigoDisciplina;
            set => codigoDisciplina = value;
        }

        public string NomeDisciplina
        {
            get => nomeDisciplina;
            set => nomeDisciplina = value;
        }

        public double NotaMinimaDisciplina
        {
            get => notaMinimaDisciplina;
            set => notaMinimaDisciplina = value;
        }
    }
}