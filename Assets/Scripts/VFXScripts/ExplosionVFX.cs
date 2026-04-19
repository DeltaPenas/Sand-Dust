using UnityEngine;

public class ExplosionVFX : MonoBehaviour
{
    public float tempoVida = 0.5f;
    
    


    void Start()
    {
        Destroy(gameObject, tempoVida);
    }
}