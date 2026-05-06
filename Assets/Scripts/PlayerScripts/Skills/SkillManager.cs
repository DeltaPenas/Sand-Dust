using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private SkillBase skillAtual;
    public SkillBase SkillAtual => skillAtual;
    public System.Action<SkillBase> OnSkillChanged;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UsarSkill();
            
        }
    }

    public void EquiparSkill(GameObject skillPrefab)
    {
        if (skillAtual != null)
        {
            Destroy(skillAtual.gameObject);
        }

        GameObject novaSkill = Instantiate(skillPrefab, transform);
        skillAtual = novaSkill.GetComponent<SkillBase>();

        Debug.Log("Skill equipada: " + skillAtual.name); // DEBUG

        OnSkillChanged?.Invoke(skillAtual); 
    }
    public void UsarSkill()
    {
        if (skillAtual == null)return;

        skillAtual.tentaUsar();
    }
    

}