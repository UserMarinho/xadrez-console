using tabuleiro;
namespace xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez Partida;
        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            Partida = partida;
        }
        private bool PodeMover(Posicao pos)
        {
            Peca peca = this.Tabuleiro.Peca(pos);
            return peca == null || peca.Cor != this.Cor;
        }
        private bool TesteTorreParaRoque(Posicao posicao)
        {
            Peca peca = this.Tabuleiro.Peca(posicao);
            return peca != null && peca is Torre && peca.Cor == this.Cor && peca.QteMovimentos == 0;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] posicoesPossiveis = new bool[this.Tabuleiro.Linhas, this.Tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);
            // acima
            pos.DefinirValores(this.Posicao.Linha - 1, this.Posicao.Coluna);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            // nordeste
            pos.DefinirValores(this.Posicao.Linha - 1, this.Posicao.Coluna + 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            // direita
            pos.DefinirValores(this.Posicao.Linha, this.Posicao.Coluna + 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            // sudeste
            pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna + 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            // abaixo
            pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            // sudoeste
            pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna - 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            // esquerda
            pos.DefinirValores(this.Posicao.Linha, this.Posicao.Coluna - 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            // noroeste
            pos.DefinirValores(this.Posicao.Linha - 1, this.Posicao.Coluna - 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            // roque 
            if (QteMovimentos == 0 && !Partida.Xeque)
            {
                //roque pequeno
                Posicao posT1 = new Posicao(this.Posicao.Linha, this.Posicao.Coluna + 3);
                if (TesteTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(this.Posicao.Linha, this.Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(this.Posicao.Linha, this.Posicao.Coluna + 2);
                    if (this.Tabuleiro.Peca(p1) == null && this.Tabuleiro.Peca(p2) == null)
                    {
                        posicoesPossiveis[this.Posicao.Linha, this.Posicao.Coluna + 2] = true; 
                    }
                }
                //roque grande
                Posicao posT2 = new Posicao(this.Posicao.Linha, this.Posicao.Coluna - 4);
                if (TesteTorreParaRoque(posT2))
                {
                    Posicao p1 = new Posicao(this.Posicao.Linha, this.Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(this.Posicao.Linha, this.Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(this.Posicao.Linha, this.Posicao.Coluna - 3);
                    if (this.Tabuleiro.Peca(p1) == null && this.Tabuleiro.Peca(p2) == null && this.Tabuleiro.Peca(p3) == null)
                    {
                        posicoesPossiveis[this.Posicao.Linha, this.Posicao.Coluna -2] = true;
                    }
                }

            }
            return posicoesPossiveis;
        }       
        public override string ToString()
        {
            return "R";
        }
    }
}
