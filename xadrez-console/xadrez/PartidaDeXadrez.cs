using tabuleiro;
using xadrez_console;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        public PartidaDeXadrez()
        {
            this.Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
        }
        private void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            //ValidarPeca(origem);
            Peca peca = this.Tabuleiro.Peca(origem);
            bool[,] movimentosPossiveis = peca.MovimentosPossiveis();
            if (movimentosPossiveis[destino.Linha, destino.Coluna])
            {
                this.Tabuleiro.RetirarPeca(origem);
                peca.IncrementarQteMovimentos();
                Peca pecaCapturada = this.Tabuleiro.RetirarPeca(destino);
                this.Tabuleiro.ColocarPeca(peca, destino);
            }
            else
            {

                throw new ApplicationException("Destino inválida!");
            }
        }
        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            Turno++;
            MudarJogador();
        }
        private void MudarJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }
        public void ValidarPeca(Posicao posicao)
        {
            Peca peca = this.Tabuleiro.Peca(posicao);
            if (peca == null)
            {
                throw new ApplicationException("Peça Nula!");
            }
            else if (peca.Cor != JogadorAtual)
            {
                throw new ApplicationException("Peça errada!");
            }
            else if (!ExisteMovimentosPossiveis(peca))
            {
                throw new ApplicationException("Não há movimentos possíveis!");
            }
        }
        private bool ExisteMovimentosPossiveis(Peca peca)
        {
            bool[,] posicoesPossiveis = peca.MovimentosPossiveis();
            foreach (bool possivel in posicoesPossiveis)
            {
                if (possivel)
                {
                    return true;
                }
            }
            return false;
        }
        private void ColocarPecas()
        {
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('c', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('d', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Branca), new PosicaoXadrez('d', 5).ToPosicao());
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Preta), new PosicaoXadrez('c', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Preta), new PosicaoXadrez('b', 1).ToPosicao());
        }
    }
}
