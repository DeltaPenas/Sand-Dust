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
        if (collision.CompareTag("Player") && dg != null && runInfos != null)
        {
            PodeDescer();
        }
    }
    public void PodeDescer()
    {
        if(runInfos.salasConcluidas >= dg.salas.Count)
        {
            dg.LimparDungeon();
            runInfos.layer +=1;
            debugDeSalas();
            if(runInfos.layer > 3)
            {
                runInfos.layer = 0;
                runInfos.andar +=1;
                dg.qtdInimigos =1;
            }
            dg.qtdInimigos +=1;
            
        }else
        {
            Debug.Log("n pode");
        }
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
