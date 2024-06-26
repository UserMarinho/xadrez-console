﻿using tabuleiro;
using xadrez;
namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write($"{tabuleiro.Linhas - i} ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    ImprimirPeca(tabuleiro.Peca(i, j));                    
                }
                Console.WriteLine();
            }
            char[] alfabeto = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
            Console.Write("  ");
            for (int i = 0; i < tabuleiro.Colunas; i++)
            {
                if (i < alfabeto.Length)
                {
                    Console.Write($"{alfabeto[i]} ");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine();
        }
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor corOriginal = Console.BackgroundColor;
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write($"{tabuleiro.Linhas - i} ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    bool posicaoPossivel = posicoesPossiveis[i, j];
                    if (posicaoPossivel)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    ImprimirPeca(tabuleiro.Peca(i, j));
                    Console.BackgroundColor = corOriginal;
                }
                Console.WriteLine();
            }
            char[] alfabeto = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
            Console.Write("  ");
            for (int i = 0; i < tabuleiro.Colunas; i++)
            {
                if (i < alfabeto.Length)
                {
                    Console.Write($"{alfabeto[i]} ");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine();
        }
        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiro(partida.Tabuleiro);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine($"Turno: {partida.Turno}");
            if (!partida.Terminada)
            {
                Console.WriteLine($"Jogador Atual: {partida.JogadorAtual}");
                if (partida.Xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"XEQUEMATE!\nVencedor: {partida.JogadorAtual}");
            }
        }
        public static void ImprimirPartida(PartidaDeXadrez partida, bool[,] posicoesPossiveis)
        {
            ImprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine($"Turno: {partida.Turno}");
            Console.WriteLine($"Jogador Atual: {partida.JogadorAtual}");
            if (partida.Xeque)
            {
                Console.WriteLine("XEQUE!");
            }
            Console.WriteLine();
        }
        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write($"Brancas: ");
            ImprimirConjunto(partida.ObterPecasCapturadas(Cor.Branco));
            Console.WriteLine();
            Console.Write($"Pretas: ");
            ConsoleColor corOriginal = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.ObterPecasCapturadas(Cor.Preto));
            Console.ForegroundColor = corOriginal;
            Console.WriteLine();
        }
        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[ ");
            foreach (Peca peca in conjunto)
            {
                Console.Write($"{peca} ");
            }
            Console.Write("]");
        }
        public static PosicaoXadrez LerPosicaoXadrez()
        {
            try
            {
                string s = Console.ReadLine();
                char coluna = s[0];
                int linha = int.Parse(s[1] + "");
                return new PosicaoXadrez(coluna, linha);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Posição inválida!");
            }
        }
        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branco)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor corOriginal = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = corOriginal;
                }
                Console.Write(" ");
            }
        }
    }
}
