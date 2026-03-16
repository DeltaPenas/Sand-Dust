using UnityEngine;

public class UltRevolver : UltBase
{
    public Transform posPlayer; //usar pra criar um ponto inicial pra ult
    public LayerMask layerInimigos; //seleciona a layer "inimigos", ou algo assim sla
    public WepAtaque wep;
    protected override void tentaUsarUlt()
    {
        //saca a arma automaticamente, dps fazer uma função bonitinha pra isso
        wep.taRanged = true;
        wep.rangedWep.SetActive(true);
        wep.meleeWep.SetActive(false);
        
        Collider2D[] hitAlvosUlt = Physics2D.OverlapCircleAll(
            posPlayer.position,
            ultRange,
            layerInimigos
        );

        foreach (Collider2D alvos in hitAlvosUlt)
        {
            
            
            
            /*
            Vida vida = alvos.GetComponent<Vida>();
            if(vida != null)
            {
                vida.receberDano(ultDmg);
            }
            */
        }
        
    }

    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(posPlayer.position, ultRange);
    }

}
