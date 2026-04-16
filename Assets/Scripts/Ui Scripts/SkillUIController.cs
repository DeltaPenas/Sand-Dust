using UnityEngine;

public class SkillUIController : MonoBehaviour
{

    [Header("Slots")]
    [SerializeField] private SkillsSlotUI dashSlot;
    [SerializeField] private SkillsSlotUI ultSlot;
    [SerializeField] private SkillsSlotUI skillSlot;
    [Header("Habilidades")]
    [SerializeField] private DashBase dash;
    [SerializeField] private SkillBase skill;
    [SerializeField] private UltBase ult;
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

    }

    void Update()
    {
        float dashRestante = Mathf.Max(0, dash.cooldown - (Time.time - dash.ultimoUso));
        float skillRestante = Mathf.Max(0, skill.cooldown - (Time.time - skill.ultimoUso));
        float ultRestante = Mathf.Max(0, ult.ultCooldown - (Time.time - ult.ultimoUsoUlt));

        dashSlot.UpdateCooldown(dashRestante, dash.cooldown);
        skillSlot.UpdateCooldown(skillRestante, skill.cooldown);
        ultSlot.UpdateCooldown(ultRestante, ult.ultCooldown);
    }
}