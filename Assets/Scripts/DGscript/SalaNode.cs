using UnityEngine;

internal enum TipoSala
    {
        Inicial,
        Normal,
        Tesouro,
        SalaProxLayer,
        Loja,
        Secreta,
        SalaBoss,
        evento

    
    }
internal class SalaNode
    {
        public Vector2Int Posicao;
        public TipoSala tipo;

        public SalaNode(Vector2Int pos)
        {
            Posicao = pos;
            tipo = TipoSala.Normal;
        }

    }