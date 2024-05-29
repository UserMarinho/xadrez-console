using tabuleiro;
namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
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
            while (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                Peca peca = this.Tabuleiro.Peca(pos);
                if (peca != null)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna);

            }
            // direita
            pos.DefinirValores(this.Posicao.Linha, this.Posicao.Coluna + 1);
            while (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                Peca peca = this.Tabuleiro.Peca(pos);
                if (peca != null)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha, pos.Coluna + 1);

            }
            // abaixo
            pos.DefinirValores(this.Posicao.Linha + 1, this.Posicao.Coluna);
            while (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                Peca peca = this.Tabuleiro.Peca(pos);
                if (peca != null)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna);

            }
            // esquerda
            pos.DefinirValores(this.Posicao.Linha, this.Posicao.Coluna - 1);
            while (this.Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                Peca peca = this.Tabuleiro.Peca(pos);
                if (peca != null)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha, pos.Coluna - 1);

            }
            return mat;
        }
        public override string ToString()
        {
            return "T";
        }
    }
}
