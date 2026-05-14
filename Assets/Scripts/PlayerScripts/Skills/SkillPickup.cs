using TMPro;
using UnityEngine;

public class SkillPickup : MonoBehaviour
{
    public GameObject skillPrefab;
    public TextMeshProUGUI nomeSkill;
    public TextMeshProUGUI descricaoSkill;
    public TextMeshProUGUI valor; 
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SkillManager sm = collision.GetComponent<SkillManager>();

            sm.EquiparSkill(skillPrefab);

            Destroy(gameObject);
        }
    }
}