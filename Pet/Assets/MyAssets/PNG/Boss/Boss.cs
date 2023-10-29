using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int bossHp = 300;
    public int bossDamage = 30;
    public int goldReceived = 500;

    public GameObject CurrentTarget;
    public Vector3 direction;
    public GameManager gameManager;
    public GameObject bossBullet;
    public Vector3 bossBulletPostion;

    public float attackDelayTime = 3;

    void Start()
    {
        StartCoroutine(AutoAttack());

    }
    public void CreateBullet()
    {
        if (CurrentTarget == null)
        {
            Debug.Log(1);
            if (gameManager.Pet.Count > 0)
            {
                CurrentTarget = gameManager.Pet[Random.Range(0, gameManager.Pet.Count)];
                Debug.Log(CurrentTarget);
            }

        }
        if (CurrentTarget != null)
        {
           var bullet = Instantiate(bossBullet, bossBulletPostion, Quaternion.identity);
           bullet.GetComponent<BossBullet>().direction = CurrentTarget.transform.position - transform.position;
           bullet.GetComponent<BossBullet>().damage = bossDamage;

        }

    }
    public IEnumerator AutoAttack()
    {

        while (true)
        {
            yield return new WaitForSeconds(attackDelayTime);
            CreateBullet();
        }
    }
    public void LostHP(int damage)
    {
        bossHp = bossHp - damage;
        if (bossHp <= 0)
        {
            //gameObject.SetActive(false);
            //gameManager.enemy.Remove(gameObject);
            //Destroy(gameObject);
            gameManager.BossDie(gameObject);
        }
    }
}
