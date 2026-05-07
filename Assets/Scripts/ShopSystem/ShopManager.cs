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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            AbrirLoja();
        }
    }

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

    void GerarLoja()
    {
        foreach (Transform child in cardsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (ShopItemData item in itensLoja)
        {
            GameObject card =
                Instantiate(cardPrefab, cardsContainer);

            ShopCardUI cardUI =
                card.GetComponent<ShopCardUI>();

            cardUI.Setup(item, this);
        }
    }

    public void Comprar(ShopItemData item)
    {
        if (RunManager.Instance.currentRun.moedasRun < item.preco)
        {
            Debug.Log("Sem moedas");
            return;
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

                    return;
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
    }
}