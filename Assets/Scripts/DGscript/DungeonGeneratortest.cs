using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using Unity.VisualScripting;
using JetBrains.Annotations;
public class DungeonGeneratortest : MonoBehaviour
{   
    public int qtdminSalas;
    public int qtdmaxSalas;
    public int andar;
    public int layer;
    public Transform DungeonParent;

    List<SalaNode> salas = new List<SalaNode>();
    //GameObject salaGO = Instantiate(prefabSala, posicaoMundo, Quaternion.identity);
    //salaGO.transform.parent = dungeonParent;

    void Start()
    {
        gerarSala();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //descerAndar();
        }
    }

    void gerarSala()
    {
        

        int qtdSalas = Random.Range(qtdminSalas,qtdmaxSalas);

        if (qtdmaxSalas <= 4 ||qtdminSalas <= 4 || qtdminSalas > qtdmaxSalas )
        {

            Debug.Log("quantidade de salas invalida");
            return;
        }
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
        andar += 1;
        Debug.Log("andar atual: " + andar);
        


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
        SalaNode salaProxLayer = null;

        foreach (SalaNode sala in salas)
        {
            if(sala.Posicao == Vector2Int.zero) continue;
            int distancia = Mathf.Abs(sala.Posicao.x) + Mathf.Abs(sala.Posicao.y);
            if (distancia > maiorDistancia)
            {
                maiorDistancia = distancia;
                salaProxLayer = sala;
            }
        }
        if (salaProxLayer != null)
        {
            salaProxLayer.tipo = TipoSala.SalaProxLayer;
        }
        List<SalaNode> salasValidas = new List<SalaNode>();
        Vector2Int posSalaBau = Vector2Int.zero;
        if (qtdSalas > 6)
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (SalaNode sala in salas)
        {
            if (sala.Posicao == Vector2Int.zero) continue;
            if (sala.Posicao == salaProxLayer.Posicao) continue;
            salasValidas.Add(sala);
        }

            SalaNode salaBau = salasValidas[Random.Range(0, salasValidas.Count)];
            salaBau.tipo = TipoSala.Tesouro;
                
            }  
        }else if (qtdSalas <= 6)
        {
            foreach (SalaNode sala in salas)
        {
            if (sala.Posicao == Vector2Int.zero) continue;
            if (sala.Posicao == salaProxLayer.Posicao) continue;
            salasValidas.Add(sala);
        }
        SalaNode salaBau = salasValidas[Random.Range(0, salasValidas.Count)];
        salaBau.tipo = TipoSala.Tesouro;
        }
        foreach (SalaNode sala in salas)
    {
         Debug.Log(sala.Posicao + " - " + sala.tipo);
    }
               
    }

    void LimparDungeon()
    {
        //foreach (Transform filho in dungeonParent)
        //{
        //Destroy(filho.gameObject);
        //}

    //salas.Clear();
    }
    
    
}
