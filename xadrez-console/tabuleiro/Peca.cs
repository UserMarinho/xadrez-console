﻿namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            this.Posicao = null;
            this.Tabuleiro = tabuleiro;
            this.Cor = cor;
            QteMovimentos = 0;
        }
        public void IncrementarQteMovimentos()
        {
            QteMovimentos++;
        }
        public void DecrementarQteMovimentos()
        {
            QteMovimentos--;
        }
        public abstract bool[,] MovimentosPossiveis();
    }
}
