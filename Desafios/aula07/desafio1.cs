using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static Random sorteio = new Random();

    static void Main(string[] args)
    {
        int escolha;

        do
        {
            Console.WriteLine("Menu: 1) nova simulacao ou 2) sair");
            Console.Write("> ");
            escolha = int.Parse(Console.ReadLine()!);

            if (escolha == 1)
            {
                FazerSimulacao();
            }
            else if (escolha == 2)
            {
                Console.WriteLine("Tchau!");
            }
            else
            {
                Console.WriteLine("Opcao invalida.");
            }

            Console.WriteLine();

        } while (escolha != 2);
    }

    static void FazerSimulacao()
    {
        Console.Write("Digite a quantidade de amostras: ");
        int totalAmostras = int.Parse(Console.ReadLine()!);

        Console.Write("Digite a quantidade de elementos para cada amostra: ");
        int totalElementos = int.Parse(Console.ReadLine()!);

        double alturaTotalBST = 0;
        double alturaTotalAVL = 0;

        double tempoTotalBST = 0;
        double tempoTotalAVL = 0;

        Stopwatch relogio = new Stopwatch();

        for (int rodada = 0; rodada < totalAmostras; rodada++)
        {
            List<int> dados = CriarValoresSemRepetir(totalElementos);

            BST arvoreBST = new BST();

            relogio.Restart();

            foreach (int numero in dados)
            {
                arvoreBST.Inserir(numero);
            }

            relogio.Stop();
            tempoTotalBST += relogio.Elapsed.TotalMilliseconds;

            AVL arvoreAVL = new AVL();

            relogio.Restart();

            foreach (int numero in dados)
            {
                arvoreAVL.Inserir(numero);
            }

            relogio.Stop();
            tempoTotalAVL += relogio.Elapsed.TotalMilliseconds;

            alturaTotalBST += arvoreBST.CalcularAltura();
            alturaTotalAVL += arvoreAVL.CalcularAltura();
        }

        double mediaAlturaBST = alturaTotalBST / totalAmostras;
        double mediaAlturaAVL = alturaTotalAVL / totalAmostras;
        double mediaAlturaFinal = (mediaAlturaBST + mediaAlturaAVL) / 2.0;

        double mediaTempoBST = tempoTotalBST / totalAmostras;
        double mediaTempoAVL = tempoTotalAVL / totalAmostras;
        double mediaTempoFinal = (mediaTempoBST + mediaTempoAVL) / 2.0;

        Console.WriteLine();
        Console.WriteLine($"Experimento com A = {totalAmostras} e N = {totalElementos}");
        Console.WriteLine("----------------------------------");
        Console.WriteLine($"Altura media geral:              {mediaAlturaFinal:F2}");
        Console.WriteLine($"Tempo medio geral de construcao: {mediaTempoFinal:F4} ms");
        Console.WriteLine("---");
        Console.WriteLine($"Altura media BST comum:          {mediaAlturaBST:F2}");
        Console.WriteLine($"Tempo medio de construcao BST:   {mediaTempoBST:F4} ms");
        Console.WriteLine("---");
        Console.WriteLine($"Altura media AVL:                {mediaAlturaAVL:F2}");
        Console.WriteLine($"Tempo medio de construcao AVL:   {mediaTempoAVL:F4} ms");
        Console.WriteLine("----------------------------------");
    }

    static List<int> CriarValoresSemRepetir(int quantidade)
    {
        HashSet<int> conjunto = new HashSet<int>();

        while (conjunto.Count < quantidade)
        {
            conjunto.Add(sorteio.Next(1, quantidade * 10 + 1));
        }

        return new List<int>(conjunto);
    }
}

class NoAVL
{
    public int Dado;
    public int Altura;
    public NoAVL? FilhoEsquerdo;
    public NoAVL? FilhoDireito;

    public NoAVL(int dado)
    {
        Dado = dado;
        Altura = 1;
    }
}

class AVL
{
    public NoAVL? Inicio;

