using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float velocidade;
    public bool podeMover = true;
    public Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
       mover();
    }

    void mover()
    {
        if (podeMover)
        {
            Vector2 movimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rig.linearVelocity = movimento.normalized * velocidade;
            
        }
    }

    
}
