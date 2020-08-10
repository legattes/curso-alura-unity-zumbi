using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBaseController, IKillable, IHealeable
{
    public LayerMask GroundMask;
    public FixedJoystick MoveJoystick;
    //public FixedJoystick ShootJoystick;

    public InterfaceController InterfaceController;
    public CharacterStatus Status;
    public PlayerAnimator Animator;
    public int RunOffset = 10;
    public int IdleOffset = 30;

    private Vector3 direction;
    private WeaponController weapon;
    public int initialShootingSpeed = 20;
    private int shootingSpeed;
    
    private void Start()
    {
        Status = GetComponent<CharacterStatus>();
        Animator = GetComponent<PlayerAnimator>();
        weapon = GetComponent<WeaponController>();
        shootingSpeed = initialShootingSpeed;
    } 

    void Update()
    {
        //float x = MoveJoystick.Horizontal;
        //float z = MoveJoystick.Vertical;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        direction = new Vector3(x, 0, z);

        Animator.Run(direction.magnitude);
    }

    void FixedUpdate()
    {
        Move(direction, Status.Speed);

        Rotate(direction);
        //RotateAndShoot();
    }

    public void TakeDamage(int damage)
    {
        Status.Life -= damage;

        InterfaceController.UpdateLifeSlider();

        if(Status.Life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        InterfaceController.GameOver();
    }

    public void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Rotate(direction, RunOffset);
        }

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

    /*void RotateAndShoot()
    {
        Vector3 shootDirection = new Vector3(ShootJoystick.Horizontal, 0, ShootJoystick.Vertical);

        if (shootDirection != Vector3.zero)
        {
            if(direction != Vector3.zero)
            {
                Rotate(shootDirection, RunOffset);
            } else
            {
                Rotate(shootDirection, IdleOffset);
            }

            if(shootingSpeed <= 0)
            {
                weapon.Shoot();
                shootingSpeed = initialShootingSpeed;
            } else
            {
                shootingSpeed--;
            }
        }
    }*/

    public void Shoot()
    {
        weapon.Shoot();
    }

    public void Heal(int healingAmount)
    {
        Status.Life += healingAmount;

        if(Status.Life > Status.InitialLife)
        {
            Status.Life = Status.InitialLife;
        }

        InterfaceController.UpdateLifeSlider();
    }
}
