using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float velocidade;
    public Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
       Vector2 movimento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
       rig.linearVelocity = movimento.normalized * velocidade;
    }

    
}
