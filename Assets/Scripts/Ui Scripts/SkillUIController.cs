using Unity.VisualScripting;
using UnityEngine;

public class SkillUIController : MonoBehaviour
{

    [Header("Slots")]
    [SerializeField] private UnityEngine.UI.Image skillIcon;
    [SerializeField] private UnityEngine.UI.Image ultIcon;
    [SerializeField] private SkillsSlotUI dashSlot;
    [SerializeField] private SkillsSlotUI ultSlot;
    [SerializeField] private SkillsSlotUI skillSlot;
    [Header("Habilidades")]
    [SerializeField] private DashBase dash;
    private SkillManager skillManager;
    private UltManager ultManager;
    
    [Header("Objeto")]
    [SerializeField] private GameObject dashFrame;
    [SerializeField] private GameObject ultFrame;
    [SerializeField] private GameObject skillFrame;
    [SerializeField] private GameObject lifeFrame;
    

    
        void Start()
    {
        dashFrame.SetActive(true);
        skillFrame.SetActive(true);
        ultFrame.SetActive(true);
        lifeFrame.SetActive(true);

        skillManager = FindAnyObjectByType<SkillManager>();
        ultManager = FindAnyObjectByType<UltManager>();

        if (ultManager == null)
        {
            return;
        }

        if (skillManager == null)
        {
            
            return;
        }

        skillManager.OnSkillChanged += AtualizarSkillUI;
        ultManager.OnUltChanged += AtualizarUltUI;

    
    }

    void Update()
{
    float dashRestante = Mathf.Max(0, dash.cooldown - (Time.time - dash.ultimoUso));

    float skillRestante = 0f;
    float skillCooldown = 1f;

    float ultRestante = 0f;
    float ultCooldown = 1f;

    if (skillManager != null && skillManager.SkillAtual != null)
        {
            SkillBase skill = skillManager.SkillAtual;

            skillRestante = Mathf.Max(0, skill.cooldown - (Time.time - skill.ultimoUso));
            skillCooldown = skill.cooldown;
        }

        if (ultManager != null && ultManager.UltAtual != null)
        {
            UltBase ult = ultManager.UltAtual;

            ultRestante = Mathf.Max(0, ult.ultCooldown - (Time.time - ult.ultimoUsoUlt));
            ultCooldown = ult.ultCooldown;
        }

    dashSlot.UpdateCooldown(dashRestante, dash.cooldown);
    skillSlot.UpdateCooldown(skillRestante, skillCooldown);
    ultSlot.UpdateCooldown(ultRestante, ultCooldown);
}
    void AtualizarSkillUI(SkillBase skill)
    {
        
        if (skill == null)
        {
            
            return;
        }

        if (skill.skillIcone == null)
        {
            
            return;
        }

        skillSlot.SetIcon(skill.skillIcone);
    }
    void AtualizarUltUI(UltBase ult)
    {
    if (ult == null)
    {
        return;
    }

    if (ult.ultIcone == null)
    {
        return;
    }

    ultSlot.SetIcon(ult.ultIcone);
    }

}