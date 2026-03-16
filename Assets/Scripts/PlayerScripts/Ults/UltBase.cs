using Unity.VisualScripting;
using UnityEngine;

public abstract class UltBase : MonoBehaviour
{
    public float ultRange;
    public int ultDmg;
    public float ultCooldown;
    public float ultimoUso;
    
    public virtual bool podeUsarUlt()
    {
        if(Time.time < ultimoUso + ultCooldown) return false;

        return true;
    }

    public void tentaUsar()
    {
        if (podeUsarUlt())
        {
            tentaUsarUlt();
            ultimoUso = Time.time;
        }
    }

    protected abstract void tentaUsarUlt();
}
