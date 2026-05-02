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
        if (armas.Count > 0)
        {
            AtualizarArmas();
        }
        
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
        if(armas.Count == 0) return;

        indexAtual++;
        if (indexAtual >= armas.Count) indexAtual = 0;
        
        AtualizarArmas();
    }

    void AtualizarArmas()
    {
        for (int i = 0; i < armas.Count; i++)
        {
            
            armas[i].SetActive(i == indexAtual);
        }
    }

    public void AdicionarArma(GameObject novaArma)
    {
        armas.Add(novaArma);

        //desativando armas antes
        for (int i = 0; i < armas.Count; i++)
        {
            armas[i].SetActive(false);
        }
        //ativando a arma atual
        indexAtual = armas.Count - 1;
        armas[indexAtual].SetActive(true);


    }
}