using UnityEngine;

public class DashSimples : DashBase
{
    protected override void usadash()
    {
        Vector2 direcao;

        if (pc.movimento != Vector2.zero)
        {
            direcao = pc.movimento.normalized;
        }
        else if (pc.ultimadireção != Vector2.zero)
        {
            direcao = pc.ultimadireção;
        }
        else
        {
            direcao = Vector2.right; // fallback
        }

        pc.rig.linearVelocity = direcao * forçaDash;
    }
}