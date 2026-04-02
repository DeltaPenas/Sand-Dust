using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class UltRevolver : UltBase
{
    public Transform posPlayer; //usar pra criar um ponto inicial pra ult
    public LayerMask layerInimigos; //seleciona a layer "inimigos", ou algo assim sla
    public WepAtaque wep;
    public GameObject ultProjetilPrefab;
    public GameObject MarkPrefab;
    public AudioClip fireSoundClip;
    public PlayerController pc;

    protected override void tentaUsarUlt()
    {
        SoundController soundController = FindAnyObjectByType<SoundController>();
        wep.taRanged = true;
        wep.rangedWep.SetActive(true);
        wep.meleeWep.SetActive(false);

        StartCoroutine(atirarCadenciado());
        
        
        IEnumerator atirarCadenciado()
        {
            GameObject mira;

            Collider2D[] hitAlvosUlt = Physics2D.OverlapCircleAll(
                posPlayer.position,
                ultRange,
                layerInimigos
            );
            
                foreach (Collider2D alvos in hitAlvosUlt)
            {
                
                Vector2 posAlvos = alvos.transform.position;
                mira = Instantiate(MarkPrefab, posAlvos, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                
                Vida vida = alvos.GetComponent<Vida>();
                if(vida != null)
                    {
                        vida.receberDano(ultDmg);
                    }
                soundController.TocarSom(fireSoundClip);
                
                Destroy(mira);
            }
            
            

        }

        
       

        
        
    }

    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(posPlayer.position, ultRange);
    }

}