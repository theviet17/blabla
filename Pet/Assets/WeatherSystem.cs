using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public string weatherType;
    private void Start()
    {
       
    }

    public void RanDomWeather()
    {
        int a = Random.Range(0, 100);
        if (a % 4 == 0) { SetWeatherIcon(gameObject.transform.GetChild(0).gameObject); weatherType = "rain"; }
        if (a % 4 == 1) { SetWeatherIcon(gameObject.transform.GetChild(1).gameObject); weatherType = "sun"; }
        if (a % 4 == 2) { SetWeatherIcon(gameObject.transform.GetChild(2).gameObject); weatherType = "clouds"; }
        if (a % 4 == 3) { SetWeatherIcon(gameObject.transform.GetChild(3).gameObject); weatherType = "storm"; }
    }
    public void SetWeatherIcon(GameObject objIcon)
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        objIcon.gameObject.SetActive(true);
    }
}
