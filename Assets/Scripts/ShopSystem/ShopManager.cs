using UnityEngine;
using System.Collections.Generic;


public class ShopManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject shopPanel;

    public Transform cardsContainer;

    public GameObject cardPrefab;

    [Header("Itens")]
    public List<ShopItemData> itensLoja;

    [Header("Managers")]
    public WeaponManager weaponManager;
    public SkillManager skillManager;
    public UltManager ultManager;

    public HeartUi heartUi;

    public Transform player;
    public int QuantidadeItensLoja = 4;

    void Start()
    {
        GerarLoja();

        shopPanel.SetActive(false);
    }

    public void AbrirLoja()
    {
        shopPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void FecharLoja()
    {
        shopPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public List<ShopItemData> ObterItensParaLoja()
    {
        HashSet<ShopItemData> listaNovaLoja = new HashSet<ShopItemData>();

        

        while(listaNovaLoja.Count < QuantidadeItensLoja)
        {
            ShopItemData itemAleatorio =
            itensLoja[Random.Range(0, itensLoja.Count)];
            listaNovaLoja.Add(itemAleatorio);
        }
        
        return new List<ShopItemData>(listaNovaLoja);
    }

    public void GerarLoja()
{
    List<ShopItemData> itensSelecionados = ObterItensParaLoja();

    foreach (Transform child in cardsContainer)
    {
        Destroy(child.gameObject);
    }

    foreach (ShopItemData item in itensSelecionados)
    {
        GameObject card =
            Instantiate(cardPrefab, cardsContainer);

        ShopCardUI cardUI =
            card.GetComponent<ShopCardUI>();

        cardUI.Setup(item, this);
    }
}

    public bool Comprar(ShopItemData item)
    {
        if (RunManager.Instance.currentRun.moedasRun < item.preco)
        {
            Debug.Log("Sem gemas");
            return false;
        }

        switch (item.tipo)
        {
            case ShopItemType.Weapon:

                GameObject novaArma =
                    Instantiate(item.prefab, player);

                bool comprou =
                    weaponManager.AdicionarArma(
                        novaArma,
                        item.itemID
                    );

                if (!comprou)
                {
                    Destroy(novaArma);

                    return false;
                }

                break;

            case ShopItemType.Skill:

                skillManager.EquiparSkill(item.prefab);

                break;

            case ShopItemType.Ult:

                ultManager.EquiparUlt(item.prefab);

                break;
        }

        RunManager.Instance.currentRun.moedasRun -= item.preco;

        heartUi.AtualizarGemas();

        Debug.Log("Comprou: " + item.itemNome);
        return true;
    }
}