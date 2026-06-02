public class Treinador
{
    public string Nome { get; set; }
    public LinkedList<Pokemon> Pokemons { get; set; }

    public Treinador(string nome, LinkedList<Pokemon> pokemon)
    {
        Nome = nome;
        Pokemons = pokemon;
    }
    public void AdicionarPokemon(Pokemon p)
    {
        LinkedList<Pokemon> listaPokemons = Pokemons;
        listaPokemons.AddLast(p);
    }

    public void ListarPokemons()
    {
        int i = 1;
        Console.WriteLine("Pokemóns:");
        foreach (Pokemon pokemon in Pokemons)
        {
            Console.WriteLine($"{i}. {pokemon.Nome}");
            i++;
        }
    }

    public Pokemon EscolherPokemon(string nome)
    {
        Pokemon p = null;
        foreach (Pokemon pokemon in Pokemons)
        {
            if (pokemon.Nome == nome)
            {
                p = pokemon;
            }
        }
        return p;
    }
}