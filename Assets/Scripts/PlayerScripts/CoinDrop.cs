using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    [SerializeField] private PlayerController pc;
    [SerializeField] private int valorGem;
    [SerializeField] private AudioClip ac;

    void Start()
    {
        pc = FindAnyObjectByType<PlayerController>();
    }



    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            FoiPego();
        }
            
    }



    void FoiPego()
    {
        SoundController soundController = FindAnyObjectByType<SoundController>();
        pc.gems += valorGem;

        soundController.TocarSom(ac);
        Destroy(gameObject);
    }

    
}
