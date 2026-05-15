using System;
using UnityEngine;

public class VidaBoss : Vida
{
    public bool isSegundaFase;
    public float danoParaTrocaDeFase;
    public float danoParaProximoBuff;
    
    [SerializeField] private float porcentagemBuff = 0.75f;
    [SerializeField] private float proximoLimiteBuff;
    [SerializeField] FirstBossController boss;



    protected override void Start()
    {
        boss = GetComponent<FirstBossController>();

        vidaAtual = vidaTotal;

        proximoLimiteBuff = vidaTotal * porcentagemBuff;
    }


    


    protected override void AoReceberDano()
    {
        if (danoAcumulado >= danoParaTrocaDeFase)
        {
            danoAcumulado = 0;
            boss.EntrarEstadoRanged();

            //
            if (vidaAtual <= proximoLimiteBuff)
            {
                AplicarBuff();

                porcentagemBuff -= 0.25f;

                proximoLimiteBuff = vidaTotal * porcentagemBuff;
            }
                        
        }
        
    }

    protected override void morrer()
    {
        boss.TrocarEstado(BossState.Morreu);
    }

    void AplicarBuff()
    {
        boss.cooldownDisparo -= 0.4f;
        boss.cooldownTrap -= 0.4f;
        boss.tempoAtivarTrap -= 0.1f;

        boss.velocidadeDisparo +=3f;
        boss.velocidadeRotacao +=100;
        boss.tempoFlutuando -= 0.3f;

        Debug.Log("Boss buffado!");
    }

}