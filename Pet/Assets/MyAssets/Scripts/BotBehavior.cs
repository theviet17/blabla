using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehavior : MonoBehaviour
{
    public BotData botData;
    public int botHp;
    public int botDamage ;
    public int goldReceived;
    public GameObject petTarget;
    public Vector3 direction;

    public float botSpeed = 4;
    float distance;

    public GameManager gameManager;

    public GameObject Thunder;

    public int aoeTargetNumber;
    void Start()
    {
        botHp = botData.Hp;
        botDamage = botData.Damage;
        //goldReceived = botData.Gold;

        direction = petTarget.transform.position - transform.position;
        transform.forward = direction;
    }

    void FixedUpdate()
    {
        distance = botSpeed * Time.deltaTime;
        transform.position += distance * transform.forward;
    }
    private void Update()
    {
        aoeTargetNumber = gameManager.yellowPet.gameObject.GetComponent<YellowSkill>().numberAoe;
    }
    public void LostHP(int damage)
    {
        botHp = botHp - damage;
        if(botHp <= 0)
        {
            //gameObject.SetActive(false);
            //gameManager.enemy.Remove(gameObject);
            //Destroy(gameObject);
            gameManager.BotDie(gameObject);
        }
    }
    public void AoeThunder(int damage)
    {
        var listEnemy = new List<GameObject>(gameManager.enemy);
        //List<float> Distance = new List<float>();
        float minDistance = 100;
        GameObject target = null;
        //for (int i = 0; i < listEnemy.Count; i++)
        //{
        //    if(listEnemy[i] != gameObject)
        //    {
        //        float distance = Vector3.Distance(gameObject.transform.position, listEnemy[i].transform.position);
        //        if (distance < minDistance)
        //        {
        //            minDistance = distance;
        //            target = listEnemy[i];
        //            //Debug.Log(distance);
        //        }
        //    }
        //}
        for (int i = 0; i < aoeTargetNumber; i++)
        {
            for (int j = 0; j < listEnemy.Count; j++)
            {
                if (listEnemy[j] != gameObject)
                {
                    float distance = Vector3.Distance(gameObject.transform.position, listEnemy[j].transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = listEnemy[j];
                        listEnemy.Remove(target);
                        //Debug.Log(distance);
                    }
                }
            }
            if (target != null)
            {
                var targetBotBehavior = target.GetComponent<BotBehavior>();

                var thunder = Instantiate(Thunder);
                //thunder.GetComponent<Electric>().transformPointA = gameObject.transform;
                //thunder.GetComponent<Electric>().transformPointB = target.transform;
                thunder.transform.GetChild(0).position = gameObject.transform.position;
                thunder.GetComponent<Electric>().a = gameObject;
                thunder.transform.GetChild(1).position = target.transform.position;
                thunder.GetComponent<Electric>().b = target;

                targetBotBehavior.LostHP(damage);

            }

        }



    }

}
