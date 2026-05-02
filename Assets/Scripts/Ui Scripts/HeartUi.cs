using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;

public class HeartUi : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartsContainer;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI gemValor;
    [SerializeField] private GameObject gem;
    [SerializeField] private PlayerVida pv;
    [SerializeField] private PlayerController pc;
    private List<GameObject> hearts = new List<GameObject>();

    public void Start()
    {
        pv = FindAnyObjectByType<PlayerVida>();
        pc = FindAnyObjectByType<PlayerController>();
        gem.SetActive(true);
    }

    public void UpdateHearts(int vidaAtual, int vidaMax)
    {
    foreach (GameObject heart in hearts)
    {
        Destroy(heart);
    }

    hearts.Clear();

    for (int i = 0; i < vidaMax; i++)
    {
        GameObject newHeart = Instantiate(heartPrefab, heartsContainer);

        if (i >= vidaAtual)
        {
            CanvasGroup cg = newHeart.GetComponent<CanvasGroup>();

            if (cg == null)
                cg = newHeart.AddComponent<CanvasGroup>();

            cg.alpha = 0.3f;
        }

        hearts.Add(newHeart);
    }
    }
    public void AtualizarGemas()
    {
        gemValor.text = ""+ pc.gems;
    }

}
