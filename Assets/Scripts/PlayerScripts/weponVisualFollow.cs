using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class weponVisualFollow : MonoBehaviour
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

    Vector2 direction = mousePos - rotacaoVisual.position;

    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    rotacaoVisual.rotation = Quaternion.Euler(0f, 0f, angle);

    Vector3 scale = rotacaoVisual.localScale;

    if (direction.x < 0)
        scale.y = -1;
    else
        scale.y = 1;

    rotacaoVisual.localScale = scale;
}

}
