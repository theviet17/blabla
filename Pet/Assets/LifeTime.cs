using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    public float life = 1;

    void Awake()
    {
        Destroy(gameObject, life);
    }
}
