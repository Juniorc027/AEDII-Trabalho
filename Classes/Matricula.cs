using System;
using System.Collections.Generic;
using System.Text;

namespace AEDII_Trabalho.Classes
{
    public class Matricula
    {
        private int codigodisciplina;
        private int matriculaAluno;
        private double nota1;
        private double nota2;
        public int CodigoDisciplina
        {
            get => codigodisciplina;
            set => codigodisciplina = value;
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

