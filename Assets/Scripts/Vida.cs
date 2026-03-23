using UnityEngine;

public class Vida : MonoBehaviour
{
    public float vidaTotal = 5;
    public float vidaAtual;
    public bool isEnvenenado;
    public bool isQueimando;


    private void Start()
    {
        vidaAtual = vidaTotal;
    }

    public void receberDano(int dano)
    {
        vidaAtual -= dano;
        Debug.Log(gameObject.name + " recebeu dano. Vida atual: " + vidaAtual);
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
