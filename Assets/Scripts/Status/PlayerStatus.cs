using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
    [Header("Basico")]
    public float vidaMax;
    public float velocidade;

    public float danoMelee;
    public float danoRanged;
    public float atqCooldown;

    [Header("Skill")]
    public float danoSkill;
    public float rangeSkill;
    public float cooldownSkill;

    [Header("Ultimate")]

    public float danoUlt;
    public float rangeUlt;
    public float cooldownUlt;

    public PlayerStatus Clone()
{
    return new PlayerStatus
    {
        vidaMax = vidaMax,
        velocidade = velocidade,
        danoMelee = danoMelee,
        danoRanged = danoRanged,
        danoSkill = danoSkill,
        rangeSkill = rangeSkill,
        cooldownSkill = cooldownSkill,
        danoUlt = danoUlt,
        rangeUlt = rangeUlt,
        cooldownUlt = cooldownUlt,
        atqCooldown = atqCooldown
    };
}
    public enum StatsType
    {
        vidaMax,
        velocidade,
        danoMelee,
        danoRanged,
        danoSkill,
        rangeSkill,
        cooldownSkill,
        danoUlt,
        rangeUlt,
        cooldownUlt,
        atqCooldown
        
    }
}
