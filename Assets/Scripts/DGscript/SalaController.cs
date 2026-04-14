using UnityEngine;

public class SalaController : MonoBehaviour
{
    [Header("Portas")]
    public GameObject portaCima;
    public GameObject portaBaixo;
    public GameObject portaEsquerda;
    public GameObject portaDireita;
    public TipoSala tipoSala;
    public Vector2Int posicaoGrid;

   

    public void ConfigurarSala(SalaNode sala)
    {
        tipoSala = sala.tipo;
        posicaoGrid = sala.Posicao; 
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