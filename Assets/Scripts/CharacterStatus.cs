using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public int InitialLife = 100;
    [HideInInspector]
    public int Life;
    public float Speed = 10;

    void Awake()
    {
        Life = InitialLife;
    }
}
