public class PokemonGrama : Pokemon
{
    public PokemonGrama(string nome, string tipo, int vida, int ataque, int defesa) : base(nome, tipo, vida, ataque, defesa)
    {

    }

    public override void Atacar(Pokemon alvo)
    {
        Random random = new Random();

        int dano = Ataque - alvo.Defesa;

        if (dano < 1) dano = 1;

        if (random.Next(1, 11) <= 2) dano += dano * 2;

        alvo.Vida -= dano;
    }
}