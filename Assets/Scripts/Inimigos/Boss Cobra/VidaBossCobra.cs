using UnityEngine;

public class VidaBossCobra : Vida
{
    [Header("Fase/Buff")]

    public float danoParaTrocaDeFase;
    public float danoParaProximoBuff;

    [SerializeField] private BossCobra boss;
    [SerializeField] private PauseMenu pauseMenu;

    private float danoAcumuladoBuff;

    protected override void Start()
    {
        boss = GetComponent<BossCobra>();
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
        pauseMenu.ChamarConclusao();
        
    }

    void AplicarBuff()
    {
    
        Debug.Log("Boss buffado!");
    }
}