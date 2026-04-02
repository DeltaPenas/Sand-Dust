using UnityEngine;

public abstract class UltBase : MonoBehaviour
{
    public float ultRange;
    public int ultDmg;
    public float ultCooldown;
    public float ultimoUsoUlt;
    public SoundController sc;

    private void Start()
{
    ultimoUsoUlt = -ultCooldown;
}
    public virtual bool podeUsarUlt()
    {
        if(Time.time < ultimoUsoUlt + ultCooldown) return false;

        return true;
    }

    public void tentaUsar()
    {
        if (podeUsarUlt())
        {
            tentaUsarUlt();
            ultimoUsoUlt = Time.time;
        }
    }

    protected abstract void tentaUsarUlt();
}