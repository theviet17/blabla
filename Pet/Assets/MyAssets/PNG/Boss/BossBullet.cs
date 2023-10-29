using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed = 10;
    float distance;
    public Vector3 direction;
    public int damage;
    private void Start()
    {
        transform.forward = direction;
    }
    void FixedUpdate()
    {
        distance = bulletSpeed * Time.deltaTime;
        transform.position += distance * transform.forward;
    }
}
