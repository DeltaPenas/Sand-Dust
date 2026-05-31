using UnityEngine;

public class VidaBoss : Vida
{
    [Header("Fase/Buff")]

    public float danoParaTrocaDeFase;
    public float danoParaProximoBuff;

    [SerializeField] private FirstBossController boss;
    [SerializeField] private PauseMenu pauseMenu;

    private float danoAcumuladoBuff;

    protected override void Start()
    {
        boss = GetComponent<FirstBossController>();
        pauseMenu = FindAnyObjectByType<PauseMenu>();


        vidaAtual = vidaTotal;
    }

    protected override void AoReceberDano()
    {
        danoAcumuladoBuff += ultimoDanoRecebido;

        // troca de fase apos dano acumulado
        if (danoAcumulado >= danoParaTrocaDeFase)
        {
            danoAcumulado = 0;

            boss.TrocarEstado(BossState.FaseDois);
            boss.TeleportarPontoAleatorio();
        }

       
        if (danoAcumuladoBuff >= danoParaProximoBuff)
        {
            danoAcumuladoBuff = 0;

            AplicarBuff();
        }
    }

    protected override void morrer()
    {
        SoundController.instance.PlayDungeonMusic();
        boss.TrocarEstado(BossState.Morreu);
        boss.salaBoss.BossDerrotado();
        
        
    }

    void AplicarBuff()
    {
        boss.cooldownDisparo -= 0.4f;
        boss.cooldownTrap -= 0.3f;

        boss.velocidadeDisparo += 3f;
        boss.velocidadeRotacao += 100;

        boss.tempoFlutuando -= 0.3f;

        if (boss.currentState == BossState.FaseDois)
        {
            boss.cooldownTeleporte -= 2;
        }


        Debug.Log("Boss buffado!");
    }
}