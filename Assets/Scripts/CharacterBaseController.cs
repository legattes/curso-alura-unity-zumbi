using UnityEngine;

public class CharacterBaseController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 direction, float speed)
    {
        rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);
    }

    public void Rotate(Vector3 direction, int yOffset = 0)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        if(yOffset != 0)
        {
            rotation *= Quaternion.Euler(0, yOffset, 0);
        }
        rb.MoveRotation(rotation);
    }

    public void AnimateBool(string animation, bool value)
    {
        animator.SetBool(animation, value);
    }

    public void AnimateFloat(string animation, float value)
    {
        animator.SetFloat(animation, value);
    }
}
