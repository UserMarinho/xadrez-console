using tabuleiro;
namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
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
            // acima
            pos.DefinirValores(this.Posicao.Linha - 2, this.Posicao.Coluna + 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(this.Posicao.Linha - 2, this.Posicao.Coluna - 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            // direita
            pos.DefinirValores(this.Posicao.Linha - 1, this.Posicao.Coluna + 2);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna + 2);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            // abaixo
            pos.DefinirValores(this.Posicao.Linha + 2, this.Posicao.Coluna + 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(this.Posicao.Linha + 2, this.Posicao.Coluna - 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            // esquerda
            pos.DefinirValores(this.Posicao.Linha - 1, this.Posicao.Coluna - 2);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna - 2);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                posicoesPossiveis[pos.Linha, pos.Coluna] = true;
            }

            return posicoesPossiveis;
        }
        public override string ToString()
        {
            return "C";
        }
    }
}