    public void Inserir(int dado)
    {
        Inicio = Inserir(Inicio, dado);
    }

    private NoAVL Inserir(NoAVL? atual, int dado)
    {
        if (atual == null)
            return new NoAVL(dado);

        if (dado < atual.Dado)
            atual.FilhoEsquerdo = Inserir(atual.FilhoEsquerdo, dado);
        else if (dado > atual.Dado)
            atual.FilhoDireito = Inserir(atual.FilhoDireito, dado);
        else
            return atual;

        AtualizarAltura(atual);

        int fator = FatorBalanceamento(atual);

        if (fator > 1 && dado < atual.FilhoEsquerdo!.Dado)
            return RotacaoDireita(atual);

        if (fator < -1 && dado > atual.FilhoDireito!.Dado)
            return RotacaoEsquerda(atual);

        if (fator > 1 && dado > atual.FilhoEsquerdo!.Dado)
        {
            atual.FilhoEsquerdo = RotacaoEsquerda(atual.FilhoEsquerdo!);
            return RotacaoDireita(atual);
        }

        if (fator < -1 && dado < atual.FilhoDireito!.Dado)
        {
            atual.FilhoDireito = RotacaoDireita(atual.FilhoDireito!);
            return RotacaoEsquerda(atual);
        }

        return atual;
    }

    private int AlturaDoNo(NoAVL? atual)
    {
        return atual == null ? 0 : atual.Altura;
    }

    private void AtualizarAltura(NoAVL atual)
    {
        atual.Altura = 1 + Math.Max(AlturaDoNo(atual.FilhoEsquerdo), AlturaDoNo(atual.FilhoDireito));
    }

    private int FatorBalanceamento(NoAVL atual)
    {
        return AlturaDoNo(atual.FilhoEsquerdo) - AlturaDoNo(atual.FilhoDireito);
    }

    private NoAVL RotacaoDireita(NoAVL antigoTopo)
    {
        NoAVL novoTopo = antigoTopo.FilhoEsquerdo!;
        NoAVL? auxiliar = novoTopo.FilhoDireito;

        novoTopo.FilhoDireito = antigoTopo;
        antigoTopo.FilhoEsquerdo = auxiliar;

        AtualizarAltura(antigoTopo);
        AtualizarAltura(novoTopo);

        return novoTopo;
    }

    private NoAVL RotacaoEsquerda(NoAVL antigoTopo)
    {
        NoAVL novoTopo = antigoTopo.FilhoDireito!;
        NoAVL? auxiliar = novoTopo.FilhoEsquerdo;

        novoTopo.FilhoEsquerdo = antigoTopo;
        antigoTopo.FilhoDireito = auxiliar;

        AtualizarAltura(antigoTopo);
        AtualizarAltura(novoTopo);

        return novoTopo;
    }

    public int CalcularAltura()
    {
        return AlturaDoNo(Inicio);
    }
}

class NoBST
{
    public int Dado;
    public NoBST? FilhoEsquerdo;
    public NoBST? FilhoDireito;

    public NoBST(int dado)
    {
        Dado = dado;
    }
}

class BST
{
    public NoBST? Inicio;

    public void Inserir(int dado)
    {
        Inicio = Inserir(Inicio, dado);
    }

    private NoBST Inserir(NoBST? atual, int dado)
    {
        if (atual == null)
            return new NoBST(dado);

        if (dado < atual.Dado)
            atual.FilhoEsquerdo = Inserir(atual.FilhoEsquerdo, dado);
        else if (dado > atual.Dado)
            atual.FilhoDireito = Inserir(atual.FilhoDireito, dado);

        return atual;
    }

    public int CalcularAltura()
    {
        return CalcularAltura(Inicio);
    }

    private int CalcularAltura(NoBST? atual)
    {
        if (atual == null)
            return 0;

        return 1 + Math.Max(CalcularAltura(atual.FilhoEsquerdo), CalcularAltura(atual.FilhoDireito));
    }
}
