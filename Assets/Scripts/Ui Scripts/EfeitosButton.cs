using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EfeitosButton : MonoBehaviour,
    IPointerEnterHandler, 
    IPointerExitHandler
{
    private Vector3 tamanhoOriginal;
    [SerializeField] private float escalaAumentada = 1.2f;
    [SerializeField] private TextMeshProUGUI texto;


    void Start()
    {
        tamanhoOriginal = transform.localScale;
    }

    //entrou
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = tamanhoOriginal * escalaAumentada;
        texto.color = Color.yellow;
    }

    //saiu
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = tamanhoOriginal;
        texto.color = Color.white;
    }
    
    public void OnDisable()
    {
        transform.localScale = tamanhoOriginal;
        texto.color = Color.white;
    }
}
