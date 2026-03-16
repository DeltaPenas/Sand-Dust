using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class UltRevolver : UltBase
{
    public Transform posPlayer; //usar pra criar um ponto inicial pra ult
    public LayerMask layerInimigos; //seleciona a layer "inimigos", ou algo assim sla
    public WepAtaque wep;
    public GameObject ultProjetilPrefab;
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
            Vector2 posAlvos = alvos.bounds.center; //Vector2 posAlvos = alvos.transform.position;
            Vector2 direcaoUlt = (posAlvos - (Vector2)posPlayer.position).normalized;
            
            atirarProjetil(posAlvos, direcaoUlt);
        }

        void atirarProjetil(Vector2 alvoPos, Vector2 direcaoAlvos)
        {
            GameObject projetil = Instantiate(ultProjetilPrefab, posPlayer.position, Quaternion.identity);
            RevolverUltProjetil scriptProjetil = projetil.GetComponent<RevolverUltProjetil>();

            scriptProjetil.definirDireção(direcaoAlvos);
            scriptProjetil.ult = this;

        }

        IEnumerator()
        
    }

    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(posPlayer.position, ultRange);
    }

}
