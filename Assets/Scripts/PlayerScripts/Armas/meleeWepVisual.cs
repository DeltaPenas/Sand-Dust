using UnityEngine;

public class meleeWepVisual : MonoBehaviour
{
    

    public Transform rotacaoVisual;
    public SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    private void Update()
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
