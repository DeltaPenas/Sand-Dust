using UnityEngine;

public class RunInfos : MonoBehaviour
{
    public int inimigosDerrotados;
    public int salasConcluidas;
    public int andar;
    public int layer;
    public int playerScore;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
