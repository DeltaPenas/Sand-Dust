using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject inimigoTeste1;
    public GameObject inimigoTeste2;
    public Vector2 areaMin;
    public Vector2 areaMax;
    public int qtdInimigos;
    public CatalogoInimigos catalogoInimigos;


    
public int SpawnarInimigos()
{
    int total = 0;

    SalaController sala = GetComponentInParent<SalaController>();

    for (int i = 0; i < qtdInimigos; i++)
    {
        int indice = Random.Range(0, catalogoInimigos.inimigos.Count);

        GameObject prefab =
            catalogoInimigos.inimigos[indice];

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

        InimigoPerseguidor ip =
            inimigo.GetComponent<InimigoPerseguidor>();

        if (ip != null)
        {
            ip.DefinirSalaOrigem(sala);
        }

        total++;
    }

        return total;
}

}
