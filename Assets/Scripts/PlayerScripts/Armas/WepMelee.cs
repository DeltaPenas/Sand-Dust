using UnityEngine;

public class WepMelee : MonoBehaviour
{
    public float meleeRange = 1f;
    public LayerMask layerInimigos;
    public AudioClip meleeSoundEffect;
    public Transform pontoAtaque;

    private float tempoProximoMelee;
    private PlayerController pc;
    private SoundController soundController;

    void Start()
    {
        pc = GetComponent<PlayerController>();
        soundController = GetComponent<SoundController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && pc.movimento.magnitude == 0)
        {
            if (Time.time >= tempoProximoMelee)
            {
                Atacar();
                tempoProximoMelee = Time.time + pc.currentStatus.atqCooldown;
            }
        }
    }

    void Atacar()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(
            pontoAtaque.position,
            meleeRange,
            layerInimigos
        );

        foreach (Collider2D alvo in hit)
        {
            Vida vida = alvo.GetComponent<Vida>();
            if (vida != null)
            {
                vida.receberDano(pc.currentStatus.danoMelee);
            }
        }

        pc.anim.SetTrigger("attack");
        soundController.TocarSom(meleeSoundEffect);
    }
}