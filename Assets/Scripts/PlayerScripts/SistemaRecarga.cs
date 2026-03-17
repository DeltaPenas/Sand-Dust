using UnityEngine;

public class SistemaRecarga : MonoBehaviour
{
    [Header("Configurações")]
    public int municaoMaxima = 20;
    public int municaoAtual;
    
    void Start()
    {
        municaoAtual = municaoMaxima;
    }
    
    public void Recarregar()
    {
        if (municaoAtual < municaoMaxima)
        {
            municaoAtual = municaoMaxima;
            Debug.Log("Arma Recarregada! Munição: " + municaoAtual + "/" + municaoMaxima);
        }
        else
        {
            Debug.Log("Arma já está cheia!");
        }
    }
    
    // Método útil para verificar se pode atirar
    public bool PodeAtirar()
    {
        return municaoAtual > 0;
    }
    
    // Método para gastar munição
    public void GastarMunicao()
    {
        if (municaoAtual > 0)
        {
            municaoAtual--;
        }
    }
}