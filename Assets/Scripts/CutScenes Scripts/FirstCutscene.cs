using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FirstCutscene : MonoBehaviour
{

    [Header("Objetos da cena")]
    
    [SerializeField] private FadeController fade;
    [SerializeField] private GameObject carro;
    [SerializeField] private GameObject primeiroCenario;
    [SerializeField] private GameObject mili;
    [SerializeField] private float velocidadeMili = 3;
    [SerializeField] private AudioClip somDePassos;
    public float intervaloPassos = 0.8f;
    private float timerPassos;
    public bool miliPodeMover;

    void Start()
    {
        fade = FindAnyObjectByType<FadeController>();
        TirarOFade();

        Invoke(nameof(AtivarMovimento), 0.5f);
        Invoke(nameof(DesativarMovimento), 5.5f);
        Invoke(nameof(ChamarOFade), 5.6f);
        Invoke(nameof(ChamarSegundoCenario),6.5f);
        Invoke(nameof(TirarOFade),6.5f);
        
        //Invoke(nameof(IniciarRun), 6.9f);
    }

    
    void Update()
    {
        if (miliPodeMover)
        {
           moverMili(); 
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RunManager.Instance.StartRun();
        }
    }


    void AtivarMovimento()
    {
        miliPodeMover = true;
    }
    void DesativarMovimento()
    {
        miliPodeMover = false;
    }

    
    void moverMili()
    {
        mili.transform.position += Vector3.right * velocidadeMili * Time.deltaTime;

        timerPassos -= Time.deltaTime;

        if (timerPassos <= 0f)
        {
            SoundController.instance.TocarSom(somDePassos);
            timerPassos = intervaloPassos;
        }
    }

    void ChamarOFade()
    {
        fade.ChamarFade();
    }
    void TirarOFade()
    {
        fade.TirarFade();
    }
    void ChamarSegundoCenario()
    {
        primeiroCenario.SetActive(false);
    }
    void IniciarRun()
    {
        RunManager.Instance.StartRun();
    }

    

   
}
