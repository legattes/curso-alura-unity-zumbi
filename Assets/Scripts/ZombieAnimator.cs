using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : BaseAnimator
{
    public void Run(Vector3 direction)
    {
        AnimateFloat("Moving", direction.magnitude);
    }

    public void Attack(bool attack)
    {
        AnimateBool("Attacking", attack);
    }

}
