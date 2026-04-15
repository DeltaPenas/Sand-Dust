using UnityEngine;

public class Vida : MonoBehaviour
{
    public float vidaTotal = 5;
    public float vidaAtual;
    private bool morreu;

    private InimigoPerseguidor ip;

    private void Start()
    {
        ip = GetComponent<InimigoPerseguidor>();
        vidaAtual = vidaTotal;
    }

    public void receberDano(int dano)
    {
    if (morreu)
        return;

    vidaAtual -= dano;

    Debug.Log(gameObject.name + " recebeu dano. Vida atual: " + vidaAtual);

    if (vidaAtual <= 0)
    {
        morreu = true;

        if (ip != null)
        {
            ip.contabilizarPerda();
        }

        morrer();
    }
    }

    private void morrer()
    {
        Destroy(gameObject);
    }
}