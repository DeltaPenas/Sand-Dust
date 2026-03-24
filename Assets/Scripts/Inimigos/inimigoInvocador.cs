using UnityEngine; 
public class InimigoInvocador : MonoBehaviour 
{ 
    [Header("Referências")] 
    public Transform player; 
    private Vida vidaDoPlayer; 
    private Rigidbody2D rb; 

    [Header("Movimento")] 
    public float velocidade = 0.7f;
    public float distanciaParada = 1.5f; // distância necessária para que o inimigo fique parado perto do player 
    
    [Header("Desvio Local")] 
    public float distanciaDeteccaoObstaculo = 1.5f; 
    public LayerMask layerObstaculos; 
     
    [Header("Persistência do Desvio")] 
    public float tempoDesvio = 0.7f; 
    private Vector2 direcaoDesvioAtual; 
    private float fimDesvio;
    [Header("Invocação")]
    public GameObject inimigoPrefab;
    public Transform pontoInvocacao; 
    public int quantidadeInvocada = 1;
    public int maxInimigos = 5;
    private int inimigosAtuais = 0;
    [Header("Cooldown De Invocação")]
    public float intervaloInvocacao = 3f;
    private float proximaInvocacao;
    private void Start() 
    { 
        rb = GetComponent<Rigidbody2D>(); 
       
       if (player == null) 
       { GameObject alvo = GameObject.FindGameObjectWithTag("Player"); 
       if (alvo != null)
            {
            player = alvo.transform;
            }
        } 
       
        if (player != null) 
        {
            vidaDoPlayer = player.GetComponent<Vida>();
        }
        else 
        {
            Debug.LogWarning("Player não encontrado. Verifique a tag 'Player'.");
        }
    } 
    private void Update() 
    { 
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position); 

            if (distancia <= 4f && distancia > distanciaParada && Time.time >= proximaInvocacao)
            {
                Invocar();
                proximaInvocacao = Time.time + intervaloInvocacao;
            }
     
    } 
    private void FixedUpdate() 
    { 
        if (player == null) return; 
        
        float distancia = Vector2.Distance(rb.position, player.position);
        
        if (distancia < 4f && distancia > distanciaParada)// deixa o inimigo parado apartir de certa distancia do player 
        {
            Vector2 direcaoEscolhida = ObterDirecaoDeMovimento();
            
            Vector2 novaPosicao = rb.position + direcaoEscolhida * velocidade * Time.fixedDeltaTime;
              rb.MovePosition(novaPosicao);
        } 
        else
        { // se não estiver loge para de se mover 
        } 
    } 
    private Vector2 ObterDirecaoDeMovimento()
    { 
        // Se ainda estiver em modo de desvio, mantém a direção escolhida 
        if (Time.time < fimDesvio) 
        { 
            return direcaoDesvioAtual; 
        } 
        // Direção principal: do inimigo até o player 
        Vector2 direcaofuga = (rb.position - (Vector2)player.position).normalized;
        
        // Testa se há obstáculo exatamente na frente
        float raio = 0.3f; // tamanho do "corpo" do inimigo 
        RaycastHit2D hitFrente = Physics2D.CircleCast( rb.position, raio, direcaofuga, distanciaDeteccaoObstaculo, layerObstaculos ); // Se não bateu em nada, segue reto 
               if (hitFrente.collider == null) 
                { 
                return direcaofuga; 
                } 

        // Cria duas direções perpendiculares 
        Vector2 direcaoEsquerda = new Vector2(-direcaofuga.y, direcaofuga.x).normalized; 
        Vector2 direcaoDireita = new Vector2(direcaofuga.y, -direcaofuga.x).normalized; 
               
        Vector2 origemEsquerda = rb.position + direcaoEsquerda * 0.2f;
        Vector2 origemDireita = rb.position + direcaoDireita * 0.2f; 
                
        RaycastHit2D hitEsquerda = Physics2D.CircleCast( origemEsquerda, raio, direcaoEsquerda, distanciaDeteccaoObstaculo, layerObstaculos );
        RaycastHit2D hitDireita = Physics2D.CircleCast( origemDireita, raio, direcaoDireita, distanciaDeteccaoObstaculo, layerObstaculos );
        bool esquerdaLivre = hitEsquerda.collider == null; bool direitaLivre = hitDireita.collider == null; // Caso 1: só esquerda está livre 
        if (esquerdaLivre && !direitaLivre) 
        { 
            direcaoDesvioAtual = direcaoEsquerda; 
            fimDesvio = Time.time + tempoDesvio; 
            return direcaoDesvioAtual; 
        } 

        // Caso 2: só direita está livre 
        if (direitaLivre && !esquerdaLivre) 
        { 
            direcaoDesvioAtual = direcaoDireita; 
            fimDesvio = Time.time + tempoDesvio; 
            return direcaoDesvioAtual; 
        }

        // Caso 3: as duas estão livres 
        if (esquerdaLivre && direitaLivre) 
        { 
            float distanciaSeForEsquerda = Vector2.Distance(rb.position + direcaoEsquerda, player.position);
            float distanciaSeForDireita = Vector2.Distance(rb.position + direcaoDireita, player.position);


            direcaoDesvioAtual = distanciaSeForEsquerda > distanciaSeForDireita 
            ? direcaoEsquerda 
            : direcaoDireita; 
            
            fimDesvio = Time.time + tempoDesvio; 
            return direcaoDesvioAtual; 
        } 
        Debug.Log("Frente: " + (hitFrente.collider != null));
        Debug.Log("Esquerda livre: " + esquerdaLivre);
        Debug.Log("Direita livre: " + direitaLivre); 
        Debug.Log("Tudo bloqueado. Inimigo parado.");
        
        // tenta continuar no último desvio antes de desistir 
        if (direcaoDesvioAtual != Vector2.zero)
        { 
            return direcaoDesvioAtual; 
        } 
        return Vector2.zero; 
    }
    void Invocar()
    {
        if (inimigosAtuais >= maxInimigos) return;

        for (int i = 0; i < quantidadeInvocada; i++)
        {
            Vector2 posicaoSpawn;

            if (pontoInvocacao != null)
            {
                posicaoSpawn = pontoInvocacao.position;
            }
            else
            {
                posicaoSpawn = (Vector2)transform.position + Random.insideUnitCircle * 1.5f;
            }

            GameObject novo = Instantiate(inimigoPrefab, posicaoSpawn, Quaternion.identity);
            inimigosAtuais++;
        }
    } 
}