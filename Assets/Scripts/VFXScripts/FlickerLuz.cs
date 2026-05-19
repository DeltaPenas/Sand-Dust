using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickerLuz : MonoBehaviour
{
    [SerializeField] private Light2D luz;

    [SerializeField] private float intensidadeMin = 0.0f;
    [SerializeField] private float intensidadeMax = 0.0f;

    void Update()
    {
        luz.intensity = Random.Range(intensidadeMin, intensidadeMax);
    }
}