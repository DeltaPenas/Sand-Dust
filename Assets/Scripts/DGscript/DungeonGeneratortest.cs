using UnityEngine;
using System.Collections.Generic;

public class DungeonGeneratortest : MonoBehaviour
{   
    

    void Start()
    {

        List<Vector2Int> salas = new List<Vector2Int>();
        int qtdSalas = Random.Range(6,11);
        
        

        Vector2Int[] direções = new Vector2Int[]
        {
            new Vector2Int(1,0),
            new Vector2Int(-1,0),
            new Vector2Int(0,1),
            new Vector2Int(0,-1)
        };


        salas.Add(new Vector2Int(0,0));

        while (salas.Count < qtdSalas)
        {
            Vector2Int salaAleatoria = salas[Random.Range(0, salas.Count)];
            Vector2Int direcaoAleatoria = direções[Random.Range(0, direções.Length)];
            Vector2Int novaPosicao = salaAleatoria + direcaoAleatoria;
            if (!salas.Contains(novaPosicao))
        {
            salas.Add(novaPosicao);
        }
            
            
        }

        int maiorDistancia = 0;
        Vector2Int posBoss = Vector2Int.zero;

        foreach (Vector2Int sala in salas)
        {
            if(sala == Vector2Int.zero) continue;

            int distancia = Mathf.Abs(sala.x) + Mathf.Abs(sala.y);

            if (distancia > maiorDistancia)
            {
                maiorDistancia = distancia;
                posBoss = sala;
            }
            
            
        }
        

        foreach (Vector2Int sala in salas)
    {
        Debug.Log(sala);
        
        
        
    }
        Debug.Log("sala do boss:" + posBoss );
        
    }

    
}
