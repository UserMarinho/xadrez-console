using tabuleiro;
using xadrez;
namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            PartidaDeXadrez partida = new PartidaDeXadrez();
            while (!partida.Terminada)
            {
                Console.Clear();
                Tela.ImprimirTabuleiro(partida.Tabuleiro);
                Console.WriteLine("\n");
                Console.WriteLine($"Turno: {partida.Turno}");
                Console.WriteLine($"Jogador Atual: {partida.JogadorAtual}");
                Console.WriteLine();
                try
                {
                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ParaPosicao();
                    partida.ValidarPeca(origem);
                    Peca peca = partida.Tabuleiro.Peca(origem);
                    bool[,] posicoesPossiveis = peca.MovimentosPossiveis();
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);
                    Console.WriteLine("\n");
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ParaPosicao();
                    partida.RealizarJogada(origem, destino);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Thread.Sleep(1000);
                }

            }
        }
    }
}
