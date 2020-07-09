using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10;
    public int Life = 100;
    public LayerMask GroundMask;
    public GameObject GameOverText;
    public InterfaceController interfaceController;

    private Rigidbody rbPlayer;
    private Animator animPlayer;
    private Vector3 direction;

    private void Start()
    {
        Time.timeScale = 1;
        rbPlayer = GetComponent<Rigidbody>();
        animPlayer = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        direction = new Vector3(x,0,z);

        if(direction != Vector3.zero)
        {
            animPlayer.SetBool("Running", true);
        } else
        {
            animPlayer.SetBool("Running", false);
        }

        if(Life <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game");
            }
        }
    }

    void FixedUpdate()
    {
        rbPlayer.MovePosition(rbPlayer.position + (direction * Speed * Time.deltaTime));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit impact;

        if (Physics.Raycast(ray, out impact, 100, GroundMask))
        {
            Vector3 crosshairDiff = impact.point - transform.position;
            crosshairDiff.y = transform.position.y;

            Quaternion rotation = Quaternion.LookRotation(crosshairDiff);
            rbPlayer.MoveRotation(rotation);
        }
    }

    public void TakeDamage(int damage)
    {
        Life -= damage;

        interfaceController.UpdateLifeSlider();

        if(Life <= 0)
        {
            Time.timeScale = 0;
            GameOverText.SetActive(true);
        }
    }
}
