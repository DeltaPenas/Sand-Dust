using System.Collections;
using UnityEngine;

public class DanoVisual : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float duracao = 0.1f;
    public float intervalo = 0.1f;

    private bool piscando = false;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TomouDano()
    {
        if (!piscando)
        {
            StartCoroutine(Piscar());
        }
    }

    IEnumerator Piscar()
{
    piscando = true;

    float tempo = 0f;
    Color corOriginal = spriteRenderer.color;

    while (tempo < duracao)
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.3f); 
        yield return new WaitForSeconds(intervalo);

        spriteRenderer.color = corOriginal;
        //yield return new WaitForSeconds(intervalo);//deixa isso pra la, acho que uma piscada só ta bom

        tempo += intervalo * 2;
    }

    spriteRenderer.color = corOriginal;
    piscando = false;
}
}