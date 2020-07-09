using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject WeaponPoint;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(Bullet, WeaponPoint.transform.position, WeaponPoint.transform.rotation);
        }
    }
}
