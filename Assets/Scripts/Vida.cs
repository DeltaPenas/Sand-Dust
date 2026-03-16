using UnityEngine;

public class Vida : MonoBehaviour
{
    public float vidaTotal = 5;
    public float vidaAtual;



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
