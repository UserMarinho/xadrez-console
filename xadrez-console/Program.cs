using tabuleiro;
using xadrez;
namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);
            tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(4, 5));
            tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 1));
            tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(6, 0));
            Tela.ImprimirTabuleiro(tab);
        }
    }
}
