using UnityEngine;

public class ExplosionVFX : MonoBehaviour
{
    public float tempoVida = 0.5f;
    public BombProjetil bmbp;


    void Start()
    {
        Destroy(gameObject, tempoVida);
    }
}