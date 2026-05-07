using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCardUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI nomeText;
    public TextMeshProUGUI descricaoText;
    public TextMeshProUGUI precoText;

    public Image icon;

    private ShopItemData itemData;
    private ShopManager shopManager;
    [SerializeField] private Button botaoComprar;

    public void Setup(ShopItemData data, ShopManager manager)
{
    itemData = data;
    shopManager = manager;

    nomeText.text = data.itemNome;
    descricaoText.text = data.descricao;
    precoText.text = data.preco.ToString();

    icon.sprite = data.icone;

    // limpa listeners antigos
    botaoComprar.onClick.RemoveAllListeners();

    // adiciona funçao
    botaoComprar.onClick.AddListener(Comprar);
}

    public void Comprar()
    {
        shopManager.Comprar(itemData);
    }
}