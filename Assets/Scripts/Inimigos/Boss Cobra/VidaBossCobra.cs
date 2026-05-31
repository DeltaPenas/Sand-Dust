using UnityEngine;

public class VidaBossCobra : Vida
{
    [Header("Fase/Buff")]


    [SerializeField] private BossCobra boss;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private float vidaParaTrocaDeFase;
    

    private float danoAcumuladoBuff;

    protected override void Start()
    {
        boss = GetComponent<BossCobra>();
        pauseMenu = FindAnyObjectByType<PauseMenu>();


        vidaAtual = vidaTotal;
        vidaParaTrocaDeFase = vidaTotal/2;
    }

    protected override void AoReceberDano()
    {
        if (vidaAtual <= 0)
        {
            boss.TrocarEstado(BossState.Morreu);
        }
        if (vidaAtual <= vidaParaTrocaDeFase && boss.currentState == BossState.FaseUm)
        {
            boss.EntrarNaFaseDois();
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