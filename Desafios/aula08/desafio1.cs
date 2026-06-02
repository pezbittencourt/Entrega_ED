using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
    static Dictionary<string, int>? textoRecente = null;

    static List<Dictionary<string, int>> textosAnteriores =
        new List<Dictionary<string, int>>();

    static void Main()
    {
        int escolha;

        do
        {
            Console.WriteLine();
            Console.WriteLine("Menu:");
            Console.WriteLine("1 - Novo texto");
            Console.WriteLine("2 - Buscar palavra");
            Console.WriteLine("3 - Comparar textos");
            Console.WriteLine("4 - Sair");
            Console.Write("Opção: ");

            escolha = int.Parse(Console.ReadLine()!);

            switch (escolha)
            {
                case 1:
                    LerNovoTexto();
                    break;

                case 2:
                    ProcurarPalavra();
                    break;

                case 3:
                    MostrarComparacao();
                    break;

                case 4:
                    Console.WriteLine("Tchau!");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

        } while (escolha != 4);
    }

    static void ProcurarPalavra()
    {
        if (textoRecente == null)
        {
            Console.WriteLine("Nenhum texto carregado.");
            return;
        }

        Console.Write("Qual palavra? ");

        string termo =
            PrepararTexto(Console.ReadLine()!);

        if (textoRecente.ContainsKey(termo))
        {
            Console.WriteLine(
                $"\"{termo}\" aparece {textoRecente[termo]} vez(es).");
        }
        else
        {
            Console.WriteLine(
                $"\"{termo}\" não aparece no texto.");
        }
    }

    static void LerNovoTexto()
    {
        Dictionary<string, int> contagem =
            new Dictionary<string, int>();

        int somaPalavras = 0;

        Console.WriteLine();
        Console.WriteLine("Digite o texto (linha vazia para encerrar):");

        while (true)
        {
            string? entrada = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entrada))
                break;

            entrada = PrepararTexto(entrada);

            string[] partes =
                entrada.Split(' ',
                StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in partes)
            {
                somaPalavras++;

                if (contagem.ContainsKey(item))
                    contagem[item]++;
                else
                    contagem[item] = 1;
            }
        }

        textoRecente = contagem;
        textosAnteriores.Add(contagem);

        Console.WriteLine();
        Console.WriteLine("=== Resultado ===");
        Console.WriteLine($"Total de palavras: {somaPalavras}");
        Console.WriteLine($"Palavras distintas: {contagem.Count}");

        Console.WriteLine();
        Console.WriteLine("Top 10 palavras mais frequentes:");

        var maisFrequentes = contagem
            .OrderByDescending(x => x.Value)
            .ThenBy(x => x.Key)
            .Take(10);

        int ordem = 1;

        foreach (var registro in maisFrequentes)
        {
            Console.WriteLine(
                $"{ordem,2}. \"{registro.Key}\" - {registro.Value} ocorrência(s)");
            ordem++;
        }
    }

    static void MostrarComparacao()
    {
        if (textosAnteriores.Count < 2)
        {
            Console.WriteLine(
                "É necessário possuir pelo menos dois textos.");
            return;
        }

        Dictionary<string, int> penultimoTexto =
            textosAnteriores[textosAnteriores.Count - 2];

        Dictionary<string, int> ultimoTexto =
            textosAnteriores[textosAnteriores.Count - 1];

        HashSet<string> palavrasIguais =
            new HashSet<string>(penultimoTexto.Keys);

        HashSet<string> palavrasDoUltimo =
            new HashSet<string>(ultimoTexto.Keys);

        palavrasIguais.IntersectWith(palavrasDoUltimo);

        Console.WriteLine();
        Console.WriteLine("Palavras em comum:");

        if (palavrasIguais.Count == 0)
        {
            Console.WriteLine("Nenhuma palavra encontrada.");
            return;
        }

        foreach (string palavra in palavrasIguais.OrderBy(x => x))
        {
            Console.WriteLine(palavra);
        }
    }

    static string PrepararTexto(string texto)
    {
        texto = texto.ToLower();

        StringBuilder resultado = new StringBuilder();

        foreach (char caractere in texto)
        {
            if (char.IsLetterOrDigit(caractere) || char.IsWhiteSpace(caractere))
                resultado.Append(caractere);
        }

        return resultado.ToString();
    }
}