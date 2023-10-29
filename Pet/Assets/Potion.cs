using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public string namePet;
    public bool inPet = false;
    public GameManager gameManager;
    public GameObject petConnet;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == namePet)
        {
            petConnet = other.gameObject;
           inPet = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == namePet)
        {
           
            inPet = false;
        }
    }
    public void Update()
    {
       gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10) ;
        if (Input.GetMouseButtonDown(0))
        {
            if (inPet)
            {
                petConnet.GetComponent<PetBehavior>().UpLevel();
                gameManager.havePotionInMouse = false;
                Destroy(gameObject);
            }
            else
            {
                gameManager.RefundPotion(namePet);
                gameManager.havePotionInMouse = false;
                Destroy(gameObject);
            }
        }
    }
}
