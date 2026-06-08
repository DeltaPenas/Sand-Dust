using UnityEngine;

public class Wisp : MonoBehaviour
{
    public float velocidadeRotacao;
    public float distancia = 1.5f;
    public float dano;
    public float angulo;
    public Transform player;
    public SkillWisp skillWisp;



    private void Start()
    {
        skillWisp = FindAnyObjectByType<SkillWisp>();
        Destroy(gameObject, 30f);
        dano = skillWisp.skillDmg;
    }

    private void Update()
    {
        if(player == null) return;

        angulo += velocidadeRotacao * Time.deltaTime;
        
        float rad = angulo * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(rad),
            Mathf.Sin(rad),
            0
        ) * distancia;

        transform.position = player.position + offset;
    }

    public void DefinirAlvo(Transform alvo)
    {
        player = alvo;
    }


}