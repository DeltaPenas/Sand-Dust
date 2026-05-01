using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{   
   
    public static ProgressionManager Instance;

    public int xpTotal;
    public int level = 1;
    public int xpAtual = 0;
    public int xpParaProximoNivel = 10;
    public int pontosDisponiveis =0;

     [Header("Status Permanentes")]
    public float vidaBonus;
    public float danoRangedBonus;
    public float danoMeleeBonus;
    public float danoSkillBonus;
    public float danoUltBonus;
    public float velocidadeBonus;




    /*
        então, tu vai salvar as seguintes coisas:
        a run com mais salas concluidas e xp total
    
    */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddXPTotal(1);
        }
    }


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddXPTotal(int valor)
    {
        xpTotal +=valor;
        xpAtual +=valor;
        

        if (xpAtual >= xpParaProximoNivel)
        {
            Debug.Log("Upou");
            LevelUp();
            
        }
    }

    public void LevelUp()
    {
        xpAtual -=xpParaProximoNivel;
        level++;
        xpParaProximoNivel +=5;
        pontosDisponiveis++;
    }

    public void Retry()
{
    RunManager.Instance.StartRun();
}


   

    public void AddVida()
    {
        if(pontosDisponiveis <= 0) return;

        vidaBonus+=1;
        pontosDisponiveis--;
    }

    public void AddDanoRanged()
    {
        if(pontosDisponiveis <= 0) return;

        danoRangedBonus+=1;
        pontosDisponiveis--;
    }
    public void AddDanoMelee()
    {
        if(pontosDisponiveis <= 0) return;

        danoMeleeBonus +=1;
        pontosDisponiveis--;
    }
    public void AddDanoSkill()
    {
        if(pontosDisponiveis <= 0) return;

        danoSkillBonus +=1;
        pontosDisponiveis--;
    }
    public void AddDanoUlt()
    {
        if(pontosDisponiveis <= 0) return;
        danoUltBonus+=1;
        pontosDisponiveis--;
    }
    public void AddVelocidade()
    {
        if(pontosDisponiveis <= 0) return;
        velocidadeBonus+=0.5f;
        pontosDisponiveis--;
    }
}