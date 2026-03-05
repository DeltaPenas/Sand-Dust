using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{
    
    public float velocidade;
    public bool podeMover = true;
    public float forçadash = 20;
    public Vector2 movimento;
    public Rigidbody2D rig;
    public BoxCollider2D boxCollider2D;
    public float cooldownDash = 0;
    public float intervaloDash;
    public Vector2 ultimadireção;
    private bool querDash;
    public float iframetempo = 0.3F;

    public float iframeTempoBuff = 0;
    private bool iframeAtivo = false;





    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        if(cooldownDash > 0)
        {
           cooldownDash -= Time.deltaTime; 
        }


        movimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movimento != Vector2.zero)
        {
            ultimadireção = movimento.normalized;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && cooldownDash <= 0)
        {
            querDash = true;
            StartCoroutine(Iframe());

            cooldownDash = intervaloDash;
        }

    }

    void FixedUpdate()
    {
        mover();

        if (querDash)
        {
           dash();
           querDash = false; 
           
           
        }
        
    }

    void mover()
    {
        rig.linearVelocity = movimento * velocidade;
    }

    void dash()
    {
        
        
        if (movimento != Vector2.zero)
        {
            rig.AddForce(movimento.normalized * forçadash, ForceMode2D.Impulse);
        }
        else if (ultimadireção != Vector2.zero)
        {
             rig.AddForce(ultimadireção * forçadash, ForceMode2D.Impulse);
        }
        else
        {
            rig.AddForce(Vector2.up * forçadash, ForceMode2D.Impulse);
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("inimigo") && iframeAtivo)
    {
        Debug.Log("não colidiu");
    }

    if (collision.gameObject.CompareTag("inimigo") && !iframeAtivo)
    {
        Debug.Log("colidiu");
    }
}

    IEnumerator Iframe()
    {
        iframeAtivo = true;
        boxCollider2D.enabled = false;

        Debug.Log("ativou o iframe");

        yield return new WaitForSeconds(iframetempo + iframeTempoBuff);

        iframeAtivo = false;
        boxCollider2D.enabled = true;

        Debug.Log("desativou o iframe");


    }


}
