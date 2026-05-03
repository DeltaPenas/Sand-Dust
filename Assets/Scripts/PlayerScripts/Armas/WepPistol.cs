using UnityEngine;

public class WepPistol : MonoBehaviour
{
    public GameObject prefabTiro;
    public Transform pontoInicialDoTiro;
    public AudioClip fireSoundEffect;

    private float tempoProximoTiro;
    private PlayerController pc;
    private SoundController soundController;

    void Start()
    {
        pc = GetComponentInParent<PlayerController>();
        soundController = pc.soundController;
    }

    void Update()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;

        Vector2 direcao = (mouse - pontoInicialDoTiro.position).normalized;

        pc.anim.SetFloat("mousePosHorizontal", direcao.x);
        pc.anim.SetFloat("mousePosVertical", direcao.y);

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= tempoProximoTiro)
            {
                Atirar(direcao);
                tempoProximoTiro = Time.time + pc.currentStatus.atqCooldown;
            }
        }
    }

    void Atirar(Vector2 direcao)
    {
        GameObject projetil = Instantiate(prefabTiro, pontoInicialDoTiro.position, Quaternion.identity);
        Projetil proj = projetil.GetComponent<Projetil>();
        proj.Inicializar(direcao, pc);

        soundController.TocarSom(fireSoundEffect);
        //pc.anim.SetTrigger("attack"); nao usar
    }
}