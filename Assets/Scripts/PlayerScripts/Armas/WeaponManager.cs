using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [Header("Configurações")]
    public KeyCode teclaTroca = KeyCode.T;
    public List<GameObject> armas; 
    private int indexAtual = 0;
    private string armaAtualID;

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

    public bool AdicionarArma(GameObject novaArma, string weaponID)
    {
        // tem essa arma
        if (armaAtualID == weaponID)
        {
            return false;
        }

        //  remove arma antiga
        if (armas.Count > 0)
        {
            Destroy(armas[0]);
            armas.Clear();
        }

        armas.Add(novaArma);
        armaAtualID = weaponID;

        indexAtual = 0;
        AtualizarArmas();

        return true;
    }
}