using UnityEngine;

public class ShopItemData : MonoBehaviour
{
    [Header("Info")]
    public string itemID;
    public string itemNome;
    public GameObject prefab;

    [TextArea]
    public string descricao;
    public Sprite icone;
    public int preco;
    public ShopItemType tipo;
}