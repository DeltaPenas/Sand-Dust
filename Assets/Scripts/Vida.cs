using UnityEngine;

public class Vida : MonoBehaviour
{
    public float vidaTotal = 5;
    public float vidaAtual;
    private bool morreu;

    private InimigoController ic;

    private void Start()
    {
        ic = GetComponent<InimigoController>();
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

        

        if (ic != null)
        {
            ic.contabilizarPerda();
            ic.inimigoMorrendo();
            Invoke("morrer", 1f);
            return;
        }
        morrer(); //gambiarra pra separar inimigos de props
    }
    }

    private void morrer()
    {
        Destroy(gameObject);
    }
}