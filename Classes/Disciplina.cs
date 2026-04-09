using System;
using System.Collections.Generic;
using System.Text;

namespace AEDII_Trabalho.Classes
{
    public class Disciplina
    {
        private int codigodisciplina;
        private string Nome;
        private double NotaMinima;

        public int CodigoDisciplina
        {
            get => codigodisciplina;
            set => codigodisciplina = value;
        }

        public string NomeDisciplina
        {
            get => Nome;
            set => Nome = value;
        }

        public double NotaMinimaDisciplina
        {
            get => NotaMinima;
            set => NotaMinima = value;
        }
    }
}