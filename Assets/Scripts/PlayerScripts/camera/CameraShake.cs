using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 posInicial;

    
    public void ShakeCamera(float duração, float intensidade)
    {
        posInicial = transform.localPosition;
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", duração);
    }

    // treme
    private void DoShake()
    {
        float offsetX = Random.Range(-0.1f, 0.1f);
        float offsetY = Random.Range(-0.1f, 0.1f);
        transform.localPosition = posInicial + new Vector3(offsetX, offsetY, 0);
    }

    // para de tremer
    private void StopShake()
    {
        CancelInvoke("DoShake");
        transform.localPosition = posInicial;
    }
}