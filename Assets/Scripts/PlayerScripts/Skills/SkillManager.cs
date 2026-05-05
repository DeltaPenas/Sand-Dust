using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private SkillBase skillAtual;
    public SkillBase SkillAtual => skillAtual;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UsarSkill();
            
        }
    }

    public void EquiparSkill(GameObject skillPrefab)
    {
        //remove skill antiga
        if (skillAtual != null)
        {
            Destroy(skillAtual.gameObject);
        }
        //instanciar nova skill como filha do player

        GameObject novaSkill = Instantiate(skillPrefab, transform);

        skillAtual = novaSkill.GetComponent<SkillBase>();
    }
    public void UsarSkill()
    {
        if (skillAtual == null)return;

        skillAtual.tentaUsar();
    }
    

}