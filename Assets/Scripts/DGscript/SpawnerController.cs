using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject inimigoTeste1;
    public GameObject inimigoTeste2;
    public Vector2 areaMin;
    public Vector2 areaMax;
    public Vector2 areaMinProp;
    public Vector2 areaMaxProp;
    public int qtdInimigos;
    public int qtdProps;
    public CatalogoInimigos catalogoInimigos;
    public CatalogoProps catalogoProps;

    


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

    public void SpawnarProps()
    {
        if(catalogoProps.props.Count ==0) return;

        for (int i = 0; i < qtdProps; i++)
        {
            int indice = Random.Range(0, catalogoProps.props.Count);
            GameObject propEscolhido = 
                catalogoProps.props[indice];

            float x = Random.Range(areaMinProp.x, areaMaxProp.x);
            float y = Random.Range(areaMinProp.y, areaMaxProp.y);

            Vector3 posicaoLocalProps = new Vector3(x, y, 0);
            Vector3 posicaoFinalProps = transform.position + posicaoLocalProps;
            

            Instantiate(
                propEscolhido,
                posicaoFinalProps, 
                Quaternion.identity);
        }
    }

}
