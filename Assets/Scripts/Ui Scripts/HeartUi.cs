using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class HeartUi : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartsContainer;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerVida pv;
    private List<GameObject> hearts = new List<GameObject>();

    public void Start()
    {
        pv = FindAnyObjectByType<PlayerVida>();
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

}
