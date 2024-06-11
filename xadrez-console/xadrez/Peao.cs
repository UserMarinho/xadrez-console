using tabuleiro;
namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }
        private bool PodeMover(Posicao pos, bool capturar)
        {
            Peca peca = this.Tabuleiro.Peca(pos);
            if (capturar)
            {
                return peca != null && peca.Cor != this.Cor;
            }
            else
            {
                return peca == null;
            }
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] posicoesPossiveis = new bool[this.Tabuleiro.Linhas, this.Tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);
            if (this.Cor == Cor.Branco)
            {
                // acima
                pos.DefinirValores(this.Posicao.Linha - 1, this.Posicao.Coluna);
                if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos, false))
                {
                    posicoesPossiveis[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(this.Posicao.Linha - 2, this.Posicao.Coluna);
                if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos, false) && QteMovimentos == 0)
                {
                    posicoesPossiveis[pos.Linha, pos.Coluna] = true;
                }
                // nordeste
                pos.DefinirValores(this.Posicao.Linha - 1, this.Posicao.Coluna + 1);
                if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos, true))
                {
                    posicoesPossiveis[pos.Linha, pos.Coluna] = true;
                }
                // noroeste
                pos.DefinirValores(this.Posicao.Linha - 1, this.Posicao.Coluna - 1);
                if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos, true))
                {
                    posicoesPossiveis[pos.Linha, pos.Coluna] = true;
                }
                return posicoesPossiveis;
            }
            else
            {
                // abaixo
                pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna);
                if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos, false))
                {
                    posicoesPossiveis[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(this.Posicao.Linha + 2, this.Posicao.Coluna);
                if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos, false) && QteMovimentos == 0)
                {
                    posicoesPossiveis[pos.Linha, pos.Coluna] = true;
                }
                // sudeste
                pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna + 1);
                if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos, true))
                {
                    posicoesPossiveis[pos.Linha, pos.Coluna] = true;
                }
                // sudoeste
                pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna - 1);
                if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos, true))
                {
                    posicoesPossiveis[pos.Linha, pos.Coluna] = true;
                }
                return posicoesPossiveis;
            }
        }
        public override string ToString()
        {
            return "P";
        }
    }
}
