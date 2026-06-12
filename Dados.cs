using AEDII_Trabalho.Listas;

namespace AEDII_Trabalho
{
    // Classe central que armazena as três listas encadeadas do sistema
    public static class Dados
    {
        public static ListaAlunos listaAlunos = new ListaAlunos();
        public static ListaDisciplinas listaDisciplinas = new ListaDisciplinas();
        public static ListaMatricula listaMatriculas = new ListaMatricula();
    }
}
