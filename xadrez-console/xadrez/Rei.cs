using tabuleiro;
namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {
        }
        private bool PodeMover(Posicao pos)
        {
            Peca peca = this.Tabuleiro.Peca(pos);
            return peca == null || peca.Cor != this.Cor;
        }
        public override bool[,] MovimentosPossíveis()
        {
            bool[,] mat = new bool[this.Tabuleiro.Linhas, this.Tabuleiro.Colunas];
            Posicao pos = new Posicao(0, 0);
            // acima
            pos.DefinirValores(this.Posicao.Linha - 1, this.Posicao.Coluna);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // nordeste
            pos.DefinirValores(this.Posicao.Linha - 1, this.Posicao.Coluna + 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // direita
            pos.DefinirValores(this.Posicao.Linha, this.Posicao.Coluna + 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // sudeste
            pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna + 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // abaixo
            pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // sudoeste
            pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna - 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // esquerda
            pos.DefinirValores(this.Posicao.Linha, this.Posicao.Coluna - 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // noroeste
            pos.DefinirValores(this.Posicao.Linha - 1, this.Posicao.Coluna - 1);
            if (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            return mat;
        }       
        public override string ToString()
        {
            return "R";
        }
    }
}
