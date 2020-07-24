using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject WeaponPoint;

    public void Shoot()
    {
        Instantiate(Bullet, WeaponPoint.transform.position, WeaponPoint.transform.rotation);
    }
}
