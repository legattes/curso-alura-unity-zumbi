using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Animator animator;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 direction, float speed)
    {
        rigidbody.MovePosition(rigidbody.position + direction.normalized * speed * Time.deltaTime);
    }

    public void Rotate(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        rigidbody.MoveRotation(rotation);
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
