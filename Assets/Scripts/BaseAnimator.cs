using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnimator : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimateBool(string animation, bool value)
    {
        animator.SetBool(animation, value);
    }

    public void AnimateFloat(string animation, float value)
    {
        animator.SetFloat(animation, value);
    }

    public void AnimateTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
