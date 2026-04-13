using UnityEngine;

public class SalaController : MonoBehaviour
{
    [Header("Portas")]
    public GameObject portaCima;
    public GameObject portaBaixo;
    public GameObject portaEsquerda;
    public GameObject portaDireita;

    public TipoSala tipoSala;

    public void ConfigurarSala(TipoSala tipo)
    {
        tipoSala = tipo;
    }

    public void ConfigurarPortas(
        bool cima,
        bool baixo,
        bool esquerda,
        bool direita
    )
    {
        portaCima.SetActive(cima);
        portaBaixo.SetActive(baixo);
        portaEsquerda.SetActive(esquerda);
        portaDireita.SetActive(direita);
    }
}