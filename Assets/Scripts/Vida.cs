using UnityEngine;

public class Vida : MonoBehaviour
{
    public int vidaTotal = 5;
    public int vidaAtual;



    private void Start()
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

    private void morrer()
    {
        Destroy(gameObject);
        
    }
    
}
