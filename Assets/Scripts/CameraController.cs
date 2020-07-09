using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    Vector3 PlayerCameraDistance;
    
    void Start()
    {
        PlayerCameraDistance = transform.position - Player.transform.position;
    }
    
    void Update()
    {
        transform.position = Player.transform.position + PlayerCameraDistance;
    }
}
