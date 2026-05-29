using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public Vector2 areaMin;
    public Vector2 areaMax;

    private SalaController sc;
    private DungeonGeneratortest dg;

    public int quantidadeSpawn;

    [Header("Delay entre spawns")]
    public float delaySpawn = 0.5f;

    private void Awake()
    {
        dg = GetComponentInParent<DungeonGeneratortest>();
        sc = GetComponentInParent<SalaController>();
    }

    public void IniciarSpawn()
    {
        StartCoroutine(SpawnarInimigos());
    }

   IEnumerator SpawnarInimigos()
{
    int total = 0;

    if (dg != null)
    {
        SalaController sala = GetComponentInParent<SalaController>();

        if (sala.tipoSala == TipoSala.SalasMiniBoss)
        {
            quantidadeSpawn = 1;
        }
        else
        {
            quantidadeSpawn = dg.qtdInimigos;
        }

        // RESETA
        sala.qtdInimigosVivos = 0;

        for (int i = 0; i < quantidadeSpawn; i++)
        {
            GameObject prefab;

            // MINI BOSS
            if (sala.tipoSala == TipoSala.SalasMiniBoss)
            {
                int indiceMiniBoss = Random.Range(
                    0,
                    sc.dg.catalogoInimigos.miniBosses.Count
                );

                prefab =
                    sc.dg.catalogoInimigos.miniBosses[indiceMiniBoss];
            }

            // INIMIGO NORMAL
            else
            {
                int indice = Random.Range(
                    0,
                    sc.dg.catalogoInimigos.inimigos.Count
                );

                int indiceElite = Random.Range(
                    0,
                    sc.dg.catalogoInimigos.inimigosDeElite.Count
                );

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

            yield return StartCoroutine(
                SpawnCadenciado(prefab, posicaoFinal, sala)
            );

            
            sala.qtdInimigosVivos++;

            total++;
        }
    }

    Debug.Log("Total de inimigos spawnados: " + total);
}

    IEnumerator SpawnCadenciado(
        GameObject prefab,
        Vector3 posicaoFinal,
        SalaController sala
    )
    {
        yield return new WaitForSeconds(delaySpawn);

        GameObject inimigo = Instantiate(
            prefab,
            posicaoFinal,
            Quaternion.identity,
            transform
        );

        InimigoController ic =
            inimigo.GetComponent<InimigoController>();

        if (ic != null)
        {
            ic.DefinirSalaOrigem(sala);
        }
    }
}