using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaTotal = 5;
    public int vidaAtual;


    void Start()
    {
        vidaAtual = vidaTotal;
    }

    public void receberDano(int dano)
    {
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            morrer();
        }
    }

    void morrer()
    {
        Destroy(gameObject);
    }
    
}
