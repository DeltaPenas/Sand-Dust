
using UnityEngine;

public class SkillHeal : SkillBase
{
    void Start()
    {
        cooldown +=cooldownBase;
        definirReferencias(); 
    }
    protected override bool TentaUsarSkill()
    {
        if(pv.playerVidaAtual < pv.playerVidaTotal)
        {
            pv.CurarPlayer(1);
            sc.TocarSom(ac);
            
            
            return true;
        }
        else
        {
            return false;
        }


    }

    public void definirReferencias()
    {
        if (pv == null || sc == null || pc == null)
        {
            pv = FindAnyObjectByType<PlayerVida>();
            sc = FindAnyObjectByType<SoundController>();
            pc = FindAnyObjectByType<PlayerController>();
        }
    }
}