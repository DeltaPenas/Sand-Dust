using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashLuz : MonoBehaviour
{
    [SerializeField] private Light2D luz;

    [SerializeField] private float velocidade = 20f;

    void Update()
    {
        luz.intensity -= velocidade * Time.deltaTime;

        if(luz.intensity <= 0)
        {
            Destroy(gameObject);
        }
    }
}