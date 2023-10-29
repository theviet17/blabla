using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed = 10;
    float distance;
    public Vector3 direction;
    public float life = 2;
    public int damage;
    public float lifetime = 0;

    [Header("TypeBullet")]
    public bool isYellow = false;
    public GameObject fire_1;
    public GameObject fire_2;

    public bool haveYellowBuff = false;
    public PetBehavior yellow;



    void Awake()
    {
        Destroy(gameObject, life+0.05f);
    }
    
    private void Start()
    {
        transform.forward = direction;
    }
    void FixedUpdate()
    {
        lifetime += Time.deltaTime;
        if(lifetime <= life)
        {
            distance = bulletSpeed * Time.deltaTime;
            transform.position += distance * transform.forward;
        }
        if (lifetime >=  life + 0.04f)
        {
            if (isYellow)
            { Instantiate(fire_2, gameObject.transform.position, Quaternion.identity); }
                
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bot")
        {
            var botBehavior = other.gameObject.GetComponent<BotBehavior>();
            if (isYellow)
            {
                Instantiate(fire_1, gameObject.transform.position, Quaternion.identity);
                botBehavior.AoeThunder(damage);
            }
            if (haveYellowBuff)
            {
                botBehavior.AoeThunder(yellow.petDamage);
            }
            Destroy(gameObject);
            botBehavior.LostHP(damage);
        }
        if (other.tag == "boss")
        {
            Debug.Log(21);
            var bossBehavior = other.gameObject.GetComponent<Boss>();
            if (isYellow)
            {
                Instantiate(fire_1, gameObject.transform.position, Quaternion.identity);
                //botBehavior.AoeThunder(damage);
            }
            //if (haveYellowBuff)
            //{
            //    botBehavior.AoeThunder(yellow.petDamage);
            //}
            Destroy(gameObject);
            bossBehavior.LostHP(damage);
        }
    }
}
