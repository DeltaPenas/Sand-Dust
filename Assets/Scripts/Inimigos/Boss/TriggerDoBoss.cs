using UnityEngine;


public class TriggerDoBoss : MonoBehaviour
{
    public FirstBossController firstBossController;

    public void Start()
    {
      firstBossController = FindObjectOfType<FirstBossController>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            firstBossController.IniciarBoss();

            Destroy(gameObject);
        }
    }
}