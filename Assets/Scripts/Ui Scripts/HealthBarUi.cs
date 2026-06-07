using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarUi : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private Image barraVida;
    [SerializeField] private Image barraDano;

    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float velocidadeDescida = 1.5f;

    private Coroutine animacaoDano;



    public void AtivarBarraDeVidaBoss()
    {
        container.SetActive(true);
        barraVida.fillAmount = 1;
        barraDano.fillAmount = 1;
    }
    public void DesativarBarraDeVidaBoss()
    {
        container.SetActive(false);
    }

    public void AtualizarVida(float vidaAtual, float vidaMaxima)
    {
        float porcentagem = vidaAtual / vidaMaxima;

    
        barraVida.fillAmount = porcentagem;

        
        if (animacaoDano != null)
            StopCoroutine(animacaoDano);

        animacaoDano = StartCoroutine(AnimarBarraDano());
    }

    private IEnumerator AnimarBarraDano()
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("atualizando barra de vida");

        while (barraDano.fillAmount > barraVida.fillAmount)
        {
            barraDano.fillAmount = Mathf.Lerp(
            barraDano.fillAmount,
            barraVida.fillAmount,
            Time.deltaTime * 5f
            );

            yield return null;
        }
    }
}