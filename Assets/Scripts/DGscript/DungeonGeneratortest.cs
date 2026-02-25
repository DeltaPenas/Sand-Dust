using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
 

public class DungeonGeneratortest : MonoBehaviour
{   


    

    void Start()
    {

        List<SalaNode> salas = new List<SalaNode>();
        int qtdSalas = Random.Range(6,11);
        
        
        Vector2Int[] direções = new Vector2Int[]
        {
            new Vector2Int(1,0),
            new Vector2Int(-1,0),
            new Vector2Int(0,1),
            new Vector2Int(0,-1)
        };


        SalaNode salainicial = new SalaNode(Vector2Int.zero);
        salas.Add(salainicial);
        
        salainicial.tipo = TipoSala.Inicial;

        while (salas.Count < qtdSalas)
        {
            SalaNode salaAleatoria = salas[Random.Range(0, salas.Count)];
            Vector2Int direcaoAleatoria = direções[Random.Range(0, direções.Length)];
            Vector2Int novaPosicao = salaAleatoria.Posicao + direcaoAleatoria;

            bool existe = false;

        foreach (SalaNode sala in salas)
            {
            if (sala.Posicao == novaPosicao)
            {
            existe = true;
            break;
            }
        }

        if (!existe)
        {
            salas.Add(new SalaNode(novaPosicao));
        }
          
        }

        int maiorDistancia = 0;
        SalaNode salaBoss = null;

        foreach (SalaNode sala in salas)
        {
            if(sala.Posicao == Vector2Int.zero) continue;

            int distancia = Mathf.Abs(sala.Posicao.x) + Mathf.Abs(sala.Posicao.y);

            if (distancia > maiorDistancia)
            {
                maiorDistancia = distancia;
                salaBoss = sala;
            }
            
            
        }

        if (salaBoss != null)
        {
            salaBoss.tipo = TipoSala.Boss;
        }

        List<SalaNode> salasValidas = new List<SalaNode>();
        Vector2Int posSalaBau = Vector2Int.zero;

        foreach (SalaNode sala in salas)
        {
            if (sala.Posicao == Vector2Int.zero) continue;
            if (sala.Posicao == salaBoss.Posicao) continue;
            salasValidas.Add(sala);
        }

        SalaNode salaBau = salasValidas[Random.Range(0, salasValidas.Count)];
        salaBau.tipo = TipoSala.Tesouro;

        foreach (SalaNode sala in salas)
    {
         Debug.Log(sala.Posicao + " - " + sala.tipo);
        
        
        
    }
        
        
    }

}
