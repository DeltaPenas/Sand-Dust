using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TesteBuff : MonoBehaviour
{
    private PlayerController pc;
    void Start()
    {
        pc = FindAnyObjectByType<PlayerController>();
        Debug.Log(pc);
    }


    public void TestarBuff()
    {
        Debug.Log("o pc esta" + pc);
        StatModifier buff = new StatModifier
        {
            stat = PlayerStatus.StatsType.danoSkill,
            valor = 0.5f, // 
            ehporcentagem = true
        };

        pc.AddModifier(buff);

        Debug.Log(" Novo dano: " + pc.currentStatus.danoSkill);
    }
}
