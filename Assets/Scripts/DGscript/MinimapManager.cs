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
                return Color.green;

            case TipoSala.SalaBoss:
                return Color.red;

            case TipoSala.Tesouro:
                return Color.yellow;

            case TipoSala.Loja:
                return Color.cyan;

            case TipoSala.SalasMiniBoss:
                return new Color(0.7f, 0f, 1f);

            case TipoSala.SalaAntesDoBoss:
                return new Color(1f, 0.5f, 0f);

            case TipoSala.SalaProxLayer:
                return Color.blue;

            case TipoSala.Secreta:
                return Color.magenta;

            default:
                return Color.white;
        }
    }
}