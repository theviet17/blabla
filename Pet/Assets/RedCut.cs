using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCut : MonoBehaviour
{
    public int damage;
    public bool haveYellowBuff = false;
    public PetBehavior yellow;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bot")
        {
            if (haveYellowBuff)
            {
                var botBehavior = other.gameObject.GetComponent<BotBehavior>();
                botBehavior.AoeThunder(yellow.petDamage);
            }
            other.gameObject.GetComponent<BotBehavior>().LostHP(damage);
        }
        if (other.tag == "boss")
        {
            Debug.Log(20);
            other.gameObject.GetComponent<Boss>().LostHP(damage);
        }
    }
}
