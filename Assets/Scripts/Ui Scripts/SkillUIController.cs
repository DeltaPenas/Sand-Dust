using UnityEngine;

public class SkillUIController : MonoBehaviour
{
    [SerializeField] private SkillsSlotUI dashSlot;
    [SerializeField] private SkillsSlotUI ultSlot;
    [SerializeField] private SkillsSlotUI skillSlot;

    [SerializeField] private DashBase dash;
    [SerializeField] private SkillBase skill;
    [SerializeField] private UltBase ult;
    


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