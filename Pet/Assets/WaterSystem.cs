using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isRain = false;
    public bool isRisingtWater = false;
    public void RisingWater()
    {
        StartCoroutine(3f.Tweeng((p) => transform.position = p,
            new Vector3(0, -3.5f, 1), 
            new Vector3(0, -1.5f, 1)));
        isRisingtWater = true;
    }
    public void LowWater()
    {
        StartCoroutine(3f.Tweeng((p) => transform.position = p,
            new Vector3(0, -1.5f, 1),
            new Vector3(0, -3.5f, 1)));
        isRisingtWater = false;
    }
    public void CheckRain()
    {
        if(isRain == true && isRisingtWater == false)
        {
            RisingWater();
        }
        if(isRain == false && isRisingtWater == true)
        {
            LowWater();
        }
    }
}
