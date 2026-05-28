using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapManager : MonoBehaviour
{
    public static MinimapManager Instance;

    [Header("Referências")]
    public RectTransform minimapContainer;
    public GameObject salaPrefab;

    [Header("Configurações")]
    public float espacamento = 20f;

    private Dictionary<Vector2Int, Image> salasNoMinimap = new Dictionary<Vector2Int, Image>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegistrarSala(SalaController sala)
    {
        if (sala == null)
            return;

        if (salasNoMinimap.ContainsKey(sala.posicaoGrid))
            return;

        GameObject salaObj = Instantiate(salaPrefab, minimapContainer);

        RectTransform rect = salaObj.GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector2(
            sala.posicaoGrid.x * espacamento,
            sala.posicaoGrid.y * espacamento
        );

        Image imagem = salaObj.GetComponent<Image>();

        imagem.color = new Color(0.15f, 0.15f, 0.15f, 1f);

        salasNoMinimap.Add(sala.posicaoGrid, imagem);
    }

    public void RevelarSala(SalaController sala)
    {
        if (sala == null)
            return;

        if (!salasNoMinimap.ContainsKey(sala.posicaoGrid))
            return;

        salasNoMinimap[sala.posicaoGrid].color = CorPorTipo(sala.tipoSala);
    }

    private Color CorPorTipo(TipoSala tipo)
{
    switch (tipo)
    {
       
        case TipoSala.Inicial:
            return new Color(0.85f, 0.85f, 0.85f);

     
        case TipoSala.SalaBoss:
            return new Color(0.15f, 0.15f, 0.15f);

      
        case TipoSala.Tesouro:
            return new Color(1f, 0.85f, 0.2f);


        case TipoSala.Loja:
            return new Color(0.95f, 0.75f, 0.15f);

 
        case TipoSala.SalasMiniBoss:
            return new Color(0.45f, 0.45f, 0.45f);

   
        case TipoSala.SalaAntesDoBoss:
            return new Color(0.3f, 0.3f, 0.3f);

        case TipoSala.SalaProxLayer:
            return new Color(1f, 0.95f, 0.5f);

     
        case TipoSala.Secreta:
            return new Color(0.6f, 0.6f, 0.6f);

        default:
            return new Color(0.75f, 0.75f, 0.75f);
    }
}

public void ResetarMapa()
{
    foreach (Image imagem in salasNoMinimap.Values)
    {
        if (imagem != null)
        {
            Destroy(imagem.gameObject);
        }
    }

    salasNoMinimap.Clear();
}
}