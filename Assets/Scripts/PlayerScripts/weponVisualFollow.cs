using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class weponVisualFollow : MonoBehaviour
{
    public Transform rotacaoVisual;
    public SpriteRenderer sp;
   
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    
    void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - rotacaoVisual.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rotacaoVisual.rotation = Quaternion.Euler(0f, 0f, angle);

        if (mousePos.x < 0)
        {
            sp.flipY = true;
        }
        else if (mousePos.x > 0)
        {
            sp.flipY = false;
        }
        
        
    }

}
