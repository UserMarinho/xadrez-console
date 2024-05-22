using tabuleiro;
using xadrez_console;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        private int Turno;
        private Cor JogadorAtual;
        public bool Terminada { get; private set; }
        public PartidaDeXadrez()
        {
            this.Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = this.Tabuleiro.RetirarPeca(origem);
            if (peca != null)
            {
                peca.IncrementarQteMovimentos();
                Peca pecaCapturada = this.Tabuleiro.RetirarPeca(destino);
                this.Tabuleiro.ColocarPeca(peca, destino);
            }
            else
            {
                throw new TabuleiroException("Peça nula!");
            }
        }
        private void ColocarPecas()
        {
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('c', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('d', 1).ToPosicao());
        }
    }
}
