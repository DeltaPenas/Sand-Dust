
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
   
    public static ProgressionManager Instance;
    [SerializeField] private bool forcarComoPrincipal = false;

    public int xpTotal;
  
    public int xpParaProximoNivel = 10;
    public int pontosDisponiveis =0;

    [Header("Status Permanentes")]

    public int level = 1;
    public int xpAtual = 0;
    public float vidaBonus;
    public float danoRangedBonus;
    public float danoMeleeBonus;
    public float danoSkillBonus;
    public float danoUltBonus;
    public float velocidadeBonus;




    /*
        então, tu vai salvar as seguintes coisas:
        a run com mais salas concluidas e xp total, além dos status
    
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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SaveSystem.LoadProgression(this);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddXPTotal(int valor)
    {
        xpTotal += valor;
        xpAtual += valor;

        while (xpAtual >= xpParaProximoNivel)
        {
            LevelUp();
        }
        SaveSystem.SaveProgression(this);
    }

    public void LevelUp()
    {
        xpAtual -= xpParaProximoNivel;
        level++;
        pontosDisponiveis++;

        xpParaProximoNivel += 5;

        Debug.Log($"Level: {level} | XP Total: {xpTotal}");
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

        danoRangedBonus+= 0.25f;
        pontosDisponiveis--;
    }
    public void AddDanoMelee()
    {
        if(pontosDisponiveis <= 0) return;

        danoMeleeBonus +=0.25f;
        pontosDisponiveis--;
    }
    public void AddDanoSkill()
    {
        if(pontosDisponiveis <= 0) return;

        danoSkillBonus +=0.25f;
        pontosDisponiveis--;
    }
    public void AddDanoUlt()
    {
        if(pontosDisponiveis <= 0) return;
        danoUltBonus+=0.25f;
        pontosDisponiveis--;
    }
    public void AddVelocidade()
    {
        if(pontosDisponiveis <= 0) return;
        velocidadeBonus+=0.5f;
        pontosDisponiveis--;
    }
}