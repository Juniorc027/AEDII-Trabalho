using AEDII_Trabalho.LerESalvar;
using AEDII_Trabalho.Menus;

namespace AEDII_Trabalho
{
    public class Program
    {
        static void Main(string[] args)
        {
            Arquivos.CarregarDados();
            MenuPrincipal.Principal();
        }
    }
}
