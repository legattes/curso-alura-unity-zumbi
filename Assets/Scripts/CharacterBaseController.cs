using UnityEngine;

public class CharacterBaseController : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
}
