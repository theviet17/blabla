using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetBehavior : MonoBehaviour
{
    public PetData petData;
    public int petLevel;
    public int petHP;
    public int petDamage;
    public GameObject petBullet;
    public GameObject CurrentTarget;

    public float attackDelayTime;

    public GameManager gameManager;

    public int timeToUpLevel = 30;

    [Header("Lv/Hp")]
    public Slider slider;
    public TextMeshProUGUI _timeToUpLevel;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI cHP_HP;

    [Header("DeBuff")]
    public bool deBuff = false;
    public bool deBuffDmg = false;
    public GameObject water;

    [Header("TypePet")]
    public bool isRed = false;
    public bool isBlue = false;
    public bool isYellow = false;
    [Header("If is Red Need Add")]
    public RedSkill redSkill;
    public RedCut redCut;

    [Header("If is Blue Need Add")]
    public BlueSkill blueSkill;
    public bool inBlueBuffStatus = false;

    [Header("If is Yellow Need Add")]
    public YellowSkill yellowSkill;
    public int aoeBuff = 0;
    public bool inBuffStatus = false;

    public bool haveYellowBuff = false;
    public PetBehavior yellow;

    public int PotionNeedToUpdate = 3;
    public TextMeshProUGUI damTxt;

    public bool haveBlueBuff = false;

    void Start()
    {
        petLevel = petData.level;
        petHP = petData.hp;
        petDamage = petData.damage;
        petBullet = petData.marbles;
        StartCoroutine(AutoAttack());
        StartCoroutine(CountDownUpLevel());
        if (isRed)
        {
            redCut.damage = petDamage;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(deBuff == true)
        if (deBuffDmg)
        {
            if (!isYellow && !haveBlueBuff)
            {
                var a = (petData.damage + (petLevel - 1)) * 0.6f;
                petDamage = (int)a;
            }
            if(!isYellow && haveBlueBuff)
            {
                petDamage = petData.damage + (petLevel - 1);
            }
            if (isYellow)
            {
                aoeBuff = 0;
                petDamage = petData.damage + (petLevel - 1);
            }
        }
        else
        {
            if (!isYellow)
            {
                petDamage = petData.damage + (petLevel - 1);
            }
          
            if (isYellow)
            {
                aoeBuff = 2;
                petDamage = petData.damage + (petLevel - 1) ;
            }
        }
        damTxt.text = petDamage.ToString();
        if (isRed)
        {
            redCut.damage = petDamage;
            redCut.haveYellowBuff = haveYellowBuff;
            redCut.yellow = yellow;

        }

    }
    public void CreateBullet()
    {
        if (CurrentTarget == null)
        {
            if (gameManager.enemy.Count > 0)
            {
                CurrentTarget = gameManager.enemy[Random.Range(0, gameManager.enemy.Count)];
            }

        }

        if (isYellow)
        {
            if (CurrentTarget != null)
            {
                if (!inBuffStatus)
                {
                    var bullet = Instantiate(petBullet, gameObject.transform.position, Quaternion.identity);
                    bullet.GetComponent<Bullet>().direction = CurrentTarget.transform.position - transform.position;
                    bullet.GetComponent<Bullet>().damage = petDamage;
                }
               
            }
        }
        else if (isRed)
        {
            if (CurrentTarget != null)
            {
                redSkill.RedCut();
            }
        }
        else if (isBlue)
        {
            if (CurrentTarget != null)
            {
                if (!inBlueBuffStatus)
                {
                    blueSkill.CreateBullet(CurrentTarget, petBullet);
                }
               
            }
        }

    }
    //public void CheckTargetDie(GameObject obj)
    //{
    //    if( CurrentTarget == obj)
    //    {
    //        //CurrentTarget = null;
    //        if (gameManager.enemy.Count > 0)
    //        {
    //            CurrentTarget = gameManager.enemy[Random.Range(0, gameManager.enemy.Count)];
    //        }
    //    }
    //}
    public IEnumerator AutoAttack()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(attackDelayTime);
            CreateBullet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bot")
        {
            var botDamage = other.GetComponent<BotBehavior>().botDamage;
            petHP = petHP - botDamage;

            var botHP = other.GetComponent<BotBehavior>().botHp;
            other.GetComponent<BotBehavior>().LostHP(botHP);

            UpdateHpUI();
        }
        if (other.tag == "bossBullet")
        {
            var bossDamage = other.GetComponent<BossBullet>().damage;
            petHP = petHP - bossDamage;

            Destroy(other.gameObject);

            UpdateHpUI();
        }

    }

    public IEnumerator CountDownUpLevel()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            //UpLevel();


        }
    }
    public void UpLevel()
    {
        //timeToUpLevel--;
        //if(timeToUpLevel < 0)
        //{
        //    timeToUpLevel = 30;
        //    petLevel++;
        //    petHP += 80;
        //}
        PotionNeedToUpdate--;
        if(PotionNeedToUpdate <= 0)
        {
            PotionNeedToUpdate = 3;
            petLevel++;
            petDamage++;
            petHP += 20;
            if (isRed)
            {
                redSkill.UpdatePetSkillLevel();
            }
            if (isBlue)
            {
                blueSkill.UpdatePetSkillLevel();
            }
            if (isYellow)
            {
                yellowSkill.UpdatePetSkillLevel();
            }
        }
        UpdateUI();
    }
    public void UpdateUI()
    {
        
        _timeToUpLevel.text = PotionNeedToUpdate.ToString();
        Level.text = petLevel.ToString();
        UpdateHpUI();


    }
    public void UpdateHpUI()
    {
        var sumHP = 80 + 20* (petLevel -1);
        slider.value = (float)petHP / (float)sumHP;
        cHP_HP.text = petHP.ToString() + "/" + sumHP.ToString();

        if (petHP <= 0)
        {
            gameManager.Pet.Remove(gameObject);
            slider.gameObject.SetActive(false);
            if (isYellow)
            {
                gameManager.bluePet.haveYellowBuff = false;
                gameManager.redPet.haveYellowBuff = false;
                gameObject.GetComponent<YellowSkill>().CloneYellow.gameObject.SetActive(false);
            }
            Destroy(gameObject);
        }
    }

    public IEnumerator LostHpByWeather(float time, int Hp)
    {
        while (deBuff == true)
        {
            yield return new WaitForSeconds(time);
            if (!haveBlueBuff)
            {
                petHP -= Hp;
                UpdateHpUI();
            }
            
        }
    }
    

    //public Slider slider;
    //public Text _timeToUpLevel;
    //public Text Level;
    //public Text cHP_HP;
}
