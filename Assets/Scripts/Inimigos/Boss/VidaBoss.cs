using System;
using UnityEngine;

public class VidaBoss : Vida
{
    public bool isSegundaFase;
    [SerializeField] FirstBossController boss;


    void Start()
    {
        boss = GetComponent<FirstBossController>();
    }


    protected override void AoReceberDano()
    {
        float vidaPersent = vidaAtual/vidaTotal;


        if (!isSegundaFase && vidaPersent <= 0.5f) //significa q ta na metade
        {
            
        }
        
    }

}