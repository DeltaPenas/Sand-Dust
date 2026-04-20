using System.Collections;
using UnityEngine;

public class PontoDeDecidaDeSala : MonoBehaviour
{
    private DungeonGeneratortest dg;
    private RunInfos runInfos;
    private TriggerDeTransicao tt;
    
    

    void Start()
    {
        tt = FindAnyObjectByType<TriggerDeTransicao>();
        dg = FindAnyObjectByType<DungeonGeneratortest>();
        runInfos = FindAnyObjectByType<RunInfos>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && dg != null && runInfos != null && runInfos.salasConcluidas >= dg.totalSalasCombate)
        {
            tt.FadeOut();
            Invoke(nameof(PodeDescer), 0.8f);
            dg.LimparInimigos();
            
            
        }
    }
    
    public void PodeDescer()
    {
            dg.LimparDungeon();
            runInfos.layer +=1;
            debugDeSalas();
            if(runInfos.layer > 3)
            {
                runInfos.layer = 0;
                runInfos.andar +=1;
                dg.qtdInimigos =1;
                dg.qtdmaxSalas +=1;
                dg.qtdminSalas +=1;
            }
        dg.qtdInimigos +=1;
        runInfos.salasConcluidas = 0;
    }
    public void debugDeSalas()
    {
        Debug.Log("Inimigos derrotados: " + runInfos.inimigosDerrotados);
        Debug.Log("Salas Concluidas: " + runInfos.salasConcluidas);
        Debug.Log("Andar: " + runInfos.andar);
        Debug.Log("Layer: " + runInfos.layer);
        Debug.Log("PlayerScore: " + runInfos.playerScore);
        
    }

}
