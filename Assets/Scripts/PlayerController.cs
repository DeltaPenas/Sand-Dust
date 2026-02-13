using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float velocidade;
    public bool podeMover = true;
    public float forçadash = 20;
    public Vector2 movimento;
    public Rigidbody2D rig;

    public Vector2 ultimadireção;
    private bool querDash;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movimento != Vector2.zero)
        {
            ultimadireção = movimento.normalized;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            querDash = true;
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



}
