using UnityEngine;

public class Vida : MonoBehaviour
{
    public float vidaTotal = 5;
    public float vidaAtual;
    public bool morreu;
    
    

    private InimigoController ic;

    private void Start()
    {
        ic = GetComponent<InimigoController>();
        vidaAtual = vidaTotal;
    }

    public void receberDano(float dano)
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
            Invoke("morrer", 3f);
            return;
        }
        morrer(); //gambiarra pra separar inimigos de props
    }
    }

    private void morrer()
    {
        Destroy(gameObject, 0.1f);
    }
}