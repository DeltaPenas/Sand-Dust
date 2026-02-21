using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform alvo;     
    public float smoothSpeed = 0.125f; 
    public Vector3 offset;    

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