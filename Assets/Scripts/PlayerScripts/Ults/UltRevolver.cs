using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class UltRevolver : UltBase
{
    //public Transform posPlayer; //usar pra criar um ponto inicial pra ult
    public LayerMask layerInimigos; //seleciona a layer "inimigos", ou algo assim sla
    public GameObject MarkPrefab;
    public AudioClip fireSoundClip;

    protected override bool tentaUsarUlt()
    {
        SoundController soundController = FindAnyObjectByType<SoundController>();

        GameObject mira;

            Collider2D[] hitAlvosUlt = Physics2D.OverlapCircleAll(
                transform.position,
                ultRange,
                layerInimigos
            );

            if (hitAlvosUlt.Length == 0)
            {
                return false;
            }

            StartCoroutine(atirarCadenciado());
            return true;
       
        IEnumerator atirarCadenciado()
        {

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
                Camera.main.GetComponent<CameraShake>().ShakeCamera(0.2f, 0.2f);
                
                Destroy(mira);
            }
            
        }
 
    }

    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ultRange);
    }

}