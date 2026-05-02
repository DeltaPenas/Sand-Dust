using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [Header("Configurações")]
    public KeyCode teclaTroca = KeyCode.T;
    public List<GameObject> armas; 
    private int indexAtual = 0;

    void Start()
    {
        AtualizarArmas();
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaTroca))
        {
            TrocarArma();
        }
    }

    void TrocarArma()
    {
        indexAtual++;
        if (indexAtual >= armas.Count) indexAtual = 0;
        
        AtualizarArmas();
        Debug.Log("Trocou para: " + armas[indexAtual].name);
    }

    void AtualizarArmas()
    {
        for (int i = 0; i < armas.Count; i++)
        {
            
            armas[i].SetActive(i == indexAtual);
        }
    }
}