using tabuleiro;
using xadrez;
namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write($"{tabuleiro.Linhas - i} ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    ImprimirPeca(tabuleiro.Peca(i, j));                    
                }
                Console.WriteLine();
            }
            char[] alfabeto = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
            Console.Write("  ");
            for (int i = 0; i < tabuleiro.Colunas; i++)
            {
                if (i < alfabeto.Length)
                {
                    Console.Write($"{alfabeto[i]} ");
                }
                else
                {
                    break;
                }
            }
            
        }
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor corOriginal = Console.BackgroundColor;
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write($"{tabuleiro.Linhas - i} ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    bool posicaoPossivel = posicoesPossiveis[i, j];
                    if (posicaoPossivel)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    ImprimirPeca(tabuleiro.Peca(i, j));
                    Console.BackgroundColor = corOriginal;
                }
                Console.WriteLine();
            }
            char[] alfabeto = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
            Console.Write("  ");
            for (int i = 0; i < tabuleiro.Colunas; i++)
            {
                if (i < alfabeto.Length)
                {
                    Console.Write($"{alfabeto[i]} ");
                }
                else
                {
                    break;
                }
            }

        }
        public static PosicaoXadrez LerPosicaoXadrez()
        {
            try
            {
                string s = Console.ReadLine();
                char coluna = s[0];
                int linha = int.Parse(s[1] + "");
                return new PosicaoXadrez(coluna, linha);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Posição inválida!");
            }
        }
        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branco)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor corOriginal = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = corOriginal;
                }
                Console.Write(" ");
            }
        }
    }
}
