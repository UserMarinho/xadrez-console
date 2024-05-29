using tabuleiro;
using xadrez;
namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();
                while (!partida.Terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tabuleiro);
                    Console.WriteLine("\n");
                    try
                    {
                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                        bool[,] posicoesPossiveis = partida.Tabuleiro.Peca(origem).MovimentosPossíveis();
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);
                        Console.WriteLine("\n");
                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                        partida.ExecutaMovimento(origem, destino);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(1000);
                    }

                }
            }
            catch (TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);   
            }
        }
    }
}
