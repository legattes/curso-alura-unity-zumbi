using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : BaseAnimator
{
    public void Attack(bool attack)
    {
        AnimateBool("Attack", attack);
    }

    public void Walk(float direction)
    {
        AnimateFloat("Movement", direction);
    }

    public void Die()
    {
        AnimateTrigger("Death");
    }
}
