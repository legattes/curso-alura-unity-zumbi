using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBaseController, IKillable, IHealeable
{
    public LayerMask GroundMask;
    public FixedJoystick MoveJoystick, ShootJoystick;
    public InterfaceController interfaceController;
    public CharacterStatus status;

    private Vector3 direction;
    private WeaponController weapon;
    public int initialShootingSpeed = 20;
    private int shootingSpeed;
    
    private void Start()
    {
        status = GetComponent<CharacterStatus>();
        weapon = GetComponent<WeaponController>();
        shootingSpeed = initialShootingSpeed;
    } 

    void Update()
    {
        float x = MoveJoystick.Horizontal;
        float z = MoveJoystick.Vertical;

       // float x = Input.GetAxis("Horizontal");
       // float z = Input.GetAxis("Vertical");
        
        direction = new Vector3(x, 0, z);

        AnimateFloat("Running", direction.magnitude);
    }

    void FixedUpdate()
    {
        Move(direction, status.Speed);
        
        RotateAndShoot();
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

    /*void RotateWithMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit impact;

        if (Physics.Raycast(ray, out impact, 100, GroundMask))
        {
            Vector3 crosshairDiff = impact.point - transform.position;
            crosshairDiff.y = transform.position.y;

            Rotate(crosshairDiff);
        }
    }*/

    void RotateAndShoot()
    {
        direction = new Vector3(ShootJoystick.Horizontal, 0, ShootJoystick.Vertical);

        if (Vector3.zero != direction)
        {
            Rotate(direction, 10);

            if(shootingSpeed <= 0)
            {
                weapon.Shoot();
                shootingSpeed = initialShootingSpeed;
            } else
            {
                shootingSpeed--;
            }
        }
    }

    public void Heal(int healingAmount)
    {
        status.Life += healingAmount;

        if(status.Life > status.InitialLife)
        {
            status.Life = status.InitialLife;
        }

        interfaceController.UpdateLifeSlider();
    }
}
