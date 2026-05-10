
using UnityEngine;

public class SkillHeal : SkillBase
{
    protected override bool TentaUsarSkill()
    {
        if(pv.playerVidaAtual <= pv.playerVidaTotal)
        {
            pv.CurarPlayer(1);
            return true;
        }
        else
        {
            return false;
        }


    }
}