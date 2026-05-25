using UnityEngine;
using UnityEngine.UI;

public class CameraZoomSlider : MonoBehaviour
{
    public Camera cam;
    public Slider sliderZoom;

    private float zoomDesejado;

void Start()
{
    zoomDesejado = cam.orthographicSize;
    sliderZoom.onValueChanged.AddListener(MudarZoom);
}

void Update()
{
    cam.orthographicSize = Mathf.Lerp(
        cam.orthographicSize,
        zoomDesejado,
        Time.deltaTime * 5f
    );
}

public void MudarZoom(float valor)
{
    zoomDesejado = valor;
}
}