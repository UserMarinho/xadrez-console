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
                Tela.ImprimirPartida(partida);
                try
                {
                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ParaPosicao();
                    partida.ValidarPeca(origem);
                    Peca peca = partida.Tabuleiro.Peca(origem);
                    bool[,] posicoesPossiveis = peca.MovimentosPossiveis();
                    Console.Clear();
                    Tela.ImprimirPartida(partida, posicoesPossiveis);
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
