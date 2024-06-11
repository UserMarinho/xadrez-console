using tabuleiro;
namespace xadrez
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }
        private bool PodeMover(Posicao pos)
        {
            Peca peca = this.Tabuleiro.Peca(pos);
            return peca == null || peca.Cor != this.Cor;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] posicoesPossiveis = new bool[this.Tabuleiro.Linhas, this.Tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);
            // nordeste
            pos.DefinirValores(this.Posicao.Linha - 1, this.Posicao.Coluna + 1);
            while (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
                Peca peca = this.Tabuleiro.Peca(pos);
                if (peca != null)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna + 1);
            }
            // noroeste
            pos.DefinirValores(this.Posicao.Linha - 1, this.Posicao.Coluna - 1);
            while (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
                Peca peca = this.Tabuleiro.Peca(pos);
                if (peca != null)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
            }
            // sudeste
            pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna + 1);
            while (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
                Peca peca = this.Tabuleiro.Peca(pos);
                if (peca != null)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);
            }
            // sudoeste
            pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna - 1);
            while (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
                Peca peca = this.Tabuleiro.Peca(pos);
                if (peca != null)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);
            }
            return posicoesPossiveis;
        }
        public override string ToString()
        {
            return "B";
        }
    }
}
