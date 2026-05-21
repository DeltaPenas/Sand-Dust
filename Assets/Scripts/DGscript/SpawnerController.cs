using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public Vector2 areaMin;
    public Vector2 areaMax;
    private SalaController sc;
    private DungeonGeneratortest dg;
    private RunInfos ri;


    public void Start()
    {
        ri = GetComponentInParent<RunInfos>();
        dg = GetComponentInParent<DungeonGeneratortest>();
        sc = GetComponent<SalaController>();
    }


public int SpawnarInimigos()
{
    int total = 0;

    if (dg != null)
    {
        SalaController sala = GetComponentInParent<SalaController>();

        for (int i = 0; i < dg.qtdInimigos; i++)
        {
            GameObject prefab;

            //MINIBOSS
            if (sala.tipoSala == TipoSala.SalasMiniBoss)
            {
                int indiceMiniBoss =
                    Random.Range(0, sc.dg.catalogoInimigos.miniBosses.Count);

                prefab =
                    sc.dg.catalogoInimigos.miniBosses[indiceMiniBoss];
            }

        
            else
            {
                int indice =
                    Random.Range(0, sc.dg.catalogoInimigos.inimigos.Count);

                int indiceElite =
                    Random.Range(0, sc.dg.catalogoInimigos.inimigosDeElite.Count);

                int peso = Random.Range(1, 10);

                if (peso >= 7)
                {
                    prefab =
                        sc.dg.catalogoInimigos.inimigosDeElite[indiceElite];
                }
                else
                {
                    prefab =
                        sc.dg.catalogoInimigos.inimigos[indice];
                }
            }

            float x = Random.Range(areaMin.x, areaMax.x);
            float y = Random.Range(areaMin.y, areaMax.y);

            Vector3 posicaoFinal =
                transform.position + new Vector3(x, y, 0);

            GameObject inimigo = Instantiate(
                prefab,
                posicaoFinal,
                Quaternion.identity,
                transform
            );

            InimigoController ic = inimigo.GetComponent<InimigoController>();

            if (ic != null)
            {
                ic.DefinirSalaOrigem(sala);
            }

            total++;
        }
    }

    return total;
}

    }
