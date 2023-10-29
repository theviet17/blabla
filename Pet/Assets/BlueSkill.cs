using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSkill : MonoBehaviour
{
    public PetBehavior bluePet;
    public int petSkillLevel = 0;
    public int numberColt = 1;

    public bool haveYellowBuff = false;
    public PetBehavior yellow;

    public GameObject chose;
    public GameObject skillBtn;
    public GameObject unSkillBtn;
    public Color cl1;
    public Color cl2;

    public PetBehavior pred;
    public PetBehavior pyellow;

    public GameObject redShell;
    public GameObject yellowShell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        haveYellowBuff = bluePet.haveYellowBuff;
        yellow = bluePet.yellow;
    }
    public void UpdatePetSkillLevel()
    {

        petSkillLevel = (int)bluePet.petLevel / 5;
        if (petSkillLevel > 5)
        {
            petSkillLevel = 5;
        }
        numberColt = 1 + petSkillLevel;
    }
    public void CreateBullet(GameObject target , GameObject petBullet)
    {
        Debug.Log("BluedoSkill" + numberColt);
       
        for (int i = 1; i<= numberColt; i++)
        {
            StartCoroutine(Colt(i-1, target, petBullet));
        }
    }
    public IEnumerator Colt(int stt, GameObject target, GameObject petBullet)
    {
        yield return new WaitForSeconds(stt * 0.1f);

        var bullet = Instantiate(petBullet, gameObject.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().direction = target.transform.position - transform.position;
        bullet.GetComponent<Bullet>().damage = bluePet.petDamage;
        bullet.GetComponent<Bullet>().haveYellowBuff = haveYellowBuff;
        bullet.GetComponent<Bullet>().yellow = yellow;

    }
    public void ClickSkillBtn()
    {
        skillBtn.gameObject.SetActive(false);
        chose.gameObject.SetActive(true);
    }
    public void ClickXBtn()
    {
        skillBtn.gameObject.SetActive(true);
        chose.gameObject.SetActive(false);
    }
    public void ClickRedBtn()
    {
        //skillBtn.gameObject.SetActive(true);
        chose.gameObject.SetActive(false);
        unSkillBtn.gameObject.SetActive(true);
        RedChose();
    }
    public void ClickYellowBtn()
    {
        //skillBtn.gameObject.SetActive(true);
        chose.gameObject.SetActive(false);
        unSkillBtn.gameObject.SetActive(true);

        YellowChose();
    }
    public void RedChose()
    {
        gameObject.GetComponent<SpriteRenderer>().color = cl1;
        pred.haveBlueBuff = true;
        //StartCoroutine(0.3f.Tweeng((p) => CloneYellow.transform.position = p,
        //    CloneYellow.transform.position,
        //    new Vector3(0.875f, 2.263f, 0)));
        //StartCoroutine(0.3f.Tweeng((p) => CloneYellow.transform.localScale = p,
        //    CloneYellow.transform.localScale,
        //    new Vector3(0.3f, 0.3f, 0.3f)));
        //CloneYellow.transform.SetParent(red);

        bluePet.inBlueBuffStatus = true;
        redShell.gameObject.SetActive(true);

        //red.gameObject.GetComponent<PetBehavior>().haveYellowBuff = true;
    }
    public void YellowChose()
    {
        gameObject.GetComponent<SpriteRenderer>().color = cl1;
        pyellow.haveBlueBuff = true;
        //StartCoroutine(0.3f.Tweeng((p) => CloneYellow.transform.position = p,
        //    CloneYellow.transform.position,
        //    new Vector3(-2.702f, 2.263f, 0)));
        //StartCoroutine(0.3f.Tweeng((p) => CloneYellow.transform.localScale = p,
        //    CloneYellow.transform.localScale,
        //    new Vector3(0.3f, 0.3f, 0.3f)));
        //CloneYellow.transform.SetParent(blue);

        bluePet.inBlueBuffStatus = true;
        yellowShell.gameObject.SetActive(true);

        //blue.gameObject.GetComponent<PetBehavior>().haveYellowBuff = true;
    }
    public void BackBtn()
    {
        var aaa = gameObject.transform.localToWorldMatrix.GetPosition();
        //StartCoroutine(0.3f.Tweeng((p) => CloneYellow.transform.position = p,
        //    CloneYellow.transform.position,
        //    aaa));
        //StartCoroutine(0.3f.Tweeng((p) => CloneYellow.transform.localScale = p,
        //    CloneYellow.transform.localScale,
        //    new Vector3(1, 1, 1)));
       // CloneYellow.transform.SetParent(gameObject.transform);
        unSkillBtn.gameObject.SetActive(false);
        skillBtn.gameObject.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().color = cl2;
        //StartCoroutine(comeback());
        bluePet.inBlueBuffStatus = false;

        pred.haveBlueBuff = false;
        pyellow.haveBlueBuff = false;

        redShell.gameObject.SetActive(false);
        yellowShell.gameObject.SetActive(false);

        //red.gameObject.GetComponent<PetBehavior>().haveYellowBuff = false;
        //blue.gameObject.GetComponent<PetBehavior>().haveYellowBuff = false;
    }

}
