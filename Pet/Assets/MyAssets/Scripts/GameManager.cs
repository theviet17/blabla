using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject botPrefab;
    public PetBehavior bluePet;
    public PetBehavior redPet;
    public PetBehavior yellowPet;

    public List<GameObject> Pet = new List<GameObject>();
    public List<GameObject> enemy = new List<GameObject>();

    public float delayWaveTime = 10;

    [Header("Coin")]
    public int coin = 60;
    public TextMeshProUGUI coinText;

    [Header("Weather")]
    public int timeToChangeWeather = 30;
    int timeToChangeWeatherReal;
    public TextMeshProUGUI timeToChangeWeatherText;
    public WeatherSystem weather;
    public WaterSystem water;

    public int bluePotion = 0;
    public int redPotion = 0;
    public int yellowPotion = 0;

    public TextMeshProUGUI bluePotionnumber;
    public TextMeshProUGUI redPotionnumber;
    public TextMeshProUGUI yellowPotionnumber;

    public Camera mainCamera;

    public GameObject _bluePotion;
    public GameObject _redPotion;
    public GameObject _yellowPotion;

    public bool havePotionInMouse = false;

    public int Wave = 1;
    public GameObject bossPrefab;
    public Vector3 bossPosition;

    private void Start()
    {
        timeToChangeWeatherReal = timeToChangeWeather;
        CreateBot();
        StartCoroutine(Waves());

        weather.RanDomWeather();
        StartCoroutine(CountDownChangeWeather());

        deBuffPet();

        for (int i = 0; i < Pet.Count; i++)
        {
            Pet[i].GetComponent<PetBehavior>().gameManager = this.GetComponent<GameManager>();
        }
    }
  

    public void CreateBot()
    {
        if(Wave == 1)
        {
            for (int i = 6; i >= 0; i--)
            {
                var petPosition = new Vector3(Random.Range(-5, 5), 9.63f, 0);
                if (Pet.Count != 0)
                {
                    var bot = Instantiate(botPrefab, petPosition, Quaternion.identity);
                    bot.GetComponent<BotBehavior>().petTarget = Pet[Random.Range(0, Pet.Count)];
                    bot.GetComponent<BotBehavior>().gameManager = this.GetComponent<GameManager>();
                    bot.GetComponent<BotBehavior>().goldReceived = 50;
                    enemy.Add(bot);
                }
            }
            Wave = 2;
        }
        else if (Wave == 2)
        {
            for (int i = 12; i >= 0; i--)
            {
                var petPosition = new Vector3(Random.Range(-5, 5), 9.63f, 0);
                if (Pet.Count != 0)
                {
                    var bot = Instantiate(botPrefab, petPosition, Quaternion.identity);
                    bot.GetComponent<BotBehavior>().petTarget = Pet[Random.Range(0, Pet.Count)];
                    bot.GetComponent<BotBehavior>().gameManager = this.GetComponent<GameManager>();
                    bot.GetComponent<BotBehavior>().goldReceived = 100;
                    enemy.Add(bot);
                }
            }
            Wave = 3;
        }
        else if(Wave == 3)
        {
            for (int i = 18; i >= 0; i--)
            {
                var petPosition = new Vector3(Random.Range(-5, 5), 9.63f, 0);
                if (Pet.Count != 0)
                {
                    var bot = Instantiate(botPrefab, petPosition, Quaternion.identity);
                    bot.GetComponent<BotBehavior>().petTarget = Pet[Random.Range(0, Pet.Count)];
                    bot.GetComponent<BotBehavior>().gameManager = this.GetComponent<GameManager>();
                    bot.GetComponent<BotBehavior>().goldReceived = 150;
                    enemy.Add(bot);
                }
            }
            Wave = 4;
        }
        else if(Wave == 4)
        {
            for (int i = 24; i >= 0; i--)
            {
                var petPosition = new Vector3(Random.Range(-5, 5), 9.63f, 0);
                if (Pet.Count != 0)
                {
                    var bot = Instantiate(botPrefab, petPosition, Quaternion.identity);
                    bot.GetComponent<BotBehavior>().petTarget = Pet[Random.Range(0, Pet.Count)];
                    bot.GetComponent<BotBehavior>().gameManager = this.GetComponent<GameManager>();
                    bot.GetComponent<BotBehavior>().goldReceived = 200;
                    enemy.Add(bot);
                }
            }
            Wave = 5;
        }
        else if(Wave == 5)
        {
            for (int i = 30; i >= 0; i--)
            {
                var petPosition = new Vector3(Random.Range(-5, 5), 9.63f, 0);
                if (Pet.Count != 0)
                {
                    var bot = Instantiate(botPrefab, petPosition, Quaternion.identity);
                    bot.GetComponent<BotBehavior>().petTarget = Pet[Random.Range(0, Pet.Count)];
                    bot.GetComponent<BotBehavior>().gameManager = this.GetComponent<GameManager>();
                    bot.GetComponent<BotBehavior>().goldReceived = 250;
                    enemy.Add(bot);
                }
            }
            Wave = 6;
        }
        else if(Wave == 6)
        {
            var Bosss = Instantiate(bossPrefab, bossPosition, Quaternion.identity);
            Bosss.GetComponent<Boss>().gameManager = this.GetComponent<GameManager>();
            enemy.Add(Bosss);
        }

    }
    public IEnumerator Waves()
    {
        //int numberBotInWave = 5;
        while (true)
        {
            yield return new WaitForSeconds(delayWaveTime);

            CreateBot();
        }
    }
    public void BotDie(GameObject obj)
    {
        enemy.Remove(obj);
        coin += obj.GetComponent<BotBehavior>().goldReceived;
        Destroy(obj);
        //obj.gameObject.SetActive(false);

        //bluePet.CheckTargetDie(obj);
        //redPet.CheckTargetDie(obj);
        //yellowPet.CheckTargetDie(obj);

        coinText.text = coin.ToString();
    }
    public void BossDie(GameObject obj)
    {
        enemy.Remove(obj);
        coin += obj.GetComponent<Boss>().goldReceived;
        Destroy(obj);
        //obj.gameObject.SetActive(false);

        //bluePet.CheckTargetDie(obj);
        //redPet.CheckTargetDie(obj);
        //yellowPet.CheckTargetDie(obj);

        coinText.text = coin.ToString();
    }
   


    public IEnumerator CountDownChangeWeather()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            ChangeWeather();
        }

    }
    public void ChangeWeather()
    {
        timeToChangeWeather--;
        if (timeToChangeWeather < 0)
        {
            weather.RanDomWeather();
            timeToChangeWeather = timeToChangeWeatherReal;
            deBuffPet();
        }
        timeToChangeWeatherText.text = timeToChangeWeather.ToString();
        


    }
    void deBuffPet()
    {
        if(weather.weatherType == "rain")
        {
            bluePet.deBuff = false;
            redPet.deBuff = true;
            StartCoroutine(redPet.LostHpByWeather(0.5f, 1));
            yellowPet.deBuff = false;

            water.isRain = true;
            water.CheckRain();

            redPet.deBuffDmg = true;
            bluePet.deBuffDmg = false;
            yellowPet.deBuffDmg = true;
        }
        if (weather.weatherType == "sun")
        {
            bluePet.deBuff = false;
            redPet.deBuff = false;
            yellowPet.deBuff = true;
            StartCoroutine(yellowPet.LostHpByWeather(1f, 1));

            water.isRain = false;
            water.CheckRain();

            bluePet.deBuffDmg = true;
            redPet.deBuffDmg = false;
            yellowPet.deBuffDmg = true;
        }
        if (weather.weatherType == "clouds")
        {
            bluePet.deBuff = false;
            redPet.deBuff = false;
            yellowPet.deBuff = false;

            water.isRain = false;
            water.CheckRain();

            redPet.deBuffDmg = false;
            bluePet.deBuffDmg = false;
            yellowPet.deBuffDmg = true;
        }
        if (weather.weatherType == "storm")
        {
            bluePet.deBuff = true;
            StartCoroutine(bluePet.LostHpByWeather(Random.Range(3,10), 8));
            redPet.deBuff = false;
            yellowPet.deBuff = false;

            water.isRain = false;
            water.CheckRain();

            redPet.deBuffDmg = false;
            bluePet.deBuffDmg = false;
            yellowPet.deBuffDmg = false;
        }


    }

    public void BuyBluePoistion()
    {
        if (coin >= 10)
        {
            bluePotion++;
            coin -= 10;
            bluePotionnumber.text = bluePotion.ToString();
            coinText.text = coin.ToString();
        }
    }
    public void BuyRedPoistion()
    {
        if (coin >= 10)
        {
            redPotion++;
            coin -= 10;
            redPotionnumber.text = redPotion.ToString();
            coinText.text = coin.ToString();
        }
    }
    public void BuyYellowPoistion()
    {
        if (coin >= 10)
        {
            yellowPotion++;
            coin -= 10;
            yellowPotionnumber.text = yellowPotion.ToString();
            coinText.text = coin.ToString();
        }
    }
    public void BuyBluePoistionClick()
    {
        //Debug.Log(1);
        if (!havePotionInMouse)
        {
            if (bluePotion >= 1)
            {
                var potion = Instantiate(_bluePotion, Camera.main.ScreenToWorldPoint(Input.mousePosition)
                    + new Vector3(0, 0, 9), Quaternion.identity);
                potion.GetComponent<Potion>().gameManager = gameObject.GetComponent<GameManager>();
                bluePotion--;
                bluePotionnumber.text = bluePotion.ToString();
                havePotionInMouse = true;
            }
        }
          
    }
    public void BuyRedPoistionClick()
    {
        if (!havePotionInMouse)
        {
            if (redPotion >= 1)
            {
                var potion = Instantiate(_redPotion, Camera.main.ScreenToWorldPoint(Input.mousePosition)
                    + new Vector3(0, 0, 9), Quaternion.identity);
                potion.GetComponent<Potion>().gameManager = gameObject.GetComponent<GameManager>();
                redPotion--;
                redPotionnumber.text = redPotion.ToString();
                havePotionInMouse = true;
            }
        }
       
    }
    public void BuyYellowPoistionClick()
    {
        if (!havePotionInMouse)
        {
            if (yellowPotion >= 1)
            {
                var potion = Instantiate(_yellowPotion, Camera.main.ScreenToWorldPoint(Input.mousePosition)
                    + new Vector3(0, 0, 9), Quaternion.identity);
                potion.GetComponent<Potion>().gameManager = gameObject.GetComponent<GameManager>();
                yellowPotion--;
                yellowPotionnumber.text = yellowPotion.ToString();
                havePotionInMouse = true;
            }
        }
        
    }
    public void RefundPotion(string nname)
    {
        Debug.Log("rf" + nname);
        if (nname == "Blue")
        {
            bluePotion++;
            bluePotionnumber.text = bluePotion.ToString();
            Debug.Log("rfBlue");
        }
        if (nname == "Red")
        {
            redPotion++;
            redPotionnumber.text = redPotion.ToString();
            Debug.Log("rfRed");
        }
        if(nname == "Yellow")
        {
            yellowPotion++;
            yellowPotionnumber.text = yellowPotion.ToString();
            Debug.Log("rfYellow");
        }
    }
    //#if UNITY_EDITOR
    //    EditorUtility.DisplayDialog("Great!", "You got the pattern right!", "Next Level!");
    //                        EditorUtility.SetDirty(testFt.vhcData);
    //                        AssetDatabase.SaveAssets();
    //                        AssetDatabase.Refresh();
    //#endif

    //                        Debug.Log("UseUnlock");

}
