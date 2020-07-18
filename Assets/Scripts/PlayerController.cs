using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController, IKillable
{
    public LayerMask GroundMask;
    public GameObject GameOverText;
    public InterfaceController interfaceController;
    
    private Vector3 direction;
    public CharacterStatus status;
    
    private void Start()
    {
        status = GetComponent<CharacterStatus>();
    } 

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        direction = new Vector3(x,0,z);

        AnimateFloat("Running", direction.magnitude);
    }

    void FixedUpdate()
    {
        Move(direction, status.Speed);

        RotateWithMouse();
    }

    public void TakeDamage(int damage)
    {
        status.Life -= damage;

        interfaceController.UpdateLifeSlider();

        if(status.Life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        interfaceController.GameOver();

    }

    void RotateWithMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit impact;

        if (Physics.Raycast(ray, out impact, 100, GroundMask))
        {
            Vector3 crosshairDiff = impact.point - transform.position;
            crosshairDiff.y = transform.position.y;

            Rotate(crosshairDiff);
        }
    }
}
