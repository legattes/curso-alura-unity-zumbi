using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : BaseAnimator
{
    public void Run(float magnitude)
    {
        AnimateFloat("Running", magnitude);
    }
}
