using tabuleiro;
namespace xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez Partida;
        public Peao(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            Partida = partida;
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
                // En passant
                if (Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if(Tabuleiro.PosicaoValida(esquerda) && PodeMover(esquerda, true) && Tabuleiro.Peca(esquerda) == Partida.VulneravelEnPassant)
                    {
                        posicoesPossiveis[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && PodeMover(direita, true) && Tabuleiro.Peca(direita) == Partida.VulneravelEnPassant)
                    {
                        posicoesPossiveis[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
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
                // En passant
                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && PodeMover(esquerda, true) && Tabuleiro.Peca(esquerda) == Partida.VulneravelEnPassant)
                    {
                        posicoesPossiveis[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && PodeMover(direita, true) && Tabuleiro.Peca(direita) == Partida.VulneravelEnPassant)
                    {
                        posicoesPossiveis[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }
            return posicoesPossiveis;
        }
        public override string ToString()
        {
            return "P";
        }
    }
}
