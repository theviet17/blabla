using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBullet : MonoBehaviour
{
    //public float bulletSpeed = 30;
    //float distance;
    //public Vector3 direction;
    public float life = 0.1f;
    //public int damage = 10;
    //public GameObject targetGameObject;
    //public bool isDestroy = false;


    //private Vector3 startPosition;
    //public float elapsedTime = 0;
    //public float desiredDuration = 1f;

    void Awake()
    {
        Destroy(gameObject, life );
    }
    //private void Start()
    //{
    //    transform.forward = direction;
    //    startPosition = transform.position;
    //}
    //void FixedUpdate()
    //{

    //    if (!isDestroy)
    //    {
    //       //direction = targetGameObject.transform.position - gameObject.transform.position;
    //       distance = bulletSpeed * Time.deltaTime;
    //       transform.position += distance * transform.forward;
    //    }


    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "bot" && other== targetGameObject)
    //    {
    //        var botBehavior = other.gameObject.GetComponent<BotBehavior>();
    //        //Destroy(gameObject);
    //        isDestroy = true;
    //        gameObject.GetComponent<BoxCollider>().enabled = false;
    //        botBehavior.LostHP(damage);
    //    }
    //}
   
}
