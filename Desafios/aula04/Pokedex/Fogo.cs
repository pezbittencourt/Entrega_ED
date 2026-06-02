public class PokemonFogo : Pokemon
{

    public PokemonFogo(string nome, string tipo, int vida, int ataque, int defesa) : base(nome, tipo, vida, ataque, defesa)
    {

    }

    public override void Atacar(Pokemon alvo)
    {
        int dano = Ataque - alvo.Defesa + 2;
        if (dano < 1) dano = 1;
        alvo.Vida -= dano;
    }

}