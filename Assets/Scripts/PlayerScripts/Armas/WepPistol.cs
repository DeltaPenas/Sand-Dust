using UnityEngine;

public class WepPistol : MonoBehaviour
{
    [Header("Referências")]
    public GameObject prefabTiro;
    public Transform pontoInicialDoTiro;
    public AudioClip fireSoundEffect;

    private float tempoProximoTiro;

    private PlayerController pc;
    private SoundController soundController;

    void Start()
    {
        pc = GetComponentInParent<PlayerController>();
        soundController = FindAnyObjectByType<SoundController>();
       
    }

    void Update()
    {
        if (pc == null) return;

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;

        Vector2 direcao =
            (mouse - pontoInicialDoTiro.position).normalized;

        
        pc.anim.SetFloat("mousePosHorizontal", direcao.x);
        pc.anim.SetFloat("mousePosVertical", direcao.y);

        
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= tempoProximoTiro)
            {
                Atirar(direcao);

                tempoProximoTiro =
                    Time.time + pc.currentStatus.atqCooldown;
            }
        }
    }

    void Atirar(Vector2 direcao)
    {
        if (prefabTiro == null) return;

        GameObject projetil = Instantiate(
            prefabTiro,
            pontoInicialDoTiro.position,
            Quaternion.identity
        );

        Projetil proj = projetil.GetComponent<Projetil>();

        float dano = pc.currentStatus.danoRanged;
        if (proj != null)
        {
            proj.Inicializar(direcao, dano);
        }
        
        if (soundController != null)
        {
            soundController.TocarSom(fireSoundEffect);
        }
        

        
    }
}

