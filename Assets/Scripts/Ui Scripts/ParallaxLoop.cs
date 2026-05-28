using UnityEngine;

public class ParallaxLoop : MonoBehaviour
{
    [SerializeField] private Transform spriteA;
    [SerializeField] private Transform spriteB;

    [SerializeField] private float velocidade = 1f;

    private float largura;

    private void Start()
    {
        SpriteRenderer sr =
            spriteA.GetComponent<SpriteRenderer>();

        largura = sr.bounds.size.x;

        spriteA.position = Vector3.zero;

        spriteB.position =
            new Vector3(largura, 0, 0);
    }

    private void Update()
    {
        Vector3 movimento =
            Vector3.left * velocidade * Time.deltaTime;

        spriteA.position += movimento;
        spriteB.position += movimento;

        if (spriteA.position.x <= -largura)
        {
            Reposicionar(spriteA, spriteB);
        }

        if (spriteB.position.x <= -largura)
        {
            Reposicionar(spriteB, spriteA);
        }
    }

    private void Reposicionar(
        Transform saiu,
        Transform outro)
    {
        saiu.position = new Vector3(
            outro.position.x + largura,
            saiu.position.y,
            saiu.position.z
        );
    }
}