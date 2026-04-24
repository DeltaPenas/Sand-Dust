using static PlayerStatus;

[System.Serializable]
    public class StatModifier
    {
        public StatsType stat;
        public float valor;
        public bool ehporcentagem;
    }

    public enum Raridade
{
    Comum,
    Raro,
    Epico,
    Lendario
}

[System.Serializable]
public class RaridadePeso
{
    public Raridade raridade;
    public int peso;
}