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
        private HashSet<Peca> Pecas;
        private HashSet<Peca> PecasCapturadas;
        public PartidaDeXadrez()
        {
            this.Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
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
                if (pecaCapturada != null)
                {
                    PecasCapturadas.Add(pecaCapturada);
                }
            }
            else
            {

                throw new ApplicationException("Posição de destino inválida!");
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
            if (JogadorAtual == Cor.Branco)
            {
                JogadorAtual = Cor.Preto;
            }
            else
            {
                JogadorAtual = Cor.Branco;
            }
        }
        public void ValidarPeca(Posicao posicao)
        {
            Peca peca = this.Tabuleiro.Peca(posicao);
            if (peca == null)
            {
                throw new ApplicationException("Não há peça!");
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
        public HashSet<Peca> ObterPecasCapturadas(Cor cor)
        {
            HashSet<Peca> pecas = new HashSet<Peca>();
            foreach (Peca x in PecasCapturadas)
            {
                if (x.Cor == cor)
                {
                    pecas.Add(x);
                }
            }
            return pecas;
        }
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> pecas = new HashSet<Peca>();
            foreach (Peca x in this.Pecas)
            {
                if (x.Cor == cor)
                {
                    pecas.Add(x);
                }
            }
            pecas.ExceptWith(ObterPecasCapturadas(cor));
            return pecas;
        }
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            this.Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ParaPosicao());
            Pecas.Add(peca);
        }
        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Peao(this.Tabuleiro, Cor.Branco));
            ColocarNovaPeca('d', 2, new Peao(this.Tabuleiro, Cor.Preto));
            ColocarNovaPeca('e', 1, new Peao(this.Tabuleiro, Cor.Preto));
            ColocarNovaPeca('f', 2, new Rei(this.Tabuleiro, Cor.Branco));
        }
    }
}
