public class PokemonAgua : Pokemon
{
    public PokemonAgua(string nome, string tipo, int vida, int ataque, int defesa) : base(nome, tipo, vida, ataque, defesa)
    {

    }

    public override void Atacar(Pokemon alvo)
    {
        base.Atacar(alvo);
        Vida += 2;
    }
}