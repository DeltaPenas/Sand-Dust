using UnityEngine; 
using System.Collections;
public class InimigoInvocador : InimigoBase // tem que usar o prefab da unity para a invocação funcionar certinho
{
    [Header("Invocação")]
    public float atrasoAntesDeInvocar = 1.2f;
    public float intervaloEntreInvocados = 0.6f;
    private bool invocando = false;
    public GameObject inimigoPrefab;
    public Transform pontoInvocacao; 
    public int quantidadeInvocada = 1;
    public int maxInimigos = 5;
    private int inimigosAtuais = 0;
    [Header("Cooldown De Invocação")]
    public float intervaloInvocacao = 3f;
    private float proximaInvocacao;  
    [Header("Alcance")]
    public float distanciaDeteccao = 8f;
    public float distanciaFuga = 4f;


    protected override void Start()
    {
        base.Start();
        // Evita que o invocador invoque imediatamente ao iniciar/entrar no alcance.
        proximaInvocacao = Time.time + 1f;
    }

    protected override Vector2 DirecaoBase()
    {
        if (distanciaPlayer < distanciaFuga)
            return (transform.position - player.position).normalized;

        return Vector2.zero;
    }
void InvocarUmInimigo()
{
    if (inimigosAtuais >= maxInimigos) return;

    Vector2 posicaoSpawn;

    if (pontoInvocacao != null)
    {
        posicaoSpawn = (Vector2)pontoInvocacao.position +
                       Random.insideUnitCircle * 1.5f;
    }
    else
    {
        posicaoSpawn = (Vector2)transform.position +
                       Random.insideUnitCircle.normalized * 2f;
    }

    float distanciaMinimaDoPlayer = 2.5f;

    if (Vector2.Distance(posicaoSpawn, player.position) < distanciaMinimaDoPlayer)
    {
        Vector2 direcaoContrariaAoPlayer =
            ((Vector2)transform.position - (Vector2)player.position).normalized;

        posicaoSpawn = (Vector2)transform.position +
                       direcaoContrariaAoPlayer * distanciaMinimaDoPlayer;
    }

    GameObject novo = Instantiate(inimigoPrefab, posicaoSpawn, Quaternion.identity);

    InimigoBase inimigo = novo.GetComponent<InimigoBase>();

    if (inimigo != null)
    {
        inimigo.invocador = this;
    }

    inimigosAtuais++;
}
protected override void Comportamento()
    {
        if (invocando) return;

        if (distanciaPlayer <= distanciaDeteccao &&
            inimigosAtuais <= 0 &&
            Time.time >= proximaInvocacao)
        {
            StartCoroutine(InvocarSequencialmente());
            proximaInvocacao = Time.time + intervaloInvocacao;
        }
    }

private IEnumerator InvocarSequencialmente()
{
    invocando = true;

    // tempo de "preparação" antes da primeira invocação
    yield return new WaitForSeconds(atrasoAntesDeInvocar);

    int quantidadeReal = Mathf.Min(quantidadeInvocada, maxInimigos - inimigosAtuais);

    for (int i = 0; i < quantidadeReal; i++)
    {
        InvocarUmInimigo();

        yield return new WaitForSeconds(intervaloEntreInvocados);
    }

    invocando = false;
}
public void InimigoMorreu()
    {
            inimigosAtuais = Mathf.Max(0, inimigosAtuais - 1); //para evitar puchar numeros negativos nas invocações
    }
}