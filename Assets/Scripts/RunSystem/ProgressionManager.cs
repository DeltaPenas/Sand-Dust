using UnityEditor;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance;

    public int xpTotal;
    /*
        então, tu vai salvar as seguintes coisas:
        a run com mais salas concluidas e xp total
    
    */
    

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
        Debug.Log("Xp total: "+ xpTotal);
    }

    public void Retry()
{
    RunManager.Instance.StartRun();
}

}