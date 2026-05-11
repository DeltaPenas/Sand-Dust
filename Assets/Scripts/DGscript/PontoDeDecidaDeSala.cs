using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PontoDeDecidaDeSala : MonoBehaviour
{
    private DungeonGeneratortest dg;
    private RunInfos runInfos;
    private TriggerDeTransicao tt;
    private PlayerVida pv;
    public GameObject portalAtivo;
    public GameObject portalDesligado;




    void Start()
    {
        tt = FindAnyObjectByType<TriggerDeTransicao>();
        dg = FindAnyObjectByType<DungeonGeneratortest>();
        pv = FindAnyObjectByType<PlayerVida>();
        runInfos = FindAnyObjectByType<RunInfos>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && dg != null && runInfos != null && RunManager.Instance.currentRun.salasConcluidas >= dg.totalSalasCombate)
        {

            tt.FadeOut();
            Invoke(nameof(PodeDescer), 0.8f);
            dg.LimparInimigos();
            if(dg.isDevMode && pv.playerVidaAtual < pv.playerVidaTotal)
            {
                pv.CurarPlayer(1);
            }
            
        }
    }
    public void AtivarPortal()
    {
        portalAtivo.SetActive(true);
        portalDesligado.SetActive(false);
    }
  
    
    public void PodeDescer()
    {
            dg.LimparDungeon();
            RunManager.Instance.currentRun.layer+=1;
       
        if (RunManager.Instance.currentRun.layer >= 5)
        {
            RunManager.Instance.currentRun.layer = 0;
            RunManager.Instance.currentRun.andar +=1;
            dg.qtdInimigos =1;
            dg.qtdmaxSalas+=1;
            dg.qtdminSalas+=1;
        }


        dg.qtdInimigos +=1;
        RunManager.Instance.currentRun.salasConcluidas = 0;
    }
    

}
