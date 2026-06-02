// Criando Pokemons

// Fogo
PokemonFogo charmander = new PokemonFogo("Charmander", "Fogo", 100, 16, 8);
PokemonFogo vulpix = new PokemonFogo("Vulpix", "Fogo", 90, 14, 7);
PokemonFogo ponyta = new PokemonFogo("Ponyta", "Fogo", 95, 18, 6);

// Água
PokemonAgua squirtle = new PokemonAgua("Squirtle", "Água", 100, 12, 11);
PokemonAgua psyduck = new PokemonAgua("Psyduck", "Água", 95, 13, 9);
PokemonAgua horsea = new PokemonAgua("Horsea", "Água", 80, 15, 8);
PokemonAgua seel = new PokemonAgua("Seel", "Água", 105, 11, 12);

// Grama
PokemonGrama bulbasaur = new PokemonGrama("Bulbasaur", "Grama", 100, 13, 10);
PokemonGrama oddish = new PokemonGrama("Oddish", "Grama", 85, 11, 9);
PokemonGrama bellsprout = new PokemonGrama("Bellsprout", "Grama", 80, 17, 5);

//Criando Treinadores

// --- Treinador: Ash ---
LinkedList<Pokemon> pokemonsAsh = new LinkedList<Pokemon>();
pokemonsAsh.AddLast(charmander);
pokemonsAsh.AddLast(squirtle);
pokemonsAsh.AddLast(bulbasaur);
pokemonsAsh.AddLast(ponyta);
Treinador ash = new Treinador("Ash", pokemonsAsh);

// --- Treinador: Misty ---
LinkedList<Pokemon> pokemonsMisty = new LinkedList<Pokemon>();
pokemonsMisty.AddLast(horsea);
pokemonsMisty.AddLast(seel);
pokemonsMisty.AddLast(psyduck);
pokemonsMisty.AddLast(oddish);
Treinador misty = new Treinador("Misty", pokemonsMisty);

// --- Treinador: Brock ---
LinkedList<Pokemon> pokemonsBrock = new LinkedList<Pokemon>();
pokemonsBrock.AddLast(vulpix);
pokemonsBrock.AddLast(bellsprout);
pokemonsBrock.AddLast(squirtle);
pokemonsBrock.AddLast(bulbasaur);
Treinador brock = new Treinador("Brock", pokemonsBrock);

// --- Treinador: Gary ---
LinkedList<Pokemon> pokemonsGary = new LinkedList<Pokemon>();
pokemonsGary.AddLast(charmander);
pokemonsGary.AddLast(ponyta);
pokemonsGary.AddLast(horsea);
pokemonsGary.AddLast(bellsprout);
Treinador gary = new Treinador("Gary", pokemonsGary);

List<Treinador> treinadores = new List<Treinador>();
treinadores.Add(ash);
treinadores.Add(misty);
treinadores.Add(brock);
treinadores.Add(gary);

//Funções
static Treinador EscolherTreinador(string resposta, List<Treinador> treinadores)
{
    foreach (Treinador treinador in treinadores)
    {
        if (treinador.Nome == resposta)
        {
            return treinador;
        }
    }
    Console.WriteLine("Nome incorreto!");
    return null;
}

static Treinador SelecionarJogador(string label, List<Treinador> treinadores)
{
    Console.WriteLine($"==={label}===");
    Console.WriteLine("Escolha seu Treinador:");
    string resposta = Console.ReadLine();
    Treinador jogador = EscolherTreinador(resposta, treinadores);

    if (jogador == null)
    {
        Console.WriteLine("Treinador não encontrado!");
    }
    else
    {
        Console.WriteLine($"Treinador escolhido: {jogador.Nome}");
    }

    return jogador;
}

static Pokemon SelecionarPokemon(Treinador jogador)
{
    Console.WriteLine($"{jogador.Nome}, escolha seu Pokémon:");
    jogador.ListarPokemons();
    string resposta = Console.ReadLine();
    Pokemon escolhido = jogador.EscolherPokemon(resposta);
    if (escolhido == null)
    {
        Console.WriteLine("Pokemón não encontrado!");
        return null;
    }
    escolhido.ExibirStatus();
    Console.WriteLine("\n");

    return escolhido;
}

static void Batalhar(Pokemon p1, Pokemon p2)
{
    Console.WriteLine($"\n=== BATALHA: {p1.Nome} vs {p2.Nome} ===\n");

    while (p1.Vida > 0 && p2.Vida > 0)
    {
        int vidaAntes = p2.Vida;
        p1.Atacar(p2);
        int dano = vidaAntes - p2.Vida;
        Console.WriteLine($"{p1.Nome} atacou {p2.Nome} e causou {dano} de dano.");
        Console.WriteLine($"{p2.Nome} agora está com {p2.Vida} de vida.\n");

        if (p2.Vida <= 0) break;

        vidaAntes = p1.Vida;
        p2.Atacar(p1);
        dano = vidaAntes - p1.Vida;
        Console.WriteLine($"{p2.Nome} atacou {p1.Nome} e causou {dano} de dano.");
        Console.WriteLine($"{p1.Nome} agora está com {p1.Vida} de vida.\n");
    }

    Pokemon vencedor = p1.Vida > 0 ? p1 : p2;
    Console.WriteLine($"=== {vencedor.Nome} venceu a batalha! ===");
}

//Começo da batalha
Treinador jogador1;
Treinador jogador2;
Pokemon pokemonJogador1;
Pokemon pokemonJogador2;

Console.WriteLine("===BATALHA POKEMÓN===\n");

Console.WriteLine("===TREINADORES===\n");
foreach (Treinador treinador in treinadores)
{
    Console.WriteLine($"{treinador.Nome}");
    treinador.ListarPokemons();
    Console.WriteLine("\n");
}

//Escolha de treinadores
do
{
    jogador1 = SelecionarJogador("JOGADOR1", treinadores);
} while (jogador1 == null);

do
{
    jogador2 = SelecionarJogador("JOGADOR2", treinadores);
} while (jogador2 == null);

//Escolha do Pokemon
Console.WriteLine("===POKEMÓN===");
do
{
    pokemonJogador1 = SelecionarPokemon(jogador1);
} while (pokemonJogador1 == null);

do
{
    pokemonJogador2 = SelecionarPokemon(jogador2);
} while (pokemonJogador2 == null);


//Batalha
Batalhar(pokemonJogador1, pokemonJogador2);