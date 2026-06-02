public class Pokemon
{
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public int Vida { get; set; }
    public int Ataque { get; set; }
    public int Defesa { get; set; }

    public Pokemon(string nome, string tipo, int vida, int ataque, int defesa)
    {
        Nome = nome;
        Tipo = tipo;
        Vida = vida;
        Ataque = ataque;
        Defesa = defesa;
    }

    public void ExibirStatus()
    {
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Tipo: {Tipo}");
        Console.WriteLine($"Vida: {Vida}");
        Console.WriteLine($"Ataque: {Ataque}");
        Console.WriteLine($"Defesa: {Defesa}");
    }

    public virtual void Atacar(Pokemon alvo)
    {
        int dano = Ataque - alvo.Defesa;
        if (dano < 1) dano = 1;
        alvo.Vida -= dano;
    }
}