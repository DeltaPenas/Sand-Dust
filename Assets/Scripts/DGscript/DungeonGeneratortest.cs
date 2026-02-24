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


        salas.Add(new SalaNode(Vector2Int.zero));

        while (salas.Count < qtdSalas)
        {
            SalaNode salaAleatoria = salas[Random.Range(0, salas.Count)];
            Vector2Int direcaoAleatoria = direções[Random.Range(0, direções.Length)];
            Vector2Int novaPosicao = salaAleatoria.Posicao + direcaoAleatoria;

            bool existe = false;

            if (salaAleatoria.Posicao == novaPosicao)
            {
                existe = true;
                break;
            }
            else
            {
                salas.Add(new SalaNode(novaPosicao));
            }
        
            
        
            
            
        }

        int maiorDistancia = 0;
        Vector2Int posBoss = Vector2Int.zero;

        foreach (SalaNode sala in salas)
        {
            if(sala == SalaNode.zero) continue;

            int distancia = Mathf.Abs(sala.x) + Mathf.Abs(sala.y);

            if (distancia > maiorDistancia)
            {
                maiorDistancia = distancia;
                posBoss = sala;
            }
            
            
        }

        List<Vector2Int> salasValidas = new List<Vector2Int>();
        Vector2Int posSalaBau = Vector2Int.zero;

        foreach (Vector2Int sala in salas)
        {
            if (sala == Vector2Int.zero) continue;
            if (sala == posBoss) continue;
            salasValidas.Add(sala);
        }

        posSalaBau = salasValidas[Random.Range(0, salasValidas.Count)];

        foreach (Vector2Int sala in salas)
    {
        Debug.Log(sala);
        
        
        
    }
        Debug.Log("sala do boss:" + posBoss );
        Debug.Log("sala do báu" + posSalaBau);
        
    }

    

    
}
