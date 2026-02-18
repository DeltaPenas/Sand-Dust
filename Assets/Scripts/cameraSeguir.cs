using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform alvo;     // Alvo a seguir
    public float smoothSpeed = 0.125f; // Suavidade do movimento
    public Vector3 offset;       // Distância da câmera em relação ao personagem

    void LateUpdate()
    {
        if (alvo == null)
        {
            return;
        }

        Vector3 desiredPosition = alvo.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}