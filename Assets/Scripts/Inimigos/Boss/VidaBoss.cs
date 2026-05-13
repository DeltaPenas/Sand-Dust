using UnityEngine;

public class VidaBoss : Vida
{
    public bool isSegundaFase;


    protected override void AoReceberDano()
    {
        float vidaPersent = vidaAtual/vidaTotal;


        if (!isSegundaFase && vidaPersent <= 0.5f) //significa q ta na metade
        {
            
        }
        
    }

}