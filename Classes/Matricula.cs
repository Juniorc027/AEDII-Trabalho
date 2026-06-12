using System;

namespace AEDII_Trabalho.Classes
{
    // Representa o vínculo entre um aluno e uma disciplina, armazenando suas notas
    public class Matricula
    {
        // Encapsulamento dos campos.
        private int codigoDisciplina;
        private int matriculaAluno;
        private double nota1;
        private double nota2;

        public int CodigoDisciplina
        {
            get => codigoDisciplina;
            set => codigoDisciplina = value;
        }
        public int MatriculaAluno
        {
            get => matriculaAluno;
            set => matriculaAluno = value;
        }

        public double Nota1
        {
            get => nota1;
            set => nota1 = value;
        }

        public double Nota2
        {
            get => nota2;
            set => nota2 = value;
        }
    }
}
