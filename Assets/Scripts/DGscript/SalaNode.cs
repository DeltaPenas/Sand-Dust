using UnityEngine;

public enum TipoSala
{
    Inicial,
    Normal,
    Tesouro,
    SalaProxLayer,
    Loja,
    SalasMiniBoss,
    SalaBoss,
    Secreta,
    SalaAntesDoBoss
}

public class SalaNode
{
    public Vector2Int Posicao;
    public TipoSala tipo;

    public SalaNode(Vector2Int pos)
    {
        Posicao = pos;
        tipo = TipoSala.Normal;
    }
}