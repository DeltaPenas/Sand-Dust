using UnityEngine;
using UnityEngine.UI;

public class SkillsSlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image cooldownOverlay;


    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    public void UpdateCooldown(float atual, float max)
    {
        if (atual <= 0)
        {
            cooldownOverlay.fillAmount = 0;
            return;
        }
        cooldownOverlay.fillAmount = atual/max;
    }
   
}
