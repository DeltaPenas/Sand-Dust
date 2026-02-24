using UnityEngine;

enum TipoSala
    {
        Inicial,
        Normal,
        Tesouro,
        Boss,
        Loja,
        Secreta

    
    }
    class SalaNode
    {
        public Vector2Int Posicao;
        public TipoSala tipo;

        public SalaNode(Vector2Int pos)
        {
            Posicao = pos;
            tipo = TipoSala.Normal;
        }

    }