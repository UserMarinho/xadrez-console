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
        public bool Xeque { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> PecasCapturadas;
        public Peca VulneravelEnPassant { get; private set; }
        public PartidaDeXadrez()
        {
            this.Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Xeque = false;
            Pecas = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
            VulneravelEnPassant = null;
            ColocarPecas();
        }
        private Peca ExecutarMovimento(Posicao origem, Posicao destino)
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
                // #jogada especial roque pequeno
                if (peca is Rei && destino.Coluna == origem.Coluna + 2)
                {
                    Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                    Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                    Peca t = this.Tabuleiro.RetirarPeca(origemTorre);
                    t.IncrementarQteMovimentos();
                    this.Tabuleiro.ColocarPeca(t, destinoTorre);
                }
                // #jogada especial roque grande
                if (peca is Rei && destino.Coluna == origem.Coluna - 2)
                {
                    Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                    Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                    Peca t = this.Tabuleiro.RetirarPeca(origemTorre);
                    t.IncrementarQteMovimentos();
                    this.Tabuleiro.ColocarPeca(t, destinoTorre);
                }
                // En Passant 
                if (peca is Peao)
                {
                    if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                    {
                        Posicao posP;
                        if (peca.Cor == Cor.Branco)
                        {
                            posP = new Posicao(destino.Linha + 1, destino.Coluna);
                        }
                        else
                        {
                            posP = new Posicao(destino.Linha - 1, destino.Coluna);
                        }
                        pecaCapturada = Tabuleiro.RetirarPeca(posP);
                        PecasCapturadas.Add(pecaCapturada);
                    }
                }

                return pecaCapturada;
            }
            else
            {

                throw new ApplicationException("Posição de destino inválida!");
            }
        }
        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);
            if (ReiEmXeque(JogadorAtual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new ApplicationException("Você não pode se colocar em xeque!");
            }
            if (ReiEmXeque(CorAdversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (XequeMate(CorAdversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudarJogador();
            }
            Peca p = Tabuleiro.Peca(destino);
            // En Passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }
        }
        public void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca pecaDoJogador = this.Tabuleiro.RetirarPeca(destino);
            pecaDoJogador.DecrementarQteMovimentos();
            this.Tabuleiro.ColocarPeca(pecaDoJogador, origem);
            if (pecaCapturada != null)
            {
                this.Tabuleiro.ColocarPeca(pecaCapturada, destino);
                PecasCapturadas.Remove(pecaCapturada);
            }
            // #jogada especial roque pequeno
            if (pecaDoJogador is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca t = this.Tabuleiro.RetirarPeca(destinoTorre);
                t.DecrementarQteMovimentos();
                this.Tabuleiro.ColocarPeca(t, origemTorre);
            }
            // #jogada especial roque grande
            if (pecaDoJogador is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca t = this.Tabuleiro.RetirarPeca(destinoTorre);
                t.DecrementarQteMovimentos();
                this.Tabuleiro.ColocarPeca(t, origemTorre);
            }
            // En passant 
            if (pecaDoJogador is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tabuleiro.RetirarPeca(destino);
                    Posicao posP;
                    if (pecaDoJogador.Cor == Cor.Branco)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    Tabuleiro.ColocarPeca(peao, posP);
                }
            }
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
            ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branco));
            ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branco, this));
            ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branco, this));

            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preto));
            ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preto, this));
            ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preto, this));
        }
        private Cor CorAdversaria(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branco;
            }
        }
        private Peca ObterRei(Cor cor)
        {
            foreach (Peca peca in PecasEmJogo(cor))
            {
                if (peca is Rei)
                {
                    return peca;
                }
            }
            return null;
        }
        public bool ReiEmXeque(Cor cor)
        {
            Peca rei = ObterRei(cor);
            if (rei == null)
            {
                throw new ApplicationException($"Não há o rei {cor} no tabuleiro!");
            }
            foreach (Peca peca in PecasEmJogo(CorAdversaria(cor))) 
            {
                bool[,] movimentosPeca = peca.MovimentosPossiveis();
                if (movimentosPeca[rei.Posicao.Linha, rei.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public bool XequeMate(Cor cor)
        {
            if (!ReiEmXeque(cor))
            {
                return false;
            }
            foreach (Peca peca in PecasEmJogo(cor))
            {
                bool[,] movimentosPeca = peca.MovimentosPossiveis();
                for (int i = 0; i < Tabuleiro.Linhas; i++)
                {
                    for (int j = 0; j < Tabuleiro.Colunas; j++)
                    {
                        if (movimentosPeca[i, j])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = ReiEmXeque(cor);
                            DesfazerMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
